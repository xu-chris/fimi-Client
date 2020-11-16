using _Project.Scripts.DomainObjects;
using _Project.Scripts.DomainObjects.Configurations;
using _Project.Scripts.Periphery.Clients;
using _Project.Scripts.Periphery.Configurations;
using _Project.Scripts.Source.Calibration;
using UnityEngine;

namespace _Project.Scripts.Source
{
    public class SkeletonSceneController : MonoBehaviour
    {
        public TextAsset applicationConfigurationFile;
        protected ApplicationConfiguration applicationConfiguration;
        protected WebSocketClient webSocketClient;
        
        public GameObject skeletonPrefab;
        private protected int maxNumberOfPeople = 1;
        
        public void Start()
        {
            SetUpWebSocket();
            InitializeAllSkeletons();
        }

        public void SetUpWebSocket()
        {
            Application.runInBackground = true;

            applicationConfiguration = new ApplicationConfigurationService(applicationConfigurationFile).configuration;
            webSocketClient = gameObject.AddComponent<WebSocketClient>();
            webSocketClient.webSocketConfiguration = applicationConfiguration.webSocket;
        }
        
        
        
        public void InitializeAllSkeletons()
        {
            for (var p = 0; p < maxNumberOfPeople; p++)
            {
                var skeleton = Instantiate(skeletonPrefab, gameObject.transform, true);
                skeleton.SetActive(false);
            }
        }
        
        public void Update()
        {
            var detectedPersons = webSocketClient.detectedPersons;
            Update(detectedPersons);
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
        
        private void UpdateSkeleton(int index, Person person)
        {
            var skeletonGameObject = transform.GetChild(index).gameObject; 
            var script = skeletonGameObject.GetComponent<CalibrationSkeleton>();

            script.person = person;
            skeletonGameObject.SetActive(true);
        }
    }
}