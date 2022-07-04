using Newtonsoft.Json;

namespace HeliumCat.Responses.Transactions;

public class PaymentTransaction : Transaction
{
    [JsonProperty("time")] public int Time { get; set; }

    [JsonProperty("payer")] public string Payer { get; set; }

    [JsonProperty("payee")] public string Payee { get; set; }

    [JsonProperty("nonce")] public int Nonce { get; set; }

    [JsonProperty("height")] public int Height { get; set; }

    [JsonProperty("hash")] public string Hash { get; set; }

    [JsonProperty("fee")] public int Fee { get; set; }

    [JsonProperty("amount")] public long Amount { get; set; }
}