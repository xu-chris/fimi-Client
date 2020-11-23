using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

// ReSharper disable ClassNeverInstantiated.Global - Class is instantiated by the YAML configuration reader

namespace _Project.Scripts.Source.DomainObjects.Rules
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

        public override string ToString()
        {
            return "Rule: " + GetType().Name + ", threshold range: (" +
                   lowerThreshold + ", " + upperThreshold + "), bones: " + string.Join(", ", bones.ToArray());
        }
    }
}