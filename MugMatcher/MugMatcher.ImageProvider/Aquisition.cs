using System.Collections.Generic;

namespace MugMatcher.ImageProvider
{
    public class Acquisition
    {
        private readonly IEnumerable<IImageFetcher> _imageFetchers;
        private readonly IImageStore _imageStore;

        public Acquisition(IImageStore imageStore)
        {
            _imageFetchers = new List<IImageFetcher> {new LocalImageFetcher(), new GoogleCSEFetcher(), new InstagramImageFetcher(), new TwitterImageFetcher()};
            _imageStore = imageStore;
        }

        public IEnumerable<ImageFetchResult> FetchAll(ImageFetchRequest request)
        {
            var reuslts = new List<ImageFetchResult>();
            foreach (var imageFetcher in _imageFetchers)
            {
                reuslts.AddRange(imageFetcher.Fetch(request));
            }
            return _imageStore.StoreImages(reuslts);
        }
    }
}