using System;

namespace Chameleon.StatementPackage
{
    public class TitleStatement : Statement
    {
        private string title;

        public TitleStatement(string title)
        {
            this.title = title;
        }

        public void Execute()
        {
            Console.Title = title;
        }
    }
}
