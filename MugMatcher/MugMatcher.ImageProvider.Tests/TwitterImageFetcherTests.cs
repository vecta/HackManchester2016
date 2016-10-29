using System;
using System.Configuration;
using System.Linq;
using NUnit.Framework;

namespace MugMatcher.ImageProvider.Tests
{
    [TestFixture]
    public class TwitterImageFetcherTests
    {
        [Test]
        public void CanGetImagesFromTwitter()
        {
            ConfigurationManager.AppSettings["TwitterAccessKey"] = "qLy8zHnWBYMC17x7IH6jx3fee";
            ConfigurationManager.AppSettings["TwitterSecretKey"] = "osxaNQhJbHvsfvEkKB4qsJnV4GbGAtFN65aQyJ3S1Sj3ClA6es";

            var imageFetcher = new TwitterImageFetcher();
            var images =
                imageFetcher.Fetch(new ImageFetchRequest(new GeoLocation {Latitude = 53.477328, Longtitude = -2.254980}, 20)
                {
                    StartDate = DateTime.Today.AddDays(-30),
                    EndDate = DateTime.Today
                });
            Assert.That(images.Count(), Is.GreaterThan(0));
        }
    }
}