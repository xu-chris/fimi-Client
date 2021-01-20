using System;
using System.Collections.Generic;
using System.Linq;
using Clients.WebController.WebServer;
using Clients.WebController.WebServer.uHTTP;
using General;
using General.Session;
using UnityEngine;
using UnityEngine.UI;

namespace InExercise
{
    public class InExerciseSceneManager : SceneManager
    {
        public string afterTrainingSceneName = "PostTraining";
        
        public GameObject progressBarGameObject;
        public Text remainingPanel;
        
        public int ignoreRuleViolationsOlderThanSeconds = 5;

        private long end;

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
        
        private void Update()
        {
            var ruleSet = sessionManager.GetCurrentExercise().rules;
            var reports = skeletonManager.CheckRules(ruleSet);
            sessionManager.AddToTrainingReports(reports);
            CheckReports(reports);

            // TODO: Add logic to check remaining time
            if (!IsTimeUp())
            {
                ChangeRemainingTimeText();
            }
            else
            {
                Dispatcher.Invoke(() =>
                {
                    StartCoroutine(TransitionToNewScene(sessionManager.IsLastExercise()
                        ? afterTrainingSceneName
                        : nextSceneName));
                });
            }
        }

        protected override uHTTP.Response CancelTraining()
        {
            Dispatcher.Invoke(() =>
            {
                sessionManager.EndTraining();
                StartCoroutine(TransitionToNewScene(afterTrainingSceneName));
            });
            return BuildResponse(true, "");
        }
        
        private bool IsTimeUp()
        {
            var now = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
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

        private void CheckReports(List<ExerciseReport> reports)
        {
            Result highestInfectedRule = null;
            
            foreach (var report in reports.Where(report => report != null))
            {
                if (highestInfectedRule == null)
                {
                    highestInfectedRule = report.GetFirstResultInTimeFrame(ignoreRuleViolationsOlderThanSeconds);
                    continue;
                }

                if (report.Results().Where(result => result.count == 0).Any(result => report.Results()[0].count > highestInfectedRule.count))
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