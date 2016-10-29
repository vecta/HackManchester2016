using System.Collections.Generic;

namespace MugMatcher.ImageProvider
{
    public class Acquisition
    {
        private readonly IEnumerable<IImageFetcher> _imageFetchers;

        public Acquisition() { _imageFetchers = new List<IImageFetcher> {new LocalImageFetcher()}; }

        public IEnumerable<ImageFetchResult> FetchAll(ImageFetchRequest request)
        {
            var reuslts = new List<ImageFetchResult>();
            foreach (var imageFetcher in _imageFetchers)
            {
                reuslts.AddRange(imageFetcher.Fetch(request));
            }
            return reuslts;

            //            List<Task<IEnumerable<ImageFetchResult>>> results = new List<Task<IEnumerable<ImageFetchResult>>>();
            //            foreach (var fetcher in _imageFetchers)
            //            {
            //                results.Add(fetcher.Fetch(request));
            //            }
            //
            //            var i = results.Select(async task => await task);
            //
            //            var m = results.Select(imageResults => imageResults.Result.Select(result => result)).ToList();
        }
    }
}