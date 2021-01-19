using System;
using System.Collections.Generic;
using General.Skeleton;

namespace General.Rules
{
    public abstract class Rule : IEquatable<Rule>
    {
        public bool colorize;
        public string notificationText;
        public string improvementText;
        public string watchOutText;
        public int priority;
        
        public abstract bool IsInvalidated(List<Bone> boneObjects);

        public virtual bool Equals(Rule other)
        {
            return other != null && colorize.Equals(other.colorize) && 
                   notificationText.Equals(other.notificationText) && 
                   priority.Equals(other.priority);
        }
        public abstract override bool Equals(object other);

        public abstract override int GetHashCode();
    }
}