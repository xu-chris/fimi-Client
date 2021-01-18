using System;
using System.Collections.Generic;
using General.Exercises;
using General.Trainings;

namespace General.Session
{
    [Serializable]
    public class TrainingsDTO
    {
        public List<TrainingDTO> trainings;

        public TrainingsDTO(List<Training> trainings, List<Exercise> exercises, int currentExercise)
        {
            this.trainings = new List<TrainingDTO>();

            for (var i = 0; i < trainings.Count; i++)
            {
                var training = trainings[i];
                this.trainings.Add(new TrainingDTO(i, training, currentExercise, exercises));
            }
        }
    }
}