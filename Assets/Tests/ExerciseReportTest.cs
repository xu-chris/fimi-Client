using System.Collections.Generic;
using General.Exercises;
using General.Rules;
using General.Session;
using NUnit.Framework;

namespace Tests
{
    public class ExerciseReportTest
    {
        private ViolatedRules violatedRules;

        private readonly AngleRule rule1 = new AngleRule{
            priority = 1,
            bones = new List<string>{"a", "b"},
            colorize = false,
            expectedAngle = 90,
            lowerTolerance = 10,
            upperTolerance = 10,
            notificationText = "b"
        };

        private readonly LinearityRule rule2 = new LinearityRule {
            bones = new List<string>{"a", "b"},
            colorize = false,
            notificationText = "b",
            priority = 2,
            tolerance = 10
        };
        
        private readonly AngleRule rule3 = new AngleRule{
            priority = 2,
            bones = new List<string>{"a", "b"},
            colorize = false,
            expectedAngle = 90,
            lowerTolerance = 10,
            upperTolerance = 10,
            notificationText = "b"
        };

        [SetUp]
        public void SetUp()
        {
            var rulesArray = new Rule[]{rule1, rule2};
            violatedRules = new ViolatedRules(0, rulesArray);
        }
    }
}