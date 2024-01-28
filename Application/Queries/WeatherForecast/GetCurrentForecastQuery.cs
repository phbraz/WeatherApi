namespace Application.Queries.WeatherForecast;

public class GetCurrentForecastQuery
{
}

public record CurrentForecastSearchParam
{
    public string? City { get; set; }
    public string? PostCode { get; set; }
}

public record CurrentForecastResponse
{
    public string WeatherInfo { get; set; }
    public string City { get; set; }
}