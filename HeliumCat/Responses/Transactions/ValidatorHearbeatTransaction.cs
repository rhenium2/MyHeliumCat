using Newtonsoft.Json;

namespace HeliumCat.Responses.Transactions;

public class ValidatorHearbeatTransaction : Transaction
{
    [JsonProperty("version")] public int Version { get; set; }

    [JsonProperty("time")] public int Time { get; set; }

    [JsonProperty("signature")] public string Signature { get; set; }

    [JsonProperty("reactivated_gws")] public List<string> ReactivatedGws { get; set; }

    [JsonProperty("poc_key_proposals")] public List<string> PocKeyProposals { get; set; }

    [JsonProperty("height")] public int Height { get; set; }

    [JsonProperty("hash")] public string Hash { get; set; }

    [JsonProperty("address")] public string Address { get; set; }
}