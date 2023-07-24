using System.Diagnostics;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Mvc;

namespace WeatherApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly Counter<long> _weatherRequestCounter;
    private readonly ActivitySource _activitySource;


    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
        var meter = new Meter("WeatherMeter");
        _weatherRequestCounter = meter.CreateCounter<long>("weather_requests_total");
        _activitySource = new ActivitySource("weather-service");
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        using var activity = _activitySource.StartActivity("GetWeatherForecast");
        _weatherRequestCounter.Add(1);
            
        _logger.LogInformation("GetWeatherForecast called");
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
           .ToArray();
    }
}
