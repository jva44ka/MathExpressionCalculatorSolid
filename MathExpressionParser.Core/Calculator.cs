using MathExpression.Core;
using MathExpressionParser.Core;

namespace MathExpressionParser
{
    // Внешний интерфейс для других проектов в решении
    public class Calculator : ICalculator
    {
        private TreeBuilder<decimal> _treeBuilder = new TreeBuilder<decimal>();

        public decimal CalculateExpression(string expression)
        {
            //return _parser.Evaluate(expression);
            //var compiledExp = _parser.GetExpression(expression).Compile();
            return _treeBuilder.BuildTree(expression).Compile()();
            //return compiledExp.Invoke();
        }
    }
}
