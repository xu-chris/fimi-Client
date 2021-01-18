using System.Collections;
using Clients.TTSClient;
using Clients.WebController;
using Clients.WebController.WebServer;
using General.Session;
using UnityEngine;
using UnityEngine.Serialization;
using static Clients.WebController.ControlCommands;

namespace General
{
    public class SceneManager : MonoBehaviour
    {
        public GameObject webControllerPrefab;
        public GameObject sessionManagerPrefab;
        public GameObject textToSpeechClientPrefab;
        [FormerlySerializedAs("trainingOverviewSceneName")] 
        public string nextSceneName;

        internal WebController webController;
        internal SessionManager sessionManager;
        internal TTSClient ttsClient;

        private GameObject webControllerGameObject;
        private GameObject sessionManagerGameObject;
        private GameObject textToSpeechGameObject;

        public GameObject overlay;

        protected void Awake()
        {
            // It could be that the web controller is missing. If so, recreate it
            Debug.LogWarning("Re-instantiated the web controller since it was missing.");
            webControllerGameObject = Utils.GetOrInstantiate(Tag.WEB_CONTROLLER, webControllerPrefab);

            // Same with session controller
            sessionManagerGameObject = Utils.GetOrInstantiate(Tag.SESSION_MANAGER, sessionManagerPrefab);

            // And for TTS client
            textToSpeechGameObject = Utils.GetOrInstantiate(Tag.TTS_CLIENT, textToSpeechClientPrefab);
        }

        protected void OnEnable()
        {
            // Set up web controller
            webController = webControllerGameObject.GetComponent<WebController>();
            webController.onMessage += OnWebControllerMessage;
            
            sessionManager = sessionManagerGameObject.GetComponent<SessionManager>();
            
            ttsClient = textToSpeechGameObject.GetComponent<TTSClient>();
        }

        internal string OnWebControllerMessage(string message)
        {
            Debug.Log("Received: " + message);

            var info = message.Split('\n').Length > 1 ? message.Split('\n')[1] : "";
            
            if (message.StartsWith(REGISTER_NEW_USER.ToString()))
            {
                return RegisterNewUser();
            }
            
            if (message.StartsWith(GET_TRAININGS.ToString()))
            {
                return GetTrainings();
            }

            if (message.StartsWith(SELECT_TRAINING.ToString()))
            {
                SelectTraining(int.Parse(info));
                return true.ToString();
            }
            
            if (message.StartsWith(GET_TRAINING.ToString()))
            {
                return GetTraining();
            }

            if (message.StartsWith(IS_TRAINING_ACTIVE.ToString()))
            {
                return sessionManager.GetIsInTraining().ToString();
            }
            
            if (message.StartsWith(CANCEL_TRAINING.ToString()))
            {
                CancelTraining();
                return true.ToString();
            }
            
            if (message.StartsWith(GET_APP_STATE.ToString()))
            {
                return sessionManager.GetState().ToString();
            }
            
            if (message.StartsWith(UNSELECT_TRAINING.ToString()))
            {
                return UnselectTraining().ToString();
            } 
            
            if (message.StartsWith(GET_RESULTS.ToString()))
            {
                return GetResults(int.Parse(info));
            } 

            return "Wrong input! Received Input: " + message;
        }

        private string GetResults(int userId)
        {
            return sessionManager.GetSerializedResult(userId);
        }

        private string RegisterNewUser()
        {
            return sessionManager.RegisterNewUser().ToString();
        }

        private string GetTrainings()
        {
            return sessionManager.GetSerializedTrainings();
        }

        private void SelectTraining(int id)
        {
            Dispatcher.Invoke(() =>
            {
                sessionManager.SetSelectedTraining(id);
                StartCoroutine(TransitionToNewScene());
            });
        }

        private string GetTraining()
        {
            return sessionManager.GetSerializedTraining();
        }
        
        public IEnumerator TransitionToNewScene()
        {
            overlay.GetComponent<Animator>().SetBool("blendOut", true);
            yield return new WaitForSeconds(1.0f);
            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneName);
        }

        protected IEnumerator TransitionToNewScene(string sceneName)
        {
            overlay.GetComponent<Animator>().SetBool("blendOut", true);
            yield return new WaitForSeconds(1.0f);
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }

        protected virtual void CancelTraining()
        {
            Debug.LogWarning("Cancel training is called but no overriding method found.");
        }

        protected virtual bool UnselectTraining()
        {
            Debug.LogWarning("Cannot unselect training, overriding method missing. Will return false.");
            return false;
        }
    }
}