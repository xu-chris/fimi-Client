using System;
using System.Collections;
using Clients.WebController.WebServer;
using Clients.WebController.WebServer.uHTTP;
using General;
using General.TPose;
using UnityEngine;
using UnityEngine.UI;

namespace TrainingOverview
{
    public class TrainingOverviewSceneManager : SceneManager
    {
        public TPoseController tPoseController;
        public Text descriptionLabel;
        public Text headlineLabel;
        public Text durationLabel;
        public int transitionTime;

        public string startSceneName = "Start";

        private bool isTransitioning;

        private void Start()
        {
            sessionManager.SetToSelectedTraining();
            tPoseController.TPoseDetected += OnTPoseDetectionStart;
            tPoseController.TPoseStopped += OnTPoseDetectionStop;
            tPoseController.SetDurationUntilNextSceneInSeconds(transitionTime);

            var selectedTraining = sessionManager.GetSelectedTraining();
            var totalDuration = sessionManager.GetTotalDuration();
            headlineLabel.text = selectedTraining.name;
            descriptionLabel.text = selectedTraining.description;
            durationLabel.text = GetTotalDurationString(totalDuration);
        }

        private string GetTotalDurationString(int totalDurationInSeconds)
        {
            var minutes = Decimal.Floor((decimal) (totalDurationInSeconds / 60f));
            var seconds = totalDurationInSeconds % 60;
            return (minutes != 0 ? minutes + " min" : "") + (seconds != 0 ? " " + seconds + " sec" : "");
        }
        
        private void OnTPoseDetectionStart(object source, EventArgs args)
        {
            isTransitioning = true;
            StartCoroutine(CheckCalibrationSuccess());
        }

        private void OnTPoseDetectionStop(object source, EventArgs args)
        {
            isTransitioning = false;
        }

        private IEnumerator CheckCalibrationSuccess()
        {
            if (isTransitioning) yield break;
            yield return new WaitForSeconds(transitionTime);
            if (isTransitioning) yield break;
            overlay.GetComponent<Animator>().SetBool("blendOut", true);
            sessionManager.SetToInTraining();
            StartCoroutine(TransitionToNewScene());
        }

        protected override uHTTP.Response UnselectTraining()
        {
            Dispatcher.Invoke(() =>
            {
                sessionManager.UnselectTraining();
                StartCoroutine(TransitionToNewScene(startSceneName));
            });
            return BuildResponse(true, "");
        }
    }
}