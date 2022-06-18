namespace HBeacons.Responses.Transactions;

public class RewardsTransaction : Transaction
{
    public int Time { get; set; }
    public int StartEpoch { get; set; }
    public List<Reward> Rewards { get; set; }
    public int Height { get; set; }
    public string Hash { get; set; }
    public int EndEpoch { get; set; }
}

public class Reward
{
    public string Type { get; set; }
    public string Gateway { get; set; }
    public object Amount { get; set; }
    public string Account { get; set; }
}