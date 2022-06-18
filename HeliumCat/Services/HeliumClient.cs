using System.Net;
using System.Text.Json.Nodes;
using Polly;

namespace HeliumCat.Services;

public static class HeliumClient
{
    private static readonly HttpClient HttpClient;
    private static string baseUri = "https://api.helium.io";

    static HeliumClient()
    {
        HttpClient = new HttpClient();
        HttpClient.DefaultRequestHeaders.Add("User-Agent", "HeliumCat");
    }

    private static async Task<HttpResponseMessage> PollyGet(Func<Task<HttpResponseMessage>> func)
    {
        return await Policy.HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.TooManyRequests)
            .WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(10 * retryAttempt)).ExecuteAsync(func);
    }

    public static async Task<List<string>> Get(string relativePath)
    {
        var allData = new List<string>();
        Response response = new Response();
        do
        {
            response = await CallAndGetResponse(relativePath, response.Cursor);
            if (!string.IsNullOrEmpty(response.Data))
            {
                allData.Add(response.Data);
            }
        } while (!string.IsNullOrEmpty(response.Cursor));

        return allData;
    }

    private static async Task<Response> CallAndGetResponse(string relativePath, string cursor)
    {
        var uri = baseUri + relativePath;
        if (!string.IsNullOrEmpty(cursor))
        {
            uri += uri.Contains('?') ? $"&cursor={cursor}" : $"?cursor={cursor}";
        }

        var responseMessage = await PollyGet(() => HttpClient.GetAsync(uri));
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

        var data = result["data"].ToJsonString();
        var cursorString = result["cursor"]?.ToJsonString();
        var response = new Response();
        if (!string.IsNullOrEmpty(data) && data != "[]")
        {
            response.Data = data;
        }

        if (!string.IsNullOrEmpty(cursorString))
        {
            response.Cursor = cursorString.Substring(1, cursorString.Length - 2);
        }

        return response;
    }
}