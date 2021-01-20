using System.Collections.Generic;
using General.Skeleton;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

namespace General.Rules
{
    /// <summary>
    ///     Vertically is defined as an angle difference of 0 between a given set of bones and a directional vector.
    ///     Given the circumstances, we define Vector2(0, 1) as our reference vector which means up straight in the Y
    ///     direction. We ignore the Z axis for our calculation completely.
    /// </summary>
    public class VerticallyRule : Rule
    {
        private readonly string axis = "y"; // Y or Z axis
        public List<string> bones;
        public float tolerance;
        [JsonConverter(typeof(StringEnumConverter))]
        public new RuleType ruleType = RuleType.VERTICALLY_RULE;

        public override bool IsInvalidated(List<Bone> boneObjects)
        {
            var referenceVector = new Vector2(0, 1);
            var angleDifference = 0f;
            foreach (var bone in boneObjects)
            {
                var yAxisForComparison = axis == "y" ? bone.boneVector.y : bone.boneVector.z;
                var comparisonVector = new Vector2(bone.boneVector.x, yAxisForComparison);
                angleDifference += Vector2.Angle(referenceVector, comparisonVector);
            }

            return angleDifference > tolerance || angleDifference < -tolerance;
        }
        
        public override bool Equals(Rule other)
        {
            return other != null && Equals(other as VerticallyRule);
        }

        public override bool Equals(object other)
        {
            return Equals(other as VerticallyRule);
        }

        private bool Equals(VerticallyRule other)
        {
            return other != null &&
                   bones.Equals(other.bones) &&
                   tolerance.Equals(other.tolerance) && 
                   axis.Equals(other.axis);
        }
        
        public override int GetHashCode() => (bones, tolerance, axis).GetHashCode();

        public override string ToString()
        {
            return "Rule: " + GetType().Name + ", tolerance: " + tolerance + ", bones: " +
                   string.Join(", ", bones.ToArray());
        }
    }
}