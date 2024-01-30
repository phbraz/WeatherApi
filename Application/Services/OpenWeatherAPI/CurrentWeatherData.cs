using Application.DataModels;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Application.Services.OpenWeatherAPI;

public class CurrentWeatherData : IWeatherRequest
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly string _openWeatherApiKey = null!;
    
    public CurrentWeatherData(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _openWeatherApiKey = _configuration["OpenWeatherAPI:key"];
    }

    private HttpClient CreateClient()
    {
        var client = _httpClientFactory.CreateClient();
        client.BaseAddress = new Uri(_configuration["OpenWeatherAPI:baseUrl"]);

        return client;
    }
    
    public async Task<WeatherDataResponse> GetWeatherDataAsync(WeatherDataRequest request)
    {

        var result = !string.IsNullOrEmpty(request.CityName)
            ? await GetDataUsingCity(request)
            : await GetDataUsingPostCode(request);

        return result;
    }

    private async Task<WeatherDataResponse> GetDataUsingPostCode(WeatherDataRequest request)
    {
        var geoConversion = new GeoCodingConversion(_httpClientFactory, _configuration);
        var conversion = await geoConversion.ConvertUsingPostCode(request);
        var requestUrl = $"data/2.5/weather?lat={conversion.lat}&lon={conversion.lon}&appid={_openWeatherApiKey}&units=metric";
        var client = CreateClient();

        var response = await client.GetAsync(requestUrl);
        var responseBody = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<WeatherDataResponse>(responseBody);
    }

    private async Task<WeatherDataResponse> GetDataUsingCity(WeatherDataRequest request)
    {
        var geoConversion = new GeoCodingConversion(_httpClientFactory, _configuration);
        var conversion = await geoConversion.ConvertUsingLocationName(request);
        var requestUrl = $"data/2.5/weather?lat={conversion.FirstOrDefault().lat}&lon={conversion.FirstOrDefault().lon}&appid={_openWeatherApiKey}&units=metric";
        var client = CreateClient();

        var response = await client.GetAsync(requestUrl);
        var responseBody = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<WeatherDataResponse>(responseBody);
    }
}