using Asp.Versioning;
using ChustaSoft.Templates.WebApi.WeatherForecasts.Endpoints;

namespace ChustaSoft.Templates.WebApi.Configuration;


public static class EndpointsConfigurationHelper
{

    public static void MapEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var apiVersionSet = endpointRouteBuilder.NewApiVersionSet()
                .HasApiVersion(new ApiVersion(1))
                .ReportApiVersions()
                .Build();

        var featureGroup = endpointRouteBuilder
            .MapGroup("api/v{apiVersion:apiVersion}/weather-forecasts")
            .WithApiVersionSet(apiVersionSet);

        featureGroup
            .MapGet("/", GetWeather.HandleAsync)
            .MapToApiVersion(1);

    }
}