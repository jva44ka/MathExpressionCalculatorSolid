using MathExpression.Core;
using MathExpressionParser.Core;
using TParser = MathExpressionParser.Core.Parser;

namespace MathExpressionParser
{
    // Внешний интерфейс для других проектов в решении
    public class Calculator : ICalculator
    {
        private TParser _parser = new TParser();
        private TreeBuilder _treeBuilder = new TreeBuilder();

        public decimal CalculateExpression(string expression)
        {
            //return _parser.Evaluate(expression);
            //var compiledExp = _parser.GetExpression(expression).Compile();
            return _treeBuilder.Evaluate(expression);
            //return compiledExp.Invoke();
        }
    }
}
