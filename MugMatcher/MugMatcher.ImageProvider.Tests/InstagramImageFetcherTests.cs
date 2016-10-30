using System.Configuration;
using System.Linq;
using NUnit.Framework;

namespace MugMatcher.ImageProvider.Tests
{
    [TestFixture]
    public class InstagramImageFetcherTests
    {
        [Test]
        public void CanGetImagesFromInstagram()
        {
            ConfigurationManager.AppSettings["InstagramAccessToken"] = "4092770693.e029fea.71fb21e304284dce9496cb4cdb635b8b";

            var imageFetcher = new InstagramImageFetcher();
            var images = imageFetcher.Fetch(new ImageFetchRequest(new GeoLocation { Latitude = 53.477328, Longtitude = -2.254980 }, 20));

            Assert.That(images.Count(), Is.GreaterThan(0));
        }
    }
}