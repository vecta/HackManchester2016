using System;
using System.Collections.Generic;
using System.Configuration;

namespace MugMatcher.ImageProvider
{
    public class FacebookImageFetcher : IImageFetcher
    {
        private string APIKey;
        private string SecretKey;

        public IEnumerable<ImageFetchResult> Fetch(ImageFetchRequest request)
        {
            APIKey = ConfigurationManager.AppSettings["FacebookApiKey"];
            SecretKey = ConfigurationManager.AppSettings["FacebookSecretKey"];
            //Facebook.
            return null;
        }
    }
}


//GraphRequest request = GraphRequest.newGraphPathRequest(
//  accessToken,
//  "/19292868552/photos",
//  new GraphRequest.Callback() {
//    @Override
//    public void onCompleted(GraphResponse response)
//{
//    // Insert your code here
//}
//});

//request.executeAsync();