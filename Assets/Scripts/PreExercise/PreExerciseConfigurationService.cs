using UnityEngine;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace PreExercise
{
    public class PreExerciseConfigurationService
    {
        public PreExerciseConfiguration configuration;

        public PreExerciseConfigurationService(TextAsset configurationFile)
        {
            // Start decoding the yaml file
            configuration = DecodeYaml(configurationFile.text);
        }

        private static PreExerciseConfiguration DecodeYaml(string document)
        {
            var namingConvention = new CamelCaseNamingConvention();
            var deserializer = new DeserializerBuilder().WithNamingConvention(namingConvention).Build();

            return deserializer.Deserialize<PreExerciseConfiguration>(document);
        }
    }
}