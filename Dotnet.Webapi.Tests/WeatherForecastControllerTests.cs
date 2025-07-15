using dotnet_webapi;
using System.Net.Http.Json;

namespace dotnet.webapi.tests;

public class WeatherForecastControllerTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    [Fact]
    public async Task GetWeatherForecast_ReturnsSuccessAndForecastData()
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var response = await client.GetAsync("/weatherforecast");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299

        var forecasts = await response.Content.ReadFromJsonAsync<IEnumerable<WeatherForecast>>();

        Assert.NotNull(forecasts);
        var forecastList = Assert.IsAssignableFrom<IEnumerable<WeatherForecast>>(forecasts);
        Assert.Equal(5, forecastList.Count());

        foreach (var forecast in forecastList)
        {
            Assert.NotEqual(default, forecast.Date);
            Assert.True(forecast.TemperatureC >= -20 && forecast.TemperatureC <= 55);
            Assert.NotNull(forecast.Summary);
        }
    }
}