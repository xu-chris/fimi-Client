using System;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace General.Session
{
    [Serializable]
    public struct UserSession
    {
        public long timeStampCreated;
        public TrainingReport report;

        public UserSession(int trainingId)
        {
            timeStampCreated = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            report = new TrainingReport(trainingId);  
        }

        [JsonConstructor]
        public UserSession(long timeStampCreated, TrainingReport trainingReport)
        {
            this.timeStampCreated = timeStampCreated;
            this.report = trainingReport;
        }
        
        [CanBeNull]
        public Result GetHighestViolatedResult(int intervalInSeconds)
        {
            return report.GetFirstResultInTimeFrame(intervalInSeconds);
        }
    }
}