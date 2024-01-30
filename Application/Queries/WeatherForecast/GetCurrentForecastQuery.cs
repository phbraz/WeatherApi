using Application.DataModels;
using Application.Interfaces;
using MediatR;

namespace Application.Queries.WeatherForecast;

public class GetCurrentForecastQuery : IRequest<WeatherDataResponse>
{
    public WeatherDataRequest Filters { get; set; }
};


public class
    GetCurrentWeatherForecastQueryHandler : IRequestHandler<GetCurrentForecastQuery, WeatherDataResponse>
{
    private readonly IWeatherRequest _weatherRequest;

    public GetCurrentWeatherForecastQueryHandler(IWeatherRequest weatherRequest)
    {
        _weatherRequest = weatherRequest;
    }
    
    
    public async Task<WeatherDataResponse> Handle(GetCurrentForecastQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var weatherData = await _weatherRequest.GetWeatherDataAsync(request.Filters);
            return weatherData;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
}