using System.Runtime.Serialization;

namespace General.Rules
{
    public enum RuleType
    {
        [EnumMember(Value = "ANGLE_RULE")]
        ANGLE_RULE,
        [EnumMember(Value = "HORIZONTALLY_RULE")]
        HORIZONTALLY_RULE,
        [EnumMember(Value = "LINEARITY_RULE")]
        LINEARITY_RULE,
        [EnumMember(Value = "RANGE_OF_MOTION_RULE")]
        RANGE_OF_MOTION_RULE,
        [EnumMember(Value = "SPEED_RULE")]
        SPEED_RULE,
        [EnumMember(Value = "SYMMETRY_RULE")]
        SYMMETRY_RULE,
        [EnumMember(Value = "VERTICALLY_RULE")]
        VERTICALLY_RULE
    }
}