using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MSA.Phase2.Weatherman.Models;
using MSA.Phase2.Weatherman.Data;
using Swashbuckle.AspNetCore;


namespace MSA.Phase2.Weatherman.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeathermanController : ControllerBase
    {
        private readonly HttpClient _client;
        private readonly IWeatherRepo _weatherRepo;

        public WeathermanController(IHttpClientFactory clientFactory, IWeatherRepo weatherRepo)
        {
            if (clientFactory is null)
            {
                throw new ArgumentNullException(nameof(clientFactory));
            }
            _client = clientFactory.CreateClient("weathermman");
            _weatherRepo = weatherRepo;
        }

        /// <summary>
        /// This endpoint takes the name of a city, then gives you the weather details from the database
        /// </summary>
        /// <param name="city"></param>
        /// <returns>A JSON object describing the weather details</returns>
        [HttpGet]
        [Route("weather")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> getWeather(String city)
        {
            WeatherInfo weather = _weatherRepo.get(city);
            return Ok(weather);
        }

        /// <summary>
        /// This endpoint takes the name of a city, then adds the weather details to the database
        /// </summary>
        /// <param name="city"></param>
        /// <returns>A JSON object describing the weather details</returns>
        [HttpPost("AddWeather")]
        [ProducesResponseType(201)]
        public async Task<IActionResult> addWeather(string city)
        {
            string url = "/data/2.5/weather?q=" + city + "&APPID=3eb4ed4db4ff2ab0947c704aabe32694";
            var content = await _client.GetStringAsync(url);
            WeatherInfo weather = JsonConvert.DeserializeObject<WeatherInfo>(content);
            _weatherRepo.add(weather);

            return Created(new Uri(_client.BaseAddress + url), weather);
        }

        /// <summary>
        /// This endpoint takes the name of a city, then updates or adds the weather details in the database
        /// </summary>
        /// <param name="city"></param>
        /// <returns>A JSON object describing the weather details</returns>
        [HttpPut("UpdateWeather")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateWeather(string city)
        {

            WeatherInfo info = _weatherRepo.update(city);
            if (info == null)
            {
                string url = "/data/2.5/weather?q=" + city + "&APPID=3eb4ed4db4ff2ab0947c704aabe32694";
                var content = await _client.GetStringAsync(url);
                WeatherInfo weather = JsonConvert.DeserializeObject<WeatherInfo>(content);
                _weatherRepo.add(weather);
                return Created(new Uri(_client.BaseAddress + url), weather);
            }
            return Ok();
        }

        /// <summary>
        /// This endpoint takes the name of a city, and deletes the weather details in the database
        /// </summary>
        /// <param name="city"></param>
        /// <returns>A JSON object describing the weather details</returns>
        [HttpDelete("DeleteWeather")]
        [ProducesResponseType(204)]
        public ActionResult<WeatherInfo> deleteWeather(String city)
        {
            WeatherInfo info = _weatherRepo.remove(city);
            return NoContent();
        }

    }
}
