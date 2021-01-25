using System.Collections.Generic;
using System.Linq;
using General.Rules;
using General.Session;
using General.Skeleton;
using Library;
using UnityEngine;

namespace InExercise
{
    public class InExerciseSkeleton : Skeleton
    {
        public ViolatedRules GetViolatedRules(List<Rule> rules)
        {
            var violatedRules = new ViolatedRules(id);

            foreach (var rule in rules)
            {
                List<Bone> bonesConsideredForGivenRule;
                bool isViolated;
                switch (rule)
                {
                    case AngleRule angleRule:
                        bonesConsideredForGivenRule = angleRule.bones.ToBoneTypes().Select(GetBone).ToList();
                        isViolated = angleRule.IsInvalidated(bonesConsideredForGivenRule);
                        if (angleRule.colorize) GreenRedColoring(bonesConsideredForGivenRule, isViolated);
                        if (isViolated) violatedRules.RegisterViolation(angleRule);
                        break;
                    case RangeOfMotionRule rangeOfMotionRule:
                        bonesConsideredForGivenRule = rangeOfMotionRule.bones.ToBoneTypes().Select(GetBone).ToList();
                        isViolated = rangeOfMotionRule.IsInvalidated(bonesConsideredForGivenRule);
                        if (rangeOfMotionRule.colorize) RedNeutralColoring(bonesConsideredForGivenRule, isViolated);
                        if (isViolated) violatedRules.RegisterViolation(rangeOfMotionRule);
                        break;
                    case SymmetryRule symmetryRule:
                        var leftBones = symmetryRule.leftBones.ToBoneTypes().Select(GetBone).ToList();
                        var rightBones = symmetryRule.rightBones.ToBoneTypes().Select(GetBone).ToList();
                        var referenceBone = GetBone(symmetryRule.centerBone.ToBoneType());
                        isViolated = symmetryRule.IsInvalidated(leftBones, rightBones, referenceBone);
                        if (symmetryRule.colorize) RedNeutralColoring(leftBones, isViolated);
                        if (symmetryRule.colorize) RedNeutralColoring(rightBones, isViolated);
                        if (isViolated) violatedRules.RegisterViolation(symmetryRule);
                        break;
                    case LinearityRule linearityRule:
                        bonesConsideredForGivenRule = linearityRule.bones.ToBoneTypes().Select(GetBone).ToList();
                        isViolated = linearityRule.IsInvalidated(bonesConsideredForGivenRule);
                        if (linearityRule.colorize) GreenRedColoring(bonesConsideredForGivenRule, isViolated);
                        if (isViolated) violatedRules.RegisterViolation(linearityRule);
                        break;
                    case HorizontallyRule horizontallyRule:
                        bonesConsideredForGivenRule = horizontallyRule.bones.ToBoneTypes().Select(GetBone).ToList();
                        isViolated = horizontallyRule.IsInvalidated(bonesConsideredForGivenRule);
                        if (horizontallyRule.colorize) RedNeutralColoring(bonesConsideredForGivenRule, isViolated);
                        if (isViolated) violatedRules.RegisterViolation(horizontallyRule);
                        break;
                    case VerticallyRule verticallyRule:
                        bonesConsideredForGivenRule = verticallyRule.bones.ToBoneTypes().Select(GetBone).ToList();
                        isViolated = verticallyRule.IsInvalidated(bonesConsideredForGivenRule);
                        if (verticallyRule.colorize) RedNeutralColoring(bonesConsideredForGivenRule, isViolated);
                        if (isViolated) violatedRules.RegisterViolation(verticallyRule);
                        break;
                    case SpeedRule speedRule:
                        bonesConsideredForGivenRule = speedRule.bones.ToBoneTypes().Select(GetBone).ToList();
                        isViolated = speedRule.IsInvalidated(bonesConsideredForGivenRule);
                        if (isViolated) violatedRules.RegisterViolation(speedRule);
                        break;
                }
            }

            return violatedRules;
        }

        private static void RedNeutralColoring(List<Bone> bones, bool colorRed)
        {
            var color = colorRed ? Color.red : skeletonColor;
            ColorizeAllBones(bones, color);
        }

        private static void GreenRedColoring(List<Bone> bones, bool colorToRed)
        {
            var color = colorToRed ? Color.red : Color.green;
            ColorizeAllBones(bones, color);
        }

        private static void ColorizeAllBones(List<Bone> bones, Color color)
        {
            foreach (var bone in bones) bone.Colorize(color);
        }
    }
}