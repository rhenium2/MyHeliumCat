using GeoCoordinatePortable;
using HeliumCat.Responses;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HeliumCat;

public static class Extensions
{
    public static List<T> DeserializeAll<T>(params string[] jsonStrings)
    {
        var result = new List<T>();
        foreach (var jsonString in jsonStrings)
        {
            if (string.IsNullOrWhiteSpace(jsonString))
            {
                continue;
            }

            var settings = new JsonSerializerSettings();
            settings.ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() };
            var collection = JsonConvert.DeserializeObject<List<T>>(jsonString, settings);
            result.AddRange(collection);
        }

        return result;
    }

    public static decimal GetGain(int gain)
    {
        return (decimal)gain / 10;
    }

    public static double CalculateDistance(Hotspot first, Hotspot second)
    {
        var g1 = new GeoCoordinate(Convert.ToDouble(first.Lat), Convert.ToDouble(first.Lng));
        var g2 = new GeoCoordinate(Convert.ToDouble(second.Lat), Convert.ToDouble(second.Lng));
        return g1.GetDistanceTo(g2);
    }

    public static string GetDirectionString(Hotspot first, Hotspot second)
    {
        var bearing = Extensions.DegreeBearing(first.Lat, first.Lng, second.Lat, second.Lng);
        var bearingDirection = Extensions.ToDirection(bearing);
        var distance = Extensions.CalculateDistance(first, second);
        return $"({distance.ToString("F1")}m/{bearingDirection}/{bearing.ToString("0")}°)";
    }
    
    public static double DegreeBearing(
        double lat1, double lon1,
        double lat2, double lon2)
    {
        var dLon = ToRad(lon2 - lon1);
        var dPhi = Math.Log(
            Math.Tan(ToRad(lat2) / 2 + Math.PI / 4) / Math.Tan(ToRad(lat1) / 2 + Math.PI / 4));
        if (Math.Abs(dLon) > Math.PI)
            dLon = dLon > 0 ? -(2 * Math.PI - dLon) : (2 * Math.PI + dLon);
        return ToBearing(Math.Atan2(dLon, dPhi));
    }

    private static double ToRad(double degrees)
    {
        return degrees * (Math.PI / 180);
    }

    private static double ToDegrees(double radians)
    {
        return radians * 180 / Math.PI;
    }

    private static double ToBearing(double radians)
    {
        // convert radians to degrees (as bearing: 0...360)
        return (ToDegrees(radians) + 360) % 360;
    }

    public static string ToDirection(double d)
    {
        if (d == 0) return "N";
        if (d == 90) return "E";
        if (d == 180) return "S";
        if (d == 270) return "W";

        if (d > 0 && d < 90) return "NE";
        if (d > 90 && d < 180) return "SE";
        if (d > 180 && d < 270) return "SW";
        if (d > 270 && d < 360) return "NW";

        throw new ArgumentException(d.ToString());
    }
}