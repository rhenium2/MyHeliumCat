using Newtonsoft.Json;

namespace HeliumCat.Responses;

public class HotspotRolesResponse
{
    [JsonProperty("type")] public string Type { get; set; }

    [JsonProperty("time")] public int Time { get; set; }

    [JsonProperty("role")] public string Role { get; set; }

    [JsonProperty("height")] public int Height { get; set; }

    [JsonProperty("hash")] public string Hash { get; set; }
}