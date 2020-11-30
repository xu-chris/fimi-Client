using System.Collections.Generic;
using System.Linq;
using General.Skeleton;

namespace Library
{
    public static class ListExtension
    {
        public static List<BoneType> ToBoneTypes(this List<string> bones)
        {
            return bones.Select(boneType => boneType.ToBoneType()).ToList();
        }
    }
}