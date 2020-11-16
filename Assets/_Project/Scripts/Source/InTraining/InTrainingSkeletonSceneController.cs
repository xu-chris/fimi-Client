using System.Collections;
using System.Linq;
using _Project.Scripts.DomainObjects;
using _Project.Scripts.DomainObjects.Configurations;
using _Project.Scripts.Periphery.Configurations;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Source.InTraining
{
    public class InTrainingSkeletonSceneController : SkeletonSceneController
    {
        public TextAsset exercisesConfigurationFile;

        public TextAsset inTrainingConfigurationFile;

        public Text reportingTextField;

        public GameObject notificationPanel;
        private ExercisesConfiguration exercisesConfiguration;
        private InTrainingConfiguration inTrainingConfiguration;
        private bool notificationShown;

        private Vector3 offset;

        public new void Start()
        {
            base.Start();
            Application.runInBackground = true;

            var inTrainingConfigurationService = new InTrainingConfigurationService(inTrainingConfigurationFile);
            inTrainingConfiguration = inTrainingConfigurationService.configuration;

            var exerciseConfigurationService = new ExercisesConfigurationService(exercisesConfigurationFile);
            exercisesConfiguration = exerciseConfigurationService.configuration;
            
            SetCurrentExercise(exercisesConfiguration.exercises[0]);
            ActivateCheckingRules();
        }

        public new void Update()
        {
            base.Update();

            var reports = GetReports();
            if (reports != null)
                CheckReports(reports);
        }

        private void SetCurrentExercise(Exercise exercise)
        {
            foreach (var skeletonScript in transform.GetComponentsInChildren<InTrainingSkeleton>())
            {
                skeletonScript.rules = exercise.rules;
            }
        }

        private void ActivateCheckingRules()
        {
            foreach (var skeletonScript in transform.GetComponentsInChildren<InTrainingSkeleton>())
            {
                skeletonScript.shouldCheckRules = true;
            }
        }

        private void DeactivateCheckingRules()
        {
            foreach (var skeletonScript in transform.GetComponentsInChildren<InTrainingSkeleton>())
            {
                skeletonScript.shouldCheckRules = false;
            }
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
            yield return new WaitForSeconds(inTrainingConfiguration.showNotificationDurationInSeconds);
            animator.SetBool("show", false);
            yield return new WaitForSeconds(1); // Can be removed when more stable
            notificationShown = false;
        }
    }
}