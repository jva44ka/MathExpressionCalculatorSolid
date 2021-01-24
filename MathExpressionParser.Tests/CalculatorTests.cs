using MathExpressionParser.Core;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using NUnit.Framework;

namespace MathExpressionParser.Tests
{
    public class Tests
    {
        private static readonly string[] _setForCalculateExpression =
        {
            "2 + 2 * 2",
            "3 - 2 + (2 + 5)",
            "3 - 7 * (2 + 5)",
            "3 * (2 + 5) / 3"
        };

        private readonly Calculator<decimal> _calculator = Calculator<decimal>.GetDefaultInstance();

        [SetUp]
        public void Setup()
        { }

        // positive tests
        [Test, TestCaseSource(nameof(_setForCalculateExpression))]
        public void CalculateExpressionPositive(string testExpression)
        {
            var parserExpressionRes = _calculator.CalculateExpression(testExpression);
            var rightExpressionRes = CSharpScript.EvaluateAsync<decimal>(testExpression).Result;
            Assert.AreEqual(parserExpressionRes, rightExpressionRes);
        }
    }
}