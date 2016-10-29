using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Licensing;

namespace MugMatcher
{
    public class Scanner
    {
	    const string BiometricsComponents= "Biometrics.FaceExtraction,Biometrics.FaceMatching";

		private static NSubject CreateSubject(string fileName, bool isMultipleSubjects)
		{
			var subject = new NSubject { IsMultipleSubjects = isMultipleSubjects };
			var face = new NFace { FileName = fileName };
			subject.Faces.Add(face);
			return subject;
		}

		public ScanResult Scan(string referencePath, string candidatePath)
	    {
		    NLicense.ObtainComponents("/local", 5000, BiometricsComponents);

		    var biometricClient = new NBiometricClient
		    {
			    MatchingThreshold = 48,
			    FacesMatchingSpeed = NMatchingSpeed.Low
		    };
			//SqlLite.Register(biometricClient);

		    var reference = CreateSubject(referencePath, false);
		    var candidate = CreateSubject(candidatePath, true);
		    var result = new ScanResult();

		    biometricClient.CreateTemplate(reference);
		    biometricClient.CreateTemplate(candidate);

		    var enrollTask = biometricClient.CreateTask(NBiometricOperations.Enroll, null);
		    candidate.Id = "Candidate_0";
		    enrollTask.Subjects.Add(candidate);
		    var subjectIndex = 0;
			foreach (var subject in candidate.RelatedSubjects)
			{
				//subjectIndex++;
				subject.Id = $"Candidate_{++subjectIndex}";
				enrollTask.Subjects.Add(subject);
			}
			biometricClient.PerformTask(enrollTask);

		    result.Status = biometricClient.Identify(reference);
			if (reference.MatchingResults.Count>0)
				result.Score = reference.MatchingResults[0].Score;

		    return result;
	    }
    }
}
