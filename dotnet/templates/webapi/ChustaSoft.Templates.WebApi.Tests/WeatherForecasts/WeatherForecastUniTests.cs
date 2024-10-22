using ChustaSoft.Templates.WebApi.WeatherForecasts.Models;

namespace ChustaSoft.Templates.WebApi.Tests.WeatherForecasts;

public class WeatherForecastUniTests
{

    [Theory]
    [InlineData("Barcelona", "2024-9-10", 25, "Sunny")]
    [InlineData("London", "2018-12-21", 10, "Overcasted")]
    [InlineData("Paris", "2012-5-10", 18, "Partially cloud")]
    public void Given_ValidCreationData_When_ConstructorInvoked_Then_ModelCreatedAndTemperatureFCalculated(string city, string dateStr, int temperatureC, string summary)
    {
        // Arrange
        var dateStrSplit = dateStr.Split('-');
        var date = DateOnly.FromDateTime(new DateTime(int.Parse(dateStrSplit[0]), int.Parse(dateStrSplit[1]), int.Parse(dateStrSplit[2])));

        // Act
        var result = new WeatherForecast(city, date, temperatureC, summary);

        // Assert
        Assert.Equal(city, result.City);
        Assert.Equal(date, result.Date);
        Assert.Equal(temperatureC, result.TemperatureC);
        Assert.Equal(summary, result.Summary);
        Assert.Equal(32 + (int)(temperatureC / 0.5556), result.TemperatureF);
    }
}