using System;

namespace Chameleon.StatementPackage
{
    public class ExitStatement : Statement
    {
        public void Execute()
        {
            Environment.Exit(0);
        }
    }
}
