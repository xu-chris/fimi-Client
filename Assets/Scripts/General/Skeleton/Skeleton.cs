using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace General.Skeleton
{
    public class Skeleton : MonoBehaviour
    {
        // Skeleton design
        protected static readonly Color skeletonColor = Color.black;
        private const float sphereRadius = 0.05f;

        private readonly List<Joint> joints = new List<Joint>();
        private readonly List<Bone> bones = new List<Bone>();
        
        private new MeshCollider collider;
        public delegate void FullCollisionEventHandler(object source, EventArgs args);

        public event FullCollisionEventHandler CollisionStarted;
        public event FullCollisionEventHandler CollisionStopped;

        public List<Collider> colliders;
        
        public int id;

        public void Start()
        {
            id = transform.parent.childCount - 1;
            gameObject.name = GameObjectNames.GetPrefix(GameObjectNames.NameType.SKELETON) + id;
            gameObject.tag = Tag.SKELETON.ToString();

            CreateBones();
            CreateJoints();
            
            collider = gameObject.AddComponent<MeshCollider>();
            var mesh = new Mesh();
            gameObject.AddComponent<MeshFilter>();
            gameObject.GetComponent<MeshFilter>().mesh = mesh;
            collider.sharedMesh = this.gameObject.GetComponent<MeshFilter>().mesh;

            gameObject.AddComponent<Rigidbody>();
            var newRigidBody = gameObject.GetComponent<Rigidbody>();
            newRigidBody.useGravity = false;
        }

        internal void UpdateSkeleton(Person person)
        {
            var basePoint = transform.parent.position;
            gameObject.SetActive(true);

            foreach (var joint in joints)
            {
                joint.SetJointPosition(person.joints, person.lowestY, basePoint);
            }

            foreach (var bone in bones)
            {
                bone.SetBoneSizeAndPosition(person.joints, person.lowestY, basePoint);
            }
        }

        /**
         * Returns the bone for a given BoneType.
         * @return Bone the bone.
         */
        protected Bone GetBone(BoneType boneType)
        {
            return bones.Find(item => item.boneType.Equals(boneType));
        }
        
        private void CreateBones()
        {
            foreach (var bone in BonesToIndexes.dictionary)
            {
                CreateBone(bone.Key, bone.Value);
            }
        }

        private void CreateJoints()
        {
            foreach (var joint in JointToIndex.dictionary)
            {
                CreateJoint(joint.Key, joint.Value);
            }
        }

        private void CreateBone(BoneType boneType, BoneIndexes boneIndexes)
        {
            var bone = new Bone(boneType, boneIndexes.indexA, boneIndexes.indexB, skeletonColor, gameObject, true);
            bones.Add(bone);
        }

        private void CreateJoint(JointType jointType, int index)
        {
            var joint = new Joint(index, jointType, skeletonColor, sphereRadius, gameObject, true);
            joints.Add(joint);
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