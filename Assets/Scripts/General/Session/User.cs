using System.Collections.Generic;
using System.Linq;
using General.Exercises;
using JetBrains.Annotations;
using NSubstitute.Core;
using NUnit.Framework;
using AssertionException = UnityEngine.Assertions.AssertionException;

namespace General.Session
{
    public class User
    {
        private readonly List<Session> sessions = new List<Session>();
        private int id;

        private int currentSession = -1;

        public User(int id)
        {
            this.id = id;
        }

        public int GetId()
        {
            return id;
        }

        public void StartNewSession(int trainingId)
        {
            var newSession = new Session(trainingId);
            sessions.Add(newSession);
            currentSession = sessions.Count - 1;
        }

        [CanBeNull]
        public TrainingReport GetLastReport(int trainingId)
        {
            return (from session in sessions where session.report.GetTrainingId() == trainingId select session.report).FirstOrDefault();
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
            currentSession = -1;
        }
    }
}