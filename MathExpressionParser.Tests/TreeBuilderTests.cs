using MathExpressionParser.Core;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MathExpressionParser.Tests
{
    public class TreeBuilderTests
    {
        #region reflectedMembers
        private Type _treeBuilderType;
        private TypeInfo _treeBuilderTypeInfo;
        private MethodInfo _removeSpacesMethod;
        #endregion

        private TreeBuilder<decimal> _treeBuilderInstance = new TreeBuilder<decimal>();
        private object _treeBuilderInstanceConstructed;


        [SetUp]
        public void Setup()
        {
            _treeBuilderType = typeof(TreeBuilder<>);
            _treeBuilderTypeInfo = _treeBuilderType.GetTypeInfo();
            Type[] typeArgs = { typeof(decimal) };
            Type constructed = _treeBuilderType.MakeGenericType(typeArgs);
            _treeBuilderInstanceConstructed = Activator.CreateInstance(constructed);

            _removeSpacesMethod = constructed.GetMethod("RemoveSpaces", 
                BindingFlags.NonPublic | BindingFlags.Instance);
        }

        [Test]
        public void RemoveSpacesMethod()
        {
            var realResult = _removeSpacesMethod.Invoke(_treeBuilderInstanceConstructed, new object[] { "5 + 5  * 5" });
            var expectedResult = "5+5*5";
            Assert.AreEqual(realResult, expectedResult);
        }
    }
}
