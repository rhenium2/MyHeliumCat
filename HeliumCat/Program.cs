using CommandLine;
using HeliumCat.CommandOptions;
using HeliumCat.Commands;
using HeliumCat.Helpers;
using LocalObjectCache;

var parserResult =
    Parser.Default
        .ParseArguments<FrontOptions, RadiusOptions, BoxOptions, DirectionOptions, WitnessedOptions,
            RewardsOptions>(args);
if (parserResult.Errors.Any())
{
    return;
}

Extensions.WriteHeader();
await Extensions.CheckForNewVersion();

await parserResult.WithParsedAsync<FrontOptions>(async options => await Commands.FrontBeaconStats(options));
await parserResult.WithParsedAsync<RadiusOptions>(async options => await Commands.RadiusBeaconStats(options));
await parserResult.WithParsedAsync<BoxOptions>(async options => await Commands.BoxBeaconStats(options));
await parserResult.WithParsedAsync<DirectionOptions>(async options => await Commands.Direction(options));
await parserResult.WithParsedAsync<WitnessedOptions>(async options => await Commands.Witnessed(options));
await parserResult.WithParsedAsync<RewardsOptions>(async options => await Commands.Rewards(options));

Cache.Default.Dispose();
Console.WriteLine("");
Console.WriteLine("Done.");