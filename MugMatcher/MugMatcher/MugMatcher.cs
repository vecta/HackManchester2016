using System.Linq;
using MugMatcher.ImageProvider;
using Neurotec.Biometrics;

namespace MugMatcher
{
	public class MugMatcher
	{
		private readonly IImageFetcher _imageFetcher;

		public MugMatcher(IImageFetcher imageFetcher)
		{
			_imageFetcher = imageFetcher;
		}

		public bool Find(string referenceImagePath, ImageFetchRequest fetchRequest)
		{
			var scanner=new Scanner();
			var imageFetchResults = _imageFetcher.Fetch(fetchRequest);
			scanner.AddCandidateImages(imageFetchResults.Select(result => result.ImageLocation));

			var scanResult = scanner.Scan(referenceImagePath);
			return scanResult.Score>0;
		}
	}
}
