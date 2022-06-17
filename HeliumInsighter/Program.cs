using HeliumInsighter.Commands;

Console.WriteLine("Hello, World! Helium Insigher v0.0");

if (args.Length == 0)
{
    throw new Exception("Hotspot Id missing");
}

var command = args[0].Trim().ToLower();
var hotspotId = args[1].Trim();
switch (command)
{
    case "radius":
        await Commands.RadiusBeaconStats(hotspotId);
        return;
    case "front":
        await Commands.FrontSemiCircleBeaconStats(hotspotId);
        return;
    case "box":
        await Commands.BoxBeaconStats(hotspotId);
        return;
}

Console.WriteLine("Done");