using CommandLine;

namespace HeliumInsighter.Commands;

[Verb("radius", HelpText = "beacon stats of hotspots in a radius")]
public class RadiusCommand
{
    [Value(0, MetaName = "id", Required = true, HelpText = "hotspot address")]
    public string hotspotId { get; set; }
}