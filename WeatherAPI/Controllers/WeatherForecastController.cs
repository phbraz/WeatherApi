using Application.DataModels;
using Application.Services.OpenWeatherAPI;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WeatherAPI.Controllers;

[Route("WeatherForecast")]
public class WeatherForecastController : ControllerBase
{
    private readonly CurrentWeatherData _currentWeatherData;
    public WeatherForecastController(CurrentWeatherData currentWeatherData)
    {
        _currentWeatherData = currentWeatherData;
    }
    
    [HttpGet("GetCurrentForecast")]
    public async Task<WeatherDataResponse> FetchCurrentForecast()
    {
        var request = new WeatherDataRequest()
        {
            CityName = "Manchester",
            CountryCode = "GB"
        };

        var result = await _currentWeatherData.GetCurrentWeather(request);

        return result;
    }
}