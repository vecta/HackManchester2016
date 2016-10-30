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
            const string faceToMatch = @"C:\Users\Matthew\Documents\HackManchester2016\TestImages\MissingPeople\Matt.jpg";
		    Acquisition acquisition =new Acquisition(new ImageStore());
		    var matcher=new MugMatcher(acquisition);
		    ImageFetchRequest imageFetchRequest=new ImageFetchRequest(null);
	        
            var found = matcher.Find(faceToMatch, imageFetchRequest);
			Assert.That(found, Is.True);
	    }

        private void SetupKeys()
        {
   
           
                ConfigurationManager.AppSettings["LocalImageSearchPath"] = @"ImageStore";
            ConfigurationManager.AppSettings["TemporaryImageStorePath"] = @"C:\Users\robert.marshall.FIOFFICE\Documents\HackManchester\TestImages\ImagesToScan";

            //ConfigurationManager.AppSettings["LocalImageSearchPath"] = @"C:\Temp\scanned";
            ConfigurationManager.AppSettings["TwitterAccessKey"] = "qLy8zHnWBYMC17x7IH6jx3fee";
            ConfigurationManager.AppSettings["TwitterSecretKey"] = "osxaNQhJbHvsfvEkKB4qsJnV4GbGAtFN65aQyJ3S1Sj3ClA6es";
            ConfigurationManager.AppSettings["InstagramAccessToken"] = "4092770693.e029fea.71fb21e304284dce9496cb4cdb635b8b";
            ConfigurationManager.AppSettings["GoogleAPIKey"] = "AIzaSyB-LOommQ-bfWysfqgnql6aOweiHy4KZCM";
            ConfigurationManager.AppSettings["CustomSearchEngineId"] = "002232642848038589219%3Aoizy299vm8k";
            //ConfigurationManager.AppSettings["TemporaryImageStorePath"] = @"C:\Users\Matthew\Documents\HackManchester2016\TestImages";
            
        }
    }
}
