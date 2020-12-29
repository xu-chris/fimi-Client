using General;
using UnityEngine;
using UnityEngine.UI;

namespace PreExercise
{
    public class PreExerciseSceneManager : SceneManager
    {
        public TextAsset preTrainingConfigurationFile;
        public Text headlineObject;
        public Text descriptionObject;
        public Text durationObject;
        public Text interactionPromptObject;

        private PreExerciseConfiguration configuration;

        public new void Awake()
        {
            base.Awake();
            configuration = new PreExerciseConfigurationService(preTrainingConfigurationFile).configuration;
            interactionPromptObject.text = configuration.interactionPrompt;
        }

        private void Start()
        {
            var currentExercise = sessionManager.GetCurrentExercise();
            headlineObject.text = currentExercise.name;
            descriptionObject.text = currentExercise.description;
            durationObject.text = sessionManager.GetCurrentExerciseDuration() + "s";
            ttsClient.Synthesize("The next exercise is: " + currentExercise.name);
            ttsClient.Synthesize("Here's what you should watch while performing the exercise: " + currentExercise.description);
        }
    }
}