using System.Collections.Generic;
using _Project.Scripts.DomainObjects;
using _Project.Scripts.Source.DomainValues;

namespace _Project.Scripts.Source.DomainObjects
{
    public struct Training
    {
        public string name;
        public List<ExerciseItem> exercises;
    }
}