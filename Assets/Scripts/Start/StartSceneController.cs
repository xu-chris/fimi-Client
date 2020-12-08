using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using General;
using General.Exercises;
using General.Trainings;
using General.WebController;
using General.WebController.Scripts;
using JWT.Serializers;
using Newtonsoft.Json;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using static General.WebController.ControlCommands;

namespace Start
{
    public class StartSceneController : MonoBehaviour
    {
        public string trainingOverviewSceneName;
        
        public TextAsset trainingsConfigurationFile;

        private TrainingsConfiguration trainingsConfiguration;

        private readonly List<GameObject> players = new List<GameObject>();
        private void Awake()
        {
            trainingsConfiguration = new TrainingsConfigurationService(trainingsConfigurationFile).configuration;
            var webController = GameObject.Find("WebController").GetComponent<WebController>();
            webController.onMessage += OnWebControllerMessage;
        }

        private string OnWebControllerMessage(string message)
        {
            Debug.Log("Received: " + message);

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
            
            for (var i = 0; i < trainingsConfiguration.trainings.Count; i++)
            {
                var trainingDict = new Dictionary<string, string>();
                var duration = trainingsConfiguration.trainings[i].exercises.Sum(exercise => exercise.durationInSeconds);
                trainingDict["id"] = i.ToString();
                trainingDict["name"] = trainingsConfiguration.trainings[i].name;
                trainingDict["duration"] = (duration / 60) + " minutes"; 
                result.Add(trainingDict);
            }

            return JsonConvert.SerializeObject(result);
        }

        private void SelectTraining(int id)
        {
            Dispatcher.Invoke(() =>
            {
                var trainingRun = GameObject.Find("TrainingRun").GetComponent<TrainingRun>();
                trainingRun.SetSelectedTraining(trainingsConfiguration.trainings[id]);
                StartCoroutine(TransitionToNewScene(trainingOverviewSceneName));
            });
        }
        
        private IEnumerator TransitionToNewScene(string nextSceneName)
        {
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene(nextSceneName);
        }

        private void AddPlayer(string name)
        {
            var player = new GameObject();
            players.Add(player);
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
