using CommandLine;

namespace HeliumCat.CommandOptions;

[Verb("direction", HelpText = "calculates the direction between two hotspots")]
public class DirectionOptions
{
    [Value(0, MetaName = "first hotspot name", Required = true, HelpText = "first hotspot animal name")]
    public string HotspotName { get; set; }

    [Value(1, MetaName = "second hotspot name", Required = true, HelpText = "second hotspot animal name")]
    public string HotspotName2 { get; set; }
}