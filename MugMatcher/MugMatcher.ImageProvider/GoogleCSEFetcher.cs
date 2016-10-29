using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace MugMatcher.ImageProvider
{
    public class GoogleCSEFetcher : IImageFetcher
    {
        private string CseUrl = "https://www.googleapis.com/customsearch/v1?";
        private string APIKey;
        private string CustomSearchEngineId;

        public IEnumerable<ImageFetchResult> Fetch(ImageFetchRequest request)
        {
            var queryString = BuildQueryString(request);
            APIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];
            CustomSearchEngineId = ConfigurationManager.AppSettings["CustomSearchEngineId"];
            var data = GetFromUrl(queryString);
            var results = JsonConvert.DeserializeObject<GoogleCSESearchResult>(data);
            return results.items.Select(image => new ImageFetchResult(image.link, image.image.contextLink));
        }

        private string BuildQueryString(ImageFetchRequest request)
        {
            var sb = new StringBuilder();
            sb.Append(CseUrl);
            sb.Append($"key={APIKey}");
            sb.Append($"&cx={CustomSearchEngineId}");
            sb.Append($"q=crowd%20of%20people%20manchester");
            sb.Append($"&searchType=image");
            sb.Append($"&imgType=face");
            sb.Append($"&daterestrict={request.StartDate:ddmmyyyy}:{request.EndDate:ddmmyyyy}");
            sb.Append($"&country=uk");

            return sb.ToString();
        }

        private string GetFromUrl(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = request.GetResponse();
            using (var responseStream = response.GetResponseStream())
            {
                var reader = new StreamReader(responseStream, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }
    }
}
