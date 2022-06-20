using CommandLine;

namespace HeliumCat.CommandOptions;

[Verb("box", HelpText = "beacon stats of hotspots in box area")]
public class BoxOptions
{
    [Value(0, MetaName = "hotspot name", Required = true, HelpText = "hotspot animal name")]
    public string Name { get; set; }

    [Option("past", Default = 1, HelpText = "past n minutes to report")]
    public int Past { get; set; }

    [Option("radius", Default = 1, HelpText = "radius n km to report")]
    public int Radius { get; set; }
}