using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WeatherAPI.Controllers;

public class MainBase : ControllerBase
{
    private readonly IMediator _mediator;

    public MainBase(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task<TResponse> SenQuery<TRequest, TResponse>(TRequest query, CancellationToken cancellationToken) where TRequest 
        : IRequest<TResponse>
    {
        return await _mediator.Send(query, cancellationToken);
    }
}