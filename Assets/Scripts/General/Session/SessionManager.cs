using System;
using System.Collections.Generic;
using System.Linq;
using General.Exercises;
using General.Trainings;
using Library;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Assertions;

namespace General.Session
{
    public class SessionManager : MonoBehaviour
    {
        
        public TextAsset trainingsConfigurationFile;
        public TextAsset exercisesConfigurationFile;
        public int maxNumberOfPeople = 1;

        private TrainingsConfiguration trainingsConfiguration;
        private ExercisesConfiguration exercisesConfiguration;

        private int selectedTraining = 0;
        private int currentExercise = 0;

        private readonly List<User> users = new List<User>();

        private ApplicationState state;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            
            // Read configurations
            trainingsConfiguration = new TrainingsConfigurationService(trainingsConfigurationFile).configuration;
            exercisesConfiguration = new ExercisesConfigurationService(exercisesConfigurationFile).configuration;
        }

        public List<Training> GetTrainings()
        {
            return trainingsConfiguration.trainings;
        }

        public void SetSelectedTraining(int id)
        {
            Debug.Log("Training selected: " + trainingsConfiguration.trainings[id].name);
            SetToSelectedTraining();
            foreach (var user in users)
            {
                user.StartNewSession(id);
            }
            selectedTraining = id;
        }

        public Training GetSelectedTraining()
        {
            return trainingsConfiguration.trainings[selectedTraining];
        }

        public void SetToNextExercise()
        {
            Assert.IsTrue(currentExercise < trainingsConfiguration.trainings[selectedTraining].exercises.Count);
            currentExercise++;
        }

        public void SetCurrentExercise(int id)
        {
            Debug.Log("Exercise set to current: " + trainingsConfiguration.trainings[selectedTraining].exercises[id].type + " (ID: " + id + ")");
            currentExercise = id;
        }

        public TrainingsDTO GetTrainingsDTO()
        {
            var dto = new TrainingsDTO(trainingsConfiguration.trainings, exercisesConfiguration.exercises,
                currentExercise);
            return dto;
        }

        private Exercise GetExerciseForExerciseType(ExerciseType exerciseType)
        {
            return exercisesConfiguration.exercises.Find(exercise =>
                exercise.type.ToExerciseType() == exerciseType);
        }

        public Exercise GetCurrentExercise()
        {
            var currentExerciseType =
                trainingsConfiguration.trainings[selectedTraining].exercises[currentExercise].type.ToExerciseType();
            return GetExerciseForExerciseType(currentExerciseType);
        }

        public int GetCurrentExerciseDuration()
        {
            Assert.IsTrue(IsCurrentExerciseByDuration());
            return trainingsConfiguration.trainings[selectedTraining].exercises[currentExercise].durationInSeconds;
        }

        public int GetTotalDuration()
        {
            return trainingsConfiguration.trainings[selectedTraining].exercises.Sum(exerciseItem => exerciseItem.durationInSeconds);
        }

        private bool IsCurrentExerciseByDuration()
        {
            return trainingsConfiguration.trainings[selectedTraining].exercises[currentExercise].durationInSeconds !=
                   null;
        }

        public TrainingDTO GetTrainingDTO()
        {
            var training = trainingsConfiguration.trainings[selectedTraining];
            var dto = new TrainingDTO(selectedTraining, training, currentExercise, exercisesConfiguration.exercises);
            return dto;
        }

        public InterpretedResultDTO GetInterpretedResultDTO(string userId)
        {
            var user = GetUser(userId);
            var previousReport = user.GetPreviousReport(selectedTraining);
            var lastReport = user.GetLastReport(selectedTraining);
            var result = new InterpretedResultDTO(trainingsConfiguration.trainings[selectedTraining].name, GetTotalDuration(), lastReport, previousReport);
            return result;
        }

        public bool GetIsInTraining()
        {
            return state == ApplicationState.IN_TRAINING;
        }

        public void EndTraining()
        {
            foreach (var user in users)
            {
                user.EndCurrentSession();
            }
            SetToPostTraining();
        }

        public ApplicationState GetState()
        {
            return state;
        }

        public void SetToStart()
        {
            state = ApplicationState.START;
        }

        public void SetToSelectedTraining()
        {
            state = ApplicationState.SELECTED_TRAINING;
        }

        public void SetToInTraining()
        {
            state = ApplicationState.IN_TRAINING;
        }

        public void SetToPostTraining()
        {
            state = ApplicationState.POST_TRAINING;
        }

        public void UnselectTraining()
        {
            selectedTraining = 0;
            state = ApplicationState.START;
        }

        public bool IsLastExercise()
        {
            return currentExercise >= trainingsConfiguration.trainings[selectedTraining].exercises.Count - 1;
        }

        public string RegisterNewUser(string userName)
        {
            var newUser = new User(userName); 
            users.Add(newUser);
            return newUser.GetId().ToString();
        }

        private void StoreUserIfNotGiven(User user)
        {
            if (!users.Exists(u => u.GetId() == user.GetId()))
            {
                users.Add(user);
            }
        }

        public User LogInUser(string json)
        {
            try
            {
                var userJson = JsonConvert.DeserializeObject<User>(json);
                StoreUserIfNotGiven(userJson);
                return userJson;
            }
            catch (Exception e)
            {
                throw new JsonSerializationException("Cannot deserialize given input. Is this a valid JSON? Exception: " + e);
            }
        }

        /// <summary>
        /// Adds an exercise report to an overall training report.
        /// Because there is no dedicated signifier to which user which skeleton belongs to, we use the skeleton ID for users.
        /// </summary>
        /// <param name="skeletonId">The skeleton Id</param>
        /// <param name="report">The obtained exercise report</param>
        private void AddToTrainingReport(int skeletonId, ExerciseReport report)
        {
            if (users.Count <= skeletonId)
                RegisterNewUser("unknown");

            try
            {
                users[skeletonId].AddToCurrentSession(report);
            }
            catch (NoCurrentSessionException exception)
            {
                Debug.LogWarning("Failed adding current session to report. Reason: " + exception.Message + " . Will try to create new session on the fly");
                users[skeletonId].StartNewSession(selectedTraining);
                AddToTrainingReport(skeletonId, report);
            }
        }

        public void AddToTrainingReports(List<ExerciseReport> reports)
        {
            foreach (var exerciseReport in reports)
            {
                AddToTrainingReport(exerciseReport.GetSkeletonId(), exerciseReport);
            }
        }

        public User GetUser(string userId)
        {
            return users.Find(u => u.GetId().ToString().Equals(userId));
        }
    }
}