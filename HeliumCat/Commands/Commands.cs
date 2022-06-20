using HeliumCat.CommandOptions;
using HeliumCat.Responses;
using HeliumCat.Services;

namespace HeliumCat.Commands;

public static class Commands
{
    private static bool _cancelKeyPressed;

    public static async Task FrontBeaconStats(FrontCommandOptions options)
    {
        Console.WriteLine($"front semi-circle beacon stats ({options.ToString()})");

        var hotspotName = EnsureCorrectName(options.name);
        var minDateTime = DateTime.UtcNow.AddMinutes(-options.pastMinutes);

        var myHotspot = await HotspotService.GetHotspotByName(hotspotName);

        Console.Write("Fetching hotspots in front ... ");
        var hotspots = await HotspotService.GetHotspotsByRadius(myHotspot.Lat, myHotspot.Lng, options.radius);
        var frontHotspots = hotspots.Where(hotspot =>
        {
            var bearing = Extensions.DegreeBearing(myHotspot.Lat, myHotspot.Lng, hotspot.Lat, hotspot.Lng);
            return (bearing > 0 && bearing <= 90) || (bearing >= 270 && bearing < 360);
        }).ToArray();
        Console.WriteLine($"{frontHotspots.Length}");

        await BeaconStats2(frontHotspots, myHotspot, minDateTime);
    }

    public static async Task BoxBeaconStats(BoxCommandOptions options)
    {
        Console.WriteLine($"box beacon stats for the past {options.past} minutes ...");

        var hotspotName = EnsureCorrectName(options.name);
        var minDateTime = DateTime.UtcNow.AddMinutes(-options.past);

        var myHotspot = await HotspotService.GetHotspotByName(hotspotName);
        var challenges = await HotspotService.GetWitnessed(myHotspot.Address);

        // calculating witnessed box
        var allLats = challenges.Select(x => x.Lat).ToList();
        var allLngs = challenges.Select(x => x.Lng).ToList();
        var swLat = allLats.Min();
        var swLon = allLngs.Min();
        var neLat = allLats.Max();
        var neLon = allLngs.Max();

        var hotspots = await HotspotService.GetHotspotsByBox(swLat, swLon, neLat, neLon);
        Console.WriteLine(
            $"There are {hotspots.Count} hotspots in my witnessed box. SW({swLat}, {swLon}) and NE({neLat}, {neLon})");

        await BeaconStats2(hotspots.ToArray(), myHotspot, minDateTime);
    }

    public static async Task RadiusBeaconStats(RadiusCommandOptions options)
    {
        Console.WriteLine($"radius beacon stats ({options.ToString()})");

        var hotspotName = EnsureCorrectName(options.name);
        var minDateTime = DateTime.UtcNow.AddMinutes(-options.pastMinutes);

        Console.Write("Fetching hotspots in the radius ... ");
        var myHotspot = await HotspotService.GetHotspotByName(hotspotName);
        var hotspots = await HotspotService.GetHotspotsByRadius(myHotspot.Lat, myHotspot.Lng, options.radius);
        Console.WriteLine($"{hotspots.Count()}");

        await BeaconStats2(hotspots.ToArray(), myHotspot, minDateTime);
    }

    public static async Task Direction(DirectionCommandOptions options)
    {
        var hotspotName1 = EnsureCorrectName(options.hotspotName);
        var hotspotName2 = EnsureCorrectName(options.hotspotName2);

        Console.WriteLine($"direction between {hotspotName1} and {hotspotName2}");
        var hotspot1 = await HotspotService.GetHotspotByName(hotspotName1);
        var hotspot2 = await HotspotService.GetHotspotByName(hotspotName2);

        Console.WriteLine($"- {hotspot2.ToString()} {Extensions.GetDirectionString(hotspot1, hotspot2)}");
    }

