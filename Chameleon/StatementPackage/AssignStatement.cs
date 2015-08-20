using Chameleon.ExpressionPackage;

namespace Chameleon.StatementPackage
{
    public class AssignStatement : Statement
    {
        private string name;
        private Expression value;
        private Chameleon chameleon;

        public AssignStatement(string name, Expression value, Chameleon chameleon)
        {
            this.name = name;
            this.value = value;
            this.chameleon = chameleon;
        }

        public void Execute()
        {
            chameleon.variables[name] = value.Evaluate();
        }
    }
}
