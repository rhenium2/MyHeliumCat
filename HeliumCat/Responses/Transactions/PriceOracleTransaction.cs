using Newtonsoft.Json;

namespace HeliumCat.Responses.Transactions;

public class PriceOracleTransaction : Transaction
{
    [JsonProperty("time")] public int Time { get; set; }

    [JsonProperty("public_key")] public string PublicKey { get; set; }

    [JsonProperty("price")] public int Price { get; set; }

    [JsonProperty("height")] public int Height { get; set; }

    [JsonProperty("hash")] public string Hash { get; set; }

    [JsonProperty("fee")] public int Fee { get; set; }

    [JsonProperty("block_height")] public int BlockHeight { get; set; }
}