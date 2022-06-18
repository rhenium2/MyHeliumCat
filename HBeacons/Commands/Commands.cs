using HBeacons.Responses;
using HBeacons.Services;

namespace HBeacons.Commands;

public static class Commands
{
    static readonly int MaxHotspot = 400;
    private static bool _cancelKeyPressed;

    public static async Task FrontSemiCircleBeaconStats(FrontCommand options)
    {
        Console.WriteLine($"front semi-circle beacon stats for the past {options.past} minutes ...");

        var minDateTime = DateTime.UtcNow.AddMinutes(-options.past);

        var myHotspot = await HotspotService.GetHotspotByName(options.name);
        var hotspots = await HotspotService.GetHotspotsByRadius(myHotspot.Lat, myHotspot.Lng, options.radius);
        var frontHotspots = hotspots.Where(hotspot =>
        {
            var bearing = Extensions.DegreeBearing(myHotspot.Lat, myHotspot.Lng, hotspot.Lat, hotspot.Lng);
            return (bearing > 0 && bearing <= 90) || (bearing >= 270 && bearing < 360);
        }).ToArray();
        Console.WriteLine(
            $"There are {frontHotspots.Length} hotspots in front of me, in {options.radius}km semi-circle radius");

        await BeaconStats2(frontHotspots, myHotspot, minDateTime);
    }

    public static async Task BoxBeaconStats(BoxCommand options)
    {
        Console.WriteLine($"box beacon stats for the past {options.past} minutes ...");

        var minDateTime = DateTime.UtcNow.AddMinutes(-options.past);

        var myHotspot = await HotspotService.GetHotspotByName(options.name);
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

    public static async Task RadiusBeaconStats(RadiusCommand options)
    {
        Console.WriteLine($"radius beacon stats for the past {options.past} minutes ...");

        var minDateTime = DateTime.UtcNow.AddMinutes(-options.past);

        var myHotspot = await HotspotService.GetHotspotByName(options.name);
        var hotspots = await HotspotService.GetHotspotsByRadius(myHotspot.Lat, myHotspot.Lng, options.radius);
        Console.WriteLine($"There are {hotspots.Count} hotspots in the {options.radius}km radius");

        await BeaconStats2(hotspots.ToArray(), myHotspot, minDateTime);
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

        var workingCount = Math.Min(MaxHotspot, hotspots.Length);
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
        var challenges = await HotspotService.GetChallenges(minTime);
        var myWitnessed =
            challenges.Where(c => c.Path.Any()
                                  && c.Path.First().Witnesses
                                      .Any(x => x.IsValid && x.Gateway.Equals(myHotspot.Address)));
        var myWitnessedHashes = myWitnessed.Select(x => x.Hash).ToList();

        var beaconedHotspots =
            hotspots.Where(h => challenges.Select(c => c.Path.First().Challengee).Contains(h.Address));

        Console.WriteLine($"There has been {challenges.Count} beacons in the world");


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

            var bearing = Extensions.DegreeBearing(myHotspot.Lat, myHotspot.Lng, hotspot.Lat, hotspot.Lng);
            var bearingDirection = Extensions.ToDirection(bearing);
            var distance = Extensions.CalculateDistance(myHotspot, hotspot);
            Console.Write($"- {hotspot.ToString()} ");
            Console.Write($"({distance.ToString("F1")}m/{bearingDirection}/{bearing.ToString("0")}°) ... ");

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
}