using System.Collections.Generic;
using System.Linq;
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
        
        public List<ViolatedRules> GetViolatedRulesForAllSkeletons(List<Rule> ruleSet)
        {
            return transform.GetComponentsInChildren<InExerciseSkeleton>().Select(skeleton => skeleton.GetViolatedRules(ruleSet)).ToList();
        }
    }
}