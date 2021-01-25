using System;
using System.Collections.Generic;
using System.Linq;
using General.Rules;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace General.Session
{
    [Serializable]
    public class TrainingReport
    {
        public readonly List<Result> results;
        public readonly int id;
        private ulong totalChecks = 0;

        public TrainingReport(int trainingId)
        {
            id = trainingId;
            results = new List<Result>();
        }

        [JsonConstructor]
        public TrainingReport(List<Result> results, int id)
        {
            this.results = results;
            this.id = id;
        }

        public int GetTrainingId()
        {
            return id;
        }

        public void RegisterCheck(Rule rule, bool violated)
        {
            totalChecks += 1;
            try
            {
                results.First(i => i.rule.Equals(rule)).RegisterCheck(violated, totalChecks);
            }
            catch (Exception e)
            {
                var result = new Result(rule);
                result.RegisterCheck(violated, totalChecks);
                results.Add(result);
            }
        }
        
        
        public Result[] GetResults()
        {
            return OrderResults().ToArray();
        }
        
        private List<Result> OrderResults()
        {
            return new List<Result>(results.
                OrderBy(i => i.rule.priority).
                ThenByDescending(i => i.lastCollected).
                ThenByDescending(i => i.violationRatio));
        }

        public List<Result> GetImprovementsComparedTo(TrainingReport previousReport)
        {
            if (id != previousReport.GetTrainingId())
            {
                throw new ArgumentException("Training IDs aren't matching");
            }

            // Rules that aren't violated anymore
            var result = new List<Result>();
            var notViolatedRules = previousReport.results.Except(results).ToList();
            result.AddRange(notViolatedRules);
            
            // New rule violations aren't results that we can compare for now here
            var comparableResultList = results.Except(previousReport.results).ToList();
            
            // Add existing rules in both result lists, taking the one from the previous report list
            comparableResultList.AddRange(previousReport.results.Intersect(results).ToList());
            
            // Compare remaining rule violations
            result.AddRange(
                from previousReportResult in comparableResultList 
                let currentResultForGivenRule = results.First(i => i.rule.Equals(previousReportResult.rule)) 
                where currentResultForGivenRule.violationRatio < previousReportResult.violationRatio 
                select previousReportResult);

            return result;
        }
        
        public List<Result> GetViolationsComparedTo(TrainingReport previousReport)
        {
            if (id != previousReport.GetTrainingId())
            {
                throw new ArgumentException("Training IDs aren't matching");
            }
            
            // Rules that are violated now which weren't beforehand
            var result = new List<Result>();
            var newlyViolatedRules = results.Except(previousReport.results).ToList();
            result.AddRange(newlyViolatedRules);
            
            // Add existing rules in both result lists, taking the one from the previous report list
            var comparableResultList = previousReport.results.Intersect(results).ToList();

            // Compare remaining rule violations
            result.AddRange(
                from previousReportResult in comparableResultList 
                let currentResultForGivenRule = results.First(i => i.rule.Equals(previousReportResult.rule)) 
                where currentResultForGivenRule.violationRatio >= previousReportResult.violationRatio 
                select previousReportResult);

            return result;
        }

        public void AddRuleViolationCheckToReport(ViolatedRules report)
        {
            totalChecks += 1;
            foreach (var rule in report.violatedRules)
            {
                // Check if existing
                var existingResultId = results.FindIndex(i => i.rule.Equals(rule));
                if (existingResultId == -1)
                {
                    var newResult = new Result(rule, totalChecks);
                    newResult.RegisterCheck(true, totalChecks);
                    results.Add(newResult);
                }
                else
                {
                    results[existingResultId].RegisterCheck(true, totalChecks);
                }
            }
        }

        [CanBeNull]
        public Result GetFirstResultInTimeFrame(double maxTimeDifferenceInSeconds)
        {
            var list = OrderResults();
            var timeStampNow = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            list.RemoveAll(r => timeStampNow - r.lastCollected > (maxTimeDifferenceInSeconds*1000));

            try
            {
                return list[0];
            }
            catch
            {
                return null;
            }
        }
    }
}