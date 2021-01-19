using System;
using System.Collections.Generic;
using System.Linq;
using General.Exercises;
using General.Trainings;
using Library;

namespace General.Session
{
    [Serializable]
    public class TrainingDTO
    {
        public string name;
        public int durationInSeconds;
        public List<ExerciseDTO> exercises;
        public int id;
        public int currentExercise;
        public string description;

        public TrainingDTO(int id, Training training, int currentExercise, List<Exercise> exercises)
        {
            this.id = id;
            name = training.name;
            durationInSeconds = GetTotalDurationInSeconds(training.exercises);
            this.currentExercise = currentExercise;
            this.exercises = GetExerciseDTOs(training.exercises, exercises, currentExercise);
            description = training.description;
        }

        private static int GetTotalDurationInSeconds(List<ExerciseItem> exerciseItems)
        {
            return exerciseItems.Sum(exerciseItem => exerciseItem.durationInSeconds);
        }

        private static List<ExerciseDTO> GetExerciseDTOs(List<ExerciseItem> exerciseItems, List<Exercise> exercises, int currentExercise)
        {
            var result = new List<ExerciseDTO>();

            for (var i = 0; i < exerciseItems.Count; i++)
            {
                var exerciseItem = exerciseItems[i];
                var exercise = Utils.GetExerciseForExerciseType(exerciseItem.type.ToExerciseType(), exercises);
                result.Add(new ExerciseDTO(i, exerciseItem.durationInSeconds, exercise));
            }

            return result;
        }
        
    }
}