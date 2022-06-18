namespace HBeacons.Responses.Transactions;

public class StateChannelCloseTransaction : Transaction
{
    public int Time { get; set; }
    public StateChannel StateChannel { get; set; }
    public int Height { get; set; }
    public string Hash { get; set; }
    public object ConflictsWith { get; set; }
    public string Closer { get; set; }
}

public class StateChannel
{
    public List<Summary> Summaries { get; set; }
    public string State { get; set; }
    public string RootHash { get; set; }
    public string Owner { get; set; }
    public int Nonce { get; set; }
    public string Id { get; set; }
    public int ExpireAtBlock { get; set; }
}

public class Summary
{
    public string Owner { get; set; }
    public int NumPackets { get; set; }
    public int NumDcs { get; set; }
    public string Location { get; set; }
    public string Client { get; set; }
}