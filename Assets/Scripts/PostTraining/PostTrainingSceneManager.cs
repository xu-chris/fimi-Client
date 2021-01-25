using Clients.WebController;
using General;
using UnityEngine.UI;
using uHTTP = Clients.WebController.uHTTP.uHTTP;

namespace PostTraining
{
    public class PostTrainingSceneManager : SceneManager
    {
        private void Start()
        {
            sessionManager.SetToPostTraining();
        }
        
        protected override uHTTP.Response UnselectTraining()
        {
            Dispatcher.Invoke(() =>
            {
                sessionManager.UnselectTraining();
                StartCoroutine(TransitionToNewScene());
            });
            return BuildResponse(true, "");
        }
    }
}