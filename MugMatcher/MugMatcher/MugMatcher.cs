using System.Collections.Generic;
using MugMatcher.ImageProvider;
using Neurotec.Biometrics;

namespace MugMatcher
{
	public class MugMatcher
	{
		private readonly Acquisition _acquisition;

		public MugMatcher(Acquisition acquisition)
		{
			_acquisition = acquisition;
		}

		public IEnumerable<ScanResult> Find(string referenceImagePath, ImageFetchRequest fetchRequest)
		{
			var scanner=new Scanner();
			var imageFetchResults = _acquisition.FetchAll(fetchRequest);
			var results = new List<ScanResult>();
			foreach (var fetchResult in imageFetchResults)
			{
				scanner.Reset();
				var scanResult = scanner.Scan(referenceImagePath, fetchResult.ImageLocation);
				if (scanResult.Status == NBiometricStatus.Ok && scanResult.Score > 0)
					results.Add(scanResult);
			}

			return results;
		}
	}
}
