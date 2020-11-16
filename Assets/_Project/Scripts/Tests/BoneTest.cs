using _Project.Scripts.Source;
using _Project.Scripts.Source.DomainValues;
using NUnit.Framework;
using UnityEngine;

namespace Scripts.Tests
{
    public class BoneTest
    {
        [Test]
        public void ShouldUpdateBoneVectorAfterSettingSizeAndRotation()
        {
            // GIVEN
            var start = new Vector3(0, 0, 0);
            var end = new Vector3(0, 0, 1);
            var expectedResult = end - start;
            
            var bone = new Bone(BoneType.RIGHT_HAND, 15, 16, Color.black, new GameObject(), false);
            
            // WHEN
            bone.SetBoneSizeAndPosition(start, end, 0f);
            var result = bone.boneVector;
            
            // THEN
            Assert.AreEqual(result, expectedResult);
        }
    }
}
