using System;
using System.Collections.Generic;
using General.Skeleton;
using UnityEngine;

namespace General.Rules
{
    public class HorizontallyRule : Rule
    {
        public List<string> bones;
        public float tolerance;

        public override bool IsInvalidated(List<Bone> boneObjects)
        {
            var runningAngle = 0f;
            var plane = new Plane(Vector3.up, Vector3.zero);
            foreach (var bone in boneObjects)
            {
                var referenceVector = plane.normal;
                runningAngle += 90 - Vector3.Angle(bone.boneVector, referenceVector);
            }

            return runningAngle < -tolerance || runningAngle > tolerance;
        }
        
        public override bool Equals(Rule other)
        {
            return other != null && Equals(other as HorizontallyRule);
        }

        public override bool Equals(object other)
        {
            return Equals(other as HorizontallyRule);
        }

        public override int GetHashCode() => (bones, tolerance).GetHashCode();

        private bool Equals(HorizontallyRule other)
        {
            return other != null &&
                   bones.Equals(other.bones) &&
                   tolerance.Equals(other.tolerance);
        }

        public override string ToString()
        {
            return "Rule: " + GetType().Name + ", tolerance: " + tolerance + ", bones: " +
                   string.Join(", ", bones.ToArray());
        }
    }
}