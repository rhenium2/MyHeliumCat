using CommandLine;

namespace HeliumCat.CommandOptions;

public abstract class TimedOption
{
    [Value(0, MetaName = "hotspot name or address", Required = true, HelpText = "hotspot name or address")]
    public string Identifier { get; set; }

    [Option("past-m", Default = 10, HelpText = "past n minutes to report", Group = "past")]
    public int PastMinutes { get; set; }

    [Option("past-h", HelpText = "past n hours to report", Group = "past")]
    public int PastHours { get; set; }

    [Option("past-d", HelpText = "past n days to report", Group = "past")]
    public int PastDays { get; set; }

    public override string ToString()
    {
        return $"{Identifier}, {GetPastText()}";
    }

    public string GetPastText()
    {
        if (PastDays > 0)
        {
            return $"past {PastDays} days";
        }

        if (PastHours > 0)
        {
            return $"past {PastHours} hours";
        }

        return $"past {PastMinutes} minutes";
    }

    public int GetPastMinutes()
    {
        if (PastDays > 0)
        {
            return PastDays * 1440;
        }

        if (PastHours > 0)
        {
            return PastHours * 60;
        }

        return PastMinutes;
    }
}