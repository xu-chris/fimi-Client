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
        private ulong countViolated = 0;
        private ulong countChecked = 0;
        public long lastCollected;
        public double violationRatio;

        public Result(Rule rule, ulong countChecked = default)
        {
            this.rule = rule;
            this.countChecked = countChecked;
        }

        [JsonConstructor]
        public Result(Rule rule, double violationRatio, long lastCollected)
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

        public void RegisterCheck(bool violated, ulong totalChecks)
        {
            countChecked = totalChecks;
            if (violated) IncrementViolation();
            SetViolationRatio();
        }

        private void SetViolationRatio()
        {
            violationRatio = (double) countViolated / countChecked;
        }

        private void UpdateTimestamp()
        {
            lastCollected = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
        }

        public BigInteger GetCount()
        {
            return countViolated;
        }

        public double GetViolationRatio()
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