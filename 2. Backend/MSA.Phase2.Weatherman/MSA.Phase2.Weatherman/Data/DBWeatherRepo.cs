using MSA.Phase2.Weatherman.Models;
namespace MSA.Phase2.Weatherman.Data
{
    class DBWeatherRepo : IWeatherRepo
    {
        private readonly WeatherDbContext dbContext;
        public DBWeatherRepo(WeatherDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public WeatherInfo get(string city)
        {
            WeatherInfo info = dbContext.WeatherInfo.FirstOrDefault(e => e.name == city);
            return info;
        }
        public WeatherInfo add(WeatherInfo weather)
        {
            dbContext.WeatherInfo.Add(weather);
            dbContext.SaveChanges();
            return weather;
        }

        public WeatherInfo update(string city)
        {
            WeatherInfo info = dbContext.WeatherInfo.FirstOrDefault(e => e.name == city);
            if (info != null)
            {
                dbContext.Update(info);
                dbContext.SaveChanges();
                return info;
            }
            return null;

            
        }

        public WeatherInfo remove(string city)
        {
            WeatherInfo info = dbContext.WeatherInfo.FirstOrDefault(e => e.name == city);
            if (info != null)
            {
                Console.WriteLine(info.name);
                dbContext.Remove(info);
                dbContext.SaveChanges();
            }
            

            return info;
        }
    }

}
