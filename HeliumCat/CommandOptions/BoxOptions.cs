using CommandLine;

namespace HeliumCat.CommandOptions;

[Verb("box", HelpText = "beacon stats of hotspots in box area")]
public class BoxOptions : TimedOption
{
}