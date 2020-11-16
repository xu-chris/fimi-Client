using System.Collections.Generic;
using _Project.Scripts.DomainObjects;
using _Project.Scripts.Source.DomainValues;
using UnityEngine;

namespace _Project.Scripts.Source
{
    public class Skeleton : MonoBehaviour
    {
        // Skeleton design
        protected static readonly Color skeletonColor = Color.black;
        private static readonly float sphereRadius = 0.05f;

        private readonly List<Joint> joints = new List<Joint>();
        private readonly List<Bone> bones = new List<Bone>();
        
        internal Person person = new Person();
        public int id;

        public void Start()
        {
            id = transform.parent.childCount;
            gameObject.name = GameObjectNames.GetPrefix(GameObjectNames.NameType.SKELETON) + id;

            CreateBones();
            CreateJoints();
        }
        
        public void FixedUpdate()
        {
            gameObject.SetActive(true);
            
            foreach (var joint in joints)
            {
                joint.SetJointPosition(person.joints, person.lowestY);
            }

            foreach (var bone in bones)
            {
                bone.SetBoneSizeAndPosition(person.joints, person.lowestY);
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
    }
}