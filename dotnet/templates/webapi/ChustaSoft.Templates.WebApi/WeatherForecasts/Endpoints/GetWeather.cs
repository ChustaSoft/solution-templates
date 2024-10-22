using ChustaSoft.Templates.WebApi.WeatherForecasts.Models;

namespace ChustaSoft.Templates.WebApi.WeatherForecasts.Endpoints;


public static class GetWeather
{

    public static async Task<IResult> HandleAsync([AsParameters] GetWeatherRequest request)
    {
        var forecast = new WeatherForecast[]
        {
            new("Barcelona", DateOnly.FromDateTime(DateTime.Now).AddDays(-1), 24, "Sunny"),
            new("Barcelona", DateOnly.FromDateTime(DateTime.Now), 25, "Sunny"),
            new("London", DateOnly.FromDateTime(DateTime.Now), 10, "Overcasted"),
            new("Paris", DateOnly.FromDateTime(DateTime.Now), 15, "Windy"),
            new("Moscow", DateOnly.FromDateTime(DateTime.Now), 7, "Partially cloudy"),
            new("New York", DateOnly.FromDateTime(DateTime.Now), Random.Shared.Next(-20, 55), "Partially cloudy"),
        };

        forecast = forecast.Where(x => x.City == request.City).ToArray();

        return TypedResults.Ok(await Task.FromResult(forecast));
    }


    public record GetWeatherRequest(string City);

}
