using System.Collections.Generic;
using System.IO;
using Neurotec.Biometrics;

namespace MugMatcher
{
	public class ScanResult
	{

		public ScanResult(string candidatePath)
		{
			File = Path.GetFileNameWithoutExtension(candidatePath);
		}

		public string File { get; private set; }
		public int Score { get; set; }
		public NBiometricStatus Status { get; set; }
		public IEnumerable<FaceResult> FaceResults { get; set; }
	}
}