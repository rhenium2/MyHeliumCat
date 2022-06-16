namespace HeliumInsighter.Responses;

public class Maker
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int LocationNonceLimit { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}