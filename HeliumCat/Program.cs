using CommandLine;
using HeliumCat.CommandOptions;
using HeliumCat.Commands;
using HeliumCat.Helpers;
using HeliumCat.Services;

var parserResult =
    Parser.Default
        .ParseArguments<FrontOptions, RadiusOptions, BoxOptions, DirectionOptions, WitnessedOptions>(args);
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
await parserResult.WithParsedAsync<WitnessedOptions>(async options => await Commands.Witnessed(options));

CacheService.Default.Dispose();
Console.WriteLine("");
Console.WriteLine("Done.");