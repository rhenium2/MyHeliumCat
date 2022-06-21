using CommandLine;

namespace HeliumCat.CommandOptions;

[Verb("radius", HelpText = "beacon stats of hotspots in a radius")]
public class RadiusOptions
{
    [Value(0, MetaName = "hotspot name or address", Required = true, HelpText = "hotspot name or address")]
    public string Identifier { get; set; }

    [Option("past", Default = 1, HelpText = "past n minutes to report")]
    public int PastMinutes { get; set; }

    [Option("radius", Default = 1, HelpText = "radius n km to report")]
    public int Radius { get; set; }

    public override string ToString()
    {
        return $"{Identifier}, past {PastMinutes} minutes, {Radius}km";
    }
}