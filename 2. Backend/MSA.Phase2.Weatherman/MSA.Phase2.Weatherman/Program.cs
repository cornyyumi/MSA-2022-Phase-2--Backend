using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using MSA.Phase2.Weatherman.Data;


var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("AppDb");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IWeatherRepo, DBWeatherRepo>();
if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
{
    builder.Services.AddSwaggerDocument(options =>
    {
        options.DocumentName = "Weatherman API";
        options.Version = "V1";
        options.Description = "Running Production build (SQLite database)";

    });
    builder.Services.AddDbContext<WeatherDbContext>(options => options.UseSqlite(builder.Configuration["DataConnection"]));
}
if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
{
    builder.Services.AddSwaggerDocument(options =>
    {
        options.DocumentName = "Weatherman API";
        options.Version = "V1";
        options.Description = "Running Development build (In-Memory database)";

    });
    builder.Services.AddDbContext<WeatherDbContext>(options => options.UseInMemoryDatabase(builder.Configuration["DataConnection"]));
}

builder.Services.AddHttpClient("weatherman", configureClient: client =>
{
    client.BaseAddress = new Uri(@"https://api.openweathermap.org");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseOpenApi();
    app.UseSwaggerUi3();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();