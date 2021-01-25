using System.Collections.Generic;
using General.Rules;

namespace General.Session
{
    public class ViolatedRules
    {
        internal List<Rule> violatedRules = new List<Rule>();
        private int skeletonId;

        public ViolatedRules(int skeletonId)
        {
            this.skeletonId = skeletonId;
        }

        public void RegisterViolation(Rule rule)
        {
            violatedRules.Add(rule);
        }
        
        public int GetSkeletonId()
        {
            return skeletonId;
        }
    }
}