using System;
using System.Linq.Expressions;

namespace MathExpressionParser.Core.Interfaces
{
    public interface ITreeBuilder<T> where T : struct, IConvertible
    {
        Expression<Func<T>> BuildTree(string expression);
    }
}
