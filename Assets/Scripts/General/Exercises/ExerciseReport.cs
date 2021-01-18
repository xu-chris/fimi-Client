using System;
using System.Collections.Generic;
using System.Linq;
using General.Rules;
using General.Session;
using JetBrains.Annotations;

namespace General.Exercises
{
    public class ExerciseReport
    {
        private readonly Result[] results;
        private int skeletonId;

        public ExerciseReport(int skeletonId, IEnumerable<Rule> rules)
        {
            this.skeletonId = skeletonId;
            results = rules.Select(rule => new Result(rule)).ToArray();
        }

        public void Count(Rule rule)
        {
            results.First(i => i.rule == rule).Increment();
        }

        public Result[] Results()
        {
            return results;
        }

        public void Reset()
        {
            foreach (var result in results)
            {
                result.count = 0;
            }
        }

        public int GetSkeletonId()
        {
            return skeletonId;
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
        
        private List<Result> OrderResults()
        {
            return new List<Result>(results.
                OrderBy(i => i.rule.priority).
                ThenByDescending(i => i.lastCollected).
                ThenByDescending(i => i.count));
        }
    }
}