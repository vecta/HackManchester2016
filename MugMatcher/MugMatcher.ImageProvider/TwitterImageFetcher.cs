
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

namespace MugMatcher.ImageProvider
{
    public class TwitterImageFetcher : IImageFetcher
    {
        private TwitterCredentials Credentials { get; set; }

        public TwitterImageFetcher()
        {
            Credentials = new TwitterCredentials(ConfigurationManager.AppSettings["TwitterAccessKey"], ConfigurationManager.AppSettings["TwitterSecretKey"]);
        }

        public IEnumerable<ImageFetchResult> Fetch(ImageFetchRequest request)
        {
            var results = new List<ImageFetchResult>();
            var searchTweetParameters = new SearchTweetsParameters(request.Location.Latitude, request.Location.Longtitude, request.SearchRadiusMiles, DistanceMeasure.Miles)
            {
                MaximumNumberOfResults = 100,
                Filters = TweetSearchFilters.Images
            };

            var tweets = Search.SearchTweets(searchTweetParameters).Where(tweet => tweet.Media != null);
            results.AddRange(tweets.Select(tweet => new ImageFetchResult(tweet.Url)));

            return results;
        }
    }
}
