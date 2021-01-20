using System;
using Library;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

// Idea derived from StackOverflow
// https://stackoverflow.com/questions/20995865/deserializing-json-to-abstract-class
// The RuleConverter helps to convert the correct JSON string of a rule into the correct rule object.

namespace General.Rules
{
    public class RuleSpecifiedConcreteClassConverter : DefaultContractResolver
    {
        protected override JsonConverter ResolveContractConverter(Type objectType)
        {
            if (typeof(Rule).IsAssignableFrom(objectType) && !objectType.IsAbstract)
                return null; // pretend TableSortRuleConvert is not specified (thus avoiding a stack overflow)
            return base.ResolveContractConverter(objectType);
        }
    }
    
    public class RuleConverter : JsonConverter
    {
        static JsonSerializerSettings SpecifiedSubclassConversion = new JsonSerializerSettings() { ContractResolver = new RuleSpecifiedConcreteClassConverter() };

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Rule));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jo = JObject.Load(reader);
            switch (jo["ruleType"].Value<string>().ToRuleType())
            {
                case RuleType.ANGLE_RULE:
                    return JsonConvert.DeserializeObject<AngleRule>(jo.ToString(), SpecifiedSubclassConversion);
                case RuleType.SPEED_RULE:
                    return JsonConvert.DeserializeObject<SpeedRule>(jo.ToString(), SpecifiedSubclassConversion);
                case RuleType.SYMMETRY_RULE:
                    return JsonConvert.DeserializeObject<SymmetryRule>(jo.ToString(), SpecifiedSubclassConversion);
                case RuleType.LINEARITY_RULE:
                    return JsonConvert.DeserializeObject<LinearityRule>(jo.ToString(), SpecifiedSubclassConversion);
                case RuleType.VERTICALLY_RULE:
                    return JsonConvert.DeserializeObject<VerticallyRule>(jo.ToString(), SpecifiedSubclassConversion);
                case RuleType.HORIZONTALLY_RULE:
                    return JsonConvert.DeserializeObject<HorizontallyRule>(jo.ToString(), SpecifiedSubclassConversion);
                case RuleType.RANGE_OF_MOTION_RULE:
                    return JsonConvert.DeserializeObject<RangeOfMotionRule>(jo.ToString(), SpecifiedSubclassConversion);
                default:
                    throw new Exception();
            }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException(); // won't be called because CanWrite returns false
        }
    }
}