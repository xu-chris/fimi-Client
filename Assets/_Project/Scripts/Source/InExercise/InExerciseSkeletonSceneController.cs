using System.Collections;
using System.Linq;
using _Project.Scripts.DomainObjects;
using _Project.Scripts.Source.DomainObjects;
using _Project.Scripts.Source.DomainObjects.Configurations;
using _Project.Scripts.Source.Periphery.Configurations;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Source.InTraining
{
    public class InTrainingSkeletonSceneController : SkeletonSceneController
    {
        public TextAsset exercisesConfigurationFile;
        public TextAsset inTrainingConfigurationFile;

        public GameObject notificationPanel;
        private ExercisesConfiguration exercisesConfiguration;
        private InExerciseConfiguration inExerciseConfiguration;
        private bool notificationShown;

        private Exercise currentExercise;
        private const bool checkRules = true;

        private Vector3 offset;

        public new void Start()
        {
            base.Start();

            var inTrainingConfigurationService = new InExerciseConfigurationService(inTrainingConfigurationFile);
            inExerciseConfiguration = inTrainingConfigurationService.configuration;

            var exerciseConfigurationService = new ExercisesConfigurationService(exercisesConfigurationFile);
            exercisesConfiguration = exerciseConfigurationService.configuration;

            currentExercise = exercisesConfiguration.exercises[0];
        }

        public new void Update()
        {
            base.Update();

            if (checkRules)
                foreach (var skeleton in transform.GetComponentsInChildren<InTrainingSkeleton>())
                    skeleton.CheckRules(currentExercise.rules);

            var reports = GetReports();
            if (reports != null)
                CheckReports(reports);
        }

        protected override GameObject AddAdditionalSpecimenForSkeleton(GameObject skeleton)
        {
            return skeleton;
        }

        private ExerciseReport[] GetReports()
        {
            return transform.GetComponentsInChildren<InTrainingSkeleton>().Select(skeletonScript => skeletonScript.GetReport()).ToArray();
        }

        private void CheckReports(ExerciseReport[] reports)
        {
            ExerciseReport.Result highestInfectedRule = null;
            foreach (var report in reports)
            {
                if (report == null) continue;
                if (highestInfectedRule == null)
                {
                    highestInfectedRule = report.Results()[0];
                    continue;
                }

                foreach (var result in report.Results())
                {
                    if (result.count != 0) continue;

                    if (!(report.Results()[0].count > highestInfectedRule.count)) continue;
                    
                    highestInfectedRule = report.Results()[0];
                    break;
                }
            }

            if (highestInfectedRule != null) NotifyUser(highestInfectedRule.rule.notificationText);
        }

        private void NotifyUser(string text)
        {
            var animator = notificationPanel.GetComponent<Animator>();

            if (animator == null) return; // Exists
            if (animator.GetBool("show") || notificationShown) return; // Is not already shown
            notificationPanel.GetComponentInChildren<Text>().text = text;
            animator.SetBool("show", true);
            StartCoroutine(HideNotification(animator));
            notificationShown = true;
        }

        private IEnumerator HideNotification(Animator animator)
        {
            if (!animator.GetBool("show")) yield break;
            yield return new WaitForSeconds(inExerciseConfiguration.showNotificationDurationInSeconds);
            animator.SetBool("show", false);
            yield return new WaitForSeconds(1); // Can be removed when more stable
            notificationShown = false;
        }
    }
}