using Clients;
using General.Exercises;
using General.Trainings;
using Library;
using UnityEngine;
using UnityEngine.UI;

namespace PreExercise
{
    public class PreExerciseSceneController : MonoBehaviour
    {
        public TextAsset preTrainingConfigurationFile;
        public TextAsset trainingsConfigurationFile;
        public TextAsset exercisesConfigurationFile;
        public Text headlineObject;
        public Text descriptionObject;
        public Text durationObject;
        public Text interactionPromptObject;
        
        public int currentTrainingIndex = 0;
        public int currentExerciseIndex = 0;

        private PreExerciseConfiguration configuration;
        private TrainingsConfiguration trainingsConfiguration;
        private ExercisesConfiguration exercisesConfiguration;

        public void Start()
        {
            configuration = new PreExerciseConfigurationService(preTrainingConfigurationFile).configuration;
            trainingsConfiguration = new TrainingsConfigurationService(trainingsConfigurationFile).configuration;
            exercisesConfiguration = new ExercisesConfigurationService(exercisesConfigurationFile).configuration;

            interactionPromptObject.text = configuration.interactionPrompt;

            var currentExerciseType = trainingsConfiguration.trainings[currentTrainingIndex].exercises[currentExerciseIndex].type.ToExerciseType();
            var exercise =
                exercisesConfiguration.exercises.Find(exercise1 =>
                    exercise1.type.ToExerciseType() == currentExerciseType);
            headlineObject.text = exercise.name;
            descriptionObject.text = exercise.description;
            durationObject.text = trainingsConfiguration.trainings[currentTrainingIndex].exercises[currentExerciseIndex]
                .durationInSeconds + "s";
            GetComponent<TTSClient>().Synthesize("The next exercise is: " + exercise.name);
            GetComponent<TTSClient>().Synthesize("Here's what you should watch while performing the exercise: " + exercise.description);
        }
    }
}