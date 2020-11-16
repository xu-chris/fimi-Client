using System;
using System.Collections.Generic;
using _Project.Scripts.Source.DomainValues;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace _Project.Scripts.Source.Calibration
{
    public class CalibrationSkeleton : MonoBehaviour
    {
        private MeshCollider collider;
        private EventHandler collisionEventHandler;

        // Skeleton design
        private static readonly Color skeletonColor = Color.black;
        private static readonly float sphereRadius = 0.05f;
        
        public int id;
        public bool withGameObjects = true;

        public List<Collider> colliders;

        public void Start()
        {
            id = transform.parent.childCount;
            gameObject.name = GameObjectNames.GetPrefix(GameObjectNames.NameType.SKELETON) + id;
            
            CreateBones();
            CreateJoints();

            collider = this.gameObject.AddComponent<MeshCollider>();
            Mesh mesh = new Mesh();
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
            foreach (var child in gameObject.GetComponentsInChildren<Transform>())
            {
                if (child.CompareTag(Tag.JOINT.ToString()))
                {
                    // Mapping of joint to index
                    var jointType = (JointType) Enum.Parse(typeof(JointType), child.name);
                    if (!JointToIndex.dictionary.ContainsKey(jointType)) continue;
                    var index = JointToIndex.dictionary[jointType];
                    var vector = new Vector3(jointEstimation[index][0], jointEstimation[index][1] - lowestY, jointEstimation[index][2]);
                    child.position = vector;
                }
                else if (child.CompareTag(Tag.BONE.ToString()))
                {
                    var boneType = (BoneType) Enum.Parse(typeof(BoneType), child.name);
                    if (!BonesToIndexes.dictionary.ContainsKey(boneType)) continue;
                    var boneIndexes = BonesToIndexes.dictionary[boneType];
                    var startJoint = jointEstimation[boneIndexes.indexA];
                    var endJoint = jointEstimation[boneIndexes.indexB];
                    
                    
                    // Go to unit sphere
                    child.position = Vector3.zero;
                    child.rotation = Quaternion.identity;
                    child.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                    var boneVector = endJoint - startJoint;

                    // Set z-axis of sphere to align with bone
                    var zScale = boneVector.magnitude * 0.95f;
                    var xyScale = zScale * 0.28f;
                    child.localScale = new Vector3(xyScale, xyScale, zScale);

                    // Reducing noise 
                    if (!(boneVector.magnitude > 0.00001)) return;

                    // Rotate z-axis to align with bone vector
                    child.rotation = Quaternion.LookRotation(boneVector.normalized);
                    // Position at middle
                    child.position = (startJoint + endJoint) / 2.0f - new Vector3(0, lowestY, 0);

                    Assert.AreEqual(boneVector, endJoint - startJoint);
                }

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
            var bone = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            bone.name = boneType.ToString();
            bone.transform.parent = gameObject.transform;
            bone.GetComponent<Renderer>().material.color = skeletonColor;
            bone.tag = Tag.BONE.ToString();
        }

        private void CreateJoint(JointType jointType, int index)
        {
            var joint = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            joint.name = jointType.ToString();
            joint.transform.parent = gameObject.transform;
            joint.transform.localScale = new Vector3(sphereRadius, sphereRadius, sphereRadius);
            joint.GetComponent<Renderer>().material.color = skeletonColor;
            joint.tag = Tag.JOINT.ToString();
        }
    }
}