using CommandLine;

namespace HeliumCat.CommandOptions;

[Verb("distance", HelpText = "distance stats of hotspot witnessed")]
public class DistanceOptions
{
    [Value(0, MetaName = "hotspot name or address", Required = true, HelpText = "hotspot name or address")]
    public string Identifier { get; set; }

    public override string ToString()
    {
        return $"{Identifier}";
    }
}