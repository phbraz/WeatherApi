using Application.DataModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Application.Services.OpenWeatherAPI;

public class CurrentWeatherData
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    
    public CurrentWeatherData(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    private HttpClient CreateClient()
    {
        var client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri(_configuration["OpenWeatherAPI:baseUrl"]);

        return client;
    }
    
    public async Task<WeatherDataResponse> GetCurrentWeather(WeatherDataRequest request)
    {
        var geoConversion = new GeoCodingConversion(_httpClientFactory, _configuration);
        if (string.IsNullOrEmpty(request.CityName) && string.IsNullOrEmpty(request.CountryCode) && string.IsNullOrEmpty(request.PostCode))
        {
            throw new ArgumentException("At least one parameter must be provided");
        }

        var conversion = await geoConversion.ConvertUsingLocationName(request.CityName, request.CountryCode);

        var openApiKey = _configuration["OpenWeatherAPI:key"];
        var requestUrl = $"data/2.5/weather?lat={conversion.FirstOrDefault().lat}&lon={conversion.FirstOrDefault().lon}&appid={openApiKey}&units=metric";
        var client = CreateClient();
        var response = await client.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var weatherDataResponse = JsonConvert.DeserializeObject<WeatherDataResponse>(responseBody);
        
        return weatherDataResponse;
    }
}