using CommandLine;
using HeliumCat;
using HeliumCat.CommandOptions;
using HeliumCat.Commands;


var parserResult =
    Parser.Default
        .ParseArguments<FrontCommandOptions, RadiusCommandOptions, BoxCommandOptions, DirectionCommandOptions>(args);
if (parserResult.Errors.Any())
{
    return;
}

Extensions.WriteHeader();
await Extensions.CheckForNewVersion();

await parserResult.WithParsedAsync<FrontCommandOptions>(async options =>
    await Commands.FrontBeaconStats(options));
await parserResult.WithParsedAsync<RadiusCommandOptions>(async options => await Commands.RadiusBeaconStats(options));
await parserResult.WithParsedAsync<BoxCommandOptions>(async options => await Commands.BoxBeaconStats(options));
await parserResult.WithParsedAsync<DirectionCommandOptions>(async options => await Commands.Direction(options));

Console.WriteLine("Done");