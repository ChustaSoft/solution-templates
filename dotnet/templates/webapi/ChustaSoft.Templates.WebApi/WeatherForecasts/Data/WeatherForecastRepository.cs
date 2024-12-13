using ChustaSoft.Templates.WebApi.WeatherForecasts.Models;

namespace ChustaSoft.Templates.WebApi.WeatherForecasts.Data;

/// <summary>
/// Sample data source, this can be a Repository, a DbContext, or a external service
/// </summary>
public class WeatherForecastRepository
{
    private static WeatherForecastDto[]  _forecast = new WeatherForecastDto[]
        {
            new("Barcelona", DateOnly.FromDateTime(DateTime.Now).AddDays(-1), 24, "Sunny"),
            new("Barcelona", DateOnly.FromDateTime(DateTime.Now), 25, "Sunny"),
            new("London", DateOnly.FromDateTime(DateTime.Now), 10, "Overcasted"),
            new("Paris", DateOnly.FromDateTime(DateTime.Now), 15, "Windy"),
            new("Moscow", DateOnly.FromDateTime(DateTime.Now), 7, "Partially cloudy"),
            new("New York", DateOnly.FromDateTime(DateTime.Now), Random.Shared.Next(-20, 55), "Partially cloudy"),
        };


    public static WeatherForecastDto[] GetAll() => _forecast;
}
