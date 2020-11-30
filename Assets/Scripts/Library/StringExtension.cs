using System;
using System.Linq;
using General.Exercises;
using General.Skeleton;

namespace Library
{
    internal static class StringExtension
    {
        public static ExerciseType ToExerciseType(this string str)
        {
            Enum.TryParse(str.ToUpperCaseWithUnderScore(), out ExerciseType value);
            return value;
        }

        public static BoneType ToBoneType(this string str)
        {
            Enum.TryParse(str.ToUpperCaseWithUnderScore(), out BoneType value);
            return value;
        }

        public static string ToUpperCaseWithUnderScore(this string str)
        {
            return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString())).ToUpper();
        }
    }
}