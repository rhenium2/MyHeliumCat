using HeliumCat.Helpers;
using HeliumCat.Responses.Transactions;
using Newtonsoft.Json;

namespace HeliumCat.Responses;

public class Hotspot
{
    [JsonProperty("speculative_nonce")] public int SpeculativeNonce { get; set; }

    [JsonProperty("lng")] public double Lng { get; set; }

    [JsonProperty("lat")] public double Lat { get; set; }

    [JsonProperty("timestamp_added")] public DateTime TimestampAdded { get; set; }

    [JsonProperty("status")] public Status Status { get; set; }

    [JsonProperty("reward_scale")] public double? RewardScale { get; set; }

    [JsonProperty("payer")] public string Payer { get; set; }

    [JsonProperty("owner")] public string Owner { get; set; }

    [JsonProperty("nonce")] public int Nonce { get; set; }

    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("mode")] public string Mode { get; set; }

    [JsonProperty("location_hex")] public string LocationHex { get; set; }

    [JsonProperty("location")] public string Location { get; set; }

    [JsonProperty("last_poc_challenge")] public int? LastPocChallenge { get; set; }

    [JsonProperty("last_change_block")] public int LastChangeBlock { get; set; }

    [JsonProperty("geocode")] public Geocode Geocode { get; set; }

    [JsonProperty("gain")] public int Gain { get; set; }

    [JsonProperty("elevation")] public int Elevation { get; set; }

    [JsonProperty("block_added")] public int BlockAdded { get; set; }

    [JsonProperty("block")] public int Block { get; set; }

    [JsonProperty("address")] public string Address { get; set; }

    public override string ToString()
    {
        var maker = Constants.Makers.FirstOrDefault(x => x.Address.Equals(Payer));
        var makerName = maker != null ? maker.Name : Payer;
        var gain = Extensions.GetGain(Gain);
        return $"{Name} {{{makerName}, {gain}dBi, {Elevation}m}}";
    }
}

public class Status
{
    [JsonProperty("timestamp")] public DateTime? Timestamp { get; set; }

    [JsonProperty("online")] public string Online { get; set; }

    [JsonProperty("listen_addrs")] public List<string> ListenAddrs { get; set; }

    [JsonProperty("height")] public int? Height { get; set; }
}