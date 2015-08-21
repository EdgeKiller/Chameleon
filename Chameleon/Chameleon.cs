using Chameleon.ParserPackage;
using Chameleon.StatementPackage;
using Chameleon.TokenPackage;
using Chameleon.ValuePackage;
using System;
using System.Collections.Generic;

namespace Chameleon
{
    public class Chameleon
    {
        public Dictionary<string, Value> variables;
        public Dictionary<string, int> labels;
        public int currentStatement;

        public Chameleon()
        {
            variables = new Dictionary<string, Value>();
            labels = new Dictionary<string, int>();
        }

        public void Interpret(string source)
        {
            List<Token> tokens = Tokenizer.Tokenize(source);

            Parser parser = new Parser(tokens, this);
            List<Statement> statements = parser.Parse();

            currentStatement = 0;
            while (currentStatement < statements.Count)
            {
                int thisStatement = currentStatement;
                currentStatement++;
                statements[thisStatement].Execute();
            }
        }

    }
}
