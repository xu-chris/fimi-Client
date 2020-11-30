using System.Collections.Generic;
using General.Rules;
using Library;

namespace General.Exercises
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