using System.Collections.Generic;
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
            trainingReport.Count(rule1);
            trainingReport.Count(rule1);
            var result2 = trainingReport.GetResults().Length;
            var count = trainingReport.GetResults()[0].GetCount();
            
            // THEN
            Assert.AreEqual(result1 + 1, result2);
            Assert.AreEqual(2f, count);
        }

        [Test]
        public void ShouldAddNewResultsOnTheFly()
        {
            SetUp();
            
            // GIVEN
            var result1 = trainingReport.GetResults().Length;
            
            // WHEN
            trainingReport.Count(rule1);
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
            previousReport.Count(rule1);
            
            trainingReport.Count(rule1);
            trainingReport.Count(rule1);
            
            // WHEN
            var result = trainingReport.GetViolationsComparedTo(previousReport);
            
            var expectedResultItem = new Result(rule1);
            expectedResultItem.Increment();
            expectedResultItem.Increment();
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
            trainingReport.Count(rule1);
            trainingReport.Count(rule1);

            // WHEN
            var result = trainingReport.GetViolationsComparedTo(previousReport);
            
            var expectedResultItem = new Result(rule1);
            expectedResultItem.Increment();
            expectedResultItem.Increment();
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
            previousReport.Count(rule1);
            
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
            previousReport.Count(rule1);
            previousReport.Count(rule1);
            
            trainingReport.Count(rule1);
            
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
            previousReport.Count(rule1);
            previousReport.Count(rule1);
            
            trainingReport.Count(rule1);
            
            // WHEN
            var result = trainingReport.GetImprovementsComparedTo(previousReport);
            
            var expectedResultItem = new Result(rule1);
            expectedResultItem.Increment();
            expectedResultItem.Increment();
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
            previousReport.Count(rule1);
            previousReport.Count(rule1);

            // WHEN
            var result = trainingReport.GetImprovementsComparedTo(previousReport);
            
            var expectedResultItem = new Result(rule1);
            expectedResultItem.Increment();
            expectedResultItem.Increment();
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
            trainingReport.Count(rule1);
            
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
            previousReport.Count(rule1);
            
            trainingReport.Count(rule1);
            trainingReport.Count(rule1);
            
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
            
            Assert.Throws<AssertionException>(() => trainingReport.GetImprovementsComparedTo(previousReport));
        }
        
        [Test]
        public void ShouldExpectErrorWhenGettingViolationsOfTwoReportsForDifferentTrainings()
        {
            // GIVEN
            var previousReport = new TrainingReport(1);
            
            // WHEN
            // THEN
            
            Assert.Throws<AssertionException>(() => trainingReport.GetViolationsComparedTo(previousReport));
        }
    }
}