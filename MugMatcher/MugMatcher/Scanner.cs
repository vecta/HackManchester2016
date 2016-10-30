using System;
using System.Collections.Generic;
using System.Linq;
using Neurotec.Biometrics;
using Neurotec.Biometrics.Client;
using Neurotec.Licensing;

namespace MugMatcher
{
    public class Scanner
    {
	    private readonly NBiometricClient _biometricClient;
	    private static bool _registered;
	    const string BiometricsComponents= "Biometrics.FaceExtraction,Biometrics.FaceMatching,Biometrics.FaceSegmentsDetection";

	    public Scanner()
		{
			if (!_registered)
				NLicense.ObtainComponents("/local", 5000, BiometricsComponents);
			_registered = true;

			_biometricClient = new NBiometricClient
			{
				MatchingThreshold = 48,
				FacesMatchingSpeed = NMatchingSpeed.Low,
				FacesDetectBaseFeaturePoints = true,
				FacesDetectAllFeaturePoints = true,
				FacesRecognizeEmotion = true,
				FacesRecognizeExpression = true,
				FacesDetectProperties = true,
				FacesDetermineGender = true,
				FacesDetermineAge = true
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
		    var result = new ScanResult(candidatePath);

		    _biometricClient.CreateTemplate(reference);
		    _biometricClient.CreateTemplate(candidate);

		    var task = _biometricClient.CreateTask(NBiometricOperations.Enroll|NBiometricOperations.DetectSegments, null);
		    candidate.Id = "Candidate_0";
		    task.Subjects.Add(candidate);
		    var subjectIndex = 0;
			foreach (var subject in candidate.RelatedSubjects)
			{
				subject.Id = $"Candidate_{++subjectIndex}";
				task.Subjects.Add(subject);
			}
			_biometricClient.PerformTask(task);

		    result.Status = _biometricClient.Identify(reference);
		    if (reference.MatchingResults.Count > 0)
		    {
			    var matchingResult = reference.MatchingResults[0];
			    result.Score = matchingResult.Score;
			    var faceResults = new List<FaceResult>();
			    foreach (var face in candidate.Faces)
			    {
				    faceResults.AddRange(face.Objects.Select(attributes => new FaceResult(attributes)));
			    }
				result.FaceResults = faceResults;
			}

		    return result;
	    }

	    public void Reset()
	    {
		    _biometricClient.Clear();
	    }
    }
}
