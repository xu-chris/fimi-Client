using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace General.Session
{
    [Serializable]
    public class User
    {
        public readonly List<Session> sessions = new List<Session>();
        public Guid id;
        public string name;

        public bool inASession = false;
        public int currentSession = 0;

        public User(string name)
        {
            this.name = name;
            id = Guid.NewGuid();
        }

        [JsonConstructor]
        public User(List<Session> sessions, string name, string id, bool inASession, int currentSession)
        {
            this.sessions = sessions;
            this.name = name;
            this.id = Guid.Parse(id);
            this.inASession = inASession;
            this.currentSession = currentSession;
        }

        public Guid GetId()
        {
            return id;
        }

        public void StartNewSession(int trainingId)
        {
            var newSession = new Session(trainingId);
            sessions.Add(newSession);
            inASession = true;
            currentSession = sessions.Count - 1;
        }

        [CanBeNull]
        public TrainingReport GetLastReport(int trainingId)
        {
            return (from session in sessions where session.report.GetTrainingId() == trainingId select session.report).LastOrDefault();
        }

        public TrainingReport GetPreviousReport(int trainingId)
        {
            var fittingTrainings = sessions.FindAll(s => s.report.GetTrainingId() == trainingId);
            return fittingTrainings.Count - 2 >= 0 ? fittingTrainings[fittingTrainings.Count - 2].report : null;
        }

        public void AddToCurrentSession(ExerciseReport report) 
        {
            try
            {
                var trainingReport = sessions[currentSession].report;
                trainingReport.AddToReport(report);
            }
            catch
            {
                throw new NoCurrentSessionException("Adding report to session failed: No session currently set as active / current.");
            }
        }

        public void EndCurrentSession()
        {
            inASession = false;
        }
    }
}