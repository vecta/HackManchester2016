namespace MugMatcher.ImageProvider
{
    public class ImageFetchResult
    {
        public string ImageLocation { get; private set; }
        public string Reference { get; protected set; }

        public ImageFetchResult(string location, string reference)
        {
            ImageLocation = location;
            Reference = reference;
        }
    }
}