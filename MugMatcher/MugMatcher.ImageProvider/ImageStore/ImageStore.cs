using System;
using System.Collections.Generic;
using System.IO;
using System.Net;



namespace MugMatcher.ImageProvider.ImageStore
{
    public class ImageStore : IImageStore
    {
        private List<string> Images;

        public void AddRemoteImage(string url)
        {
            using (var webClient = new WebClient())
            {
                var data = webClient.DownloadData(url);
                SaveImage(GetNewFileName(url), data);             
            }
        }

        public void AddLocalImage(string filePath)
        {
            File.Copy(filePath, GetNewFileName(filePath));
        }

        public List<string> GetAllImagePaths()
        {
            return Images;
        }

        private string GetNewFileName(string file)
        {
            return $"{Guid.NewGuid()}.{file.Split('.')[1]}";
        }

        private void SaveImage(string uri, byte[] data)
        {
            Images.Add(uri);
            var stream = new MemoryStream(data);
            using (var fs = File.Create(uri))
            {
                stream.CopyTo(fs);
            }
        }
    }
}
