using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MathExpressionParser.Core
{
    internal sealed class TreeBuilder
    {
        private readonly Stack<Expression> expressionStack = new Stack<Expression>();
        private readonly Stack<char> operatorStack = new Stack<char>();

        public decimal Evaluate(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
            {
                return 0;
            }

            operatorStack.Clear();
            expressionStack.Clear();

            using (var reader = new StringReader(expression))
            {
                int peek;
                while ((peek = reader.Peek()) > -1)
                {
                    var next = (char)peek;

                    if (char.IsDigit(next))
                    {
                        expressionStack.Push(ReadOperand(reader));
                        continue;
                    }

                    if (Operation.IsDefined(next))
                    {
                        var currentOperation = ReadOperation(reader);

                        EvaluateWhile(() => operatorStack.Count > 0 && operatorStack.Peek() != '(' &&
                                            currentOperation.Precedence <= ((Operation)operatorStack.Peek()).Precedence);

                        operatorStack.Push(next);
                        continue;
                    }

                    if (next == '(')
                    {
                        reader.Read();
                        operatorStack.Push('(');
                        continue;
                    }

                    if (next == ')')
                    {
                        reader.Read();
                        EvaluateWhile(() => operatorStack.Count > 0 && operatorStack.Peek() != '(');
                        operatorStack.Pop();
                        continue;
                    }

                    if (next != ' ')
                    {
                        throw new ArgumentException(string.Format("Encountered invalid character {0}", next), "expression");
                    }
                }
            }

            EvaluateWhile(() => operatorStack.Count > 0);

            var compiled = Expression.Lambda<Func<decimal>>(expressionStack.Pop()).Compile();
            return compiled();
        }

        private Expression ReadOperand(TextReader reader)
        {
            var operand = string.Empty;

            int peek;

            while ((peek = reader.Peek()) > -1)
            {
                var next = (char)peek;

                if (char.IsDigit(next) || next == '.')
                {
                    reader.Read();
                    operand += next;
                }
                else
                {
                    break;
                }
            }

            return Expression.Constant(decimal.Parse(operand));
        }

        private Operation ReadOperation(TextReader reader)
        {
            var operation = (char)reader.Read();
            return (Operation)operation;
        }

        private void EvaluateWhile(Func<bool> condition)
        {
            while (condition())
            {
                var right = expressionStack.Pop();
                var left = expressionStack.Pop();

                expressionStack.Push(((Operation)operatorStack.Pop()).Apply(left, right));
            }
        }
    }
}
