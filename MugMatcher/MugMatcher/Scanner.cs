using System.Collections.Generic;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Licensing;

namespace MugMatcher
{
    public class Scanner
    {
	    private readonly NBiometricClient _biometricClient;
	    const string BiometricsComponents= "Biometrics.FaceExtraction,Biometrics.FaceMatching";

	    public Scanner()
		{
			NLicense.ObtainComponents("/local", 5000, BiometricsComponents);

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

		public ScanResult Scan(string referencePath)
	    {

			var result = new ScanResult();

			//SqlLite.Register(biometricClient);

			using (var reference = CreateSubject(referencePath, false))
			{
				_biometricClient.CreateTemplate(reference);

				result.Status = _biometricClient.Identify(reference);
				if (reference.MatchingResults.Count > 0)
					result.Score = reference.MatchingResults[0].Score;
			}
		    return result;
	    }

	    public void AddCandidateImages(IEnumerable<string> candidatePaths)
	    {
		    foreach (var candidatePath in candidatePaths)
			    using (var candidate = CreateSubject(candidatePath, true))
			    {
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
			    }
	    }
    }
}
