using System;

namespace MathExpression.Core
{
    public interface ICalculator<T> where T : struct, IConvertible
    {
        public T CalculateExpression(string expression);
    }
}
