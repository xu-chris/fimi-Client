using System;
using General.Exercises;

namespace General.Session
{
    [Serializable]
    public class ExerciseDTO
    {
        public int id;
        public string name;
        public int durationInSeconds;
        public string description;

        public ExerciseDTO(int id, int durationInSeconds, Exercise exercise)
        {
            this.id = id;
            name = exercise.name;
            this.durationInSeconds = durationInSeconds;
            description = exercise.description;
        }
    }
}