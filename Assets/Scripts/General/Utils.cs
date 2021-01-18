using System.Collections.Generic;
using General.Exercises;
using Library;
using UnityEngine;

namespace General
{
    public static class Utils
    {
        public static GameObject GetOrInstantiate(Tag tag, GameObject prefab)
        {
            var gameObjectToBeFound = GameObject.FindGameObjectWithTag(tag.ToString());
            
            if (gameObjectToBeFound != null) return gameObjectToBeFound;
            GameObject.Instantiate(prefab);
            gameObjectToBeFound  = GameObject.FindGameObjectWithTag(tag.ToString());

            return gameObjectToBeFound;
        }
        
        public static Exercise GetExerciseForExerciseType(ExerciseType exerciseType, List<Exercise> exercises)
        {
            return exercises.Find(exercise =>
                exercise.type.ToExerciseType() == exerciseType);
        }
    }
}