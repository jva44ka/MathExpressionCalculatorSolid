using MathExpressionParser.Core;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace MathExpressionParser.Tests
{
    public class TreeBuilderTests
    {
        #region reflectedMembers
        private Type _treeBuilderType;
        private MethodInfo _buildTreeMethod;
        private MethodInfo _readOperandMethod;
        private MethodInfo _readOperationMethod;
        private MethodInfo _removeSpacesMethod;
        #endregion

        private TreeBuilder<decimal> _treeBuilderInstance;


        [SetUp]
        public void Setup()
        {
            _treeBuilderType = typeof(TreeBuilder<>);
            Type[] typeArgs = { typeof(decimal) };
            Type constructed = _treeBuilderType.MakeGenericType(typeArgs);
            _treeBuilderInstance = (TreeBuilder<decimal>)Activator.CreateInstance(constructed);

            _buildTreeMethod = constructed.GetMethod("BuildTree",
                BindingFlags.Public | BindingFlags.Instance);
            _readOperandMethod = constructed.GetMethod("ReadOperand",
                BindingFlags.NonPublic | BindingFlags.Instance);
            _readOperationMethod = constructed.GetMethod("ReadOperation",
                BindingFlags.NonPublic | BindingFlags.Instance);
            _removeSpacesMethod = constructed.GetMethod("RemoveSpaces", 
                BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [TestCase("2 + 5", new int[] { 2, 5 })]
        [TestCase("10 + 15", new int[] { 10, 15 })]
        [TestCase("255 + 1", new int[] { 255, 1 })]
        public void BuildSimpleBinaryTreeTest(string testExp, int[] testNums)
        {
            var realResult = (Expression<Func<decimal>>)_buildTreeMethod.Invoke(_treeBuilderInstance, new object[] { testExp });
            var leftSubTree = (ConstantExpression)((BinaryExpression)realResult.Body).Left;
            var rightSubTree = (ConstantExpression)((BinaryExpression)realResult.Body).Right;

            Assert.AreEqual(leftSubTree.Value, testNums[0]);
            Assert.AreEqual(rightSubTree.Value, testNums[1]);
        }

        [TestCase("5")]
        [TestCase("42")]
        [TestCase("388")]
        [TestCase("25")]
        public void ReadOperandTest(string testSet)
        {
            using (var reader = new StringReader(testSet))
            {
                var realResult = (ConstantExpression)_readOperandMethod.Invoke(_treeBuilderInstance, new object[] { reader });
                var expectedResult = Expression.Constant(decimal.Parse(testSet));
                Assert.AreEqual(realResult.NodeType, expectedResult.NodeType);
                Assert.AreEqual(realResult.Value, expectedResult.Value);
            }
        }

        #region ReadOperationTest
        [Test]
        public void ReadOperationAdditionTest()
        {
            using (var reader = new StringReader("+"))
            {
                var realResult = _readOperationMethod.Invoke(_treeBuilderInstance, new object[] { reader });
                Assert.AreEqual(realResult, Operation.Addition);
            }
        }

        [Test]
        public void ReadOperationSubstractTest()
        {
            using (var reader = new StringReader("-"))
            {
                var realResult = _readOperationMethod.Invoke(_treeBuilderInstance, new object[] { reader });
                Assert.AreEqual(realResult, Operation.Subtraction);
            }
        }

        [Test]
        public void ReadOperationMultiplyTest()
        {
            using (var reader = new StringReader("*"))
            {
                var realResult = _readOperationMethod.Invoke(_treeBuilderInstance, new object[] { reader });
                Assert.AreEqual(realResult, Operation.Multiplication);
            }
        }

        [Test]
        public void ReadOperationDivideTest()
        {
            using (var reader = new StringReader("/"))
            {
                var realResult = _readOperationMethod.Invoke(_treeBuilderInstance, new object[] { reader });
                Assert.AreEqual(realResult, Operation.Division);
            }
        }
        #endregion

        [TestCase("5 + 5  * 5")]
        [TestCase("5 +5 * 5 ")]
        [TestCase("5   +     5 *      5")]
        public void RemoveSpacesTest(string testSet)
        {
            var realResult = _removeSpacesMethod.Invoke(_treeBuilderInstance, new object[] { testSet });
            var expectedResult = "5+5*5";
            Assert.AreEqual(realResult, expectedResult);
        }
    }
}
