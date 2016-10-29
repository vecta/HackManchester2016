namespace MugMatcher.ImageProvider
{
    public class LocalImageFetchResult : ImageFetchResult
    {
        public LocalImageFetchResult(string imageLocation) : base(imageLocation, "Local File Image") { }
    }
}