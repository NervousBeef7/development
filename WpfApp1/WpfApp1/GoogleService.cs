using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class GoogleService
    {
        private string _engineId = "008991497332374052650:vwfgqaiwwrk";
        private string _appKey = "AIzaSyAsYsFtcOtG7F1Q57vXhs5VeykHX5gOR6w";
        private Context context;
        public GoogleService(Context c)
        {
            context = c;
        }
        public async Task<List<SearchResult>> GetResults(string query)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync($"https://www.googleapis.com/customsearch/v1?q={query}&searchType=image&cx={_engineId}&key={_appKey}");
                var data = JsonConvert.DeserializeObject<Result>(result);

                var results = data.Items.Select(item => new SearchResult
                {
                    Title = item.Title,
                    Description = item.Snippet,
                    URL = item.Link,
                    Path = DownloadResult(item.Link)
                }).ToList();

                SaveToDB(query, results);

                return results;
            }
        }

        void SaveToDB(string query, List<SearchResult> results)
        {
            var q = context.Quieries;
            q.Add(new Query { Results = results, URL = query });
            context.SaveChanges();

        }
        string DownloadResult(string url)
        {
            using (var client = new WebClient())
            {
                try
                {
                    var path = AppDomain.CurrentDomain.BaseDirectory + $"/images/{url.GetHashCode()}.jpg";
                    client.DownloadFileTaskAsync(new Uri(url), path);
                    return path;
                }
                catch
                {
                    return "";
                }
            }
        }
    }
}
