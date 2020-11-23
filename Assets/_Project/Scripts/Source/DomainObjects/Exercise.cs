using System.Collections.Generic;
using _Project.Scripts.DomainObjects;
using _Project.Scripts.Source.DomainObjects.Rules;
using _Project.Scripts.Source.DomainValues;

namespace _Project.Scripts.Source.DomainObjects
{
    public class Exercise
    {
        public List<Rule> rules;
        public string type;
        public string name;
        public string description;

        private ExerciseType ExerciseType()
        {
            return type.ToExerciseType();
        }
    }
}