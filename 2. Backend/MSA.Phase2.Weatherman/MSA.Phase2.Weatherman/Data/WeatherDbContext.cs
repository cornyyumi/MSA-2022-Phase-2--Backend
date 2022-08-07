using Microsoft.EntityFrameworkCore;
using MSA.Phase2.Weatherman.Models;


namespace MSA.Phase2.Weatherman.Data
{
    class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            modelBuilder.Entity<WeatherInfo>()
                .HasOne(x => x.Main)
                .WithOne(y => y.WeatherInfo)
                .HasForeignKey<Main>(x => x.WeatherInfoForeignKey);
        
        }


        public DbSet<WeatherInfo> WeatherInfo { get; set; }
        public DbSet<Main> Main { get; set; }
        public DbSet<Weather> Weather { get; set; }


    }
}
