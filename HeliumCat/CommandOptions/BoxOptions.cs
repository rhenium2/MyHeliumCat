using CommandLine;

namespace HeliumCat.CommandOptions;

[Verb("box", HelpText = "beacon stats of hotspots in box area")]
public class BoxOptions
{
    [Value(0, MetaName = "hotspot name or address", Required = true, HelpText = "hotspot name or address")]
    public string Identifier { get; set; }

    [Option("past", Default = 1, HelpText = "past n minutes to report")]
    public int Past { get; set; }
}