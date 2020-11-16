using System.Collections.Generic;

namespace _Project.Scripts.Source.DomainValues
{
    public static class JointToIndex
    {
        internal static Dictionary<JointType, int> dictionary = new Dictionary<JointType, int>
        {
            {JointType.SPINE1_RX, 0},
            {JointType.SPINE2_RX, 1},
            {JointType.SPINE3_RX, 2},
            {JointType.NECK1_RX, 3},
            {JointType.HEAD_EE_RY, 4},
            {JointType.LEFT_SHOULDER_RX, 5},
            {JointType.LEFT_ELBOW_RX, 6},
            {JointType.LEFT_HAND_RX, 7},
            {JointType.LEFT_HAND_EE_RX, 8},
            {JointType.LEFT_HIP_RX, 9},
            {JointType.LEFT_KNEE_RX, 10},
            {JointType.LEFT_ANKLE_RX, 11},
            {JointType.LEFT_FOOT_EE, 12},
            {JointType.RIGHT_SHOULDER_RX, 13},
            {JointType.RIGHT_ELBOW_RX, 14},
            {JointType.RIGHT_HAND_RX, 15},
            {JointType.RIGHT_HAND_EE_RX, 16},
            {JointType.RIGHT_HIP_RX, 17},
            {JointType.RIGHT_KNEE_RX, 18},
            {JointType.RIGHT_ANKLE_RX, 19},
            {JointType.RIGHT_FOOT_EE, 20},            
        };
    }
}