namespace ChustaSoft.Templates.WebApi.WeatherForecasts.Models;


public record WeatherForecast(string City, DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
