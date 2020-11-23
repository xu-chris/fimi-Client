using System;
using System.Collections.Generic;
using _Project.Scripts.DomainObjects;
using _Project.Scripts.Source.DomainValues;
using UnityEngine;

namespace _Project.Scripts.Source.Calibration
{
    public class CalibrationSkeleton : Skeleton
    {
        private MeshCollider collider;
        private EventHandler collisionEventHandler;

        public List<Collider> colliders;

        public new void Start()
        {
            base.Start();

            collider = gameObject.AddComponent<MeshCollider>();
            var mesh = new Mesh();
            gameObject.AddComponent<MeshFilter>();
            gameObject.GetComponent<MeshFilter>().mesh = mesh;
            collider.sharedMesh = this.gameObject.GetComponent<MeshFilter>().mesh;
        }

        public void OnTriggerEnter(Collider other)
        {
            Debug.Log("Skeleton noticed a collision with object " + other.name);

            colliders.Add(other);
        }

        public void OnTriggerExit(Collider other)
        {
            colliders.Remove(other);
        }
    }
}