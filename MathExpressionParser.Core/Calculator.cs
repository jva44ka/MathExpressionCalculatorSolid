using MathExpression.Core;
using MathExpressionParser.Core.Interfaces;
using System;

namespace MathExpressionParser.Core
{
    // Внешний интерфейс для других проектов в решении
    public sealed class Calculator<T> : ICalculator<T>
        where T : struct, IConvertible
    {
        private ITreeBuilder<T> _treeBuilder;

        public Calculator(ITreeBuilder<T> treeBuilder)
        {
            _treeBuilder = treeBuilder;
        }

        public T CalculateExpression(string expression)
        {
            var tree = _treeBuilder.BuildTree(expression);
            var compiledTree = tree.Compile();
            return compiledTree.Invoke();
        }

        public static Calculator<T> GetDefaultInstance()
        {
            return new Calculator<T>(new TreeBuilder<T>());
        }
    }
}
