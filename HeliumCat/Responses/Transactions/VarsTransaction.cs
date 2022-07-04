using Newtonsoft.Json;

namespace HeliumCat.Responses.Transactions;

public class VarsTransaction : Transaction
{
    [JsonProperty("version_predicate")] public int VersionPredicate { get; set; }

    [JsonProperty("vars")] public Vars Vars { get; set; }

    [JsonProperty("unsets")] public List<object> Unsets { get; set; }

    [JsonProperty("time")] public int Time { get; set; }

    [JsonProperty("proof")] public string Proof { get; set; }

    [JsonProperty("nonce")] public int Nonce { get; set; }

    [JsonProperty("master_key")] public object MasterKey { get; set; }

    [JsonProperty("key_proof")] public string KeyProof { get; set; }

    [JsonProperty("height")] public int Height { get; set; }

    [JsonProperty("hash")] public string Hash { get; set; }

    [JsonProperty("cancels")] public List<object> Cancels { get; set; }
}

public class Vars
{
    [JsonProperty("poc_targeting_version")]
    public int PocTargetingVersion { get; set; }

    [JsonProperty("poc_target_pool_size")] public int PocTargetPoolSize { get; set; }

    [JsonProperty("poc_target_hex_parent_res")]
    public int PocTargetHexParentRes { get; set; }

    [JsonProperty("poc_target_hex_collection_res")]
    public int PocTargetHexCollectionRes { get; set; }

    [JsonProperty("poc_hexing_type")] public string PocHexingType { get; set; }

    [JsonProperty("h3dex_gc_width")] public int H3dexGcWidth { get; set; }
}