using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;

namespace MugMatcher.ImageProvider
{
    public class ImageStore : IImageStore
    {
        public IEnumerable<LocalImageFetchResult> StoreImages(IEnumerable<ImageFetchResult> imageData) { return imageData.Select(StoreImage).ToList(); }

        private LocalImageFetchResult StoreImage(ImageFetchResult image) { return image is LocalImageFetchResult ? StoreLocalImage(image) : StoreRemoteImage(image); }

        private LocalImageFetchResult StoreRemoteImage(ImageFetchResult image)
        {
            using (var webClient = new WebClient())
            {
                var imageData = webClient.DownloadData(image.ImageLocation);
                var newFileName = GetNewFileName(StripQueryString(image.ImageLocation));
                SaveImage(newFileName, imageData);
                return new LocalImageFetchResult(newFileName, image.Reference);
            }
        }

        private LocalImageFetchResult StoreLocalImage(ImageFetchResult image)
        {
            var newFileName = GetNewFileName(image.ImageLocation);
            File.Copy(image.ImageLocation, newFileName);
            return new LocalImageFetchResult(newFileName, image.Reference);
        }

        private string StripQueryString(string url) { return url.Split('?')[0]; }

        private string GetNewFileName(string file)
        {
            return Path.Combine(ConfigurationManager.AppSettings["TemporaryImageStorePath"], $"{Guid.NewGuid()}.{file.Split('.').Last()}");
        }

        private void SaveImage(string filename, byte[] imageData)
        {
            using (var fileStream = File.Create(filename))
            {
                new MemoryStream(imageData).CopyTo(fileStream);
            }
        }

        //        public ImageMetaData GetImageMetaData(string image)
        //        {
        //            var imageData = new ImageMetaData();
        //            using (Stream fs = File.Open(image, FileMode.Open, FileAccess.ReadWrite))
        //            {
        //                var decoder = BitmapDecoder.Create(fs, BitmapCreateOptions.None, BitmapCacheOption.Default);
        //                var frame = decoder.Frames[0]; 
        //                var metadata = frame.Metadata as BitmapMetadata;
        //                if (metadata != null)
        //                {
        //                    imageData.TimeStamp = metadata.DateTaken;
        //                    imageData.Location = metadata.Location;
        //                }
        //                fs.Close();
        //            }
        //
        //            return imageData;
        //        }
    }
}