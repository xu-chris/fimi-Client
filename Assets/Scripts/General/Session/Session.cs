using System;
using Newtonsoft.Json;

namespace General.Session
{
    [Serializable]
    public struct Session
    {
        public long timeStampCreated;
        public TrainingReport report;

        public Session(int trainingId)
        {
            timeStampCreated = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            report = new TrainingReport(trainingId);  
        }

        [JsonConstructor]
        public Session(long timeStampCreated, TrainingReport trainingReport)
        {
            this.timeStampCreated = timeStampCreated;
            this.report = trainingReport;
        }
    }
}