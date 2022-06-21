using CommandLine;

namespace HeliumCat.CommandOptions;

[Verb("witnessed", HelpText = "witnessed stats of hotspot")]
public class WitnessedOptions
{
    [Value(0, MetaName = "hotspot name or address", Required = true, HelpText = "hotspot name or address")]
    public string Identifier { get; set; }

    [Option("past", Default = 10, HelpText = "past n minutes to report")]
    public int PastMinutes { get; set; }

    public override string ToString()
    {
        return $"{Identifier}, past {PastMinutes} minutes";
    }
}