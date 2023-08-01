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
    private readonly WeatherMeter _weatherMeter;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, WeatherMeter weatherMeter)
    {
        _logger = logger;
        _weatherMeter = weatherMeter;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        using var activity = Diagnostics.WeatherServiceActivitySource.StartActivity("GetWeatherForecast");
        _weatherMeter.GetWeatherForecast();
            
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
