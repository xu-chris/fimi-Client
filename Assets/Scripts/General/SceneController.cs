using Clients.TTSClient;
using Clients.WebController;
using General.Session;
using UnityEngine;

namespace General
{
    public class SceneController : MonoBehaviour
    {
        public GameObject webControllerPrefab;
        public GameObject sessionControllerPrefab;
        public GameObject textToSpeechClientPrefab;

        internal WebController webController;
        internal SessionController sessionController;
        internal TTSClient ttsClient;

        private GameObject webControllerGameObject;
        private GameObject sessionControllerGameObject;
        private GameObject textToSpeechGameObject;
        
        protected void Awake()
        {
            // It could be that the web controller is missing. If so, recreate it
            Debug.LogWarning("Re-instantiated the web controller since it was missing.");
            webControllerGameObject = Utils.GetOrInstantiate(Tag.WEB_CONTROLLER, webControllerPrefab);

            // Same with session controller
            sessionControllerGameObject = Utils.GetOrInstantiate(Tag.SESSION_CONTROLLER, sessionControllerPrefab);

            // And for TTS client
            textToSpeechGameObject = Utils.GetOrInstantiate(Tag.TTS_CLIENT, textToSpeechClientPrefab);
        }

        protected void OnEnable()
        {
            // Set up web controller
            webController = webControllerGameObject.GetComponent<WebController>();
            webController.onMessage += OnWebControllerMessage;
            
            sessionController = sessionControllerGameObject.GetComponent<SessionController>();
            
            ttsClient = textToSpeechGameObject.GetComponent<TTSClient>();
        }

        internal static string OnWebControllerMessage(string message)
        {
            Debug.Log("Received: " + message);

            return "";
        }
    }
}