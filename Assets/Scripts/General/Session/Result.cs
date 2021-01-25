using System;
using System.Numerics;
using General.Rules;
using Newtonsoft.Json;

namespace General.Session
{
    [Serializable]
    public class Result
    {
        public readonly Rule rule;
        private BigInteger countViolated = 0;
        private BigInteger countChecked = 0;
        public long lastCollected;
        public float violationRatio;

        public Result(Rule rule)
        {
            this.rule = rule;
        }

        [JsonConstructor]
        public Result(Rule rule, float violationRatio, long lastCollected)
        {
            this.rule = rule;
            this.violationRatio = violationRatio;
            this.lastCollected = lastCollected;
        }

        private void IncrementViolation()
        {
            countViolated += 1;
            UpdateTimestamp();
        }

        private void IncrementChecks()
        {
            countChecked += 1;
        }

        public void RegisterCheck(bool violated)
        {
            IncrementChecks();
            if (violated) IncrementViolation();
            SetViolationRatio();
        }

        private void SetViolationRatio()
        {
            violationRatio = (float) BigInteger.Divide(countViolated, countChecked);
        }

        private void UpdateTimestamp()
        {
            lastCollected = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
        }

        public BigInteger GetCount()
        {
            return countViolated;
        }

        public float GetViolationRatio()
        {
            return violationRatio;
        }

        public long GetLastChangedTimestamp()
        {
            return lastCollected;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Result);
        }

        private bool Equals(Result other)
        {
            return other != null && Equals(rule, other.rule);
        }

        public override int GetHashCode()
        {
            return (rule != null ? rule.GetHashCode() : 0);
        }

        public void Add(BigInteger existingCountViolated, BigInteger existingCountChecks)
        {
            countChecked += existingCountChecks;
            countViolated += existingCountViolated;
            UpdateTimestamp();
        }

        public void Reset()
        {
            countViolated = 0;
        }

        public BigInteger GetChecks()
        {
            return countChecked;
        }
    }
}