    private static async Task BeaconStats(Hotspot[] hotspots, Hotspot myHotspot, DateTime minTime)
    {
        var challenges = await HotspotService.GetChallenges(myHotspot.Address, minTime);
        var myWitnessed =
            challenges.Where(c => c.Path.Any()
                                  && c.Path.First().Witnesses
                                      .Any(x => x.IsValid && x.Gateway.Equals(myHotspot.Address)));
        var myWitnessedHashes = myWitnessed.Select(x => x.Hash).ToList();
        Console.WriteLine($"I witnessed {myWitnessedHashes.Count} beacons since {minTime.ToString("R")}");

        var workingCount = Math.Min(20, hotspots.Length);
        Console.WriteLine($"let's check {workingCount} of my surrounding hotspots' beacons");

        AttachCtrlC();

        var totalCount = 0;
        var totalHitCount = 0;
        var totalMissedCount = 0;
        for (var i = 0; i < workingCount; i++)
        {
            if (_cancelKeyPressed)
            {
                Console.WriteLine("Ctrl + C");
                break;
            }


            var hotspot = hotspots[i];
            if (hotspot.Address.Equals(myHotspot.Address))
            {
                continue;
            }

            var bearing = Extensions.DegreeBearing(myHotspot.Lat, myHotspot.Lng, hotspot.Lat, hotspot.Lng);
            var bearingDirection = Extensions.ToDirection(bearing);
            var distance = Extensions.CalculateDistance(myHotspot, hotspot);
            Console.Write($"{i + 1}. {hotspot.ToString()}");
            Console.Write($"({distance.ToString("F1")}m/{bearingDirection}/{bearing.ToString("0")}°) ... ");

            var beacons = await HotspotService.GetBeaconTransactions(hotspot.Address, minTime);
            if (!beacons.Any())
            {
                Console.WriteLine("no beacon");
                continue;
            }

            var witnessedCount = beacons.Count(x => myWitnessedHashes.Contains(x.Hash));
            var missedCount = beacons.Count - witnessedCount;
            Console.WriteLine($"beacons: {beacons.Count}, witnessed: {witnessedCount}, missed: {missedCount}");

            totalHitCount += witnessedCount;
            totalMissedCount += missedCount;
            totalCount += beacons.Count;
        }

        Console.WriteLine("");
        Console.WriteLine("--- beacon statistics ---");
        Console.WriteLine($"total: {totalCount} , witnessed: {totalHitCount} , missed: {totalMissedCount}");
    }

    private static async Task BeaconStats2(Hotspot[] hotspots, Hotspot myHotspot, DateTime minTime)
    {
        Console.Write("fetching all beacons in the world ... ");
        var challenges = await HotspotService.GetChallenges(minTime);
        Console.WriteLine($"{challenges.Count}");

        Console.Write("beacons from hotspots ... ");
        var myWitnessed =
            challenges.Where(c => c.Path.Any()
                                  && c.Path.First().Witnesses
                                      .Any(x => x.IsValid && x.Gateway.Equals(myHotspot.Address)));
        var myWitnessedHashes = myWitnessed.Select(x => x.Hash).ToList();

        var beaconedHotspots =
            hotspots.Where(h => challenges.Select(c => c.Path.First().Challengee).Contains(h.Address));
        Console.WriteLine($"{beaconedHotspots.Count()}");


        var totalCount = 0;
        var totalHitCount = 0;
        var totalMissedCount = 0;
        foreach (var hotspot in beaconedHotspots)
        {
            if (hotspot.Address.Equals(myHotspot.Address))
            {
                Console.WriteLine("I sent a beacon, hora!");
                continue;
            }

            Console.Write($"- {hotspot.ToString()} {Extensions.GetDirectionString(myHotspot, hotspot)} ... ");

            var beacons = challenges.Where(c => c.Path.First().Challengee.Equals(hotspot.Address)).ToList();
            if (!beacons.Any())
            {
                Console.WriteLine("no beacon");
                continue;
            }

            var witnessedCount = beacons.Count(x => myWitnessedHashes.Contains(x.Hash));
            var missedCount = beacons.Count - witnessedCount;
            Console.WriteLine($"beacons: {beacons.Count}, witnessed: {witnessedCount}, missed: {missedCount}");

            totalHitCount += witnessedCount;
            totalMissedCount += missedCount;
            totalCount += beacons.Count;
        }

        Console.WriteLine("");
        Console.WriteLine("--- beacon statistics ---");
        Console.WriteLine($"total: {totalCount} , witnessed: {totalHitCount} , missed: {totalMissedCount}");
    }

    private static void AttachCtrlC()
    {
        Console.CancelKeyPress += (sender, args) =>
        {
            args.Cancel = true;
            _cancelKeyPressed = true;
        };
    }

    private static string EnsureCorrectName(string name)
    {
        return name.Trim().Replace(' ', '-').ToLower();
    }
}