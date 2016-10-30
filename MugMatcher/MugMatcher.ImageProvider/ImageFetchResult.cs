namespace MugMatcher.ImageProvider
{
    public class ImageFetchResult
    {
        public string ImageLocation { get; private set; }
        public string Reference { get; protected set; }
        public string Source { get; protected set; }

        public ImageFetchResult(string location, string reference, string source)
        {
            ImageLocation = location;
            Reference = reference;
            Source = source;
        }
    }
}