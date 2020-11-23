using _Project.Scripts.DomainObjects;
using _Project.Scripts.Periphery.Configurations;
using _Project.Scripts.Source.DomainObjects.Configurations;
using _Project.Scripts.Source.Periphery.Configurations;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Source.PreExercise
{
    public class PreTrainingSkeletonSceneController : MonoBehaviour
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
        }
    }
}