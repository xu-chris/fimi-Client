using System.Collections.Generic;

namespace General.Skeleton
{
    public static class BonesToIndexes
    {
        public static readonly Dictionary<BoneType, BoneIndexes> dictionary =
            new Dictionary<BoneType, BoneIndexes>
            {
                {BoneType.LOWER_BODY, new BoneIndexes(0, 1)},
                {BoneType.UPPER_BODY, new BoneIndexes(1, 2)},
                {BoneType.NECK, new BoneIndexes(2, 3)},
                {BoneType.HEAD, new BoneIndexes(3, 4)},

                // Left
                {BoneType.LEFT_SHOULDER, new BoneIndexes(2, 5)},
                {BoneType.LEFT_ELBOW, new BoneIndexes(5, 6)},
                {BoneType.LEFT_FOREARM, new BoneIndexes(6, 7)},
                {BoneType.LEFT_HAND, new BoneIndexes(7, 8)},
                {BoneType.LEFT_HIP, new BoneIndexes(0, 9)},
                {BoneType.LEFT_THIGH, new BoneIndexes(9, 10)},
                {BoneType.LEFT_LOWER_LEG, new BoneIndexes(10, 11)},
                {BoneType.LEFT_FOOT, new BoneIndexes(11, 12)},

                // Right
                {BoneType.RIGHT_SHOULDER, new BoneIndexes(2, 13)},
                {BoneType.RIGHT_ELBOW, new BoneIndexes(13, 14)},
                {BoneType.RIGHT_FOREARM, new BoneIndexes(14, 15)},
                {BoneType.RIGHT_HAND, new BoneIndexes(15, 16)},
                {BoneType.RIGHT_HIP, new BoneIndexes(0, 17)},
                {BoneType.RIGHT_THIGH, new BoneIndexes(17, 18)},
                {BoneType.RIGHT_LOWER_LEG, new BoneIndexes(18, 19)},
                {BoneType.RIGHT_FOOT, new BoneIndexes(19, 20)}
            };

        public static BoneIndexes? GetIndexes(BoneType boneType)
        {
            if (dictionary.TryGetValue(boneType, out var result))
            {
                return result;
            }
            return null;
        }
    }
}