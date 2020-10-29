using _Project.Scripts.DomainObjects.Configurations;
using _Project.Scripts.Periphery.Configurations;
using _Project.Scripts.Source;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Core.PreTraining
{
    public class PreTrainingSceneController : SceneController
    {
        public TextAsset preTrainingConfigurationFile;
        public Text title;

        private PreTrainingConfiguration configuration;

        public void Start()
        {
            configuration = new PreTrainingConfigurationService(preTrainingConfigurationFile).configuration;
        }
    }
}