using System.Collections.Generic;
using _Project.Scripts.Core;
using _Project.Scripts.Source;

namespace _Project.Scripts.DomainObjects.Rules
{
    public abstract class Rule
    {
        public bool colorize;
        public string notificationText;
        public int priority;
        
        public abstract bool IsInvalidated(List<Bone> boneObjects);
    }
}