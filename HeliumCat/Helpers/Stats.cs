using HeliumApi.SDK.Helpers;

namespace HeliumCat.Helpers;

public class Stats
{
    private readonly string _name;
    private readonly StatsKind _kind;
    private readonly List<double> _items = new();

    public Stats(string name, StatsKind kind = StatsKind.Number)
    {
        _name = name;
        _kind = kind;
    }

    public void Add(double item)
    {
        _items.Add(item);
    }

    public void Add(Nullable<double> item)
    {
        _items.AddIfNotNull(item);
    }

    public void WriteToConsole(string? format = null)
    {
        if (!_items.Any())
        {
            return;
        }

        var minValueString = ToFormattedString(_items.Min(), format);
        var avgValueString = ToFormattedString(_items.Average(), format);
        var maxValueString = ToFormattedString(_items.Max(), format);
        Console.Write($"{_name} {{ ");
        Console.Write($"min: {minValueString}, avg: {avgValueString}, max: {maxValueString}");
        Console.WriteLine(" }");
    }

    private string ToFormattedString(double num, string? format)
    {
        if (_kind == StatsKind.Distance || _kind == StatsKind.Length)
        {
            return HeliumHelpers.ToDistanceText(num);
        }

        return num.ToString(format);
    }
}

public enum StatsKind
{
    Number,
    Distance,
    Length
}