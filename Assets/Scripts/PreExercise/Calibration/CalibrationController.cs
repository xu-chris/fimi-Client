using System;
using System.Collections;
using Clients.WebSocketClient;
using General;
using General.TPose;
using UnityEngine;

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
        
        private IEnumerator checkCoroutine;

        public delegate void CalibrationEventHandler();

        public event CalibrationEventHandler CalibrationCompleted;

        public void Start()
        {
            calibrationConfiguration = new CalibrationConfigurationService(calibrationConfigurationFile).configuration;

            var tPoseController = GetComponent<TPoseController>();
            tPoseController.TPoseDetected += OnTPoseDetectionStart;
            tPoseController.TPoseStopped += OnTPoseDetectionStop;
            tPoseController.SetDurationUntilNextSceneInSeconds(calibrationConfiguration.durationOfCalibrationInSeconds);
        }

        private void OnTPoseDetectionStart(object source, EventArgs args)
        {
            checkCoroutine = CheckCalibrationSuccess();
            StartCoroutine(checkCoroutine);
            StartCalibration();
        }

        private void OnTPoseDetectionStop(object source, EventArgs args)
        {
            StopCoroutine(checkCoroutine);
            ResetCalibration();
        }

        private IEnumerator CheckCalibrationSuccess()
        {
            if (calibrationAborted) yield break;
            yield return new WaitForSeconds(calibrationConfiguration.durationOfCalibrationInSeconds);
            if (calibrationAborted) yield break;
            OnCalibrationComplete();
        }

        private void OnCalibrationComplete()
        {
            CalibrationCompleted?.Invoke();
        }

        private void StartCalibration()
        {
            calibrationAborted = false;
            GameObject.FindWithTag(Tag.WEB_SOCKET_CLIENT.ToString()).GetComponent<WebSocketClient>().SendRescaleSkeletons();
        }

        private void ResetCalibration()
        {
            if (!calibrated)
                calibrationAborted = true;
        }
    }
}