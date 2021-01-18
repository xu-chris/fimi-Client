using System.Collections.Generic;
using General.Rules;
using General.Session;
using NUnit.Framework;

namespace Tests
{
    public class ResultTest
    {
        
        private readonly AngleRule rule1 = new AngleRule{
            priority = 1,
            bones = new List<string>{"a", "b"},
            colorize = false,
            expectedAngle = 90,
            lowerTolerance = 10,
            upperTolerance = 10,
            notificationText = "b"
        };
        
        [Test]
        public void ShouldChangeTimeStampsWhenCounting()
        {
            // GIVEN
            var result1 = new Result(rule1);
            result1.Increment();

            // WHEN
            var firstTimeStamp = result1.GetLastChangedTimestamp();
            // Wait for a given millisecond to force the timestamps to differ 
            System.Threading.Thread.Sleep(1);
            result1.Increment();
            
            // THEN
            Assert.AreNotEqual(firstTimeStamp, result1.GetLastChangedTimestamp());
        }
        
        [Test]
        public void ShouldUnequalResultsWhenRulesDiffer()
        {
            // GIVEN
            var rule2 = new AngleRule{
                priority = 1,
                bones = new List<string>{"a", "b"},
                colorize = false,
                expectedAngle = 45,
                lowerTolerance = 10,
                upperTolerance = 10,
                notificationText = "b"
            };
            
            var result1 = new Result(rule1);
            var result2 = new Result(rule2);

            // WHEN
            var result = result1.Equals(result2);
            
            // THEN
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldEqualResultsEvenWhenTimestampsDiffer()
        {
            // GIVEN
            var result1 = new Result(rule1);
            result1.Increment();
            // Wait for a given millisecond to force the timestamps to differ 
            System.Threading.Thread.Sleep(1);
            var result2 = new Result(rule1);
            result2.Increment();

            // WHEN
            var result = result1.Equals(result2);
            
            // THEN
            Assert.IsTrue(result);
            Assert.AreNotEqual(result1.GetLastChangedTimestamp(), result2.GetLastChangedTimestamp());
        }
        
        [Test]
        public void ShouldEqualResultsEvenWhenIteratorsDiffer()
        {
            // GIVEN
            var result1 = new Result(rule1);
            result1.Increment();
            // Wait for a given millisecond to force the timestamps to differ 
            var result2 = new Result(rule1);
            result2.Increment();
            result2.Increment();

            // WHEN
            var result = result1.Equals(result2);
            
            // THEN
            Assert.IsTrue(result);
            Assert.AreNotEqual(result1.GetCount(), result2.GetCount());
        }
    }
}