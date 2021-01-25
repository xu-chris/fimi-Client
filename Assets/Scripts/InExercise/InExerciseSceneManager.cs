using System;
using System.Collections.Generic;
using System.Linq;
using Clients.WebController;
using General;
using General.Session;
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
            CheckReports(violatedRulesForAllSkeletons);
            
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
            // TODO: Add logic to change the text
            var now = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
            var timeRemaining = DateTimeOffset.FromUnixTimeMilliseconds(end) - DateTimeOffset.Now;
            var text = timeRemaining.Seconds + " s remaining";
            remainingPanel.text = text;
        }

        private void CheckReports(List<ViolatedRules> reports)
        {
            Result highestInfectedRule = null;
            
            foreach (var report in reports.Where(report => report != null))
            {
                if (highestInfectedRule == null)
                {
                    highestInfectedRule = report.GetFirstResultInTimeFrame(ignoreRuleViolationsOlderThanSeconds);
                    continue;
                }

                if (report.Results().Where(result => result.violationRatio == 0).Any(result => report.Results()[0].violationRatio > highestInfectedRule.violationRatio))
                {
                    var rule = report.GetFirstResultInTimeFrame(ignoreRuleViolationsOlderThanSeconds);
                    highestInfectedRule = rule ?? highestInfectedRule;
                }
            }

            if (highestInfectedRule != null) notificationManager.SetViolatedRule(highestInfectedRule.rule);
        }

        private void FillProgressBar(float decimalPercentage)
        {
            progressBarGameObject.GetComponent<Animator>().SetFloat("percentage", decimalPercentage);
        }

        
    }
}