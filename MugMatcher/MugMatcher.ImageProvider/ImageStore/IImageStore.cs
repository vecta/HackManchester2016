using System;
using System.Collections.Generic;

namespace MugMatcher.ImageProvider
{
    public interface IImageStore
    {
        void Add(ImageFetchResult fetchResult);

        List<string> GetAllImagePaths();

        ImageMetaData GetImageMetaData(string filePath);

    }
}
