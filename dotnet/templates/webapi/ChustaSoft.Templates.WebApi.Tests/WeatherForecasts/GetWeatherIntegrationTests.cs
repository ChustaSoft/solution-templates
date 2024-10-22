using ChustaSoft.Templates.WebApi.Tests.Base;
using ChustaSoft.Templates.WebApi.Tests.Configuration;
using ChustaSoft.Templates.WebApi.WeatherForecasts.Models;
using System.Net.Http.Json;

namespace ChustaSoft.Templates.WebApi.Tests.WeatherForecasts;

public class GetWeatherIntegrationTests : IntegrationTestBase, IAsyncLifetime
{

    public GetWeatherIntegrationTests(InternalWebApplicationFactory<Program> factory)
        : base(factory)
    { }


    [Theory]
    [InlineData("Barcelona", 2)]
    [InlineData("London", 1)]
    public async Task Given_GetWeatherRequest_When_GetWeatherInoked_Then_GetWeatherForThatCityRetrieved(string city, int expectedForecasts)
    {
        // Act
        var response = await Client.GetAsync($"api/v1/weather-forecasts?city={city}");
        var weatherForecasts = await response.Content.ReadFromJsonAsync<WeatherForecast[]>();

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(expectedForecasts, weatherForecasts!.Count());
        Assert.DoesNotContain(weatherForecasts!, x => x.City != city);
    }


    public async Task InitializeAsync()
    {
        //In case you need to populate data or prepare the context for this execution, this is the place
    }

    public async Task DisposeAsync()
    {
        //In case of requiring cleaning or disposing elements after this test execution
    }

}