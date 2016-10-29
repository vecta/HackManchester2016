using System.Collections.Generic;

namespace MugMatcher.ImageProvider
{
    public class LocalImageFetcher : IImageFetcher
    {
        public IEnumerable<ImageFetchResult> Fetch(ImageFetchRequest request) { yield break; }
    }
}