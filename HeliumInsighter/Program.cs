using CommandLine;
using HeliumInsighter.Commands;

Console.WriteLine("Hello, World!");

var parserResult = Parser.Default.ParseArguments<FrontCommand, RadiusCommand, BoxCommand>(args);
await parserResult.WithParsedAsync<FrontCommand>(async options =>
    await Commands.FrontSemiCircleBeaconStats(options));
await parserResult.WithParsedAsync<RadiusCommand>(async options => await Commands.RadiusBeaconStats(options.hotspotId));
await parserResult.WithParsedAsync<BoxCommand>(async options => await Commands.BoxBeaconStats(options.hotspotId));

Console.WriteLine("Done");