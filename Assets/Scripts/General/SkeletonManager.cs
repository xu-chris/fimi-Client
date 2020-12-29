using System.Collections.Generic;
using Clients.WebSocketClient;
using General.Skeleton;
using UnityEngine;

namespace General
{
    public abstract class SkeletonManager : MonoBehaviour
    {
        public GameObject webSocketClientPrefab;
        protected WebSocketClient webSocketClient;
        public GameObject skeletonSpawnPoint;
        
        public GameObject skeletonPrefab;
        private int maxNumberOfPeople = 1;
        private readonly List<GameObject> skeletons = new List<GameObject>();

        private GameObject webSocketClientGameObject;

        private void Awake()
        {
            webSocketClientGameObject = Utils.GetOrInstantiate(Tag.WEB_SOCKET_CLIENT, webSocketClientPrefab);
        }

        private void OnEnable()
        {
            webSocketClient = webSocketClientGameObject.GetComponent<WebSocketClient>();
        }

        public void Start()
        {
            Application.runInBackground = true;
            InitializeAllSkeletons();
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