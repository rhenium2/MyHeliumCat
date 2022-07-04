using Newtonsoft.Json;

namespace HeliumCat.Responses.Transactions;

public class PaymentV2Transaction : Transaction
{
    [JsonProperty("time")] public int Time { get; set; }

    [JsonProperty("payments")] public List<Payment> Payments { get; set; }

    [JsonProperty("payer")] public string Payer { get; set; }

    [JsonProperty("nonce")] public int Nonce { get; set; }

    [JsonProperty("height")] public int Height { get; set; }

    [JsonProperty("hash")] public string Hash { get; set; }

    [JsonProperty("fee")] public int Fee { get; set; }
}

public class Payment
{
    [JsonProperty("payee")] public string Payee { get; set; }

    [JsonProperty("memo")] public string Memo { get; set; }

    [JsonProperty("max")] public bool Max { get; set; }

    [JsonProperty("amount")] public long Amount { get; set; }
}