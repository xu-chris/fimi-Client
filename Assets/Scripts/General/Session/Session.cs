using System;

namespace General.Session
{
    public struct Session
    {
        public long timeStampCreated;
        public TrainingReport report;

        public Session(int trainingId)
        {
            timeStampCreated = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            report = new TrainingReport(trainingId);  
        }
    }
}