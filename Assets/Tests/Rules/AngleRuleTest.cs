using System.Collections.Generic;
using General.Rules;
using General.Skeleton;
using NUnit.Framework;
using UnityEngine;

namespace Tests.Rules
{
    public class AngleRuleTest : RuleTest
    {
        private const int expectedAngle = 90;
        private const int lowerTolerance = 20;
        private const int upperTolerance = 5;
        private readonly List<string> bones = new List<string> {"RightHand", "LeftHand"};
        private AngleRule rule;


        [SetUp]
        public void SetUp()
        {
            rule = new AngleRule
            {
                expectedAngle = expectedAngle,
                lowerTolerance = lowerTolerance,
                upperTolerance = upperTolerance,
                bones = bones
            };
        }

        [Test]
        public void ShouldReturnInvalidIfOutsideOfTolerance()
        {
            // GIVEN
            var bone1 = CreateDummyBone(Vector3.up);
            var bone2 = CreateDummyBone(Vector3.up);

            // WHEN
            var result = rule.IsInvalidated(new List<Bone> {bone1, bone2});

            // THEN
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldReturnValidIfExactExpectedAngle()
        {
            // GIVEN
            var bone1 = CreateDummyBone(Vector3.up);
            var bone2 = CreateDummyBone(Vector3.right);

            // WHEN
            var result = rule.IsInvalidated(new List<Bone> {bone1, bone2});

            // THEN
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldReturnInvalidIfOutsideOfLowerTolerance()
        {
            // GIVEN
            var bone1 = CreateDummyBone(Vector3.up);
            var bone2 = CreateRotatedDummyBone(expectedAngle - lowerTolerance - 1);

            // WHEN
            var result = rule.IsInvalidated(new List<Bone> {bone1, bone2});

            // THEN
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldReturnValidIfInsideOfLowerTolerance()
        {
            // GIVEN
            var bone1 = CreateDummyBone(Vector3.up);
            var bone2 = CreateRotatedDummyBone(expectedAngle - lowerTolerance + 1);

            // WHEN
            var result = rule.IsInvalidated(new List<Bone> {bone1, bone2});

            // THEN
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldReturnInvalidIfOutsideOfUpperTolerance()
        {
            // GIVEN
            var bone1 = CreateDummyBone(Vector3.up);
            var bone2 = CreateRotatedDummyBone(expectedAngle + upperTolerance + 1);
            
            // WHEN
            var result = rule.IsInvalidated(new List<Bone> {bone1, bone2});

            // THEN
            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldReturnValidIfInsideOfUpperTolerance()
        {
            // GIVEN
            var bone1 = CreateDummyBone(Vector3.up);
            var bone2 = CreateRotatedDummyBone(expectedAngle + upperTolerance - 1);

            // WHEN
            var result = rule.IsInvalidated(new List<Bone> {bone1, bone2});

            // THEN
            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldNotBeEqual()
        {
            // GIVEN
            var rule2 = new AngleRule()
            {
                expectedAngle = 23,
                lowerTolerance = lowerTolerance,
                upperTolerance = upperTolerance,
                bones = bones
            };

            // WHEN
            var result = rule.Equals(rule2);
            
            // THEN
            Assert.IsFalse(result);
        }
    }
}