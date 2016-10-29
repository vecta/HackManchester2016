using System;
using System.Collections.Generic;

namespace MugMatcher.ImageProvider
{
    public interface IImageStore
    {
        void AddRemoteImage(string url);

        void AddLocalImage(string uri);

        List<string> GetAllImagePaths();

    }
}
