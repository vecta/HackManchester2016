using System;
using System.Collections.Generic;

namespace MugMatcher.ImageProvider
{
    public interface IImageStore
    {
        void AddRemoteImage(string url);

        void AddLocalImage(string filePath);

        List<string> GetAllImagePaths();

        ImageMetaData GetImageMetaData(string filePath);

    }
}
