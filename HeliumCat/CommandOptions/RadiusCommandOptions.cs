using CommandLine;

namespace HeliumCat.CommandOptions;

[Verb("radius", HelpText = "beacon stats of hotspots in a radius")]
public class RadiusCommandOptions
{
    [Value(0, MetaName = "hotspot name", Required = true, HelpText = "hotspot animal name")]
    public string name { get; set; }

    [Option("past", Default = 1, HelpText = "past n minutes to report")]
    public int pastMinutes { get; set; }

    [Option("radius", Default = 1, HelpText = "radius n km to report")]
    public int radius { get; set; }

    public override string ToString()
    {
        return $"{name}, past {pastMinutes} minutes, {radius}km";
    }
}