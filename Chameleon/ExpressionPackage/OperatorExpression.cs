using System;
using Chameleon.ValuePackage;

namespace Chameleon.ExpressionPackage
{
    public class OperatorExpression : Expression
    {
        private Expression left;
        private char op;
        private Expression right;

        public OperatorExpression(Expression left, char op, Expression right)
        {
            this.left = left;
            this.op = op;
            this.right = right;
        }

        public Value Evaluate()
        {
            Value leftVal = left.Evaluate();
            Value rightVal = right.Evaluate();

            switch (op)
            {
                case '=':
                    if (leftVal is NumberValue) 
                    {
                        return new NumberValue((leftVal.ToNumber() ==
                                                rightVal.ToNumber()) ? 1 : 0);
                    } 
                    else 
                    {
                        return new NumberValue(leftVal.ToString().Equals(
                                               rightVal.ToString()) ? 1 : 0);
                    }
                case '+':
                    if (leftVal is NumberValue) 
                    {
                        return new NumberValue(leftVal.ToNumber() +
                                               rightVal.ToNumber());
                    } 
                    else 
                    {
                        return new StringValue(leftVal.ToString() +
                                rightVal.ToString());
                    }
                case '-':
                    return new NumberValue(leftVal.ToNumber() -
                            rightVal.ToNumber());
                case '*':
                    return new NumberValue(leftVal.ToNumber() *
                            rightVal.ToNumber());
                case '/':
                    return new NumberValue(leftVal.ToNumber() /
                            rightVal.ToNumber());
                case '<':
                    if (leftVal is NumberValue) {
                        return new NumberValue((leftVal.ToNumber() <
                                                rightVal.ToNumber()) ? 1 : 0);
                    } else {
                        return new NumberValue((leftVal.ToString().CompareTo(
                                               rightVal.ToString()) < 0) ? 1 : 0);
                    }
                case '>':
                    if (leftVal is NumberValue) 
                    {
                        return new NumberValue((leftVal.ToNumber() >
                                                rightVal.ToNumber()) ? 1 : 0);
                    }
                    else 
                    {
                        return new NumberValue((leftVal.ToString().CompareTo(
                                rightVal.ToString()) > 0) ? 1 : 0);
                    }
            }
            throw new Exception("Unknown operator.");
        }
    }
}
