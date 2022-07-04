using Newtonsoft.Json;

namespace HeliumCat.Responses.Transactions;

public class TransferHotspotV2Transaction : Transaction
{
    [JsonProperty("time")] public int Time { get; set; }

    [JsonProperty("owner")] public string Owner { get; set; }

    [JsonProperty("nonce")] public int Nonce { get; set; }

    [JsonProperty("new_owner")] public string NewOwner { get; set; }

    [JsonProperty("height")] public int Height { get; set; }

    [JsonProperty("hash")] public string Hash { get; set; }

    [JsonProperty("gateway")] public string Gateway { get; set; }

    [JsonProperty("fee")] public int Fee { get; set; }
}