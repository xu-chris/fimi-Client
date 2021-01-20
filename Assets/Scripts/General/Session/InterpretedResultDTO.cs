using System.Collections.Generic;
using System.Linq;
using IBM.Cloud.SDK.Plugins.WebSocketSharp;
using JetBrains.Annotations;

namespace General.Session
{
    public class InterpretedResultDTO
    {
        public class Content
        {
            public string name;
            public string explanation;
        }

        public string trainingName;
        public int totalDurationInSeconds;
        public List<Content> improvements;
        public List<Content> ruleViolations;

        public InterpretedResultDTO(string trainingName, int totalDurationInSeconds, TrainingReport trainingReport,
            [CanBeNull] TrainingReport previousReport)
        {
            this.trainingName = trainingName;
            this.totalDurationInSeconds = totalDurationInSeconds;
            improvements = GetTopThreeImprovements(trainingReport.GetImprovementsComparedTo(previousReport));
            ruleViolations = GetTopThreeViolations(trainingReport);
        }
        
        private static List<Content> GetTopThreeImprovements(List<Result> results)
        {
            if (results.Count > 3)
            {
                return results.ToArray().SubArray(0, 3).Select(
                    result => new Content
                    {
                        name = result.rule.name,
                        explanation = result.rule.improvementText
                    }
                ).ToList();
            }

            return results.Select(
                result => new Content
                {
                    name = result.rule.name,
                    explanation = result.rule.improvementText
                }
            ).ToList();
        }

        private static List<Content> GetTopThreeViolations(TrainingReport trainingReport)
        {
            if (trainingReport.GetResults().Length > 3)
            {
                return trainingReport.GetResults().SubArray(0, 3).Select(
                    trainingResult => new Content
                    {
                        name = trainingResult.rule.name,
                        explanation = trainingResult.rule.watchOutText
                    }
                ).ToList();
            }
            
            return trainingReport.GetResults().Select(
                trainingResult => new Content
                {
                    name = trainingResult.rule.name,
                    explanation = trainingResult.rule.watchOutText
                }
            ).ToList(); 
        }
    }
}