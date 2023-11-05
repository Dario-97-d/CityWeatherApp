using CityWeatherApp.Data.Models.Weather;
using Newtonsoft.Json;

namespace CityWeatherApp.Data.Services;

public class GetWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiEndpoint = "https://api.openweathermap.org/data/2.5/weather";
    private readonly string _apiKey = "[API_KEY_HERE]";

    public GetWeatherService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<WeatherApiModel> GetWeatherAsync(string latitude, string longitude)
    {
        if (latitude == null)
        {
            throw new ArgumentNullException(nameof(latitude));
        }

        if (longitude == null)
        {
            throw new ArgumentNullException(nameof(longitude));
        }

        try
        {
            string apiUrl = $"{_apiEndpoint}?lat={latitude}&lon={longitude}&appid={_apiKey}";

            // Make an HTTP GET request to the weather API.
            var response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string.
                string responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize the JSON response.
                var weatherData = JsonConvert.DeserializeObject<WeatherApiModel>(responseContent);

                return weatherData;
            }
            else
            {
                // Handle the case where the API request was not successful.
                throw new HttpRequestException($"Failed to retrieve weather data. Status code: {response.StatusCode}");
            }
        }
        catch (HttpRequestException ex)
        {
            // Handle exceptions related to the HTTP request.
            throw new Exception("Error while fetching weather data.", ex);
        }
    }
}

