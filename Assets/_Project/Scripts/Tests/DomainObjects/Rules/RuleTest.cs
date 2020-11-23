using _Project.Scripts.Source;
using _Project.Scripts.Source.DomainValues;
using UnityEngine;

namespace Scripts.Tests.DomainObjects.Rules
{
    public class RuleTest
    {
        protected static Bone CreateDummyBone(Vector3 boneVector)
        {
            return new Bone(BoneType.HEAD, 0, 1, Color.black, new GameObject(), false)
            {
                boneVector = boneVector
            };
        }
        
        protected static Bone CreateRotatedDummyBone(float degree)
        {
            return CreateDummyBone(Vector3.RotateTowards(Vector3.up, Vector3.down, degree * Mathf.Deg2Rad, 1));
        }
    }
}