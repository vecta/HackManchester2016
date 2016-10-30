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
        private readonly string _cseUrl = "https://www.googleapis.com/customsearch/v1?";
        private string _apiKey;
        private string _customSearchEngineId;

        public IEnumerable<ImageFetchResult> Fetch(ImageFetchRequest request)
        {       
            _apiKey = ConfigurationManager.AppSettings["GoogleAPIKey"];
            _customSearchEngineId = ConfigurationManager.AppSettings["CustomSearchEngineId"];
            var queryString = BuildQueryString(request);
            var data = GetFromUrl(queryString);
            var results = JsonConvert.DeserializeObject<GoogleCSESearchResult>(data);
            return results.items.Select(image => new ImageFetchResult(image.link, image.image.contextLink));
        }

        private string BuildQueryString(ImageFetchRequest request)
        {
            return $"{_cseUrl}key={_apiKey}&cx={_customSearchEngineId}&q=crowd%20of%20people%20manchester%20{DateTime.Now.Year}&searchType=image&imgType=face&daterestrict={request.StartDate:ddmmyyyy}:{request.EndDate:ddmmyyyy}&country=uk";
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
