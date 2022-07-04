using Newtonsoft.Json;

namespace HeliumCat.Responses.Transactions;

public class RewardsV2Transaction : Transaction
{
    [JsonProperty("time")] public int Time { get; set; }

    [JsonProperty("start_epoch")] public int StartEpoch { get; set; }

    [JsonProperty("rewards")] public List<Reward> Rewards { get; set; }

    [JsonProperty("height")] public int Height { get; set; }

    [JsonProperty("hash")] public string Hash { get; set; }

    [JsonProperty("end_epoch")] public int EndEpoch { get; set; }
}

public class Reward
{
    [JsonProperty("type")] public string Type { get; set; }

    [JsonProperty("gateway")] public string Gateway { get; set; }

    [JsonProperty("amount")] public object Amount { get; set; }

    [JsonProperty("account")] public string Account { get; set; }
}