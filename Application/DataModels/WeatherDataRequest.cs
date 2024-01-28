namespace Application.DataModels;

public class WeatherDataRequest
{
    public string? CityName { get; set; }
    public string? CountryCode { get; set; }
    public string? PostCode { get; set; }
}