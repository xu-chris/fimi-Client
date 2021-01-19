using Clients.WebController.WebServer;
using General;
using UnityEngine;
using UnityEngine.UI;

namespace PreExercise
{
    public class PreExerciseSceneManager : SceneManager
    {
        public Text headlineObject;
        public Text descriptionObject;
        public Text durationObject;
        public Text interactionPromptObject;
        public string afterTrainingSceneName = "PostTraining";

        public string interactionPrompt = "Start by standing in T pose";

        public new void Awake()
        {
            base.Awake();
            interactionPromptObject.text = interactionPrompt;
        }

        private void Start()
        {
            // Make sure the app is not hanging in the wrong screen
            sessionManager.SetToInTraining();
            
            var currentExercise = sessionManager.GetCurrentExercise();
            headlineObject.text = currentExercise.name;
            descriptionObject.text = currentExercise.description;
            durationObject.text = sessionManager.GetCurrentExerciseDuration() + "s";
            ttsClient.Synthesize("The next exercise is: " + currentExercise.name);
            ttsClient.Synthesize("Here's what you should watch while performing the exercise: " + currentExercise.description);
        }
        protected override void CancelTraining()
        {
            Dispatcher.Invoke(() =>
            {
                sessionManager.EndTraining();
                StartCoroutine(TransitionToNewScene(afterTrainingSceneName));
            });
        }
    }
}