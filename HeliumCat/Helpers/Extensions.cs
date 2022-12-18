using System.Reflection;
using HeliumCat.Services;

namespace HeliumCat.Helpers;

public static class Extensions
{
    public static void WriteHeader()
    {
        var assemblyName = Assembly.GetExecutingAssembly().GetName();
        Console.WriteLine($"{assemblyName.Name} {assemblyName.Version.ToString(3)}");
    }
    
    public static async Task CheckForNewVersion()
    {
        var version = Assembly.GetExecutingAssembly().GetName().Version;
        var latestRelease = await GithubService.GetLatestRelease();
        var latestReleaseVersion = new Version(latestRelease.name);
        if (latestReleaseVersion > version)
        {
            var message =
                $"New version v{latestReleaseVersion} is available. \n Download now at {latestRelease.html_url}";
            Console.WriteLine(message);
        }
    }
    
    public static void AddIfNotNull<T>(this List<T> list, Nullable<T> item) where T : struct
    {
        if (item.HasValue)
        {
            list.Add(item.Value);
        }
    }
}