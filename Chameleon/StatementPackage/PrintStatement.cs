using Chameleon.ExpressionPackage;
using System;

namespace Chameleon.StatementPackage
{
    public class PrintStatement : Statement
    {
        private Expression expression;

        public PrintStatement(Expression expression)
        {
            this.expression = expression;
        }

        public void Execute()
        {
            Console.WriteLine(expression.Evaluate().ToString());
        }
    }
}
