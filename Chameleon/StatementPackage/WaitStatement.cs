using System;

namespace Chameleon.StatementPackage
{
    public class WaitStatement : Statement
    {
        public void Execute()
        {
            Console.ReadKey();
        }
    }
}
