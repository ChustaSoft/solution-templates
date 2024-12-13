using ChustaSoft.Templates.WebApi.WeatherForecasts.Data;
using ChustaSoft.Templates.WebApi.WeatherForecasts.Models;

namespace ChustaSoft.Templates.WebApi.WeatherForecasts.Endpoints;


public static partial class GetWeather
{

    public static async Task<IResult> HandleAsync([AsParameters] GetWeatherRequest request)
    {
        var forecast = WeatherForecastRepository.GetAll();

        forecast = forecast.Where(x => x.City == request.City).ToArray();

        return TypedResults.Ok(await Task.FromResult(forecast));
    }

}
