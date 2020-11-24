// The ConfigService is heavily inspired by YamlDotNet: https://dotnetfiddle.net/rrR2Bb

using _Project.Scripts.Source.DomainObjects.Configurations;
using UnityEngine;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace _Project.Scripts.Source.Periphery.Configurations
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