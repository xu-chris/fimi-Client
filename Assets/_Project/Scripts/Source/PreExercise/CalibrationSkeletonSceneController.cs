using _Project.Scripts.DomainObjects.Configurations;
using _Project.Scripts.Periphery.Configurations;
using UnityEngine;

namespace _Project.Scripts.Source.PreExercise
{
    public class CalibrationSkeletonSceneController : SkeletonSceneController
    {
        public TextAsset calibrationConfigurationFile;
        public GameObject progressBarObject;
        public int maxWidth;

        private CalibrationConfiguration calibrationConfiguration;

        public new void Start()
        {
            base.Start();
            calibrationConfiguration = new CalibrationConfigurationService(calibrationConfigurationFile).configuration;
            maxNumberOfPeople = 1;
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
            webSocketClient.SendRescaleSkeletons();
        }

        private void ResetCalibration()
        {
            
        }
    }
}