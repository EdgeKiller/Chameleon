using Chameleon.ExpressionPackage;
using Chameleon.StatementPackage;
using Chameleon.TokenPackage;
using Chameleon.ValuePackage;
using System;
using System.Collections.Generic;

namespace Chameleon.ParserPackage
{
    public class Parser
    {
        private List<Token> tokens;

        private int position;
        private Chameleon chameleon;

        public Parser(List<Token> tokens, Chameleon chameleon)
        {
            this.tokens = tokens;
            this.chameleon = chameleon;
            position = 0;
        }

        public List<Statement> Parse()
        {
            List<Statement> statements = new List<Statement>();

            while(true)
            {
                while (Match(TokenType.LINE)) ;

                if (Match("PRINT"))
                {
                    statements.Add(new PrintStatement(Exp()));
                }
                else if (Match("INPUT"))
                {
                    statements.Add(new InputStatement(Consume(TokenType.IDENTIFIER).Text, chameleon));
                }
                else if (Match("GOTO"))
                {
                    statements.Add(new GotoStatement(Consume(TokenType.IDENTIFIER).Text, chameleon));
                }
                else if (Match("IF"))
                {
                    Expression condition = Exp();
                    Consume("THEN");
                    string label = Consume(TokenType.IDENTIFIER).Text;
                    statements.Add(new IfThenStatement(condition, label, chameleon));
                }
                else if (Match("EXIT"))
                {
                    statements.Add(new ExitStatement());
                }
                else if (Match("CLEAR"))
                {
                    statements.Add(new ClearStatement());
                }
                else if (Match("WAIT"))
                {
                    statements.Add(new WaitStatement());
                }
                else if (Match("TITLE"))
                {
                    statements.Add(new TitleStatement(Consume(TokenType.STRING).Text));
                }
                else if (Match(TokenType.LABEL))
                {
                    chameleon.labels[Last(1).Text] = statements.Count;
                }
                else if (Match(TokenType.IDENTIFIER, TokenType.ASSIGN))
                {
                    string name = Last(2).Text;
                    string op = Last(1).Text;
                    Expression value = Exp();
                    statements.Add(new AssignStatement(name, value, chameleon, op));
                }
                else if (Match(TokenType.IDENTIFIER, TokenType.SPE_OPERATOR))
                {
                    string name = Last(2).Text;
                    string op = Last(1).Text;
                    Expression value = new NumberValue(1);
                    statements.Add(new AssignStatement(name, value, chameleon, op));
                }
                else break;
            }

            return statements;
        }

        private Expression Exp()
        {
            return Op();
        }

        private Expression Op()
        {
            Expression expression = Atomic();

            while(Match(TokenType.OPERATOR) || Match(TokenType.COMP_OPERATOR))
            {
                string op = Last(1).Text;
                Expression right;
                if (op == "++" || op == "--")
                    right = new NumberValue(1);
                else
                    right = Atomic();
                expression = new OperatorExpression(expression, op, right);
            }

            return expression;
        }

        public Expression Atomic()
        {
            if(Match(TokenType.IDENTIFIER))
            {
                return new VariableExpression(Last(1).Text, chameleon);
            }
            else if(Match(TokenType.NUMBER))
            {
                return new NumberValue(double.Parse(Last(1).Text));
            }
            else if(Match(TokenType.STRING))
            {
                return new StringValue(Last(1).Text);
            }
            else if(Match(TokenType.LEFT_P))
            {
                Expression expression = Exp();
                Consume(TokenType.RIGHT_P);
                return expression;
            }

            throw new Exception("Couldn't parse.");
        }

        private bool Match(TokenType type)
        {
            if (Get(0).Type != type) return false;
            position++;
            return true;
        }

        private bool Match(TokenType type1, TokenType type2)
        {
            if (Get(0).Type != type1) return false;
            if (Get(1).Type != type2) return false;
            position += 2;
            return true;
        }

        private bool Match(string name)
        {
            if (Get(0).Type != TokenType.IDENTIFIER) return false;
            if (!Get(0).Text.Equals(name)) return false;
            position++;
            return true;
        }

        private Token Consume(TokenType type)
        {
            if (Get(0).Type != type) throw new Exception("Expected " + type + ".");
            return tokens[position++];
        }

        private Token Consume(String name)
        {
            if (!Match(name)) throw new Exception("Expected " + name + ".");
            return Last(1);
        }

        private Token Last(int offset)
        {
            if (position - offset < 0) return new Token("\n", TokenType.NULL);
            return tokens[position - offset];
        }

        private Token Get(int offset)
        {
            if (position + offset >= tokens.Count) return new Token("", TokenType.EOF);
            return tokens[position + offset];
        }
    }
}
