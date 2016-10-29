using System;
using System.IO;
using System.Linq;
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

			var biometricClient = new NBiometricClient();
			SqlLite.Register(biometricClient);
			biometricClient.MatchingThreshold = 48;
			biometricClient.FacesMatchingSpeed = NMatchingSpeed.Low;

			var reference = CreateSubject(referencePath, false);
		    var candidate = CreateSubject(candidatePath, true);
		    var result=new ScanResult();

		    var enrollTask = biometricClient.CreateTask(NBiometricOperations.Enroll, null);
		    enrollTask.Subjects.Add(candidate);
			biometricClient.PerformTask(enrollTask);

		    result.Status = biometricClient.Identify(reference);
			if (reference.MatchingResults.Count>0)
				result.Score = reference.MatchingResults[0].Score;

		    return result;
	    }
    }
}
