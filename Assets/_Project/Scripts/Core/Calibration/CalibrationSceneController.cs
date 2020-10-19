using System;
using _Project.Scripts.DomainObjects.Configurations;
using _Project.Scripts.Periphery.Configurations;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Core.Calibration
{
    public class CalibrationSceneController : SceneController
    {
        public TextAsset calibrationConfigurationFile;
        public Text title;
        public GameObject particleSystemForceField;

        private CalibrationConfiguration calibrationConfiguration;
        private CalibrationSkeletonOrchestrator skeletonOrchestrator;

        private Collider dummyCollider;

        public void Start()
        {
            SetUpWebSocket();
            calibrationConfiguration = new CalibrationConfigurationService(calibrationConfigurationFile).configuration;

            skeletonOrchestrator = new CalibrationSkeletonOrchestrator(applicationConfiguration.maxNumberOfPeople);
        }

        public void Update()
        {
            var detectedPersons = webSocketClient.detectedPersons;
            skeletonOrchestrator?.Update(detectedPersons);
        }

        public void OnTriggerEnter(Collider other1)
        {
            Debug.Log("Collision started. Will start calibration");
            StartCalibration();
        }

        public void OnTriggerExit(Collider other1)
        {
            Debug.Log("Collision started. Resetting scene...");
            ResetCalibration();
        }

        private void StartCalibration()
        {
            particleSystemForceField.SetActive(true);   
            webSocketClient.SendRescaleSkeletons();
        }

        private void ResetCalibration()
        {
            particleSystemForceField.SetActive(false);
        }
    }
}