using System;
using System.Collections;
using Clients.WebController.WebServer;
using General;
using General.TPose;
using UnityEngine;

namespace TrainingOverview
{
    public class TrainingOverviewSceneManager : SceneManager
    {
        public TPoseController tPoseController;
        public int transitionTime;
        public GameObject overlay;

        public string startSceneName = "Start";

        private bool isTransitioning;

        private void Start()
        {
            sessionManager.SetToSelectedTraining();
            tPoseController.TPoseDetected += OnTPoseDetectionStart;
            tPoseController.TPoseStopped += OnTPoseDetectionStop;
            tPoseController.SetDurationUntilNextSceneInSeconds(transitionTime);

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

        protected override bool UnselectTraining()
        {
            Dispatcher.Invoke(() =>
            {
                sessionManager.UnselectTraining();
                StartCoroutine(TransitionToNewScene(startSceneName));
            });
            return true;
        }
    }
}