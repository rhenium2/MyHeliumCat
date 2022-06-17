using System.Net;
using System.Text.Json.Nodes;
using Polly;

namespace HeliumInsighter.Services;

public static class HeliumClient
{
    private static readonly HttpClient httpClient;
    private static string baseUri = "https://api.helium.io";

    static HeliumClient()
    {
        httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("User-Agent", "Insight Client");
    }

    private static async Task<HttpResponseMessage> PollyGet(Func<Task<HttpResponseMessage>> func)
    {
        return await Policy.HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.TooManyRequests)
            .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(7 * retryAttempt)).ExecuteAsync(func);
    }

    public static async Task<List<string>> Get(string relativePath)
    {
        var uri = baseUri + relativePath;

        var responseMessage = await PollyGet(() => httpClient.GetAsync(uri));
        responseMessage.EnsureSuccessStatusCode();
        var content = await responseMessage.Content.ReadAsStringAsync();

        JsonNode? result;
        try
        {
            result = JsonNode.Parse(content);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        var allData = new List<string>();
        var data = result["data"].ToJsonString();

        // TODO: convert below to use yield 

        if (!string.IsNullOrEmpty(data) && data != "[]")
        {
            allData.Add(data);
        }

        var cursorString = result["cursor"]?.ToJsonString();
        if (!string.IsNullOrEmpty(cursorString))
        {
            var cursor = cursorString.Substring(1, cursorString.Length - 2);
            uri += uri.Contains('?') ? $"&cursor={cursor}" : $"?cursor={cursor}";
            responseMessage = await PollyGet(() => httpClient.GetAsync(uri));

            content = await responseMessage.Content.ReadAsStringAsync();
            try
            {
                result = JsonNode.Parse(content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            allData.Add(result["data"].ToJsonString());
        }

        return allData;
    }
}