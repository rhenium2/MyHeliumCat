using CommandLine;
using HBeacons.Commands;

Console.WriteLine("Hello, World!");

var parserResult = Parser.Default.ParseArguments<FrontCommand, RadiusCommand, BoxCommand>(args);
await parserResult.WithParsedAsync<FrontCommand>(async options =>
    await Commands.FrontSemiCircleBeaconStats(options));
await parserResult.WithParsedAsync<RadiusCommand>(async options => await Commands.RadiusBeaconStats(options));
await parserResult.WithParsedAsync<BoxCommand>(async options => await Commands.BoxBeaconStats(options));

Console.WriteLine("Done");