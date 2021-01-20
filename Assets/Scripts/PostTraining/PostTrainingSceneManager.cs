using Clients.WebController.WebServer;
using Clients.WebController.WebServer.uHTTP;
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