using Clients;
using General.Skeleton;
using UnityEngine;

namespace General
{
    public abstract class SkeletonSceneController : MonoBehaviour
    {
        public TextAsset applicationConfigurationFile;
        private ApplicationConfiguration applicationConfiguration;
        protected WebSocketClient webSocketClient;
        public GameObject skeletonSpawnPoint;
        
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
                var skeleton = CreateSkeleton();
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
                    CreateSkeleton();

                // Set and activate only skeletons that are detected.
                if (p >= 0 && detectedPersons.Length > p && p == detectedPersons[p].id)
                    UpdateSkeleton(p, detectedPersons[p]);
                else
                    transform.GetChild(p).gameObject.SetActive(false);
            }
        }

        private GameObject CreateSkeleton()
        {
            var skeleton = Instantiate(skeletonPrefab, gameObject.transform, true);
            skeleton = AddAdditionalSpecimenForSkeleton(skeleton);
            return skeleton;
        }

        protected abstract GameObject AddAdditionalSpecimenForSkeleton(GameObject skeleton);

        private void UpdateSkeleton(int index, Person person)
        {
            var skeletonGameObject = transform.GetChild(index).gameObject; 
            var script = skeletonGameObject.GetComponent<Skeleton.Skeleton>();
            var basePoint = skeletonSpawnPoint.transform.position;
            script.UpdateSkeleton(person, basePoint);
        }
    }
}