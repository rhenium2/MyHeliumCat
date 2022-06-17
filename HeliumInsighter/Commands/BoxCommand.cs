using CommandLine;

namespace HeliumInsighter.Commands;

[Verb("box", HelpText = "beacon stats of hotspots in box area")]
public class BoxCommand
{
    [Value(0, MetaName = "hotspot name", Required = true, HelpText = "hotspot animal name")]
    public string name { get; set; }

    [Option("past", Default = 1, HelpText = "past n minutes to report")]
    public int past { get; set; }

    [Option("radius", Default = 1, HelpText = "radius n km to report")]
    public int radius { get; set; }
}