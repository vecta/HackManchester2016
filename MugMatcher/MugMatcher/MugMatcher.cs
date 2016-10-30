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

		public bool Find(string referenceImagePath, ImageFetchRequest fetchRequest)
		{
			var scanner=new Scanner();
			var imageFetchResults = _acquisition.FetchAll(fetchRequest);
			foreach (var fetchResult in imageFetchResults)
			{
				var scanResult = scanner.Scan(referenceImagePath, fetchResult.ImageLocation);
				if (scanResult.Status == NBiometricStatus.Ok && scanResult.Score > 0)
					return true;
			}

			return false;
		}
	}
}
