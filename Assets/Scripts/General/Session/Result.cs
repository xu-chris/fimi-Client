using System;
using General.Rules;
using Newtonsoft.Json;

namespace General.Session
{
    [Serializable]
    public class Result
    {
        public readonly Rule rule;
        public ulong count;
        public long lastCollected;

        public Result(Rule rule)
        {
            this.rule = rule;
        }

        [JsonConstructor]
        public Result(Rule rule, ulong count, long lastCollected)
        {
            this.rule = rule;
            this.count = count;
            this.lastCollected = lastCollected;
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

        public void Add(ulong existingCount)
        {
            count += existingCount;
            UpdateTimestamp();
        }
    }
}