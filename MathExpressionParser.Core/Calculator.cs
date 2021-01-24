using MathExpression.Core;
using MathExpressionParser.Core.Interfaces;
using System;

namespace MathExpressionParser.Core
{
    /// <summary>
    /// Калькулятор, позволяющий просчитать мат. выражение по строке
    /// </summary>
    /// <typeparam name="T">Тип, к которому будут каститься числа при обнаружении</typeparam>
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

        /// <summary>
        /// Возвращает инстанс Calculator с дефолтными зивисимостями
        /// </summary>
        /// <returns></returns>
        public static Calculator<T> GetDefaultInstance()
        {
            return new Calculator<T>(new TreeBuilder<T>());
        }
    }
}
