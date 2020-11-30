using UnityEngine;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace General.Trainings
{
    public class TrainingsConfigurationService
    {
        public TrainingsConfiguration configuration;

        public TrainingsConfigurationService(TextAsset configurationFile)
        {
            configuration = DecodeYaml(configurationFile.text);
        }

        private static TrainingsConfiguration DecodeYaml(string document)
        {
            var namingConvention = new CamelCaseNamingConvention();
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(namingConvention)
                .Build();

            return deserializer.Deserialize<TrainingsConfiguration>(document);
        }
    }
}