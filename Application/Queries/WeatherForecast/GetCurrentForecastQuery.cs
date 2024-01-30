using Application.DataModels;
using Application.Interfaces;
using MediatR;

namespace Application.Queries.WeatherForecast;

public class GetCurrentForecastQuery : IRequest<WeatherDataResponse>
{
    public WeatherDataRequest Filters { get; set; }
};

public class
    GetCurrentWeatherForecastQueryHandler(IWeatherRequest weatherRequest)
    : IRequestHandler<GetCurrentForecastQuery, WeatherDataResponse>
{
    public async Task<WeatherDataResponse> Handle(GetCurrentForecastQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var weatherData = await weatherRequest.GetWeatherDataAsync(request.Filters);
            return weatherData;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
}