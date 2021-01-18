using System;
using General.Exercises;
using General.Rules;

namespace General.Session
{
    public class Result
    {
        internal readonly Rule rule;
        internal float count;
        internal long lastCollected;

        public Result(Rule rule)
        {
            this.rule = rule;
        }

        public void Increment()
        {
            count += 1;
            UpdateTimestamp();
        }

        private void UpdateTimestamp()
        {
            lastCollected = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
        }

        public float GetCount()
        {
            return count;
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

        public void Add(float existingCount)
        {
            count += existingCount;
            UpdateTimestamp();
        }
    }
}