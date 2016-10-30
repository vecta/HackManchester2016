using System;
using System.Collections.Generic;

namespace MugMatcher.ImageProvider
{
    public interface IImageStore
    {
//        void Add(ImageFetchResult fetchResult);

//        IEnumerable<string> GetAllImagePaths();

//        ImageMetaData GetImageMetaData(string filePath);

        IEnumerable<LocalImageFetchResult> StoreImages(IEnumerable<ImageFetchResult> imageData);
    }
}
