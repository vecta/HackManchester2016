using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Licensing;

namespace MugMatcher
{
    public class Scanner
    {
	    private readonly NBiometricClient _biometricClient;
	    private static bool _registered;
	    const string BiometricsComponents= "Biometrics.FaceExtraction,Biometrics.FaceMatching";

	    public Scanner()
		{
			if (!_registered)
				NLicense.ObtainComponents("/local", 5000, BiometricsComponents);
			_registered = true;

			_biometricClient = new NBiometricClient
			{
				MatchingThreshold = 48,
				FacesMatchingSpeed = NMatchingSpeed.Low
			};
		}

		private static NSubject CreateSubject(string fileName, bool isMultipleSubjects)
		{
			var subject = new NSubject { IsMultipleSubjects = isMultipleSubjects };
			var face = new NFace { FileName = fileName };
			subject.Faces.Add(face);
			return subject;
		}

		public ScanResult Scan(string referencePath, string candidatePath)
	    {
			//SqlLite.Register(biometricClient);

		    var reference = CreateSubject(referencePath, false);
		    var candidate = CreateSubject(candidatePath, true);
		    var result = new ScanResult();

		    _biometricClient.CreateTemplate(reference);
		    _biometricClient.CreateTemplate(candidate);

		    var enrollTask = _biometricClient.CreateTask(NBiometricOperations.Enroll, null);
		    candidate.Id = "Candidate_0";
		    enrollTask.Subjects.Add(candidate);
		    var subjectIndex = 0;
			foreach (var subject in candidate.RelatedSubjects)
			{
				subject.Id = $"Candidate_{++subjectIndex}";
				enrollTask.Subjects.Add(subject);
			}
			_biometricClient.PerformTask(enrollTask);

		    result.Status = _biometricClient.Identify(reference);
			if (reference.MatchingResults.Count>0)
				result.Score = reference.MatchingResults[0].Score;

		    return result;
	    }

	    public void Reset()
	    {
		    _biometricClient.Clear();
	    }
    }
}
