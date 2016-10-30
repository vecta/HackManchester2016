namespace MugMatcher.ImageProvider
{
    public class LocalImageFetchResult : ImageFetchResult
    {
        public LocalImageFetchResult(string imageLocation, string reference) : base(imageLocation, reference, "local") { }
    }
}