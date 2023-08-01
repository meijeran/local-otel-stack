using System.Diagnostics;

namespace WeatherApi;

public static class Diagnostics
{
    public static readonly ActivitySource WeatherServiceActivitySource = new("weather-service");
}
