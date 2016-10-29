using System.Collections.Generic;

namespace MugMatcher.ImageProvider
{
    internal interface IImageFetcher
    {
        IEnumerable<ImageFetchResult> Fetch(ImageFetchRequest request);
    }
}