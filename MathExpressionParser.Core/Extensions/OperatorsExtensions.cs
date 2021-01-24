using MathExpressionParser.Core.Enums;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MathExpressionParser.Core.Extensions
{
    internal static class OperatorsExtensions
    {
        private static char[] _operators = "+,-*/".ToCharArray();

        internal static bool IsOperator(char op)
        {
            return _operators.Any(x => x == op);
        }

        internal static char ToChat(this Operators op)
        {
            switch (op)
            {
                case Operators.Plus:
                    return '+';
                case Operators.Minus:
                    return '-';
                case Operators.Multiply:
                    return '*';
                case Operators.Divide:
                    return '/';
                default:
                    throw new ArgumentException("Unknown operator: " + op);
            }
        }

        internal static Operators FromChat(char opChar)
        {
            switch (opChar)
            {
                case '+':
                    return Operators.Plus;
                case '-':
                    return Operators.Minus;
                case '*':
                    return Operators.Multiply;
                case '/':
                    return Operators.Divide;
                default:
                    throw new ArgumentException("Unknown operator: " + opChar);
            }
        }

        internal static Expression GetExpressionByOperator(this Operators op, Expression arg1, Expression arg2)
        {
            switch (op)
            {
                case Operators.Plus:
                    return Expression.Add(arg1, arg2);
                case Operators.Minus:
                    return Expression.Subtract(arg1, arg2);
                case Operators.Multiply:
                    return Expression.Multiply(arg1, arg2);
                case Operators.Divide:
                    return Expression.Divide(arg1, arg2);
                default:
                    throw new ArgumentException("Unknown operator: " + op);
            }
        }
    }
}
