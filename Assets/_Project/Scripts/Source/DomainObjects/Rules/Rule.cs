using System.Collections.Generic;

namespace _Project.Scripts.Source.DomainObjects.Rules
{
    public abstract class Rule
    {
        public bool colorize;
        public string notificationText;
        public int priority;
        
        public abstract bool IsInvalidated(List<Bone> boneObjects);
    }
}