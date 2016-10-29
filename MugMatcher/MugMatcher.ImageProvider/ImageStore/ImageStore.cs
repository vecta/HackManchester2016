using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Media.Imaging;

namespace MugMatcher.ImageProvider.ImageStore
{
    public class ImageStore : IImageStore
    {
        private List<string> Images = new List<string>();

        public void Add(ImageFetchResult fetchResult)
        {
            if (fetchResult is LocalImageFetchResult)
            {
                AddLocalImage(fetchResult.ImageLocation);
            }
            else
            {
                AddRemoteImage(fetchResult.ImageLocation);
            }
        }

        private void AddRemoteImage(string url)
        {
            using (var webClient = new WebClient())
            {
                var data = webClient.DownloadData(url);
                SaveImage(GetNewFileName(url), data);             
            }
        }

        private void AddLocalImage(string filePath)
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

        public ImageMetaData GetImageMetaData(string filePath)
        {
            var imageData = new ImageMetaData();
            using (Stream fs = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                var decoder = BitmapDecoder.Create(fs, BitmapCreateOptions.None, BitmapCacheOption.Default);
                var frame = decoder.Frames[0]; 
                var metadata = frame.Metadata as BitmapMetadata;
                if (metadata != null)
                {
                    imageData.TimeStamp = metadata.DateTaken;
                    imageData.Location = metadata.Location;
                }
                fs.Close();
            }

            return imageData;
        }
    }
}
