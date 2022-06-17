using CommandLine;

namespace HeliumInsighter.Commands;

[Verb("front", HelpText = "beacon stats of hotspots in front semi-circle")]
public class FrontCommand
{
    [Value(0, MetaName = "hotspot id", Required = true, HelpText = "hotspot address")]
    public string hotspotId { get; set; }

    [Option("past", Default = 1, HelpText = "past n hours to report")]
    public int pastHours { get; set; }
}