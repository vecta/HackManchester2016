using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace MugMatcher.ImageProvider
{
    public class LocalImageFetcher : IImageFetcher
    {
	    private readonly string[] _imageFileExtentions = {".jpg",".png",".gif",".tif",".tiff",".jpeg"};

        public IEnumerable<ImageFetchResult> Fetch(ImageFetchRequest request)
        {
            var directoryInfo = new DirectoryInfo(ConfigurationManager.AppSettings["LocalImageSearchPath"]);
	        var files = directoryInfo.EnumerateFiles();
	        var fileInfos = files.Where(f=> _imageFileExtentions.Contains(f.Extension));
	        return fileInfos.Select(info => new LocalImageFetchResult(info.FullName, info.FullName));
        }
    }
}