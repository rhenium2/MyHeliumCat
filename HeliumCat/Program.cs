using CommandLine;
using HeliumCat;
using HeliumCat.CommandOptions;
using HeliumCat.Commands;


var parserResult =
    Parser.Default
        .ParseArguments<FrontOptions, RadiusOptions, BoxOptions, DirectionOptions, DistanceOptions>(args);
if (parserResult.Errors.Any())
{
    return;
}

Extensions.WriteHeader();
await Extensions.CheckForNewVersion();

await parserResult.WithParsedAsync<FrontOptions>(async options =>
    await Commands.FrontBeaconStats(options));
await parserResult.WithParsedAsync<RadiusOptions>(async options => await Commands.RadiusBeaconStats(options));
await parserResult.WithParsedAsync<BoxOptions>(async options => await Commands.BoxBeaconStats(options));
await parserResult.WithParsedAsync<DirectionOptions>(async options => await Commands.Direction(options));
await parserResult.WithParsedAsync<DistanceOptions>(async options => await Commands.Distance(options));

Console.WriteLine("Done");