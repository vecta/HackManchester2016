using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace MugMatcher.ImageProvider
{
    public class LocalImageFetcher : IImageFetcher
    {
        private const string ImageFileExtentions = "*.jpg;*.png;*.gif;*.tif;*.tiff;*.jpeg";

        public IEnumerable<ImageFetchResult> Fetch(ImageFetchRequest request)
        {
            var directoryInfo = new DirectoryInfo(ConfigurationManager.AppSettings["LocalImageSearchPath"]);
            return directoryInfo.GetFiles(ImageFileExtentions).Select(info => new LocalImageFetchResult(info.FullName));
        }
    }
}