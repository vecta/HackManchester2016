using System.IO;
using NUnit.Framework;

namespace MugMatcher.Tests
{
	[TestFixture]
    public class ScannerTests
    {
	    [Test]
	    public void Scan_WithMultipleFaceImage_ReturnsFaces()
	    {
		    const string faceToMatch = @"TestImages\MissingPeople\Matt.jpg";
		    //const string imageToSearch = @"TestImages\ImagesToScan\IMG_20161029_152601.jpg";
		    const string imageToSearch = @"TestImages\MissingPeople\Matt.jpg";
		    var scanner = new Scanner();
		    var result = scanner.Scan(faceToMatch, imageToSearch);
			Assert.That(result.Score, Is.GreaterThan(0));
	    }
    }
}
