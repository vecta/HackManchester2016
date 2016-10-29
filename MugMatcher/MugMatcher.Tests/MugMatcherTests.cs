using System;
using System.Configuration;
using System.IO;
using MugMatcher.ImageProvider;
using NUnit.Framework;

namespace MugMatcher.Tests
{
	[TestFixture]
    public class MugMatcherTests
    {
	    [Test]
	    public void Find_WithLocalImageFetcher_FindsFace()
	    {
		    const string faceToMatch = @"TestImages\MissingPeople\Matt.jpg";
		    IImageFetcher imageFetcher=new LocalImageFetcher();
		    var matcher=new MugMatcher(imageFetcher);
		    ImageFetchRequest imageFetchRequest=new ImageFetchRequest(null);
		    ConfigurationManager.AppSettings["LocalImageSearchPath"] = @"TestImages\ImagesToScan\";
			var found = matcher.Find(faceToMatch, imageFetchRequest);
			Assert.That(found, Is.True);
	    }
    }
}
