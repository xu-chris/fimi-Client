using System;
using System.Collections.Generic;
using System.Linq;
using General.Rules;
using Newtonsoft.Json;

namespace General.Session
{
    [Serializable]
    public class TrainingReport
    {
        public readonly List<Result> results;
        public readonly int id;

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
        
        public void Count(Rule rule)
        {
            try
            {
                results.First(i => i.rule.Equals(rule)).Increment();
            }
            catch (Exception e)
            {
                var result = new Result(rule);
                result.Increment();
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
                ThenByDescending(i => i.count));
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
                where currentResultForGivenRule.count < previousReportResult.count 
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
                where currentResultForGivenRule.count >= previousReportResult.count 
                select previousReportResult);

            return result;
        }

        public void AddToReport(ExerciseReport report)
        {
            foreach (var result in report.Results())
            {
                ImportResult(result);
            }
        }

        private void ImportResult(Result result)
        {
            // Check if existing
            var existingResultId = results.FindIndex(i => i.rule.Equals(result.rule));
            if (existingResultId == -1)
            {
                results.Add(result);
            }
            else
            {
                results[existingResultId].Add(result.count);
            }
        }
    }
}