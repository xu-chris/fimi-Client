using System.Collections.Generic;
using General.Skeleton;

namespace General.Rules
{
    public abstract class Rule
    {
        public bool colorize;
        public string notificationText;
        public int priority;
        
        public abstract bool IsInvalidated(List<Bone> boneObjects);
    }
}