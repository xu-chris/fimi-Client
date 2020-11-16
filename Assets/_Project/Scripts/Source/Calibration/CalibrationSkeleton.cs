using System;
using System.Collections.Generic;
using _Project.Scripts.Source.DomainValues;
using UnityEngine;

namespace _Project.Scripts.Source.Calibration
{
    public class CalibrationSkeleton : MonoBehaviour
    {
        private MeshCollider collider;
        private EventHandler collisionEventHandler;

        // Skeleton design
        private static readonly Color skeletonColor = Color.black;
        private const float sphereRadius = 0.05f;

        public int id;
        public bool withGameObjects = true;

        public List<Collider> colliders;

        private readonly List<Joint> joints = new List<Joint>();
        private readonly List<Bone> bones = new List<Bone>();

        public void Start()
        {
            id = transform.parent.childCount;
            gameObject.name = GameObjectNames.GetPrefix(GameObjectNames.NameType.SKELETON) + id;
            
            CreateBones();
            CreateJoints();

            collider = gameObject.AddComponent<MeshCollider>();
            var mesh = new Mesh();
            gameObject.AddComponent<MeshFilter>();
            gameObject.GetComponent<MeshFilter>().mesh = mesh;
            collider.sharedMesh = this.gameObject.GetComponent<MeshFilter>().mesh;
        }
        

        void OnTriggerEnter(Collider other)
        {
            Debug.Log("Skeleton noticed a collision with object " + other.name);

            colliders.Add(other);
        }

        private void OnTriggerExit(Collider other)
        {
            colliders.Remove(other);
        }

        public void SetIsVisible(bool visibility)
        {
            gameObject.SetActive(visibility);
        }

        public void SetSkeleton(Vector3[] jointEstimation, float lowestY)
        {
            if (joints.Count == 0 || bones.Count == 0)
            {
                Debug.Log("Skeleton is not yet ready, skipping");
                return;
            }
            gameObject.SetActive(true);
            foreach (var joint in joints)
            {
                joint.SetJointPosition(jointEstimation, lowestY);
            }

            foreach (var bone in bones)
            {
                bone.SetBoneSizeAndPosition(jointEstimation, lowestY);
            }
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
    }
}