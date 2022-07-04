using Newtonsoft.Json;

namespace HeliumCat.Responses;

public class Maker
{
    [JsonProperty("id")] public int Id { get; set; }

    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("address")] public string Address { get; set; }

    [JsonProperty("locationNonceLimit")] public int LocationNonceLimit { get; set; }

    [JsonProperty("createdAt")] public DateTime CreatedAt { get; set; }

    [JsonProperty("updatedAt")] public DateTime UpdatedAt { get; set; }
}