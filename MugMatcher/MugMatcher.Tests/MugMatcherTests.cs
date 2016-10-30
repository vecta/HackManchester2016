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
		    const string faceToMatch = @"C:\Users\richard.hopwood\Documents\HackManchester2016\TestImages\MissingPeople\Matt.jpg";
		    IImageFetcher imageFetcher=new LocalImageFetcher();
		    var matcher=new MugMatcher(imageFetcher);
		    ImageFetchRequest imageFetchRequest=new ImageFetchRequest(null);
		    ConfigurationManager.AppSettings["LocalImageSearchPath"] = @"F:\Temp\scanned";
			var found = matcher.Find(faceToMatch, imageFetchRequest);
			Assert.That(found, Is.True);
	    }
    }
}
