using Application.DataModels;
using Application.Queries.WeatherForecast;
using Application.Services.OpenWeatherAPI;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WeatherAPI.Controllers
{
    [Route("WeatherForecast")]
    public class WeatherForecastController(IMediator mediator) : MainBase(mediator)
    {
        [HttpPost("GetCurrentForecast")]
        public async Task<WeatherDataResponse> FetchCurrentForecast([FromBody] GetCurrentForecastQuery query, CancellationToken token)
            => await SenQuery<GetCurrentForecastQuery, WeatherDataResponse>(query, token);
    }
}