using System.Collections.Generic;
using General.Skeleton;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;
using UnityEngine.Assertions;

namespace General.Rules
{
    public class AngleRule : Rule
    {
        public List<string> bones;
        public int expectedAngle;
        public int lowerTolerance;
        public int upperTolerance;
        [JsonConverter(typeof(StringEnumConverter))]
        public new RuleType ruleType = RuleType.ANGLE_RULE;

        public override bool IsInvalidated(List<Bone> boneObjects)
        {
            Assert.IsTrue(boneObjects.Count == 2, "You need to specify exactly two bones to check with this rule.");

            var calculatedAngle = Vector3.Angle(boneObjects[0].boneVector, boneObjects[1].boneVector);
            return calculatedAngle < expectedAngle - lowerTolerance || calculatedAngle > expectedAngle + upperTolerance;
        }

        public override bool Equals(Rule other)
        {
            return other != null && Equals(other as AngleRule);
        }

        public override bool Equals(object other)
        {
            return Equals(other as AngleRule);
        }

        public override int GetHashCode() => (bones, expectedAngle, lowerTolerance, upperTolerance).GetHashCode();

        private bool Equals(AngleRule other)
        {
            return other != null &&
                   bones.Equals(other.bones) && 
                   expectedAngle.Equals(other.expectedAngle) &&
                   lowerTolerance.Equals(other.lowerTolerance) && 
                   upperTolerance.Equals(other.upperTolerance);
        }

        public override string ToString()
        {
            return "Rule: " + GetType().Name + ", expected angle: " + expectedAngle + ", tolerance range: (" +
                   (expectedAngle - lowerTolerance) + ", " + (expectedAngle + upperTolerance) + "), bones: " +
                   string.Join(", ", bones.ToArray());
        }
    }
}