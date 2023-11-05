using CityWeatherApp.Data.Models.Weather.NestedProperties;
using Newtonsoft.Json;

namespace CityWeatherApp.Data.Models.Weather;

public class WeatherApiModel
{
    public static string IconBaseUrl => "https://openweathermap.org/img/wn/";
    public static string IconUrlSuffix => ".png";

    [JsonProperty("weather")]
    public List<NestedProperties.Weather> WeatherList { get; set; }

    public NestedProperties.Main Main { get; set; }

    public string Visibility { get; set; }

    public Wind Wind { get; set; }

    public Clouds Clouds { get; set; }

    [JsonProperty("sys")]
    public SunClock SunClock { get; set; }

    public string Timezone { get; set; }
}

