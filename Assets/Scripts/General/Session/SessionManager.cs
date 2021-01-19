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
        public bool isInTraining = false;

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

        public string GetSerializedTrainings()
        {
            var dto = new TrainingsDTO(trainingsConfiguration.trainings, exercisesConfiguration.exercises,
                currentExercise);
            var result = JsonConvert.SerializeObject(dto);
            return result;
        }
        
        public Exercise GetExerciseForExerciseType(ExerciseType exerciseType)
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

        public string GetSerializedTraining()
        {
            var training = trainingsConfiguration.trainings[selectedTraining];
            var dto = new TrainingDTO(selectedTraining, training, currentExercise, exercisesConfiguration.exercises);
            return JsonConvert.SerializeObject(dto);
        }

        public string GetSerializedResult(int userId)
        {
            var dummyRules = trainingsConfiguration.trainings[selectedTraining].exercises[selectedTraining];
            var dummyReport = new TrainingReport(selectedTraining);
            var result = new ResultDTO(trainingsConfiguration.trainings[selectedTraining].name, GetTotalDuration(), dummyReport, dummyReport);
            // TODO: Use real reports
            return JsonConvert.SerializeObject(result);
        }

        public void SaveReport(TrainingReport trainingReport, int userId)
        {
            // TODO: Save the incoming report on the machine;
        }

        public bool GetIsInTraining()
        {
            return state == ApplicationState.IN_TRAINING;
        }

        public void CollectResult()
        {
            return;
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

        public int RegisterNewUser()
        {
            var userId = users.Count;
            var newUser = new User(userId); 
            users.Add(newUser);
            return userId;
        }

        private void AddToTrainingReport(int userId, ExerciseReport report)
        {
            if (users.Count <= userId)
                RegisterNewUser();

            try
            {
                users[userId].AddToCurrentSession(report);
            }
            catch (NoCurrentSessionException exception)
            {
                Debug.LogWarning("Failed adding current session to report. Reason: " + exception.Message + " . Will try to create new session on the fly");
                users[userId].StartNewSession(selectedTraining);
                AddToTrainingReport(userId, report);
            }
        }

        public void AddToTrainingReports(List<ExerciseReport> reports)
        {
            foreach (var exerciseReport in reports)
            {
                AddToTrainingReport(exerciseReport.GetSkeletonId(), exerciseReport);
            }
        }
    }
}