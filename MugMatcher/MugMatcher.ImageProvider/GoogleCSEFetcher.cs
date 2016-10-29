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
            APIKey = ConfigurationManager.AppSettings["GoogleAPIKey"];
            CustomSearchEngineId = ConfigurationManager.AppSettings["CustomSearchEngineId"];
            var queryString = BuildQueryString(request);
            var data = GetFromUrl(queryString);
            var results = JsonConvert.DeserializeObject<GoogleCSESearchResult>(data);
            return results.items.Select(image => new ImageFetchResult(image.link, image.image.contextLink));
        }

        private string BuildQueryString(ImageFetchRequest request)
        {
            return $"{CseUrl}key={APIKey}&cx={CustomSearchEngineId}&q=crowd%20of%20people%20manchester%20{DateTime.Now.Year}&searchType=image&imgType=face&daterestrict={request.StartDate:ddmmyyyy}:{request.EndDate:ddmmyyyy}&country=uk";
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
