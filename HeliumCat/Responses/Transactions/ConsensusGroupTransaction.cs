using Newtonsoft.Json;

namespace HeliumCat.Responses.Transactions;

public class ConsensusGroupTransaction : Transaction
{
    [JsonProperty("time")] public int Time { get; set; }

    [JsonProperty("proof")] public string Proof { get; set; }

    [JsonProperty("members")] public List<string> Members { get; set; }

    [JsonProperty("height")] public int Height { get; set; }

    [JsonProperty("hash")] public string Hash { get; set; }

    [JsonProperty("delay")] public int Delay { get; set; }
}