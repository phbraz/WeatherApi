using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WeatherAPI.Controllers;

public class MainBase(IMediator mediator) : ControllerBase
{
    protected async Task<TResponse> SenQuery<TRequest, TResponse>(TRequest query, CancellationToken cancellationToken) where TRequest 
        : IRequest<TResponse>
    {
        return await mediator.Send(query, cancellationToken);
    }
}