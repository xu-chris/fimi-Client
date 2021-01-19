using System;
using Clients.WebController.WebServer;
using General;
using UnityEngine.UI;

namespace PostTraining
{
    public class PostTrainingSceneManager : SceneManager
    {

        public Text descriptionField;
        public string proceedOnSmartphone = "You did it! Check on your smartphone what results you have achieved";
        
        private void Start()
        {
            sessionManager.SetToPostTraining();
        }
        
        protected override bool UnselectTraining()
        {
            Dispatcher.Invoke(() =>
            {
                sessionManager.UnselectTraining();
                StartCoroutine(TransitionToNewScene());
            });
            return true;
        }
    }
}