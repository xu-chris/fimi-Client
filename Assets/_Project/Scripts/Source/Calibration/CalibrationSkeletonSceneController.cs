using _Project.Scripts.DomainObjects;
using _Project.Scripts.DomainObjects.Configurations;
using _Project.Scripts.Periphery.Configurations;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Source.Calibration
{
    public class CalibrationSkeletonSceneController : SkeletonSceneController
    {
        public TextAsset calibrationConfigurationFile;
        public Text title;
        public GameObject particleSystemForceField;

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
            particleSystemForceField.SetActive(true);   
            webSocketClient.SendRescaleSkeletons();
        }

        private void ResetCalibration()
        {
            particleSystemForceField.SetActive(false);
        }
    }
}