namespace CityWeatherApp.Data.Models.Weather;

public class WeatherDisplayModel
{
    public string AirPressureBar { get; set; }
    public string Brief { get; set; }
    public string CloudCoverage { get; set; }
    public string Description { get; set; }

    public string FeelsLikeC { get; set; }
    public string FeelsLikeF { get; set; }

    public string HoursOfLight { get; set; }
    public string Humidity { get; set; }
    public string IconUrl { get; set; }

    public string Sunrise { get; set; }
    public string Sunset { get; set; }

    public string TemperatureC { get; set; }
    public string TemperatureF { get; set; }

    public string Visibility { get; set; }

    public string WindSpeedKph { get; set; }
    public string WindSpeedMph { get; set; }
    public string WindDirection { get; set; }
}
