using System.Configuration;
using NUnit.Framework;

namespace MugMatcher.ImageProvider.Tests
{
    [TestFixture]
    class AquisitionTests
    {
        [Test]
        public void CanGetImagesToDisk()
        {
            ConfigurationManager.AppSettings["LocalImageSearchPath"] = @"C:\Users\richard.hopwood\Documents\HackManchester2016\TestImages\ImagesToScan\";

            ConfigurationManager.AppSettings["GoogleAPIKey"] = "AIzaSyB-LOommQ-bfWysfqgnql6aOweiHy4KZCM";
            ConfigurationManager.AppSettings["CustomSearchEngineId"] = "002232642848038589219%3Aoizy299vm8k";

            ConfigurationManager.AppSettings["InstagramAccessTooken"] = "4092770693.e029fea.71fb21e304284dce9496cb4cdb635b8b";

            ConfigurationManager.AppSettings["TwitterAccessKey"] = "qLy8zHnWBYMC17x7IH6jx3fee";
            ConfigurationManager.AppSettings["TwitterSecretKey"] = "osxaNQhJbHvsfvEkKB4qsJnV4GbGAtFN65aQyJ3S1Sj3ClA6es";

            ConfigurationManager.AppSettings["TemporaryImageStorePath"] = @"f:\temp\scanned";

            var aquisition = new Acquisition(new ImageStore());
            var results = aquisition.FetchAll(new ImageFetchRequest(new GeoLocation(), 10));
            var a = 1;

        }
    }
}