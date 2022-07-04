using Newtonsoft.Json;

namespace HeliumCat.Responses.Transactions;

public class AddGatewayTransaction : Transaction
{
    [JsonProperty("time")] public int Time { get; set; }

    [JsonProperty("staking_fee")] public int StakingFee { get; set; }

    [JsonProperty("payer")] public string Payer { get; set; }

    [JsonProperty("owner")] public string Owner { get; set; }

    [JsonProperty("height")] public int Height { get; set; }

    [JsonProperty("hash")] public string Hash { get; set; }

    [JsonProperty("gateway")] public string Gateway { get; set; }

    [JsonProperty("fee")] public int Fee { get; set; }
}