using System;
using Chameleon.ValuePackage;

namespace Chameleon.ExpressionPackage
{
    public class OperatorExpression : Expression
    {
        private Expression left, right;
        private string op;

        public OperatorExpression(Expression left, string op, Expression right)
        {
            this.left = left;
            this.right = right;
            this.op = op;
        }

        public Value Evaluate()
        {
            Value leftVal = left.Evaluate();
            Value rightVal = right.Evaluate();

            switch (op)
            {
                case "==":
                    if (leftVal is NumberValue && rightVal is NumberValue)
                    {
                        return new NumberValue((leftVal.ToNumber() == rightVal.ToNumber()) ? 1 : 0);
                    }
                    else
                    {
                        return new NumberValue(leftVal.ToString().Equals(rightVal.ToString()) ? 1 : 0);
                    }
                case "+":
                    if (leftVal is NumberValue && rightVal is NumberValue)
                    {
                        return new NumberValue(leftVal.ToNumber() + rightVal.ToNumber());
                    }
                    else
                    {
                        return new StringValue(leftVal.ToString() + rightVal.ToString());
                    }
                case "-":
                    return new NumberValue(leftVal.ToNumber() - rightVal.ToNumber());
                case "*":
                    return new NumberValue(leftVal.ToNumber() * rightVal.ToNumber());
                case "/":
                    return new NumberValue(leftVal.ToNumber() / rightVal.ToNumber());
                case "%":
                    return new NumberValue(leftVal.ToNumber() % rightVal.ToNumber());
                case "^":
                    return new NumberValue(Math.Pow(leftVal.ToNumber(), rightVal.ToNumber()));
                case "++":
                    return new NumberValue(leftVal.ToNumber() + rightVal.ToNumber());
                case "--":
                    return new NumberValue(leftVal.ToNumber() - rightVal.ToNumber());
                case "<":
                    return new NumberValue((leftVal.ToNumber() < rightVal.ToNumber()) ? 1 : 0);
                case ">":
                    return new NumberValue((leftVal.ToNumber() > rightVal.ToNumber()) ? 1 : 0);
                case "=<":
                    return new NumberValue((leftVal.ToNumber() <= rightVal.ToNumber()) ? 1 : 0);
                case "=>":
                    return new NumberValue((leftVal.ToNumber() >= rightVal.ToNumber()) ? 1 : 0);
                case "=!":
                    if (leftVal is NumberValue && rightVal is NumberValue)
                        return new NumberValue((leftVal.ToNumber() != rightVal.ToNumber()) ? 1 : 0);
                    else
                        return new NumberValue(leftVal.ToString().Equals(rightVal.ToNumber()) ? 0 : 1);
            }
            throw new Exception("Unknown operator : " + op);
        }
    }
}
