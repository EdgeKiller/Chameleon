using Chameleon.ExpressionPackage;
using Chameleon.ValuePackage;

namespace Chameleon.StatementPackage
{
    public class AssignStatement : Statement
    {
        private string name, type;
        private Expression value;
        private Chameleon chameleon;

        public AssignStatement(string name, Expression value, Chameleon chameleon, string type)
        {
            this.name = name;
            this.value = value;
            this.chameleon = chameleon;
            this.type = type;
        }

        public void Execute()
        {
            if (type == "=")
                chameleon.variables[name] = value.Evaluate();
            else if (type == "+=")
            {
                if (chameleon.variables[name] is NumberValue)
                    chameleon.variables[name] = new NumberValue(chameleon.variables[name].ToNumber() + value.Evaluate().ToNumber());
                else
                    chameleon.variables[name] = new StringValue(chameleon.variables[name].ToString() + value.Evaluate().ToString());
            }
            else if (type == "-=")
            {
                chameleon.variables[name] = new NumberValue(chameleon.variables[name].ToNumber() - value.Evaluate().ToNumber());
            }
            else if (type == "*=")
            {
                chameleon.variables[name] = new NumberValue(chameleon.variables[name].ToNumber() * value.Evaluate().ToNumber());
            }
            else if (type == "/=")
            {
                chameleon.variables[name] = new NumberValue(chameleon.variables[name].ToNumber() / value.Evaluate().ToNumber());
            }
            else if (type == "++")
            {
                chameleon.variables[name] = new NumberValue(chameleon.variables[name].ToNumber() + value.Evaluate().ToNumber());
            }
            else if (type == "--")
            {
                chameleon.variables[name] = new NumberValue(chameleon.variables[name].ToNumber() - value.Evaluate().ToNumber());
            }
        }
    }
}
