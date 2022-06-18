using CommandLine;
using HeliumCat.Commands;

Console.WriteLine("Hello, World!");

var parserResult = Parser.Default.ParseArguments<FrontCommand, RadiusCommand, BoxCommand, DirectionCommand>(args);
await parserResult.WithParsedAsync<FrontCommand>(async options =>
    await Commands.FrontSemiCircleBeaconStats(options));
await parserResult.WithParsedAsync<RadiusCommand>(async options => await Commands.RadiusBeaconStats(options));
await parserResult.WithParsedAsync<BoxCommand>(async options => await Commands.BoxBeaconStats(options));
await parserResult.WithParsedAsync<DirectionCommand>(async options => await Commands.Direction(options));

Console.WriteLine("Done");