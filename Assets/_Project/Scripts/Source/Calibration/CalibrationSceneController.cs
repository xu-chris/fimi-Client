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

        private Collider dummyCollider;
        
        private int maxNumberOfPeople;
        private GameObject[] skeletons;

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
                if (skeletons[p] == null)
                {
                    skeletons[p] = Instantiate(skeletonPrefab);
                    Debug.LogError("Initialized a new skeleton which should be already there 🤔. p: " + p);
                }

                // Set and activate only skeletons that are detected.
                if (p >= 0 && detectedPersons.Length > p && p == detectedPersons[p].id)
                    UpdateSkeleton(skeletons[p], detectedPersons[p]);
                else
                    skeletons[p].SetActive(false);
            }
        }

        public void InitializeAllSkeletons()
        {
            skeletons = new GameObject[maxNumberOfPeople];
            for (var p = 0; p < maxNumberOfPeople; p++)
            {
                skeletons[p] = Instantiate(skeletonPrefab);
                skeletons[p].SetActive(false);
            }
        }

        private void UpdateSkeleton(GameObject skeleton, Person person)
        {
            // skeleton.SetSkeleton(person.joints, person.lowestY);
            skeleton.SetActive(true);
        }
    }
}