using Chameleon.ValuePackage;

namespace Chameleon.ExpressionPackage
{
    class VariableExpression : Expression
    {
        private string name;
        private Chameleon chameleon;

        public VariableExpression(string name, Chameleon chameleon)
        {
            this.name = name;
            this.chameleon = chameleon;
        }

        public Value Evaluate()
        {
            if (chameleon.variables.ContainsKey(name))
            {
                return chameleon.variables[name];
            }
            return new NumberValue(0);
        }
    }
}
