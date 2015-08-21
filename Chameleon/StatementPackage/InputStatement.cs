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
                if (!input.Equals(""))
                {
                    double value;
                    if(double.TryParse(input, out value))
                    {
                        chameleon.variables[name] = new NumberValue(value);
                    }
                    else
                    {
                        chameleon.variables[name] = new StringValue(input.ToString());
                    }

                }
            }
            catch { }
        }
    }
}
