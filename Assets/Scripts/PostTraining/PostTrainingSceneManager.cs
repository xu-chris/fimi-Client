using System;
using Clients.WebController.WebServer;
using General;

namespace PostTraining
{
    public class PostTrainingSceneManager : SceneManager
    {
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