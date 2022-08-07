using MSA.Phase2.Weatherman.Models;
namespace MSA.Phase2.Weatherman.Data
{
    public interface IWeatherRepo
    {
        WeatherInfo get(string city);
        WeatherInfo add(WeatherInfo info);
        WeatherInfo update(string city);
        WeatherInfo remove(string city);
    }
}
