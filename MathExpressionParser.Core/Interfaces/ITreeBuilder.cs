using System;
using System.Linq.Expressions;

namespace MathExpressionParser.Core.Interfaces
{
    interface ITreeBuilder<T> where T : struct, IConvertible
    {
        Expression<Func<T>> BuildTree(string expression);
    }
}
