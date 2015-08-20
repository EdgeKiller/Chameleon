using Chameleon.ExpressionPackage;

namespace Chameleon.StatementPackage
{
    class IfThenStatement : Statement
    {
        private Expression condition;
        private string label;
        private Chameleon chameleon;

        public IfThenStatement(Expression condition, string label, Chameleon chameleon)
        {
            this.condition = condition;
            this.label = label;
            this.chameleon = chameleon;
        }

        public void Execute()
        {
            if (chameleon.labels.ContainsKey(label))
            {
                double value = condition.Evaluate().ToNumber();
                if(value != 0)
                {
                    chameleon.currentStatement = chameleon.labels[label];
                }
            }
        }
    }
}
