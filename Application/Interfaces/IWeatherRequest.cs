using Application.DataModels;
using Application.Queries.WeatherForecast;

namespace Application.Interfaces;

public interface IWeatherRequest
{
    Task<WeatherDataResponse> GetWeatherDataAsync(WeatherDataRequest dataRequest);
}