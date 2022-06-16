using GeoCoordinatePortable;
using HeliumInsighter.Services;

namespace HeliumInsighter.Commands;

public static class Commands
{
    public static async Task BeaconStats(string hotspotId)
    {
        var lastDay = DateTime.UtcNow.AddDays(-1);
        var radiusKm = 5;
        var maxHotspot = 20;

        Console.WriteLine("Staring Beacon Stats...");

        var myHotspot = await HotspotService.GetHotspot(hotspotId);
        var challenges = await HotspotService.GetChallenges(hotspotId, lastDay);
        var myWitnessed =
            challenges.Where(c => c.Path.Any()
                                  && c.Path.First().Witnesses.Any(x => x.IsValid && x.Gateway.Equals(hotspotId)));
        var myWitnessedHashes = myWitnessed.Select(x => x.Hash).ToList();
        Console.WriteLine($"I witnessed {myWitnessedHashes.Count} beacons in the past 24hours from now");

        var hotspots = await HotspotService.GetHotspotsByRadius(myHotspot.Lat, myHotspot.Lng, radiusKm);
        Console.WriteLine($"There are {hotspots.Count} hotspots in the {radiusKm}km radius");

        var totalCount = 0;
        var hitCount = 0;
        var missedCount = 0;
        for (var i = 0; i < Math.Min(maxHotspot, hotspots.Count); i++)
        {
            var hotspot = hotspots[i];
            var bearing = Extensions.DegreeBearing(myHotspot.Lat, myHotspot.Lng, hotspot.Lat, hotspot.Lng);
            var bearingDirection = Extensions.ToDirection(bearing);
            var distance = Extensions.CalculateDistance(myHotspot, hotspot);

            Console.Write($"{i + 1}. {hotspot.ToString()}");
            Console.Write($"({distance.ToString("F1")}m/{bearingDirection}/{bearing.ToString("0")}°) ... ");

            var beacons = await HotspotService.GetBeaconTransactions(hotspot.Address, lastDay);
            if (!beacons.Any())
            {
                Console.WriteLine("no beacon");
                continue;
            }

            var witnessedCount = beacons.Count(x => myWitnessedHashes.Contains(x.Hash));
            Console.WriteLine(
                $"Beacons: {beacons.Count}, Witnessed: {witnessedCount}, Missed: {beacons.Count - witnessedCount}");

            hitCount += witnessedCount;
            missedCount += beacons.Count - witnessedCount;
            totalCount += beacons.Count;
        }

        Console.WriteLine("---");
        Console.WriteLine($"Total: {totalCount} , Witnessed: {hitCount} , Missed: {missedCount}");
    }
}