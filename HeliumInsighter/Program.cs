using HeliumInsighter.Commands;

Console.WriteLine("Hello, World! Helium Insigher v0.0");

if (args.Length == 0)
{
    throw new Exception("Hotspot Id missing");
}

var hotspotId = args[0];
await Commands.BeaconStats(hotspotId);

Console.WriteLine("Done");