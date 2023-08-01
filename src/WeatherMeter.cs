using System.Diagnostics.Metrics;

namespace WeatherApi;

public class WeatherMeter
{
    private readonly Counter<int> _weatherForecastRequestCounter;

    public WeatherMeter()
    {
        var meter = new Meter(nameof(WeatherMeter));
        Name = meter.Name;
        _weatherForecastRequestCounter = meter.CreateCounter<int>("get_weather_forecast_request_counter");
    }

    public static string Name { get; private set; } = null!;

    public void GetWeatherForecast() => _weatherForecastRequestCounter.Add(1);
}