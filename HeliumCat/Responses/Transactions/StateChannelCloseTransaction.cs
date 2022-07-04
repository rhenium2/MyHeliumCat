using Newtonsoft.Json;

namespace HeliumCat.Responses.Transactions;

public class StateChannelCloseTransaction : Transaction
{
    [JsonProperty("time")] public int Time { get; set; }

    [JsonProperty("state_channel")] public StateChannel StateChannel { get; set; }

    [JsonProperty("height")] public int Height { get; set; }

    [JsonProperty("hash")] public string Hash { get; set; }

    [JsonProperty("conflicts_with")] public object ConflictsWith { get; set; }

    [JsonProperty("closer")] public string Closer { get; set; }
}

public class StateChannel
{
    [JsonProperty("summaries")] public List<Summary> Summaries { get; set; }

    [JsonProperty("state")] public string State { get; set; }

    [JsonProperty("root_hash")] public string RootHash { get; set; }

    [JsonProperty("owner")] public string Owner { get; set; }

    [JsonProperty("nonce")] public int Nonce { get; set; }

    [JsonProperty("id")] public string Id { get; set; }

    [JsonProperty("expire_at_block")] public int ExpireAtBlock { get; set; }
}

public class Summary
{
    [JsonProperty("owner")] public string Owner { get; set; }

    [JsonProperty("num_packets")] public int NumPackets { get; set; }

    [JsonProperty("num_dcs")] public int NumDcs { get; set; }

    [JsonProperty("location")] public string Location { get; set; }

    [JsonProperty("client")] public string Client { get; set; }
}