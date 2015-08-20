using System;

namespace Chameleon.StatementPackage
{
    public class ClearStatement : Statement
    {
        public void Execute()
        {
            Console.Clear();
        }
    }
}
