using Neurotec.Biometrics;

namespace MugMatcher
{
	public class ScanResult
	{
		public int Score { get; set; }
		public NBiometricStatus Status { get; set; }
	}
}