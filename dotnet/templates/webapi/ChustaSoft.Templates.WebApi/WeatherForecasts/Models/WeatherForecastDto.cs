namespace ChustaSoft.Templates.WebApi.WeatherForecasts.Models;


public record WeatherForecastDto(string City, DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


public record GetWeatherRequest(string City);