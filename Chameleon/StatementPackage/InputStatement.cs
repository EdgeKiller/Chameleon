using Chameleon.ValuePackage;
using System;

namespace Chameleon.StatementPackage
{
    public class InputStatement : Statement
    {
        private string name;
        private Chameleon chameleon;

        public InputStatement(string name, Chameleon chameleon)
        {
            this.name = name;
            this.chameleon = chameleon;
        }

        public void Execute()
        {
            try
            {
                string input = Console.ReadLine();
                try
                {
                    double value = double.Parse(input);
                    chameleon.variables[name] = new NumberValue(value);
                }
                catch
                {
                    chameleon.variables[name] = new StringValue(input);
                }
            }
            catch { }
        }
    }
}
