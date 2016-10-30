using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace MugMatcher.ImageProvider
{
    public class LocalImageFetcher : IImageFetcher
    {
	    private readonly string[] ImageFileExtentions = {".jpg",".png",".gif",".tif",".tiff",".jpeg"};

        public IEnumerable<ImageFetchResult> Fetch(ImageFetchRequest request)
        {
	        var appSetting = ConfigurationManager.AppSettings["LocalImageSearchPath"];
	        var directoryInfo = new DirectoryInfo(appSetting);
	        var files = directoryInfo.EnumerateFiles();
	        var fileInfos = files.Where(f=> ImageFileExtentions.Contains(f.Extension));
	        return fileInfos.Select(info => new LocalImageFetchResult(info.FullName));
        }
    }
}