using UnityEngine;
using UnityEngine.XR.WSA.Input;

namespace _Project.Scripts.Core.Calibration
{
    public class CalibrationSkeleton: Skeleton
    {
        private readonly MeshCollider collider;

        public CalibrationSkeleton(int id, bool withGameObjects = true) : base(id, withGameObjects)
        {
            collider = this.gameObject.AddComponent<MeshCollider>();
            Mesh mesh = new Mesh();
            gameObject.AddComponent<MeshFilter>();
            gameObject.GetComponent<MeshFilter>().mesh = mesh;
            collider.sharedMesh = this.gameObject.GetComponent<MeshFilter>().mesh;
        }
    }
}