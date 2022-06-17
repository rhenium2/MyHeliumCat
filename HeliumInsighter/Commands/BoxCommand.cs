using CommandLine;

namespace HeliumInsighter.Commands;

[Verb("box", HelpText = "beacon stats of hotspots in box area")]
public class BoxCommand
{
    [Value(0, MetaName = "id", Required = true, HelpText = "hotspot address")]
    public string hotspotId { get; set; }
}