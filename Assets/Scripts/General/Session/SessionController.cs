using System.Collections.Generic;
using General.Exercises;
using General.Trainings;
using Library;
using UnityEngine;
using UnityEngine.Assertions;

namespace General.Session
{
    public class SessionController : MonoBehaviour
    {
        
        public TextAsset trainingsConfigurationFile;
        public TextAsset exercisesConfigurationFile;
        public int maxNumberOfPeople = 1;

        private TrainingsConfiguration trainingsConfiguration;
        private ExercisesConfiguration exercisesConfiguration;

        private int selectedTraining;
        private int currentExercise;
        
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
            selectedTraining = id;
        }

        public void SetCurrentExercise(int id)
        {
            Debug.Log("Exercise set to current: " + trainingsConfiguration.trainings[selectedTraining].exercises[id].type + " (ID: " + id + ")");
            currentExercise = id;
        }

        /**
         * {
              name: 'Move your body',
              durationInSeconds: 60,
              exercises: [
                {
                  id: 0,
                  name: 'Squat arm raise',
                  durationInSeconds: 60,
                  type: 'exercise',
                  state: 'done',
                  description: 'Raise your arms until they are lifted horizontally. Squat in the same time.'
                },
                {
                  id: 1,
                  name: 'Pause',
                  type: 'pause',
                  state: 'current',
                  durationInSeconds: 10
                },
                {
                  id: 2,
                  name: 'Squat arm raise',
                  type: 'exercise',
                  state: 'upcoming',
                  durationInSeconds: 130,
                  description: 'Raise your arms until they are lifted horizontally. Squat in the same time.'
                }
              ]
            }
         */
        // public string GetSerializedTrainingInformation()
        // {
        //     var exercises = new List<Dictionary<string, string>>();
        //     for (var i = 0; i < selectedTraining.exercises.Count; i++)
        //     {
        //         var exerciseItem = selectedTraining.exercises[i];
        //         var exercise = GetExerciseForExerciseType(exerciseItem.type.ToExerciseType());
        //         var exerciseDict = new Dictionary<string, string>();
        //         var durationInSeconds = exerciseItem.durationInSeconds;
        //
        //         exerciseDict["id"] = i.ToString();
        //         exerciseDict["name"] = exercise.name;
        //         exerciseDict["duration"] = exerciseItem.durationInSeconds.ToString();
        //         exerciseDict["description"] = exercise.description;
        //         exerciseDict["type"] = "exercise";
        //         exercises.Add(exerciseDict);
        //     }
        //         
        //     var training = new Dictionary<string, Ant>();
        //     training["name"] = selectedTraining.name;
        //     training["exercises"] = exercises;
        //
        //     for (var i = 0; i < trainingsConfiguration.trainings.Count; i++)
        //     {
        //         var trainingDict = new Dictionary<string, string>();
        //         var duration = trainingsConfiguration.trainings[i].exercises.Sum(exercise => exercise.durationInSeconds);
        //         trainingDict["id"] = i.ToString();
        //         trainingDict["name"] = trainingsConfiguration.trainings[i].name;
        //         trainingDict["duration"] = (duration / 60) + " minutes"; 
        //         result.Add(trainingDict);
        //     }
        //
        //     return JsonConvert.SerializeObject(result);
        // }

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

        private bool IsCurrentExerciseByDuration()
        {
            return trainingsConfiguration.trainings[selectedTraining].exercises[currentExercise].durationInSeconds !=
                   null;
        }
    }
}