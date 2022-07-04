using CommandLine;

namespace HeliumCat.CommandOptions;

[Verb("front", HelpText = "beacon stats of hotspots in front semi-circle")]
public class FrontOptions : TimedOption
{
    [Option("radius", Default = 1, HelpText = "radius n km to report")]
    public int Radius { get; set; }

    public override string ToString()
    {
        return $"{Identifier}, {GetPastText()}, {Radius}km";
    }
}