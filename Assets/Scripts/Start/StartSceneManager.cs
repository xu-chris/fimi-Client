using Clients.WebController;
using InExercise;
using UnityEngine;
using UnityEngine.UI;
using SceneManager = General.SceneManager;

namespace Start
{
    public class StartSceneManager : SceneManager
    {
        public RawWebImageLoader qrCode;
        public Text instructionText;

        private void Start()
        {
            sessionManager.SetToStart();
            webController.onMessage += OnWebControllerMessage;
            SetUpConnectionInfo();
        }

        private void SetUpConnectionInfo()
        {
            var url = webController.GetUrl();
            qrCode.url = "https://api.qrserver.com/v1/create-qr-code/?format=png&size=500x500&margin=10&data=" + url;
            
            instructionText.text = "Scan the QR code or visit " + url + " to select a training";
        }
    }
}
