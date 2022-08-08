using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using MSA.Phase2.Weatherman.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Registering repositories
builder.Services.AddScoped<IWeatherRepo, DBWeatherRepo>();

//Setting the database according to Environment settings
var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
if (environment == "Development")
{
    builder.Services.AddDbContext<WeatherDbContext>(options =>
        options.UseInMemoryDatabase(builder.Configuration["DatabaseConnection"]));

    builder.Services.AddSwaggerDocument(options =>
    {
        options.Title = "Weatherman API";
        options.Description = "Weatherman API in Development environment";
        options.Version = "V1";
    });

}
if (environment == "Staging")
{
    builder.Services.AddDbContext<WeatherDbContext>(options =>
        options.UseSqlite(builder.Configuration["DatabaseConnection"]));

    builder.Services.AddSwaggerDocument(options =>
    {
        options.Title = "Weatherman API";
        options.Description = "Weatherman API in Staging environment";
        options.Version = "V1";

    });
}


//Adding the base address for API
builder.Services.AddHttpClient("weathermman", configureClient: client =>
{
    client.BaseAddress = new Uri(@"https://api.openweathermap.org");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
