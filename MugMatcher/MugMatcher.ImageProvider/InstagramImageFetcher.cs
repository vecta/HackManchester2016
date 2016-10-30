using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;

namespace MugMatcher.ImageProvider
{
    public class InstagramImageFetcher : IImageFetcher
    {
        private readonly string _accessToken;

        private string GetNearbyLocationsUrl(GeoLocation location) 
            => $"https://api.instagram.com/v1/locations/search?lat={location.Latitude}&lng={location.Longtitude}&access_token={_accessToken}";

        private string GetMediaFromLocationUrl(string locationId)
            => $"https://api.instagram.com/v1/locations/{locationId}/media/recent?access_token={_accessToken}";
        

        public InstagramImageFetcher()
        {
            _accessToken = ConfigurationManager.AppSettings["InstagramAccessTooken"];
        }

        public IEnumerable<ImageFetchResult> Fetch(ImageFetchRequest request)
        {
            var locationIds = GetNearbyLocationIds(request.Location);
            var foundImages = new List<ImageFetchResult>();

            foreach (var locationId in locationIds)
            {
                foundImages.AddRange(GetPhotosFromLocation(locationId));
            }

            return foundImages;
        }

        public IEnumerable<ImageFetchResult> GetPhotosFromLocation(string locationId)
        {
            var result = new List<ImageFetchResult>();

            var resultJson = UrlGetRequest(GetMediaFromLocationUrl(locationId));

            foreach (var postJson in resultJson["data"])
            {
                result.Add(new ImageFetchResult(postJson["images"]["standard_resolution"]["url"].ToString(), postJson["link"].ToString(), "Instagram"));
            }

            return result;
        }

        private IEnumerable<string> GetNearbyLocationIds(GeoLocation location)
        {
            var locationIds = new List<string>();

            var resultJson = UrlGetRequest(GetNearbyLocationsUrl(location));

            foreach (var locationJson in resultJson["data"])
            {
                locationIds.Add(locationJson["id"].ToString());
            }

            return locationIds;
        }

        private JObject UrlGetRequest(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = WebRequestMethods.Http.Get;
            request.Accept = "application/json";

            var response = (HttpWebResponse)request.GetResponse();

            string httpResult;
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                httpResult = sr.ReadToEnd();
            }

            return JObject.Parse(httpResult);
        }
    }
}
