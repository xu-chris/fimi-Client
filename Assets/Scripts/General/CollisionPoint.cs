using UnityEngine;

namespace General
{
    public class CollisionPoint : MonoBehaviour
    {
        public Material collisionMaterial;
        public Material normalMaterial;
        private Renderer gameObjectRenderer;

        private void Start()
        {
            gameObjectRenderer = gameObject.GetComponent<Renderer>();
            gameObject.GetComponent<Renderer>().material = normalMaterial;
        }

        private void OnTriggerEnter(Collider other)
        {
            gameObject.GetComponent<Renderer>().material = collisionMaterial;
            Debug.Log("Collision with game object " + other.transform.parent.name);
        }

        private void OnTriggerExit(Collider other)
        {
            gameObject.GetComponent<Renderer>().material = normalMaterial;
        }
    }
}
