using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using NUnit.Framework;

namespace MugMatcher.ImageProvider.Tests
{
    [TestFixture]
    public class GoogleCSEImageFetcherTests
    {
        [Test]
        public void Can_Retrieve_Google_Image_Results()
        {
            var images = GetImages();
            Assert.IsNotEmpty(images);
        }

        [Test]
        public void Can_Retrieve_Individual_Image_Data()
        {
            var expected =
                "http://c8.alamy.com/comp/EEY25Y/16th-century-timbered-the-old-wellington-inn-1552-with-crowds-of-people-EEY25Y.jpg";
            var images = GetImages();
            var image = images.First().ImageLocation;
            Assert.AreEqual(image, expected);
        }

        [SetUp]
        public void Initialise()
        {
            ConfigurationManager.AppSettings["GoogleAPIKey"] = "AIzaSyB-LOommQ-bfWysfqgnql6aOweiHy4KZCM";
            ConfigurationManager.AppSettings["CustomSearchEngineId"] = "002232642848038589219%3Aoizy299vm8k";
        }

        private IEnumerable<ImageFetchResult> GetImages()
        {
            var imageFetcher = new GoogleCSEFetcher();
            var images =
            imageFetcher.Fetch(new ImageFetchRequest(new GeoLocation { Latitude = 53.477328, Longtitude = -2.254980 }, 20)
            {
                StartDate = DateTime.Today.AddDays(-30),
                EndDate = DateTime.Today
            });

            return images;
        }
    }
}
