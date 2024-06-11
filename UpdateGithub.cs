using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TM.Desktop
{
    public static class UpdateGithub
    {
        public enum SizeUnits
        {
            Byte, KB, MB, GB, TB, PB, EB, ZB, YB
        }

        public static string ToSize(Int64 value, SizeUnits unit)
        {
            return (value / (double)Math.Pow(1024, (Int64)unit)).ToString("0.00");
        }
        public static AssemblyName getAppInformation()
        {
            var AppInformation = Assembly.GetExecutingAssembly().GetName();
            return AppInformation;
        }
        public static async Task<Release> GetLatestVersion(string token, string owner, string repoName)
        {

            var client = new GitHubClient(new Octokit.ProductHeaderValue(repoName));
            client.Credentials = new Credentials(token); // NOTE: not real token
            var latestVersion = await client.Repository.Release.GetLatest(owner, repoName);
            return latestVersion;
        }
        public static async Task<IReadOnlyList<Release>> GetReleases(string token, string owner, string repoName)
        {
            var client = new GitHubClient(new Octokit.ProductHeaderValue(repoName));
            client.Credentials = new Credentials(token); // NOTE: not real token
            var rels = await client.Repository.Release.GetAll(owner, repoName);
            return rels;
        }
        public static async Task<Release> GetRelease(string token, string owner, string repoName, string release)
        {
            var client = new GitHubClient(new Octokit.ProductHeaderValue(repoName));
            client.Credentials = new Credentials(token); // NOTE: not real token
            var rels = await client.Repository.Release.GetAll(owner, repoName);
            for (int i = 0; i < rels.Count; i++)
                if (rels[i].Name.Trim() == release.Trim()) return rels[i];
            return null;
        }
        public static async Task DownloadRelease(string fileName, string url, string zipPath)
        {
            //using (WebClient webClient = new WebClient())
            //{
            //    webClient.DownloadProgressChanged += DownloadProgressCallback;
            //    webClient.Credentials = CredentialCache.DefaultNetworkCredentials;
            //    await webClient.DownloadFileTaskAsync(new Uri(release.Assets[0].BrowserDownloadUrl), zipPath);
            //}
            WebClient webClient = new WebClient();
            //webClient.Headers.Add("user-agent", "Anything");
            //webClient.Headers.Add("authorization", "token " + GitHubToken);
            webClient.DownloadProgressChanged += (s, e) =>
            {
                //Console.WriteLine("{0} {1} - {2}. {3}% complete...", fileName, e.BytesReceived, e.TotalBytesToReceive, e.ProgressPercentage);
                Console.WriteLine("{0} {1} - {2}. {3}% complete...",
                    fileName,
                    $"{ToSize(BytesPerSecond(e.BytesReceived), SizeUnits.KB)} {SizeUnits.KB}/s",
                    $"{ToSize(e.BytesReceived, SizeUnits.MB)} of {ToSize(e.TotalBytesToReceive, SizeUnits.MB)} {SizeUnits.MB}",
                    e.ProgressPercentage);
                //Thread.Sleep(10000);
            };
            webClient.Proxy = GlobalProxySelection.GetEmptyWebProxy();
            //webClient.Proxy = WebRequest.DefaultWebProxy;
            await webClient.DownloadFileTaskAsync(new Uri(url), zipPath);
        }
        static DateTime lastUpdate;
        public static long BytesPerSecond(long bytes)
        {
            try
            {
                if (lastUpdate == default(DateTime))
                {
                    lastUpdate = DateTime.Now;
                    return 0;
                }
                else
                {
                    var timeSpan = DateTime.Now - lastUpdate;
                    if (timeSpan.TotalSeconds > 0)
                    {
                        var bytesPerSecond = bytes / (long)timeSpan.TotalSeconds;
                        return bytesPerSecond;
                    }
                }
                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
