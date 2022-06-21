using HeliumCat.CommandOptions;
using HeliumCat.Responses;
using HeliumCat.Services;

namespace HeliumCat.Commands;

public static class Commands
{
    private static bool _cancelKeyPressed;

    public static async Task FrontBeaconStats(FrontOptions options)
    {
        Console.WriteLine($"front semi-circle beacon stats ({options.ToString()})");

        var minDateTime = DateTime.UtcNow.AddMinutes(-options.PastMinutes);

        var myHotspot = await GetHotspotByIdentifier(options.Identifier);

        Console.Write("Fetching hotspots in front ... ");
        var hotspots = await HotspotService.GetHotspotsByRadius(myHotspot.Lat, myHotspot.Lng, options.Radius);
        var frontHotspots = hotspots.Where(hotspot =>
        {
            var bearing = Extensions.DegreeBearing(myHotspot.Lat, myHotspot.Lng, hotspot.Lat, hotspot.Lng);
            return (bearing > 0 && bearing <= 90) || (bearing >= 270 && bearing < 360);
        }).ToArray();
        Console.WriteLine($"{frontHotspots.Length}");

        await BeaconStats(frontHotspots, myHotspot, minDateTime);
    }

    public static async Task BoxBeaconStats(BoxOptions options)
    {
        Console.WriteLine($"box beacon stats for the past {options.Past} minutes ...");

        var minDateTime = DateTime.UtcNow.AddMinutes(-options.Past);

        var myHotspot = await GetHotspotByIdentifier(options.Identifier);
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

        await BeaconStats(hotspots.ToArray(), myHotspot, minDateTime);
    }

    public static async Task RadiusBeaconStats(RadiusOptions options)
    {
        Console.WriteLine($"radius beacon stats ({options.ToString()})");

        var minDateTime = DateTime.UtcNow.AddMinutes(-options.PastMinutes);

        Console.Write("Fetching hotspots in the radius ... ");
        var myHotspot = await GetHotspotByIdentifier(options.Identifier);

        var hotspots = await HotspotService.GetHotspotsByRadius(myHotspot.Lat, myHotspot.Lng, options.Radius);
        Console.WriteLine($"{hotspots.Count()}");

        await BeaconStats(hotspots.ToArray(), myHotspot, minDateTime);
    }

    public static async Task Direction(DirectionOptions options)
    {
        Console.WriteLine($"direction between {options.Identifier1} and {options.Identifier2}");
        var hotspot1 = await GetHotspotByIdentifier(options.Identifier1);
        var hotspot2 = await GetHotspotByIdentifier(options.Identifier2);

        Console.WriteLine($"- {hotspot2.ToString()} {Extensions.GetDirectionString(hotspot1, hotspot2)}");
    }

    public static async Task Distance(DistanceOptions options)
    {
        Console.WriteLine($"distance stats ({options.ToString()}) in the last 5 days");

        var myHotspot = await GetHotspotByIdentifier(options.Identifier);
        Console.Write("Fetching witnessed ... ");
        var witnessed = await HotspotService.GetWitnessed(myHotspot.Address);
        Console.WriteLine($"{witnessed.Count}");

        var witnessedDistances = witnessed.Select(w => Extensions.CalculateDistance(myHotspot, w)).ToList();

        var min = Extensions.ToDistanceText(witnessedDistances.Min());
        var average = Extensions.ToDistanceText(witnessedDistances.Average());
        var max = Extensions.ToDistanceText(witnessedDistances.Max());
        Console.WriteLine("");
        Console.WriteLine("--- distance statistics ---");
        Console.WriteLine($"total: {witnessedDistances.Count} min: {min} avg: {average} max: {max}");
    }

    public static async Task Witnessed(WitnessedOptions options)
    {
        var minDateTime = DateTime.UtcNow.AddMinutes(-options.PastMinutes);
        Console.WriteLine($"witnessed stats ({options.ToString()}) in the last 5 days");

        var myHotspot = await GetHotspotByIdentifier(options.Identifier);
        Console.Write("Fetching witnessed ... ");
        var witnessed = await HotspotService.GetWitnessedTransactions(myHotspot.Address, minDateTime);
        Console.WriteLine($"{witnessed.Count}");

        foreach (var transaction in witnessed)
        {
            var hotspot = await HotspotService.GetHotspot(transaction.Path[0].Challengee);
            Console.Write(
                $"- {hotspot.ToString()} {Extensions.GetDirectionString(myHotspot, hotspot)} - {Extensions.GetRelativeTimeString(transaction.Time)}");
        }

        Console.WriteLine("");
    }

    private static async Task BeaconStats(Hotspot[] hotspots, Hotspot myHotspot, DateTime minTime)
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

    private static async Task<Hotspot> GetHotspotByIdentifier(string identifier)
    {
        if (identifier.Contains(' ') || identifier.Contains('-'))
        {
            return await HotspotService.GetHotspotByName(EnsureCorrectName(identifier));
        }

        return await HotspotService.GetHotspot(identifier);
    }
}