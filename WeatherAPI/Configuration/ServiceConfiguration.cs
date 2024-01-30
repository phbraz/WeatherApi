using Application.DataModels;
using Application.Interfaces;
using Application.Queries.WeatherForecast;
using Application.Services.OpenWeatherAPI;
using MediatR;

namespace WeatherAPI.Configuration;

public static class ServiceConfiguration
{ 
    public static void ConfigurationService(IServiceCollection services)
    {
        services.AddSingleton<IWeatherRequest, CurrentWeatherData>();
        services.AddScoped<IRequestHandler<GetCurrentForecastQuery, WeatherDataResponse>, GetCurrentWeatherForecastQueryHandler>();
    }
}