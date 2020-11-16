using _Project.Scripts.DomainObjects;
using _Project.Scripts.DomainObjects.Configurations;
using _Project.Scripts.Periphery.Configurations;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Source.Calibration
{
    public class CalibrationSceneController : SceneController
    {
        public TextAsset calibrationConfigurationFile;
        public Text title;
        public GameObject particleSystemForceField;
        public GameObject skeletonPrefab;

        private CalibrationConfiguration calibrationConfiguration;
        private int maxNumberOfPeople;

        public void Start()
        {
            SetUpWebSocket();
            calibrationConfiguration = new CalibrationConfigurationService(calibrationConfigurationFile).configuration;
            maxNumberOfPeople = 1;
            InitializeAllSkeletons();
        }

        public void Update()
        {
            var detectedPersons = webSocketClient.detectedPersons;
            Update(detectedPersons);
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

        private void Update(Person[] detectedPersons)
        {
            if (detectedPersons == null)
                return;

            for (var p = 0; p < maxNumberOfPeople; p++)
            {
                // Init skeleton if not given.
                if (transform.GetChild(p) == null)
                {
                    Instantiate(skeletonPrefab, gameObject.transform, true);
                    Debug.LogError("Initialized a new skeleton which should be already there 🤔. p: " + p);
                }

                // Set and activate only skeletons that are detected.
                if (p >= 0 && detectedPersons.Length > p && p == detectedPersons[p].id)
                    UpdateSkeleton(p, detectedPersons[p]);
                else
                    transform.GetChild(p).gameObject.SetActive(false);
            }
        }

        public void InitializeAllSkeletons()
        {
            for (var p = 0; p < maxNumberOfPeople; p++)
            {
                var skeleton = Instantiate(skeletonPrefab, gameObject.transform, true);
                skeleton.SetActive(false);
            }
        }

        private void UpdateSkeleton(int index, Person person)
        {
            var skeletonGameObject = transform.GetChild(index).gameObject; 
            var script = skeletonGameObject.GetComponent<CalibrationSkeleton>();

            script.person = person;
            //
            // skeletonGameObject.SetActive(true);
            // script.SetSkeleton(person.joints, person.lowestY);
        }
    }
}