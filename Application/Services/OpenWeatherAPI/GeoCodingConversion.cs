using Application.DataModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Application.Services.OpenWeatherAPI;

public class GeoCodingConversion
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    
    public GeoCodingConversion(IHttpClientFactory httpClientFactory, IConfiguration configuration)
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
    
    public async Task<WeatherDataConversionResponse[]> ConvertUsingLocationName(WeatherDataRequest request)
    {
        var openWeatherApi = _configuration["OpenWeatherAPI:key"];
        var requestUrl = $"geo/1.0/direct?q={request.CityName},{request.CountryCode}&appid={openWeatherApi}";
        
        var client = CreateClient();
        var response = await client.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        
        var weatherDataResponse = JsonConvert.DeserializeObject<WeatherDataConversionResponse[]>(responseBody);
        return weatherDataResponse;
    }

    public async Task<WeatherDataConversionResponse> ConvertUsingPostCode(WeatherDataRequest request)
    {
        var openWeatherApi = _configuration["OpenWeatherAPI:key"];
        var requestUrl = $"geo/1.0/zip?zip={request.PostCode},{request.CountryCode}&appid={openWeatherApi}";
        var client = CreateClient();
        
        var response = await client.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        var weatherDataResponse = JsonConvert.DeserializeObject<WeatherDataConversionResponse>(responseBody);
        
        return weatherDataResponse;
    }
}