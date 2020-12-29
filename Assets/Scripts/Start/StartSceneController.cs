using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Clients.WebController;
using Clients.WebController.WebServer;
using General;
using General.Session;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Clients.WebController.ControlCommands;

namespace Start
{
    public class StartSceneController : SceneController
    {
        public string trainingOverviewSceneName;
        public RawWebImageLoader qrCode;
        public Text instructionText;

        [SerializeField] private List<GameObject> players = new List<GameObject>();

        private void Start()
        {
            webController.onMessage += OnWebControllerMessage;
            var url = webController.GetUrl();
            qrCode.url = "https://api.qrserver.com/v1/create-qr-code/?format=png&size=500x500&margin=10&data=" + url;
            
            instructionText.text = "Scan the QR code or visit " + url + " to select a training";
        }

        private new string OnWebControllerMessage(string message)
        {
            SceneController.OnWebControllerMessage(message);

            var info = message.Split('\n').Length > 1 ? message.Split('\n')[1] : "";
            
            if (message.StartsWith(GET_TRAININGS.ToString()))
            {
                return GetTrainings();
            }

            if (message.StartsWith(SELECT_TRAINING.ToString()))
            {
                SelectTraining(int.Parse(info));
                return "done";
            }

            return "Wrong input!";
        }

        private string GetTrainings()
        {
            var result = new List<Dictionary<string, string>>();
            var trainings = sessionController.GetTrainings();
            for (var i = 0; i < trainings.Count; i++)
            {
                var trainingDict = new Dictionary<string, string>();
                var duration = trainings[i].exercises.Sum(exercise => exercise.durationInSeconds);
                trainingDict["id"] = i.ToString();
                trainingDict["name"] = trainings[i].name;
                trainingDict["duration"] = (duration / 60) + " minutes"; 
                result.Add(trainingDict);
            }

            return JsonConvert.SerializeObject(result);
        }

        private void SelectTraining(int id)
        {
            Dispatcher.Invoke(() =>
            {
                sessionController.SetSelectedTraining(id);
                StartCoroutine(TransitionToNewScene(trainingOverviewSceneName));
            });
        }
        
        private IEnumerator TransitionToNewScene(string nextSceneName)
        {
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
