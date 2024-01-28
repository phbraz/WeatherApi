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

    public async Task<string> ConvertUsingPostCode(string postCode, string countryCode)
    {
        var openWeatherApiKey = _configuration["OpenWeatherAPI:key"];
        var requestUrl =
            $"http://api.openweathermap.org/geo/1.0/zip?zip={postCode},{countryCode}&appid={openWeatherApiKey}";

        var client = CreateClient();
        var response = await client.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        
        return responseBody;
    }

    public async Task<WeatherDataConversionResponse[]> ConvertUsingLocationName(string cityName, string countryCode)
    {
        var openWeatherApi = _configuration["OpenWeatherAPI:key"];
        var requestUrl =
            $"geo/1.0/direct?q={cityName},{countryCode}&appid={openWeatherApi}";
        
        var client = CreateClient();
        var response = await client.GetAsync(requestUrl);
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        
        var weatherDataResponse = JsonConvert.DeserializeObject<WeatherDataConversionResponse[]>(responseBody);
        return weatherDataResponse;
    }
}