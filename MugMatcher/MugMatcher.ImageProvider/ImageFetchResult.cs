namespace MugMatcher.ImageProvider
{
    public class ImageFetchResult
    {
        public string ImageLocation { get; protected set; }

        public ImageFetchResult(string location)
        {
            ImageLocation = location;
        }
    }
}