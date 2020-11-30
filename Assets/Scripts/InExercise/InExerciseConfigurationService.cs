// The ConfigService is heavily inspired by YamlDotNet: https://dotnetfiddle.net/rrR2Bb

using UnityEngine;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace InExercise
{
    public class InExerciseConfigurationService
    {
        public InExerciseConfiguration configuration;

        public InExerciseConfigurationService(TextAsset configurationFile)
        {
            // Start decoding the yaml file
            configuration = DecodeYaml(configurationFile.text);
        }

        private static InExerciseConfiguration DecodeYaml(string document)
        {
            var namingConvention = new CamelCaseNamingConvention();
            var deserializer = new DeserializerBuilder().WithNamingConvention(namingConvention).Build();

            return deserializer.Deserialize<InExerciseConfiguration>(document);
        }
    }
}