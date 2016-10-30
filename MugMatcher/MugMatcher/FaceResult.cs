using Neurotec.Biometrics;

namespace MugMatcher
{
	public class FaceResult
	{
		public FaceResult(NLAttributes attributes)
		{
			Age = attributes.Age;
			Gender = attributes.Gender;
			Glasses = (attributes.Properties & NLProperties.Glasses) == NLProperties.Glasses;
			Beard = (attributes.Properties & NLProperties.Beard) == NLProperties.Beard;
		}

		public bool Beard { get; private set; }
		public bool Glasses { get; private set; }
		public NGender Gender { get; private set; }
		public byte Age { get; private set; }
	}
}