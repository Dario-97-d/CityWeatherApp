using System.Globalization;

namespace CityWeatherApp.Data.Models.Weather;

public static class WeatherModelConverter
{
    public static WeatherDisplayModel ApiToDisplay(this WeatherApiModel model)
    {
        return new WeatherDisplayModel
        {
            AirPressureBar = GetBarFromHpa(model.Main.Pressure),
            Brief = model.WeatherList[0].Main,
            CloudCoverage = model.Clouds.All,
            Description = model.WeatherList[0].Description,
            FeelsLikeC = GetCelsiusFromKelvin(model.Main.FeelsLike),
            FeelsLikeF = GetFarenheitFromKelvin(model.Main.FeelsLike),
            HoursOfLight = GetHoursOfLight(model.SunClock.Sunrise, model.SunClock.Sunset),
            Humidity = model.Main.Humidity,
            IconUrl = GetIconUrl(model.WeatherList[0].Icon),
            Sunrise = GetLocalTimeFromUtcUnix(model.SunClock.Sunrise, model.Timezone),
            Sunset = GetLocalTimeFromUtcUnix(model.SunClock.Sunset, model.Timezone),
            TemperatureC = GetCelsiusFromKelvin(model.Main.Temp),
            TemperatureF = GetFarenheitFromKelvin(model.Main.Temp),
            Visibility = model.Visibility,
            WindDirection = GetWindDirectionFromDegrees(model.Wind.Deg),
            WindSpeedKph = GetKphFromMps(model.Wind.Speed),
            WindSpeedMph = GetMphFromMps(model.Wind.Speed),
        };
    }

    private static string GetBarFromHpa(string hpa)
    {
        return double.TryParse(hpa, CultureInfo.InvariantCulture, out var bar) ? (bar / 1000).ToString()[0..4] : "n/a";
    }

    private static string GetCelsiusFromKelvin(string kelvin)
    {
        return double.TryParse(kelvin, CultureInfo.InvariantCulture, out var temp) ? double.Round(temp - 273.15, 1).ToString(CultureInfo.InvariantCulture) : "n/a";
    }

    private static string GetFarenheitFromKelvin(string kelvin)
    {
        return double.TryParse(kelvin, CultureInfo.InvariantCulture, out var temp) ? double.Round((temp - 273.15) * 9 / 5 + 32, 1).ToString(CultureInfo.InvariantCulture) : "n/a";
    }

    private static string GetHoursOfLight(string sunrise, string sunset)
    {
        if (!int.TryParse(sunrise, out var start) || !int.TryParse(sunset, out var end))
            return "n/a";

        var hours = DateTime.UnixEpoch.AddSeconds(end - start);

        return string.Format("{0:HH}h{0:mm}m", hours);
    }

    private static string GetIconUrl(string iconCode)
    {
        return WeatherApiModel.IconBaseUrl + iconCode + WeatherApiModel.IconUrlSuffix;
    }

    private static string GetKphFromMps(string mps)
    {
        return double.TryParse(mps, CultureInfo.InvariantCulture, out var doubleMps) ? (doubleMps * 3.6).ToString()[0..4] : "n/a";
    }

    private static string GetMphFromMps(string mps)
    {
        return double.TryParse(mps, CultureInfo.InvariantCulture, out var doubleMps) ? (doubleMps * 2.237).ToString()[0..4] : "n/a";
    }

    private static string GetLocalTimeFromUtcUnix(string unix, string timezone)
    {
        if (!int.TryParse(unix, out var xinu) || !int.TryParse(timezone, out var enozemit))
            return "n/a";

        return DateTime.UnixEpoch.AddSeconds(xinu + enozemit).ToShortTimeString();
    }

    private static string GetWindDirectionFromDegrees(string degrees)
    {
        // Try parse degrees as decimal.
        if (!decimal.TryParse(degrees, CultureInfo.InvariantCulture, out var deg)) return "n/a";

        // Check given value for degrees is inside bounds.
        if (deg < 0 || deg >= 360) return "n/a";

        // Align degrees values so that an int cast
        // of the quocient of degrees per the amplitude
        // of the division of the total degrees per number of cardinal directions
        // will give the index of a string array of those.
        // (360 / 8 => 45) and (0 <= deg < 360) --> (int) (deg/45) is in R{0 .. 7}.

        // Directions string array.
        string[] directions = { "NE", "E", "SE", "S", "SW", "W", "NW", "N" };

        // Align degrees with the indexes of the directions array.
        // North is in [0, 22.5[ U [337.5, 360[.
        {
            // If deg in [0, 22.5[ -> shift values: [0, 22.5[ -> [337.5, 360[.
            if (deg >= 0 && deg < 22.5m) deg += 337.5m;

            // Else -> shift values: [22.5, 360[ -> [0, 337.5[
            else deg -= 22.5m;
        }

        // Divide to get the index.
        int x = (int)(deg / 45m);

        return directions[x];
    }
}
