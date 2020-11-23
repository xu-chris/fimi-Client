using UnityEngine;

namespace _Project.Scripts.Source.PreExercise
{
    public class CameraRotator : MonoBehaviour
    {
        public float speed;
        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }
    }
}
