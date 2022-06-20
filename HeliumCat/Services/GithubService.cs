using System.Reflection;
using Newtonsoft.Json;

namespace HeliumCat.Services;

public class GithubService
{
    public static async Task<GithubRelease> GetLatestRelease()
    {
        var projectPath = "rhenium2/HeliumCat";
        var releasePath = $"https://api.github.com/repos/{projectPath}/releases/latest";
        var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("User-Agent", assemblyName);
        var response = await httpClient.GetAsync(releasePath);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var githubRelease = JsonConvert.DeserializeObject<GithubRelease>(content);
        return githubRelease;
    }

    public class GithubRelease
    {
        public int id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string tag_name { get; set; }
        public string name { get; set; }
        public DateTime created_at { get; set; }
        public DateTime published_at { get; set; }
        public string body { get; set; }
    }
}