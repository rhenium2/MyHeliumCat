using LocalObjectCache;

namespace HeliumCat.Models;

public class CurrencyRate
{
    [Index]
    public string BaseCurrency { get; set; }
    [Index]
    public string ExchangeCurrency { get; set; }
    // Date without time
    [Index]
    public DateTime Date { get; set; }
    public decimal Rate { get; set; }
}