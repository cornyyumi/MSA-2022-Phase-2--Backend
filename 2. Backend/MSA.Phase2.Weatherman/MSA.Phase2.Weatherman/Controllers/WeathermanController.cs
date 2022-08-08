using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MSA.Phase2.Weatherman.Models;
using MSA.Phase2.Weatherman.Data;
using Swashbuckle.AspNetCore;
using MSA.Phase2.Weatherman.Services;


namespace MSA.Phase2.Weatherman.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeathermanController : ControllerBase
    {
        private readonly HttpClient _client;
        private readonly IWeatherRepo _weatherRepo;
        private readonly WeathermanServices _service;
        private string _API_key;


        public WeathermanController(IHttpClientFactory clientFactory, IWeatherRepo weatherRepo)
        {
            if (clientFactory is null)
            {
                throw new ArgumentNullException(nameof(clientFactory));
            }
            _client = clientFactory.CreateClient("weatherman");
            _weatherRepo = weatherRepo;
            _service = new WeathermanServices();
            _API_key = ""; //Enter API key Here
        }

        /// <summary>
        /// This endpoint takes the name of a city, then returns the weather details from the database
        /// </summary>
        /// <param name="city"></param>
        /// <response  code="200">Success, returns A JSON object describing the weather details</response>
        /// <response code="404">Information not found in database</response>
        [HttpGet]
        [Route("{city}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> getWeather(String city)
        {
            WeatherInfo weather = _weatherRepo.get(city);
            if (weather != null)
            {
                return Ok(weather);
            }
            return NotFound();
        }

        /// <summary>
        /// This endpoint takes the name of a city, generates then returns the temperature warnings as a string
        /// </summary>
        /// <param name="city"></param>
        /// <response  code="200">Success, retuns String of temperature warning description</response>
        /// <response code="404">Information not found in database</response>
        [HttpGet]
        [Route("temp/{city}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> getTempWarning(String city)
        {
            WeatherInfo weather = _weatherRepo.get(city);
            if (weather != null)
            {
                Main main = _weatherRepo.getMain(weather);
                return Ok(_service.tempWarning(main));
            }
            return NotFound();

        }

        /// <summary>
        /// This endpoint takes the name of a city, generates then returns the weather warnings as a string
        /// </summary>
        /// <param name="city"></param>
        /// <response  code="200">Success, returns String of weather warning description</response>
        /// <response code="404">Information not found in database</response>
        [HttpGet]
        [Route("weather/{city}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> getWeatherWarniing(String city)
        {
            WeatherInfo weather = _weatherRepo.get(city);
            Weather weatherinfo = _weatherRepo.getWeather(weather);
            return Ok(_service.weatherWarning(weatherinfo));
        }

        /// <summary>
        /// This endpoint takes the name of a city, then adds the weather details to the database
        /// </summary>
        /// <param name="city"></param>
        /// <response  code="201">Weather information successfully created in database</response>
        /// <response code="404">Information not found in OpenWeatherAPI</response>
        [HttpPost("add/{city}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> addWeather(string city)
        {
            string url = "/data/2.5/weather?q=" + city + "&APPID="+ _API_key;
            var response = await _client.GetAsync(url);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            var content = await _client.GetStringAsync(url);
            WeatherInfo weather = JsonConvert.DeserializeObject<WeatherInfo>(content);
            _weatherRepo.add(weather);
            return Created(new Uri(_client.BaseAddress + url), weather);
            
        }

        /// <summary>
        /// This endpoint takes the name of a city, then updates or adds the weather details in the database
        /// </summary>
        /// <param name="city"></param>
        /// <response  code="200">Weather information successfully updated in database</response>
        /// <response  code="201">Weather information successfully created in database</response>
        [HttpPut("update/{city}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        public async Task<IActionResult> UpdateWeather(string city)
        {

            WeatherInfo info = _weatherRepo.update(city);
            if (info == null)
            {
                string url = "/data/2.5/weather?q=" + city + "&APPID="+_API_key;
                var content = await _client.GetStringAsync(url);
                WeatherInfo weather = JsonConvert.DeserializeObject<WeatherInfo>(content);
                _weatherRepo.add(weather);
                return Created(new Uri(_client.BaseAddress + url), weather);
            }
            return Ok(info);
        }

        /// <summary>
        /// This endpoint takes the name of a city, and deletes the weather details in the database
        /// </summary>
        /// <param name="city"></param>
        /// <response code="204">Sucessfully deleted</response>
        /// <response code="404">Information not found in Database</response>
        [HttpDelete("delete/{city}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult<WeatherInfo> deleteWeather(String city)
        {
            WeatherInfo info = _weatherRepo.remove(city);
            if (info != null)
            {
                return NoContent();
            }
            return NotFound();
            
        }

    }
}
