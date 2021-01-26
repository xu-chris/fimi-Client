using System;
using Clients.WebController;
using General;
using UnityEngine;
using UnityEngine.UI;
using uHTTP = Clients.WebController.uHTTP.uHTTP;

namespace InExercise
{
    public class InExerciseSceneManager : SceneManager
    {
        public string afterTrainingSceneName = "PostTraining";
        
        public GameObject progressBarGameObject;
        public Text remainingPanel;
        
        public int ignoreRuleViolationsOlderThanSeconds = 5;

        private long end;
        private bool isTransitioning;

        public InExerciseSkeletonManager skeletonManager;

        private void Start()
        {
            // Make sure the app is not hanging in the wrong screen
            sessionManager.SetToInTraining();
            
            // Set timing
            end = new DateTimeOffset(DateTime.UtcNow.AddSeconds(sessionManager.GetCurrentExerciseDuration())).ToUnixTimeMilliseconds();
            
            var ticksPerSecond = 1f / sessionManager.GetCurrentExerciseDuration();
            FillProgressBar(ticksPerSecond);
        }

        private void FixedUpdate()
        {
            var ruleSet = sessionManager.GetCurrentExercise().rules;
            var violatedRulesForAllSkeletons = skeletonManager.GetViolatedRulesForAllSkeletons(ruleSet);
            sessionManager.AddToTrainingReports(violatedRulesForAllSkeletons);
            SelectAndShowNotification();
            
            if (!IsTimeUp())
            {
                ChangeRemainingTimeText();
            }
            else
            {
                DecideOnNextSceneAndTransition();    
            }
        }

        private void DecideOnNextSceneAndTransition()
        {
            if (isTransitioning) return;
            
            isTransitioning = true;
            if (sessionManager.IsLastExercise())
            {
                CloseTraining();
            }
            else
            {
                NextExercise();
            }
        }

        private void NextExercise()
        {
            Dispatcher.Invoke(() =>
            {
                sessionManager.SetToNextExercise();
                StartCoroutine(TransitionToNewScene(nextSceneName));
            });
        }

        private void CloseTraining()
        {
            Dispatcher.Invoke(() =>
            {
                sessionManager.EndTraining();
                StartCoroutine(TransitionToNewScene(afterTrainingSceneName));
            });
        }

        protected override uHTTP.Response CancelTraining()
        {
            CloseTraining();
            return BuildResponse(true, "");
        }
        
        private bool IsTimeUp()
        {
            // Add one second for fluent transition
            var now = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds() + 1000;
            return now >= end;
        }

        private void ChangeRemainingTimeText()
        {
            var timeRemaining = DateTimeOffset.FromUnixTimeMilliseconds(end) - DateTimeOffset.Now;
            var text = (timeRemaining.Seconds + (timeRemaining.Minutes * 60)) + " s remaining";
            remainingPanel.text = text;
        }

        private void SelectAndShowNotification()
        {
            var mostViolatedRule =
                sessionManager.GetMostViolatedRuleForCurrentSessionInTimeInterval(ignoreRuleViolationsOlderThanSeconds);

            if (mostViolatedRule != null)
            {
                notificationManager.SetViolatedRule(mostViolatedRule);
            }
        }

        private void FillProgressBar(float decimalPercentage)
        {
            progressBarGameObject.GetComponent<Animator>().SetFloat("percentage", decimalPercentage);
        }

        
    }
}