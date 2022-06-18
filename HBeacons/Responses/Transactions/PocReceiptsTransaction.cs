namespace HBeacons.Responses.Transactions;

public class PocReceiptsTransaction : Transaction
{
    public int Time { get; set; }
    public string Secret { get; set; }
    public List<Path> Path { get; set; }
    public string OnionKeyHash { get; set; }
    public int Height { get; set; }
    public string Hash { get; set; }
    public int Fee { get; set; }
    public string ChallengerOwner { get; set; }
    public string Challenger { get; set; }
    public string BlockHash { get; set; }
}

public class Geocode
{
    public string ShortStreet { get; set; }
    public string ShortState { get; set; }
    public string ShortCountry { get; set; }
    public string ShortCity { get; set; }
    public string LongStreet { get; set; }
    public string LongState { get; set; }
    public string LongCountry { get; set; }
    public string LongCity { get; set; }
    public string CityId { get; set; }
}

public class Path
{
    public List<Witness> Witnesses { get; set; }
    public Receipt Receipt { get; set; }
    public Geocode Geocode { get; set; }
    public string ChallengeeOwner { get; set; }
    public double ChallengeeLon { get; set; }
    public string ChallengeeLocationHex { get; set; }
    public string ChallengeeLocation { get; set; }
    public double ChallengeeLat { get; set; }
    public string Challengee { get; set; }
}

public class Receipt
{
    public int TxPower { get; set; }
    public object Timestamp { get; set; }
    public double Snr { get; set; }
    public int Signal { get; set; }
    public string Origin { get; set; }
    public string Gateway { get; set; }
    public double Frequency { get; set; }
    public string Datarate { get; set; }
    public string Data { get; set; }
    public int Channel { get; set; }
}

public class Witness
{
    public object Timestamp { get; set; }
    public double Snr { get; set; }
    public int Signal { get; set; }
    public string PacketHash { get; set; }
    public string Owner { get; set; }
    public string LocationHex { get; set; }
    public string Location { get; set; }
    public bool IsValid { get; set; }
    public string Gateway { get; set; }
    public double Frequency { get; set; }
    public string Datarate { get; set; }
    public int Channel { get; set; }
    public string InvalidReason { get; set; }
}