using System;
using System.Linq.Expressions;

namespace MathExpressionParser.Core.Interfaces
{
    /// <summary>
    /// Парсер-строитель дерева по спаршенному выражению
    /// </summary>
    /// <typeparam name="T">Тип, к которому будут каститься числа при обнаружении</typeparam>
    public interface ITreeBuilder<T> where T : struct, IConvertible
    {
        internal Expression<Func<T>> BuildTree(string expression);
    }
}
