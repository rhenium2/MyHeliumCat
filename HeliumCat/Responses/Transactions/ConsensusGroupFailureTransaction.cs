using Newtonsoft.Json;

namespace HeliumCat.Responses.Transactions;

public class ConsensusGroupFailureTransaction : Transaction
{
    [JsonProperty("time")] public int Time { get; set; }

    [JsonProperty("signatures")] public List<string> Signatures { get; set; }

    [JsonProperty("members")] public List<string> Members { get; set; }

    [JsonProperty("height")] public int Height { get; set; }

    [JsonProperty("hash")] public string Hash { get; set; }

    [JsonProperty("failed_members")] public List<string> FailedMembers { get; set; }

    [JsonProperty("delay")] public int Delay { get; set; }
}