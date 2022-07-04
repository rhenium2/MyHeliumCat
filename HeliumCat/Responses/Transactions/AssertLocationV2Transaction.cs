using Newtonsoft.Json;

namespace HeliumCat.Responses.Transactions;

public class AssertLocationV2Transaction : Transaction
{
    [JsonProperty("time")] public int Time { get; set; }

    [JsonProperty("staking_fee")] public int StakingFee { get; set; }

    [JsonProperty("payer")] public string Payer { get; set; }

    [JsonProperty("owner")] public string Owner { get; set; }

    [JsonProperty("nonce")] public int Nonce { get; set; }

    [JsonProperty("location")] public string Location { get; set; }

    [JsonProperty("lng")] public double Lng { get; set; }

    [JsonProperty("lat")] public double Lat { get; set; }

    [JsonProperty("height")] public int Height { get; set; }

    [JsonProperty("hash")] public string Hash { get; set; }

    [JsonProperty("gateway")] public string Gateway { get; set; }

    [JsonProperty("gain")] public int Gain { get; set; }

    [JsonProperty("fee")] public int Fee { get; set; }

    [JsonProperty("elevation")] public int Elevation { get; set; }
}