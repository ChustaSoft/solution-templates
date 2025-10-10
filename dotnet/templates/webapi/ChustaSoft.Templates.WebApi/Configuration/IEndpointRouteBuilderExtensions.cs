using Asp.Versioning;
using ChustaSoft.Templates.WebApi.WeatherForecasts.Configuration;

namespace ChustaSoft.Templates.WebApi.Configuration;

public static class IEndpointRouteBuilderExtensions
{

    public static void MapEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var apiVersionSet = endpointRouteBuilder.NewApiVersionSet()
                .HasApiVersion(new ApiVersion(1))
                .ReportApiVersions()
                .Build();

        endpointRouteBuilder.MapWeatherForecastsFeature(apiVersionSet);
    }
}