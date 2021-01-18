using System.Collections.Generic;
using System.Linq;
using IBM.Cloud.SDK.Plugins.WebSocketSharp;
using JetBrains.Annotations;

namespace General.Session
{
    public class ResultDTO
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

        public ResultDTO(string trainingName, int totalDurationInSeconds, TrainingReport trainingReport,
            [CanBeNull] TrainingReport previousReport)
        {
            this.trainingName = trainingName;
            this.totalDurationInSeconds = totalDurationInSeconds;
            improvements = GetTopThreeImprovements(trainingReport.GetImprovementsComparedTo(previousReport));
            ruleViolations = GetTopThreeViolations(trainingReport);
        }
        
        private static List<Content> GetTopThreeImprovements(List<Result> results)
        {
            return results.ToArray().SubArray(0, 3).Select(
                result => new Content()
                {
                    name = result.rule.notificationText
                }
            ).ToList();
        }

        private static List<Content> GetTopThreeViolations(TrainingReport trainingReport)
        {
            return trainingReport.GetResults().SubArray(0, 3).Select(
                trainingResult => new Content()
                {
                    name = trainingResult.rule.notificationText
                }
                ).ToList();
        }
    }
}