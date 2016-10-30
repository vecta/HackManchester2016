using System.Collections.Generic;
using System.Configuration;
using NUnit.Framework;

namespace MugMatcher.ImageProvider.Tests
{
    [TestFixture]
    class ImageStoreTests
    {
        /// <summary>
        ///     WARNING, This will create images of chickens on your f: drive!!!!
        /// </summary>
        [Test]
        public void CanStoreImages()
        {
            ConfigurationManager.AppSettings["TemporaryImageStorePath"] = @"f:\temp";
            IImageStore imageStore = new ImageStore();
            var result = imageStore.StoreImages(new List<ImageFetchResult> {new ImageFetchResult("http://weknowyourdreams.com/images/chicken/chicken-05.jpg", "Test Image")});
        }
    }
}