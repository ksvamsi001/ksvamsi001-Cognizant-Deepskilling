using System;
using NUnit.Framework;
using CalcLibrary;

namespace CalcLibrary.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        private SimpleCalculator _calculator;

        [SetUp]
        public void SetUp()
        {
            _calculator = new SimpleCalculator();
        }

        [TearDown]
        public void TearDown()
        {
            _calculator?.AllClear();
            _calculator = null;
        }

        [Test]
        public void Addition_TwoPositiveNumbers_ReturnsCorrectSum()
        {
            double a = 5.5;
            double b = 3.2;
            double expected = 8.7;
            double actual = _calculator.Addition(a, b);
            Assert.That(actual, Is.EqualTo(expected).Within(0.001));
            Assert.That(_calculator.GetResult, Is.EqualTo(expected).Within(0.001));
        }

        [TestCase(1.0, 2.0, 3.0)]
        [TestCase(0.0, 0.0, 0.0)]
        [TestCase(-1.5, 1.5, 0.0)]
        [TestCase(-5.5, -3.2, -8.7)]
        [TestCase(100.25, 200.75, 301.0)]
        [TestCase(0.1, 0.2, 0.3)]
        public void Addition_VariousInputCombinations_ReturnsCorrectSum(double a, double b, double expected)
        {
            double actual = _calculator.Addition(a, b);
            Assert.That(actual, Is.EqualTo(expected).Within(0.001));
            Assert.That(_calculator.GetResult, Is.EqualTo(expected).Within(0.001));
        }

        [Test]
        public void Subtraction_TwoNumbers_ReturnsCorrectDifference()
        {
            double a = 10.5;
            double b = 4.2;
            double expected = 6.3;
            double actual = _calculator.Subtraction(a, b);
            Assert.That(actual, Is.EqualTo(expected).Within(0.001));
            Assert.That(_calculator.GetResult, Is.EqualTo(expected).Within(0.001));
        }

        [TestCase(10.0, 3.0, 7.0)]
        [TestCase(0.0, 0.0, 0.0)]
        [TestCase(-5.5, -2.2, -3.3)]
        [TestCase(5.0, 10.0, -5.0)]
        [TestCase(100.75, 50.25, 50.5)]
        public void Subtraction_VariousInputs_ReturnsCorrectDifference(double a, double b, double expected)
        {
            double actual = _calculator.Subtraction(a, b);
            Assert.That(actual, Is.EqualTo(expected).Within(0.001));
            Assert.That(_calculator.GetResult, Is.EqualTo(expected).Within(0.001));
        }

        [Test]
        public void Multiplication_TwoNumbers_ReturnsCorrectProduct()
        {
            double a = 4.5;
            double b = 2.0;
            double expected = 9.0;
            double actual = _calculator.Multiplication(a, b);
            Assert.That(actual, Is.EqualTo(expected).Within(0.001));
            Assert.That(_calculator.GetResult, Is.EqualTo(expected).Within(0.001));
        }

        [TestCase(2.5, 3.0, 7.5)]
        [TestCase(0.0, 5.0, 0.0)]
        [TestCase(-2.5, 3.0, -7.5)]
        [TestCase(-2.0, -3.0, 6.0)]
        [TestCase(1.0, 100.0, 100.0)]
        [TestCase(0.5, 0.5, 0.25)]
        public void Multiplication_VariousInputs_ReturnsCorrectProduct(double a, double b, double expected)
        {
            double actual = _calculator.Multiplication(a, b);
            Assert.That(actual, Is.EqualTo(expected).Within(0.001));
            Assert.That(_calculator.GetResult, Is.EqualTo(expected).Within(0.001));
        }

        [Test]
        public void Division_TwoValidNumbers_ReturnsCorrectQuotient()
        {
            double a = 10.0;
            double b = 2.0;
            double expected = 5.0;
            double actual = _calculator.Division(a, b);
            Assert.That(actual, Is.EqualTo(expected).Within(0.001));
            Assert.That(_calculator.GetResult, Is.EqualTo(expected).Within(0.001));
        }

        [TestCase(10.0, 3.0, 3.333)]
        [TestCase(15.0, 3.0, 5.0)]
        [TestCase(-10.0, 2.0, -5.0)]
        [TestCase(10.0, -2.0, -5.0)]
        [TestCase(7.5, 2.5, 3.0)]
        [TestCase(0.0, 5.0, 0.0)]
        public void Division_VariousInputs_ReturnsCorrectQuotient(double a, double b, double expected)
        {
            double actual = _calculator.Division(a, b);
            Assert.That(actual, Is.EqualTo(expected).Within(0.001));
            Assert.That(_calculator.GetResult, Is.EqualTo(expected).Within(0.001));
        }

        [Test]
        public void Division_ByZero_ThrowsArgumentException()
        {
            double a = 10.0;
            double b = 0.0;
            var exception = Assert.Throws<ArgumentException>(() => _calculator.Division(a, b));
            Assert.That(exception.Message, Is.EqualTo("Second Parameter Can't be Zero"));
        }

        [TestCase(0.0)]
        [TestCase(-0.0)]
        public void Division_ByZeroVariations_ThrowsArgumentException(double divisor)
        {
            double dividend = 5.0;
            Assert.Throws<ArgumentException>(() => _calculator.Division(dividend, divisor));
        }

        [Test]
        public void GetResult_AfterCalculation_ReturnsLastResult()
        {
            _calculator.Addition(5.0, 3.0);
            double additionResult = _calculator.GetResult;
            _calculator.Multiplication(4.0, 2.0);
            double multiplicationResult = _calculator.GetResult;
            Assert.That(additionResult, Is.EqualTo(8.0).Within(0.001));
            Assert.That(multiplicationResult, Is.EqualTo(8.0).Within(0.001));
        }

        [Test]
        public void AllClear_AfterCalculations_ResetsResultToZero()
        {
            _calculator.Addition(10.0, 5.0);
            double beforeClear = _calculator.GetResult;
            _calculator.AllClear();
            double afterClear = _calculator.GetResult;
            Assert.That(beforeClear, Is.EqualTo(15.0).Within(0.001));
            Assert.That(afterClear, Is.EqualTo(0.0).Within(0.001));
        }

        [Test]
        public void GetResult_InitialState_ReturnsZero()
        {
            double initialResult = _calculator.GetResult;
            Assert.That(initialResult, Is.EqualTo(0.0).Within(0.001));
        }

        [Test]
        public void SequentialOperations_UpdatesResultCorrectly()
        {
            _calculator.Addition(10.0, 5.0);
            Assert.That(_calculator.GetResult, Is.EqualTo(15.0).Within(0.001));
            _calculator.Subtraction(20.0, 8.0);
            Assert.That(_calculator.GetResult, Is.EqualTo(12.0).Within(0.001));
            _calculator.Multiplication(3.0, 4.0);
            Assert.That(_calculator.GetResult, Is.EqualTo(12.0).Within(0.001));
            _calculator.Division(24.0, 6.0);
            Assert.That(_calculator.GetResult, Is.EqualTo(4.0).Within(0.001));
        }
    }
}