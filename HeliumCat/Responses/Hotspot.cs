using HeliumCat.Responses.Transactions;

namespace HeliumCat.Responses;

public class Hotspot
{
    public int SpeculativeNonce { get; set; }
    public double Lng { get; set; }
    public double Lat { get; set; }
    public DateTime TimestampAdded { get; set; }
    public Status Status { get; set; }
    public double? RewardScale { get; set; }
    public string Payer { get; set; }
    public string Owner { get; set; }
    public int Nonce { get; set; }
    public string Name { get; set; }
    public string Mode { get; set; }
    public string LocationHex { get; set; }
    public string Location { get; set; }
    public int? LastPocChallenge { get; set; }
    public int? LastChangeBlock { get; set; }
    public Geocode Geocode { get; set; }
    public int Gain { get; set; }
    public int Elevation { get; set; }
    public int BlockAdded { get; set; }
    public int Block { get; set; }
    public string Address { get; set; }

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
    public DateTime? Timestamp { get; set; }
    public string Online { get; set; }
    public List<string> ListenAddrs { get; set; }
    public int? Height { get; set; }
}