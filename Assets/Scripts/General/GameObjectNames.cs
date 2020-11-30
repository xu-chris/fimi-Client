using System.Collections.Generic;

namespace General
{
    public static class GameObjectNames
    {
        public enum NameType
        {
            SKELETON,
            JOINT,
            BONE
        }

        private static Dictionary<NameType, string> names = new Dictionary<NameType, string>
        {
            {NameType.SKELETON, "Skeleton"},
            {NameType.JOINT, "Joint"},
            {NameType.BONE, "Bone"}
        };

        public static string GetPrefix(NameType nameType)
        {
            return names[nameType] + "_";
        }
    }
}