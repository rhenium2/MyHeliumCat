using CommandLine;

namespace HeliumCat.CommandOptions;

[Verb("direction", HelpText = "calculates the direction between two hotspots")]
public class DirectionOptions
{
    [Value(0, MetaName = "first hotspot name or address", Required = true, HelpText = "first hotspot name or address")]
    public string Identifier1 { get; set; }

    [Value(1, MetaName = "second hotspot name or address", Required = true,
        HelpText = "second hotspot name or address")]
    public string Identifier2 { get; set; }
}