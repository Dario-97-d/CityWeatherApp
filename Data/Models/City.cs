using System.Globalization;

namespace CityWeatherApp.Data.Models;

public class City
{
    private string _latitude;
    private string _longitude;

    public string Name { get; set; }

    public string Country { get; set; }

    public string Latitude
    {
        get { return decimal.TryParse(_latitude, CultureInfo.InvariantCulture, out var lat) ? decimal.Round(lat, 6).ToString(CultureInfo.InvariantCulture) : "n/a"; }
        set { _latitude = value; }
    }

    public string Longitude
    {
        get { return decimal.TryParse(_longitude, CultureInfo.InvariantCulture, out var lon) ? decimal.Round(lon, 6).ToString(CultureInfo.InvariantCulture) : "n/a"; }
        set { _longitude = value; }
    }
}
