using CommandLine;

namespace HeliumCat.CommandOptions;

[Verb("distance", HelpText = "distance stats of hotspot witnessed")]
public class DistanceOptions
{
    [Value(0, MetaName = "hotspot name", Required = true, HelpText = "hotspot animal name")]
    public string Name { get; set; }

    public override string ToString()
    {
        return $"{Name}";
    }
}