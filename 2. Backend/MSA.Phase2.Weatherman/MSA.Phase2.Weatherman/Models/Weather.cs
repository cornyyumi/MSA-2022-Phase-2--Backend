using System.ComponentModel.DataAnnotations;

namespace MSA.Phase2.Weatherman.Models
{

    public class Weather
    {
        public int id { get; set; }

        public int WeatherInfoId { get; set; }
        public WeatherInfo WeatherInfo { get; set; }
    }
}
