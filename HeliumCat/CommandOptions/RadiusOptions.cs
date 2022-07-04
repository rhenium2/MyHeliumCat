using CommandLine;

namespace HeliumCat.CommandOptions;

[Verb("radius", HelpText = "beacon stats of hotspots in a radius")]
public class RadiusOptions : TimedOption
{
    [Option("radius", Default = 1, HelpText = "radius n km to report")]
    public int Radius { get; set; }

    public override string ToString()
    {
        return $"{Identifier}, {GetPastText()}, {Radius}km";
    }
}