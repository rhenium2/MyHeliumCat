using Newtonsoft.Json;

namespace HeliumCat.Responses.Transactions;

public class PocReceiptsV2Transaction : Transaction
{
    [JsonProperty("time")] public int Time { get; set; }

    [JsonProperty("secret")] public string Secret { get; set; }

    [JsonProperty("path")] public List<Path> Path { get; set; }

    [JsonProperty("onion_key_hash")] public string OnionKeyHash { get; set; }

    [JsonProperty("height")] public int Height { get; set; }

    [JsonProperty("hash")] public string Hash { get; set; }

    [JsonProperty("fee")] public int Fee { get; set; }

    [JsonProperty("challenger_owner")] public string ChallengerOwner { get; set; }

    [JsonProperty("challenger")] public string Challenger { get; set; }

    [JsonProperty("block_hash")] public string BlockHash { get; set; }
}

public class Geocode
{
    [JsonProperty("short_street")] public string ShortStreet { get; set; }

    [JsonProperty("short_state")] public string ShortState { get; set; }

    [JsonProperty("short_country")] public string ShortCountry { get; set; }

    [JsonProperty("short_city")] public string ShortCity { get; set; }

    [JsonProperty("long_street")] public string LongStreet { get; set; }

    [JsonProperty("long_state")] public string LongState { get; set; }

    [JsonProperty("long_country")] public string LongCountry { get; set; }

    [JsonProperty("long_city")] public string LongCity { get; set; }

    [JsonProperty("city_id")] public string CityId { get; set; }
}

public class Path
{
    [JsonProperty("witnesses")] public List<Witness> Witnesses { get; set; }

    [JsonProperty("receipt")] public Receipt Receipt { get; set; }

    [JsonProperty("geocode")] public Geocode Geocode { get; set; }

    [JsonProperty("challengee_owner")] public string ChallengeeOwner { get; set; }

    [JsonProperty("challengee_lon")] public double ChallengeeLon { get; set; }

    [JsonProperty("challengee_location_hex")]
    public string ChallengeeLocationHex { get; set; }

    [JsonProperty("challengee_location")] public string ChallengeeLocation { get; set; }

    [JsonProperty("challengee_lat")] public double ChallengeeLat { get; set; }

    [JsonProperty("challengee")] public string Challengee { get; set; }
}

public class Receipt
{
    [JsonProperty("tx_power")] public int TxPower { get; set; }

    [JsonProperty("timestamp")] public long Timestamp { get; set; }

    [JsonProperty("snr")] public double Snr { get; set; }

    [JsonProperty("signal")] public int Signal { get; set; }

    [JsonProperty("origin")] public string Origin { get; set; }

    [JsonProperty("gateway")] public string Gateway { get; set; }

    [JsonProperty("frequency")] public double Frequency { get; set; }

    [JsonProperty("datarate")] public string Datarate { get; set; }

    [JsonProperty("data")] public string Data { get; set; }

    [JsonProperty("channel")] public int Channel { get; set; }
}

public class Witness
{
    [JsonProperty("timestamp")] public object Timestamp { get; set; }

    [JsonProperty("snr")] public double Snr { get; set; }

    [JsonProperty("signal")] public int Signal { get; set; }

    [JsonProperty("packet_hash")] public string PacketHash { get; set; }

    [JsonProperty("owner")] public string Owner { get; set; }

    [JsonProperty("location_hex")] public string LocationHex { get; set; }

    [JsonProperty("location")] public string Location { get; set; }

    [JsonProperty("is_valid")] public bool IsValid { get; set; }

    [JsonProperty("gateway")] public string Gateway { get; set; }

    [JsonProperty("frequency")] public double Frequency { get; set; }

    [JsonProperty("datarate")] public string Datarate { get; set; }

    [JsonProperty("channel")] public int Channel { get; set; }

    [JsonProperty("invalid_reason")] public string InvalidReason { get; set; }
}