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
            SetupKeys();
            const string faceToMatch = @"C:\Users\richard.hopwood\Documents\HackManchester2016\TestImages\MissingPeople\Matt.jpg";
		    var acquisition =new Acquisition(new ImageStore());
		    var matcher=new MugMatcher(acquisition);
		    var imageFetchRequest=new ImageFetchRequest(new GeoLocation());
	        
            var found = matcher.Find(faceToMatch, imageFetchRequest);
			Assert.That(found, Is.True);
	    }

        private void SetupKeys()
        {
            ConfigurationManager.AppSettings["TemporaryImageStorePath"] = @"F:\Temp\scanned"; //WHERE IMAGES DOWNLOADED OFF THE INTERNET ARE STORED

            ConfigurationManager.AppSettings["LocalImageSearchPath"] = @"C:\Users\richard.hopwood\Documents\HackManchester2016\TestImages\ImagesToScan"; //LOCAL IMAGES TO SCAN
            ConfigurationManager.AppSettings["TwitterAccessKey"] = "qLy8zHnWBYMC17x7IH6jx3fee";
            ConfigurationManager.AppSettings["TwitterSecretKey"] = "osxaNQhJbHvsfvEkKB4qsJnV4GbGAtFN65aQyJ3S1Sj3ClA6es";
            ConfigurationManager.AppSettings["InstagramAccessToken"] = "4092770693.e029fea.71fb21e304284dce9496cb4cdb635b8b";
            ConfigurationManager.AppSettings["GoogleAPIKey"] = "AIzaSyB-LOommQ-bfWysfqgnql6aOweiHy4KZCM";
            ConfigurationManager.AppSettings["CustomSearchEngineId"] = "002232642848038589219%3Aoizy299vm8k";
            
        }
    }
}
