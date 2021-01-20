using System.Collections.Generic;
using General;
using General.Rules;
using General.Session;
using UnityEngine;

namespace InExercise
{
    public class InExerciseSkeletonManager : SkeletonManager
    {
        protected override GameObject AddAdditionalSpecimenForSkeleton(GameObject skeleton)
        {
            return skeleton;
        }
        
        public List<ExerciseReport> CheckRules(List<Rule> ruleSet)
        {
            var reports = new List<ExerciseReport>();
            foreach (var skeleton in transform.GetComponentsInChildren<InExerciseSkeleton>())
            {
                skeleton.CheckRules(ruleSet);
                reports.Add(skeleton.GetReport());
            }

            return reports;
        }
    }
}