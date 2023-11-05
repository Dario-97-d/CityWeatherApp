using Newtonsoft.Json;

namespace CityWeatherApp.Data.Models.Weather.NestedProperties;

public class Main
{
    public string Temp { get; set; }

    [JsonProperty("feels_like")]
    public string FeelsLike { get; set; }

    [JsonProperty("temp_min")]
    public string TempMin { get; set; }

    [JsonProperty("temp_max")]
    public string TempMax { get; set; }

    public string Pressure { get; set; }

    public string Humidity { get; set; }
}


