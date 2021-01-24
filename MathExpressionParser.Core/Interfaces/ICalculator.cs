using System;

namespace MathExpression.Core
{
    /// <summary>
    /// Калькулятор, позволяющий просчитать мат. выражение по строке
    /// </summary>
    /// <typeparam name="T">Тип, к которому будут каститься числа при обнаружении</typeparam>
    public interface ICalculator<T> where T : struct, IConvertible
    {
        public T CalculateExpression(string expression);
    }
}
