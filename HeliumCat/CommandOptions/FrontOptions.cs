using CommandLine;

namespace HeliumCat.CommandOptions;

[Verb("front", HelpText = "beacon stats of hotspots in front semi-circle")]
public class FrontOptions
{
    [Value(0, MetaName = "hotspot name", Required = true, HelpText = "hotspot animal name")]
    public string Name { get; set; }

    [Option("past", Default = 1, HelpText = "past n minutes to report")]
    public int PastMinutes { get; set; }

    [Option("radius", Default = 1, HelpText = "radius n km to report")]
    public int Radius { get; set; }

    public override string ToString()
    {
        return $"{Name}, past {PastMinutes} minutes, {Radius}km";
    }
}