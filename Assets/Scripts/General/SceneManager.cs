using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Clients.TTSClient;
using Clients.WebController;
using Clients.WebController.WebServer;
using Clients.WebController.WebServer.uHTTP;
using General.Session;
using InExercise;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;
using static Clients.WebController.ControlCommands;
using Random = System.Random;

namespace General
{
    public class SceneManager : MonoBehaviour
    {
        public GameObject webControllerPrefab;
        public GameObject sessionManagerPrefab;
        public GameObject textToSpeechClientPrefab;
        [FormerlySerializedAs("trainingOverviewSceneName")] 
        public string nextSceneName;
        
        public NotificationManager notificationManager;

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

        internal uHTTP.Response OnWebControllerMessage(string message)
        {
            Debug.Log("Received: " + message);

            var info = message.Split('\n').Length > 1 ? message.Split('\n')[1] : "";
            
            if (message.StartsWith(REGISTER_NEW_USER.ToString()))
            {
                return RegisterNewUser(info);
            }
            
            if (message.StartsWith(GET_TRAININGS.ToString()))
            {
                return GetTrainings();
            }

            if (message.StartsWith(SELECT_TRAINING.ToString()))
            {
                return SelectTraining(int.Parse(info));
            }
            
            if (message.StartsWith(GET_TRAINING.ToString()))
            {
                return GetTraining();
            }

            if (message.StartsWith(IS_TRAINING_ACTIVE.ToString()))
            {
                return IsTrainingActive();
            }
            
            if (message.StartsWith(CANCEL_TRAINING.ToString()))
            {
                return CancelTraining();
            }
            
            if (message.StartsWith(GET_APP_STATE.ToString()))
            {
                return GetAppState();
            }
            
            if (message.StartsWith(UNSELECT_TRAINING.ToString()))
            {
                return UnselectTraining();
            } 
            
            if (message.StartsWith(GET_RESULTS.ToString()))
            {
                return GetResults(info);
            } 
            
            if (message.StartsWith(GET_USER.ToString()))
            {
                return GetUserProfileAsSerializedString(info);
            } 
            
            if (message.StartsWith(LOGIN_USER.ToString()))
            {
                return LoginUser(info);
            } 

            return BuildResponse(false, "Wrong input! Received Input: " + message);
        }

        private uHTTP.Response IsTrainingActive()
        {
            return BuildResponse(true, sessionManager.GetIsInTraining().ToString());
        }

        private uHTTP.Response GetAppState()
        {
            return BuildResponse(true, sessionManager.GetState().ToString());
        }

        protected uHTTP.Response BuildResponse(bool success, string message)
        {
            if (success)
            {
                var response = new uHTTP.Response(uHTTP.StatusCode.OK)
                {
                    body = Encoding.UTF8.GetBytes(message)
                };

                return response;
            }
            else
            {
                var response = new uHTTP.Response(uHTTP.StatusCode.ERROR)
                {
                    body = Encoding.UTF8.GetBytes(message)
                };

                return response;
            }
        }

        private uHTTP.Response LoginUser(string jsonUserData)
        {
            try
            {
                var user = sessionManager.LogInUser(jsonUserData);
                var response = BuildResponse(true, user.GetId().ToString());
                ShowGreetingToUser(user.name);

                return response;
            }
            catch (Exception e)
            {
                return BuildResponse(false, e.Message);
            }
        }

        private uHTTP.Response GetUserProfileAsSerializedString(string userId)
        {
            try
            {
                return BuildResponse(true, JsonConvert.SerializeObject(sessionManager.GetUser(userId)));
            }
            catch (Exception e)
            {
                return BuildResponse(false, "Cannot serialize object for user ID " + userId + ". Reason: " + e);
            }
        }

        private uHTTP.Response GetResults(string userId)
        {
            try
            {
                return BuildResponse(true,
                    JsonConvert.SerializeObject((sessionManager.GetInterpretedResultDTO(userId))));
            }
            catch (Exception e)
            {
                return BuildResponse(false, e.Message);
            }
        }

        private uHTTP.Response RegisterNewUser(string userName)
        {
            var response = BuildResponse(true, sessionManager.RegisterNewUser(userName));
            ShowGreetingToUser(userName);
            return response;
        }

        private static string RandomGreeting()
        {
            var listOfGreetings = new List<string>()
            {
                "Hello", "Bonjour", "Hòla", "Hi", "Welcome"
            };
            var rnd = new Random();
            var r = rnd.Next(rnd.Next(listOfGreetings.Count));
            return listOfGreetings[r];
        }

        private void ShowGreetingToUser(string userNane)
        {
            Dispatcher.Invoke(() =>
            {
                notificationManager.SendNotification(RandomGreeting() + " " + userNane);
            });
        }

        private uHTTP.Response GetTrainings()
        {
            try
            {
                return BuildResponse(true, JsonConvert.SerializeObject(sessionManager.GetTrainingsDTO()));
            }
            catch (Exception e)
            {
                return BuildResponse(false, e.Message);
            }
        }

        private uHTTP.Response SelectTraining(int id)
        {
            Dispatcher.Invoke(() =>
            {
                sessionManager.SetSelectedTraining(id);
                StartCoroutine(TransitionToNewScene());
            });
            return BuildResponse(true, "");
        }

        private uHTTP.Response GetTraining()
        {
            try
            {
                return BuildResponse(true, JsonConvert.SerializeObject(sessionManager.GetTrainingDTO()));
            }
            catch (Exception e)
            {
                return BuildResponse(false, e.Message);
            }
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

        protected virtual uHTTP.Response CancelTraining()
        {
            Debug.LogWarning("Cancel training is called but no overriding method found.");
            return BuildResponse(false, "Cancel training is called but no overriding method found.");
        }

        protected virtual uHTTP.Response UnselectTraining()
        {
            Debug.LogWarning("Cannot unselect training, overriding method missing. Will return false.");
            return BuildResponse(false, "Cannot unselect training, overriding method missing. Will return false.");
        }
    }
}