using System.Collections.Generic;
using Clients;
using General.Authentication;
using General.Skeleton;
using NUnit.Framework;
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
        private int maxNumberOfPeople = 1;
        private readonly List<GameObject> skeletons = new List<GameObject>();
        
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
                if (skeletons[p] == null)
                    CreateSkeleton();

                // Set and activate only skeletons that are detected.
                if (p >= 0 && detectedPersons.Length > p && p == detectedPersons[p].id)
                    UpdateSkeleton(detectedPersons[p]);
                else
                    skeletons[p].SetActive(false);
            }
        }

        private GameObject CreateSkeleton()
        {
            var skeleton = Instantiate(skeletonPrefab, skeletonSpawnPoint.transform, true);
            skeletons.Add(skeleton);
            skeleton = AddAdditionalSpecimenForSkeleton(skeleton);
            return skeleton;
        }

        protected abstract GameObject AddAdditionalSpecimenForSkeleton(GameObject skeleton);

        private void UpdateSkeleton(Person person)
        {
            var skeletonGameObject = skeletons[person.id]; 
            var script = skeletonGameObject.GetComponent<Skeleton.Skeleton>();
            var basePoint = skeletonSpawnPoint.transform.position;
            script.UpdateSkeleton(person);
        }
    }
}