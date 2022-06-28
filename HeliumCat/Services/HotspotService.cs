using System.Globalization;
using HeliumCat.Responses;
using HeliumCat.Responses.Transactions;
using Newtonsoft.Json;

namespace HeliumCat.Services;

public static class HotspotService
{
    public static async Task<Hotspot> GetHotspot(string hotspotId)
    {
        var uri = $"/v1/hotspots/{hotspotId}";
        var data = await HeliumClient.Get(uri);
        return JsonConvert.DeserializeObject<Hotspot>(data.First());
    }

    public static async Task<Hotspot> GetHotspotByName(string name)
    {
        var uri = $"/v1/hotspots/name/{name}";
        var data = await HeliumClient.Get(uri);
        var hotspots = Extensions.DeserializeAll<Hotspot>(data.ToArray());
        if (hotspots.Count > 1)
        {
            throw new Exception("There are more than one hotspot for this name. Please use hotspot address instead.");
        }

        return hotspots.First();
    }

    /// <summary>
    /// Retrieves the list of hotspots the given hotspot witnessed over the last 5 days.
    /// </summary>
    /// <param name="hotspotId"></param>
    /// <returns></returns>
    public static async Task<List<Hotspot>> GetWitnessed(string hotspotId)
    {
        var uri = $"/v1/hotspots/{hotspotId}/witnessed";
        var allData = await HeliumClient.Get(uri);
        return Extensions.DeserializeAll<Hotspot>(allData.ToArray());
    }

    public static async Task<List<HotspotRolesResponse>> GetRoles(string hotspotId)
    {
        var uri = $"/v1/hotspots/{hotspotId}/roles";
        var allData = await HeliumClient.Get(uri);
        return Extensions.DeserializeAll<HotspotRolesResponse>(allData.ToArray());
    }

    public static async Task<Transaction> GetTransaction(string transactionHash)
    {
        var uri = $"/v1/transactions/{transactionHash}";
        var allData = await HeliumClient.Get(uri);
        var firstData = allData.First();
        return DeserializeTransaction(firstData);
    }

    /// <summary>
    /// Lists the challenge (receipts) that the given hotspot was a challenger, challengee or witness in
    /// </summary>
    /// <param name="hotspotId"></param>
    /// <param name="minTime"></param>
    /// <returns></returns>
    public static async Task<List<PocReceiptsTransaction>> GetChallenges(string hotspotId, DateTime? minTime)
    {
        var uri = $"/v1/hotspots/{hotspotId}/challenges";
        if (minTime.HasValue)
        {
            uri += "?min_time=" + minTime.Value.ToString("o", CultureInfo.InvariantCulture);
        }

        var allData = await HeliumClient.Get(uri);
        return Extensions.DeserializeAll<PocReceiptsTransaction>(allData.ToArray());
    }

    public static async Task<List<PocReceiptsTransaction>> GetNetworkChallenges(DateTime? minTime)
    {
        var uri = $"/v1/challenges";
        if (minTime.HasValue)
        {
            uri += "?min_time=" + minTime.Value.ToString("o", CultureInfo.InvariantCulture);
        }

        var allDataStrings = await HeliumClient.Get(uri);
        var transactions = Extensions.DeserializeAll<PocReceiptsTransaction>(allDataStrings.ToArray());
        return transactions;
    }

    public static async Task<List<PocReceiptsTransaction>> GetBeaconTransactions(string hotspotId, DateTime? minTime)
    {
        var uri = $"/v1/hotspots/{hotspotId}/challenges";
        if (minTime.HasValue)
        {
            uri += "?min_time=" + minTime.Value.ToString("o", CultureInfo.InvariantCulture);
        }

        var allData = await HeliumClient.Get(uri);
        var tt = Extensions.DeserializeAll<PocReceiptsTransaction>(allData.ToArray());
        return tt.Where(t => t.Path.First().Challengee.Equals(hotspotId)).ToList();
    }

    public static async Task<List<PocReceiptsTransaction>> GetWitnessedTransactions(string hotspotId, DateTime? minTime)
    {
        var uri = $"/v1/hotspots/{hotspotId}/challenges";
        if (minTime.HasValue)
        {
            uri += "?min_time=" + minTime.Value.ToString("o", CultureInfo.InvariantCulture);
        }

        var allData = await HeliumClient.Get(uri);
        var tt = Extensions.DeserializeAll<PocReceiptsTransaction>(allData.ToArray());
        return tt.Where(c => c.Path.Any()
                             && c.Path.First().Witnesses
                                 .Any(x => x.IsValid && x.Gateway.Equals(hotspotId)))
            .OrderByDescending(x => x.Time)
            .ToList();
    }

    private static Transaction DeserializeTransaction(string transactionString)
    {
        var transaction = JsonConvert.DeserializeObject<Transaction>(transactionString);
        if (transaction.Type.Equals(Constants.TransactionType.Rewards))
        {
            return JsonConvert.DeserializeObject<RewardsTransaction>(transactionString);
        }

        if (transaction.Type.Equals(Constants.TransactionType.PocReceipts))
        {
            return JsonConvert.DeserializeObject<PocReceiptsTransaction>(transactionString);
        }

        if (transaction.Type.Equals(Constants.TransactionType.StateChannelClose))
        {
            return JsonConvert.DeserializeObject<StateChannelCloseTransaction>(transactionString);
        }

        throw new InvalidCastException(transaction.Type);
    }

    public static async Task<List<Hotspot>> GetHotspotsByRadius(double lat, double lng, double radiusKm)
    {
        var uri = $"/v1/hotspots/location/distance?lat={lat}&lon={lng}&distance={radiusKm * 1000}";
        var allData = await HeliumClient.Get(uri);
        return Extensions.DeserializeAll<Hotspot>(allData.ToArray());
    }

    public static async Task<List<Hotspot>> GetHotspotsByBox(double swLat, double swLon, double neLat, double neLon)
    {
        var uri = $"/v1/hotspots/location/box?swlat={swLat}&swlon={swLon}&nelat={neLat}&nelon={neLon}";
        var allData = await HeliumClient.Get(uri);
        return Extensions.DeserializeAll<Hotspot>(allData.ToArray());
    }
}