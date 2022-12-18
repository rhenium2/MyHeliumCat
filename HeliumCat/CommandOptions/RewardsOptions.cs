using CommandLine;

namespace HeliumCat.CommandOptions;

[Verb("rewards", HelpText = "rewards of hotspots in a financial year")]
public class RewardsOptions
{
    [Value(0, MetaName = "hotspot name or address", Required = true, HelpText = "hotspot name or address")]
    public string Identifier { get; set; }

    [Option("fy", HelpText = "financial year (e.g. 2022)", Required = true)]
    public int FinancialYear { get; set; }
    
    [Option("currency", HelpText = "currency")]
    public string Currency { get; set; }
}