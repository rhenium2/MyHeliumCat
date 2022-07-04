using Newtonsoft.Json;

namespace HeliumCat.Responses.Transactions;

public class RoutingTransaction : Transaction
{
    [JsonProperty("time")] public int Time { get; set; }

    [JsonProperty("owner")] public string Owner { get; set; }

    [JsonProperty("oui")] public int Oui { get; set; }

    [JsonProperty("nonce")] public int Nonce { get; set; }

    [JsonProperty("height")] public int Height { get; set; }

    [JsonProperty("hash")] public string Hash { get; set; }

    [JsonProperty("fee")] public int Fee { get; set; }

    [JsonProperty("action")] public Action Action { get; set; }
}

public class Action
{
    [JsonProperty("index")] public int Index { get; set; }

    [JsonProperty("filter")] public string Filter { get; set; }

    [JsonProperty("action")] public string ActionText { get; set; }
}