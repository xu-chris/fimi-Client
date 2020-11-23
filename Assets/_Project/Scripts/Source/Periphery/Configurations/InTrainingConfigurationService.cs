// The ConfigService is heavily inspired by YamlDotNet: https://dotnetfiddle.net/rrR2Bb

using _Project.Scripts.Source.DomainObjects.Configurations;
using UnityEngine;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace _Project.Scripts.Periphery.Configurations
{
    public class InTrainingConfigurationService
    {
        public InTrainingConfiguration configuration;

        public InTrainingConfigurationService(TextAsset configurationFile)
        {
            // Start decoding the yaml file
            configuration = DecodeYaml(configurationFile.text);
        }

        private static InTrainingConfiguration DecodeYaml(string document)
        {
            var namingConvention = new CamelCaseNamingConvention();
            var deserializer = new DeserializerBuilder().WithNamingConvention(namingConvention).Build();

            return deserializer.Deserialize<InTrainingConfiguration>(document);
        }
    }
}