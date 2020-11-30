using System;
using System.Collections.Generic;
using System.Linq;
using General;
using General.Skeleton;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Project.Scripts.Source.PreExercise.Calibration
{
    public class CalibrationSkeleton : Skeleton
    {
        private new MeshCollider collider;
        public delegate void FullCollisionEventHandler(object source, EventArgs args);

        public event FullCollisionEventHandler CollisionStarted;
        public event FullCollisionEventHandler CollisionStopped;

        public List<Collider> colliders;

        public new void Start()
        {
            base.Start();

            collider = gameObject.AddComponent<MeshCollider>();
            var mesh = new Mesh();
            gameObject.AddComponent<MeshFilter>();
            gameObject.GetComponent<MeshFilter>().mesh = mesh;
            collider.sharedMesh = this.gameObject.GetComponent<MeshFilter>().mesh;

            gameObject.AddComponent<Rigidbody>();
            var newRigidBody = gameObject.GetComponent<Rigidbody>();
            newRigidBody.useGravity = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Skeleton noticed a collision with object " + other.name);

            colliders.Add(other);

            if (IsAllOrNothing())
            {
                OnFullCollisionStart();   
            }
        }

        private void OnTriggerExit(Collider other)
        {
            colliders.Remove(other);
            OnFullCollisionStop();
        }

        private bool IsAllOrNothing()
        {
            Assert.IsTrue(GameObject.FindGameObjectsWithTag(Tag.COLLISION_POINT.ToString()).Length > 0);
            var res = (from x in colliders select x).Distinct().Count();
            var numberOfColliders = GameObject.FindGameObjectsWithTag(Tag.COLLISION_POINT.ToString()).Length;
            
            return res == numberOfColliders;
        }

        protected virtual void OnFullCollisionStart()
        {
            CollisionStarted?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnFullCollisionStop()
        {
            CollisionStopped?.Invoke(this, EventArgs.Empty);
        }
    }
}