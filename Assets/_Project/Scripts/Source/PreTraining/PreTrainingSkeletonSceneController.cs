using _Project.Scripts.DomainObjects.Configurations;
using _Project.Scripts.Periphery.Configurations;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Source.PreTraining
{
    public class PreTrainingSkeletonSceneController : SkeletonSceneController
    {
        public TextAsset preTrainingConfigurationFile;
        public Text title;

        private PreTrainingConfiguration configuration;

        public new void Start()
        {
            base.Start();
            configuration = new PreTrainingConfigurationService(preTrainingConfigurationFile).configuration;
        }
    }
}