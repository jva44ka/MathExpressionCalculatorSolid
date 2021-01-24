using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MathExpressionParser.Core
{
    internal sealed class Operation
    {
        private readonly int _precedence;
        private readonly string _name;
        private readonly Func<Expression, Expression, Expression> _operation;

        public static readonly Operation Addition = new Operation(1, Expression.Add, "Addition");
        public static readonly Operation Subtraction = new Operation(1, Expression.Subtract, "Subtraction");
        public static readonly Operation Multiplication = new Operation(2, Expression.Multiply, "Multiplication");
        public static readonly Operation Division = new Operation(2, Expression.Divide, "Division");

        private static readonly Dictionary<char, Operation> Operations = new Dictionary<char, Operation>
        {
            { '+', Addition },
            { '-', Subtraction },
            { '*', Multiplication},
            { '/', Division }
        };

        public int Precedence => _precedence;

        private Operation(int precedence, Func<Expression, Expression, Expression> operation, string name)
        {
            _precedence = precedence;
            _operation = operation;
            _name = name;
        }


        public static explicit operator Operation(char operation)
        {
            Operation result;

            if (Operations.TryGetValue(operation, out result))
            {
                return result;
            }
            else
            {
                throw new InvalidCastException();
            }
        }

        public Expression Apply(Expression left, Expression right)
        {
            return _operation(left, right);
        }

        public static bool IsDefined(char operation)
        {
            return Operations.ContainsKey(operation);
        }
    }
}
