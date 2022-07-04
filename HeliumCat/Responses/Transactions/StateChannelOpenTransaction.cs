using Newtonsoft.Json;

namespace HeliumCat.Responses.Transactions;

public class StateChannelOpenTransaction : Transaction
{
    [JsonProperty("time")] public int Time { get; set; }

    [JsonProperty("owner")] public string Owner { get; set; }

    [JsonProperty("oui")] public int Oui { get; set; }

    [JsonProperty("nonce")] public int Nonce { get; set; }

    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("height")] public int Height { get; set; }

    [JsonProperty("hash")] public string Hash { get; set; }

    [JsonProperty("fee")] public int Fee { get; set; }

    [JsonProperty("expire_within")] public int ExpireWithin { get; set; }

    [JsonProperty("amount")] public int Amount { get; set; }
}