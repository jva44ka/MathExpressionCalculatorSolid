using System;

namespace MathExpression.Core
{
    public interface ICalculator
    {
        public decimal CalculateExpression(string expression);
    }
}
