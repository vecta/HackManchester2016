
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
        public TwitterImageFetcher()
        {
            Auth.SetApplicationOnlyCredentials(ConfigurationManager.AppSettings["TwitterAccessKey"], ConfigurationManager.AppSettings["TwitterSecretKey"], true);
        }

        public IEnumerable<ImageFetchResult> Fetch(ImageFetchRequest request)
        {
            var results = new List<ImageFetchResult>();
            var searchTweetParameters = new SearchTweetsParameters(request.Location.Latitude, request.Location.Longtitude, request.SearchRadiusMiles, DistanceMeasure.Miles)
            {
                MaximumNumberOfResults = 300,
                Filters = TweetSearchFilters.Images,
                Since = DateTime.Today,
//                Until = request.EndDate,
                TweetSearchType = TweetSearchType.OriginalTweetsOnly,
                
                
            };

            var resultSet = Search.SearchTweets(searchTweetParameters);
            var tweets = resultSet.Where(tweet => tweet.Media.Any());
            results.AddRange(tweets.Select(GetImageUrl));

            return results;
        }

        private ImageFetchResult GetImageUrl(ITweet tweet)
        {
            return new ImageFetchResult(tweet.Media.First().MediaURL, tweet.Url, "Twitter");
        }
    }
}
