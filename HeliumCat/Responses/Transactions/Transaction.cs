using Newtonsoft.Json;

namespace HeliumCat.Responses.Transactions;

public class Transaction
{
    [JsonProperty("type")] public string Type { get; set; }
}