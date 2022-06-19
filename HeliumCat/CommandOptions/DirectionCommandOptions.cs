using CommandLine;

namespace HeliumCat.CommandOptions;

[Verb("direction", HelpText = "calculates the direction between two hotspots")]
public class DirectionCommandOptions
{
    [Value(0, MetaName = "first hotspot name", Required = true, HelpText = "first hotspot animal name")]
    public string hotspotName { get; set; }

    [Value(1, MetaName = "second hotspot name", Required = true, HelpText = "second hotspot animal name")]
    public string hotspotName2 { get; set; }
}