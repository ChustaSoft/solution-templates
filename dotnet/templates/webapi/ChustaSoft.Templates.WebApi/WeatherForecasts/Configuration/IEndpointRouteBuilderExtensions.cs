using Asp.Versioning.Builder;
using ChustaSoft.Templates.WebApi.WeatherForecasts.Endpoints;

namespace ChustaSoft.Templates.WebApi.WeatherForecasts.Configuration;

public static class IEndpointRouteBuilderExtensions
{
    public static void MapWeatherForecastsFeature(this IEndpointRouteBuilder endpointRouteBuilder, ApiVersionSet apiVersionSet)
    {
        var featureGroup = endpointRouteBuilder
            .MapGroup("api/v{apiVersion:apiVersion}/weather-forecasts")
            .WithApiVersionSet(apiVersionSet);

        featureGroup
            .MapGet("/", GetWeather.HandleAsync)
            .MapToApiVersion(1);
    }
}
