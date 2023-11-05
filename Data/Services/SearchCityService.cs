using CityWeatherApp.Data.Models;
using Newtonsoft.Json;

namespace CityWeatherApp.Data.Services;

public class SearchCityService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiEndpoint = "https://api.api-ninjas.com/v1/geocoding";
    private readonly string _apiKey = "[API_KEY_HERE]";

    public SearchCityService(HttpClient httpClient)
    {
        _httpClient = httpClient;

        // Set api key header.
        _httpClient.DefaultRequestHeaders.Add("X-Api-Key", _apiKey);
    }

    public async Task<List<City>> SearchCityAsync(string cityName, string? country)
    {
        var response = await _httpClient.GetAsync(
            $"{_apiEndpoint}?city={cityName}&country={country ?? string.Empty}");
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var result = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<List<City>>(result);
    }
}
