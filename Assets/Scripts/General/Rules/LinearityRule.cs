using System.Collections.Generic;
using General.Skeleton;
using UnityEngine;

namespace General.Rules
{
    public class LinearityRule : Rule
    {
        public List<string> bones;
        public float tolerance;

        public override bool IsInvalidated(List<Bone> boneObjects)
        {
            var runningAngle = 0f;
            for (var i = 0; i < boneObjects.Count - 1; i++)
                runningAngle += Vector3.Angle(boneObjects[i].boneVector, boneObjects[i + 1].boneVector);

            // Allow both 0 and 180 degree
            runningAngle %= 180;

            return runningAngle > tolerance || runningAngle < -tolerance;
        }

        public override bool Equals(Rule other)
        {
            return other != null && Equals(other as LinearityRule);
        }

        public override bool Equals(object other)
        {
            return Equals(other as LinearityRule);
        }

        private bool Equals(LinearityRule other)
        {
            return other != null &&
                   bones.Equals(other.bones) &&
                   tolerance.Equals(other.tolerance);
        }
        
        public override int GetHashCode() => (bones, tolerance).GetHashCode();

        public override string ToString()
        {
            return "Rule: " + GetType().Name + ", tolerance: " + tolerance + ", bones: " +
                   string.Join(", ", bones.ToArray());
        }
    }
}