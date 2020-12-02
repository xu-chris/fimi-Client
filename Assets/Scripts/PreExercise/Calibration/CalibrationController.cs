using System;
using System.Collections;
using Clients;
using General.TPose;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PreExercise.Calibration
{
    public class CalibrationController : MonoBehaviour
    {
        public TextAsset calibrationConfigurationFile;
        public string nextSceneName;
        public GameObject overlay;

        private CalibrationConfiguration calibrationConfiguration;

        private bool calibrated = false;
        private bool calibrationAborted = false;

        public void Start()
        {
            calibrationConfiguration = new CalibrationConfigurationService(calibrationConfigurationFile).configuration;

            var tPoseController = GetComponent<TPoseController>();
            tPoseController.TPoseDetected += OnTPoseDetectionStart;
            tPoseController.TPoseStopped += OnTPoseDetectionStop;
            tPoseController.SetDurationUntilNextSceneInSeconds(calibrationConfiguration.durationOfCalibrationInSeconds);
        }

        public void OnTPoseDetectionStart(object source, EventArgs args)
        {
            StartCoroutine(CheckCalibrationSuccess());
            StartCalibration();
        }

        public void OnTPoseDetectionStop(object source, EventArgs args)
        {
            ResetCalibration();
        }
        
        private IEnumerator CheckCalibrationSuccess()
        {
            yield return new WaitForSeconds(calibrationConfiguration.durationOfCalibrationInSeconds);
            if (calibrationAborted) yield break;
            
            calibrated = true;
            OnCalibrationComplete();
        }

        private void OnCalibrationComplete()
        {
            overlay.GetComponent<Animator>().SetBool("blendOut", true);
            StartCoroutine(TransitionToNewScene());
        }

        private IEnumerator TransitionToNewScene()
        {
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene(nextSceneName);
        }

        private void StartCalibration()
        {
            calibrationAborted = false;
            GetComponent<WebSocketClient>().SendRescaleSkeletons();
        }

        private void ResetCalibration()
        {
            if (!calibrated)
                calibrationAborted = true;
        }
    }
}