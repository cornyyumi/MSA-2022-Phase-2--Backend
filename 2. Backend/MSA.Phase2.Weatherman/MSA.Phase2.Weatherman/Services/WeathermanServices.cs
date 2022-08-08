using MSA.Phase2.Weatherman.Models;
using MSA.Phase2.Weatherman.Data;
namespace MSA.Phase2.Weatherman.Services
{
    public class WeathermanServices
    {
        public WeathermanServices() { }
        //Generates warning based off temperature and returns as string
        public String tempWarning(Main main)
        {
            var temp = main.temp;
            //if temp is higher than 40°C
            if (temp>=313.15)
            {
                return "Warning: Extremely hot weather today. Beware of heat strokes.";
            }
            //if temp is between 30°C~40°C
            else if (temp>303.15 && temp<=313.15)
            {
                return "Warning: Hot weather today. Make sure you stay hydrated.";
            }
            //if temp is between 15°C~30°C
            else if (temp>288.15 && temp<=303.15)
            {
                return "Warm weather today.";

            }
            //if temp is between 0°C~15°C
            else if (temp>273.15 && temp<=288.15)
            {
                return "Warning: Chilly weather today, encouraged to dress warm.";
            }
            //if temp is between -5°C~0°C
            else if (temp>268.15 && temp<=273.15)
            {
                return "Warning: Very cold weather today, encouraged to dress warm.";
            }
            else if (temp<=268.15)
            {
                return "Warning: Extremely cold weather today, encouraged to stay home or make sure to dress with multiple layers of clothing.";
            }

            return null;
        }

        //Generates warning based off weather Id and returns as string
        public String weatherWarning(Weather weather)
        {
            var weatherId = weather.id;
            //2xx Thunderstorm
            if (weatherId >= 200 && weatherId <= 202)
            {
                return "Warning: Thunderstorm with rain expected today, stay home if you can.";
            }
            else if (weatherId >= 210 && weatherId <= 221 )
            {
                return "Warning: Thunderstorm expected today.";
            }
            else if (weatherId >= 230 && weatherId <= 232)
            {
                return "Warning: Thunderstorm with drizzle expected today.";
            }
            //3xx Drizzle
            else if (weatherId >= 300 && weatherId <= 321)
            {
                return "Warning: Drizzle expected today, make sure to bring an umbrella or a raincoat.";
            }
            //5xx Rain
            else if (weatherId >= 500 && weatherId <= 531)
            {
                return "Warning: Rain expected today, make sure to bring an umbrella or a raincoat.";
            }
            //6xx Snow
            else if (weatherId >= 600 && weatherId <= 622)
            {
                return "Warning: Snow expected today, make sure to dress warm.";

            }
            //7xx Atmosphere
            else if (weatherId >= 701 && weatherId <= 781)
            {
                return "Warning: Unclear atmosphere expected today, beware of unclear sights when driving.";
            }
            //800 Clear
            else if (weatherId == 800)
            {
                return "Clear weather today, enjoy the great weater!";
            }
            //80x Clouds
            else if (weatherId >= 801 && weatherId <= 804)
            {
                return "Clouds expected today";
            }
            return null;
        }
    }
}
