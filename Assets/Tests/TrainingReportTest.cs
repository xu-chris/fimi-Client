using System;
using System.Collections.Generic;
using System.Numerics;
using General.Rules;
using General.Session;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]  
    public class TrainingReportTest
    {
        private TrainingReport trainingReport;
        
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
            trainingReport = new TrainingReport(0);
        }
        
        [Test]
        public void ShouldJustCountUpResultsForExistingRules()
        {
            SetUp();
            
            // GIVEN
            var result1 = trainingReport.GetResults().Length;
            
            // WHEN
            trainingReport.RegisterCheck(rule1, true);
            trainingReport.RegisterCheck(rule1, true);
            var result2 = trainingReport.GetResults().Length;
            var count = trainingReport.GetResults()[0].GetCount();
            
            // THEN
            Assert.AreEqual(result1 + 1, result2);
            Assert.AreEqual((BigInteger) 2, count);
        }

        [Test]
        public void ShouldAddNewResultsOnTheFly()
        {
            SetUp();
            
            // GIVEN
            var result1 = trainingReport.GetResults().Length;
            
            // WHEN
            trainingReport.RegisterCheck(rule1, true);
            var result2 = trainingReport.GetResults().Length;
            
            // THEN
            Assert.AreEqual(result1 + 1, result2);
        }

        [Test]
        public void ShouldReturnViolationForIncreasedResults()
        {
            SetUp();
            
            // GIVEN
            var previousReport = new TrainingReport(0);
            previousReport.RegisterCheck(rule1, true);
            
            trainingReport.RegisterCheck(rule1, true);
            trainingReport.RegisterCheck(rule1, true);
            
            // WHEN
            var result = trainingReport.GetViolationsComparedTo(previousReport);
            
            var expectedResultItem = new Result(rule1);
            expectedResultItem.RegisterCheck(true, 1);
            expectedResultItem.RegisterCheck(true, 2);
            var expected = new List<Result> {expectedResultItem};

            // THEN
            CollectionAssert.AreEqual(expected, result);
        }
        
        [Test]
        public void ShouldReturnNewViolationsAsViolations()
        {
            SetUp();
            
            // GIVEN
            var previousReport = new TrainingReport(0);
            trainingReport.RegisterCheck(rule1, true);
            trainingReport.RegisterCheck(rule1, true);

            // WHEN
            var result = trainingReport.GetViolationsComparedTo(previousReport);
            
            var expectedResultItem = new Result(rule1);
            expectedResultItem.RegisterCheck(true, 1);
            expectedResultItem.RegisterCheck(true, 2);
            var expected = new List<Result> {expectedResultItem};

            // THEN
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void ShouldNotReturnOldViolationsAsViolations()
        {
            SetUp();
            
            // GIVEN
            var previousReport = new TrainingReport(0);
            previousReport.RegisterCheck(rule1, true);
            
            // WHEN
            var result = trainingReport.GetViolationsComparedTo(previousReport);
            
            var expected = new List<Result>();

            // THEN
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void ShouldNotReturnBetterResultsAsViolations()
        {
            SetUp();
            
            // GIVEN
            var previousReport = new TrainingReport(0);
            previousReport.RegisterCheck(rule1, true);
            previousReport.RegisterCheck(rule1, true);
            
            trainingReport.RegisterCheck(rule1, true);
            trainingReport.RegisterCheck(rule1, false);
            
            // WHEN
            var result = trainingReport.GetViolationsComparedTo(previousReport);
            var expected = new List<Result>();

            // THEN
            CollectionAssert.AreEqual(expected, result);
        }
        
        [Test]
        public void ShouldReturnGivenRuleAsImprovement()
        {
            SetUp();
            
            // GIVEN
            var previousReport = new TrainingReport(0);
            previousReport.RegisterCheck(rule1, true);
            previousReport.RegisterCheck(rule1, true);
            
            trainingReport.RegisterCheck(rule1, true);
            trainingReport.RegisterCheck(rule1, false);
            
            // WHEN
            var result = trainingReport.GetImprovementsComparedTo(previousReport);
            
            var expectedResultItem = new Result(rule1);
            expectedResultItem.RegisterCheck(true, 1);
            expectedResultItem.RegisterCheck(true, 2);
            var expected = new List<Result> {expectedResultItem};

            // THEN
            CollectionAssert.AreEqual(expected, result);
        }
        
        [Test]
        public void ShouldReturnNonExistingViolationsAsImprovements()
        {
            SetUp();
            
            // GIVEN
            var previousReport = new TrainingReport(0);
            previousReport.RegisterCheck(rule1, true);
            previousReport.RegisterCheck(rule1, true);

            // WHEN
            var result = trainingReport.GetImprovementsComparedTo(previousReport);
            
            var expectedResultItem = new Result(rule1);
            expectedResultItem.RegisterCheck(true, 1);
            expectedResultItem.RegisterCheck(true, 2);
            var expected = new List<Result> {expectedResultItem};

            // THEN
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void ShouldNotReturnNewViolationsAsImprovements()
        {
            SetUp();
            
            // GIVEN
            var previousReport = new TrainingReport(0);
            trainingReport.RegisterCheck(rule1, true);
            
            // WHEN
            var result = trainingReport.GetImprovementsComparedTo(previousReport);
            
            var expected = new List<Result>();

            // THEN
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void ShouldNotReturnWorseResultsAsImprovements()
        {
            SetUp();
            
            // GIVEN
            var previousReport = new TrainingReport(0);
            previousReport.RegisterCheck(rule1, true);
            
            trainingReport.RegisterCheck(rule1, true);
            trainingReport.RegisterCheck(rule1, true);
            
            // WHEN
            var result = trainingReport.GetImprovementsComparedTo(previousReport);
            var expected = new List<Result>();

            // THEN
            CollectionAssert.AreEqual(expected, result);
        }

        [Test]
        public void ShouldExpectErrorWhenGettingImprovementsOfTwoReportsForDifferentTrainings()
        {
            // GIVEN
            var previousReport = new TrainingReport(1);
            
            // WHEN
            // THEN
            
            Assert.Throws<ArgumentException>(() => trainingReport.GetImprovementsComparedTo(previousReport));
        }
        
        [Test]
        public void ShouldExpectErrorWhenGettingViolationsOfTwoReportsForDifferentTrainings()
        {
            // GIVEN
            var previousReport = new TrainingReport(1);
            
            // WHEN
            // THEN
            
            Assert.Throws<ArgumentException>(() => trainingReport.GetViolationsComparedTo(previousReport));
        }
        
        [Test]
        public void ShouldPrioritizeIfBothHaveNotNullValue()
        {
            // GIVEN
            var violatedRules = new ViolatedRules(0);
            violatedRules.RegisterViolation(rule1);
            violatedRules.RegisterViolation(rule2);
            trainingReport.AddRuleViolationCheckToReport(violatedRules);
            var result1 = new Result(rule1);
            var result2 = new Result(rule2);
            result2.RegisterCheck(true, 1);
            result2.RegisterCheck(true, 1);
            var expectedResult = new [] {
                result1,
                result2
            };
            
            // WHEN
            var result = trainingReport.GetResults();
            
            Assert.AreEqual(expectedResult[0].GetType(), result[0].GetType());
            Assert.AreEqual(expectedResult[1].GetType(), result[1].GetType());
        }

        [Test]
        public void ShouldPrioritizeNoneIfOnlyValuesDiffer()
        {
            // GIVEN
            var violatedRules = new ViolatedRules(0);
            violatedRules.RegisterViolation(rule3);
            violatedRules.RegisterViolation(rule3);
            violatedRules.RegisterViolation(rule2);
            trainingReport.AddRuleViolationCheckToReport(violatedRules);
            var result2 = new Result(rule2);
            var result3 = new Result(rule3);
            result3.RegisterCheck(true, 1);
            result3.RegisterCheck(true, 1);
            var expectedResult = new []{
                result3,
                result2
            };
            
            // WHEN
            var result = trainingReport.GetResults();
            
            Assert.AreEqual(expectedResult[0].GetType(), result[0].GetType());
            Assert.AreEqual(expectedResult[1].GetType(), result[1].GetType());
        }

        [Test]
        public void ShouldReturnResultInsideMaxTimeDifference()
        {
            // GIVEN
            var violatedRules = new ViolatedRules(0);
            violatedRules.RegisterViolation(rule1);
            trainingReport.AddRuleViolationCheckToReport(violatedRules);
            var expected = new Result(rule1);
            expected.RegisterCheck(true, 1);
            System.Threading.Thread.Sleep(2);

            // WHEN
            var result = trainingReport.GetFirstResultInTimeFrame(0.002);

            // THEN
            Assert.AreEqual(expected, result);
        }
        
        
        [Test]
        public void ShouldNotReturnResultOutsideMaxTimeDifference()
        {
            // GIVEN
            var violatedRules = new ViolatedRules(0);
            violatedRules.RegisterViolation(rule1);
            trainingReport.AddRuleViolationCheckToReport(violatedRules);
            System.Threading.Thread.Sleep(2);

            // WHEN
            var result = trainingReport.GetFirstResultInTimeFrame(0.001);

            // THEN
            Assert.AreEqual(null, result);
        }

    }
}