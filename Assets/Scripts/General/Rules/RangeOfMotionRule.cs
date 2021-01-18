using System.Collections.Generic;
using General.Skeleton;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SocialPlatforms;

// ReSharper disable ClassNeverInstantiated.Global - Class is instantiated by the YAML configuration reader

namespace General.Rules
{
    public class RangeOfMotionRule : Rule
    {
        public List<string> bones;
        public float lowerThreshold;
        public float upperThreshold;

        public override bool IsInvalidated(List<Bone> boneObjects)
        {
            Assert.IsTrue(boneObjects.Count == 2, "You need to specify exactly two bones to check with this rule.");

            var calculatedAngle = Vector3.Angle(boneObjects[0].boneVector, boneObjects[1].boneVector);
            return calculatedAngle > upperThreshold || calculatedAngle < lowerThreshold;
        }
        
        public override bool Equals(Rule other)
        {
            return other != null && Equals(other as RangeOfMotionRule);
        }

        public override bool Equals(object other)
        {
            return Equals(other as RangeOfMotionRule);
        }

        private bool Equals(RangeOfMotionRule other)
        {
            return other != null &&
                   bones.Equals(other.bones) &&
                   lowerThreshold.Equals(other.lowerThreshold) &&
                   upperThreshold.Equals(other.upperThreshold);
        }
        
        public override int GetHashCode() => (bones, lowerThreshold, upperThreshold).GetHashCode();

        public override string ToString()
        {
            return "Rule: " + GetType().Name + ", threshold range: (" +
                   lowerThreshold + ", " + upperThreshold + "), bones: " + string.Join(", ", bones.ToArray());
        }
    }
}