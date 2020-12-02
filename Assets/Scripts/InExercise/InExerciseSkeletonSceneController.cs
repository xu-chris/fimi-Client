using System.Collections;
using System.Linq;
using Clients;
using General;
using General.Exercises;
using UnityEngine;
using UnityEngine.UI;

namespace InExercise
{
    public class InExerciseSkeletonSceneController : SkeletonSceneController
    {
        public TextAsset exercisesConfigurationFile;
        public TextAsset inExerciseConfigurationFile;
        public GameObject notificationPanel;
        public Text remainingPanel;
        public GameObject progressBarGameObject;
        
        private ExercisesConfiguration exercisesConfiguration;
        private InExerciseConfiguration inExerciseConfiguration;
        private bool notificationShown;

        private Exercise currentExercise;
        private const bool checkRules = true;

        private Vector3 offset;

        private int total;
        private int current;
        private int remaining;

        public new void Start()
        {
            base.Start();

            var inExerciseConfigurationService = new InExerciseConfigurationService(inExerciseConfigurationFile);
            inExerciseConfiguration = inExerciseConfigurationService.configuration;

            var exerciseConfigurationService = new ExercisesConfigurationService(exercisesConfigurationFile);
            exercisesConfiguration = exerciseConfigurationService.configuration;

            currentExercise = exercisesConfiguration.exercises[0];
        }

        public new void Update()
        {
            base.Update();

            if (checkRules)
                foreach (var skeleton in transform.GetComponentsInChildren<InExerciseSkeleton>())
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
            return transform.GetComponentsInChildren<InExerciseSkeleton>().Select(skeletonScript => skeletonScript.GetReport()).ToArray();
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

        private void ChangeRemainingTo(string text)
        {
            remainingPanel.text = text;
        }

        private void NotifyUser(string text)
        {
            var animator = notificationPanel.GetComponent<Animator>();

            if (animator == null) return; // Exists
            if (animator.GetBool("show") || notificationShown) return; // Is not already shown
            GetComponent<TTSClient>().Synthesize(text);
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