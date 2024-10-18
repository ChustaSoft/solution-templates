using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ChustaSoft.Templates.WebApi.Configuration;


public static class SwaggerConfigurationHelper
{
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.ConfigureOptions<ConfigureSwaggerOptions>();

        return services;
    }

    public static void ConfigureSwagger(this WebApplication webApplication)
    {
        webApplication.UseSwagger();
        webApplication.UseSwaggerUI(opt =>
        {
            var descriptions = webApplication.DescribeApiVersions();

            foreach (var desc in descriptions)
            {
                var url = $"/swagger/{desc.GroupName}/swagger.json";
                var name = desc.GroupName.ToUpperInvariant();

                opt.SwaggerEndpoint(url, name);
            }
        });
    }
}


public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
{

    private readonly IApiVersionDescriptionProvider _apiVersionDescriptionProvider;


    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
    {
        _apiVersionDescriptionProvider = apiVersionDescriptionProvider;
    }


    public void Configure(string? name, SwaggerGenOptions options)
    {
        Configure(options);
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var desciption in _apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            var openApiInfo = new OpenApiInfo
            {
                Title = $"Weather API {desciption.ApiVersion}",
                Version = desciption.ApiVersion.ToString()
            };

            if (desciption.IsDeprecated)
                openApiInfo.Description += " This API version has been deprecated, please use the most recent API definition.";

            options.SwaggerDoc(desciption.GroupName, openApiInfo);
        }
    }

}