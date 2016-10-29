using System.Collections.Generic;

namespace MugMatcher.ImageProvider
{
	public interface IImageFetcher
    {
        IEnumerable<ImageFetchResult> Fetch(ImageFetchRequest request);
    }
}