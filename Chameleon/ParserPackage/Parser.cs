using System;
using System.Collections.Generic;
using Chameleon.TokenPackage;
using Chameleon.StatementPackage;
using Chameleon.ExpressionPackage;
using Chameleon.ValuePackage;

namespace Chameleon.ParserPackage
{
    public class Parser
    {
        public List<Token> Tokens { get { return tokens; } }
        private List<Token> tokens;

        private int position;
        private Chameleon chameleon;

        public Parser(List<Token> tokens, Chameleon chameleon)
        {
            this.tokens = tokens;
            this.chameleon = chameleon;
            position = 0;
        }

        public List<Statement> Parse(Dictionary<string, int> labels)
        {
            List<Statement> statements = new List<Statement>();

            while (true)
            {
                while (match(TokenType.LINE)) ;

                if (match(TokenType.LABEL))
                {
                    labels[last(1).Text] = statements.Count;
                }
                else if (match(TokenType.WORD, TokenType.EQUALS))
                {
                    String name = last(2).Text;
                    Expression value = exp();
                    statements.Add(new AssignStatement(name, value, chameleon));
                }
                else if (match("print"))
                {
                    statements.Add(new PrintStatement(exp()));
                }
                else if (match("input"))
                {
                    statements.Add(new InputStatement(
                        consume(TokenType.WORD).Text, chameleon));
                }
                else if (match("goto"))
                {
                    statements.Add(new GotoStatement(
                        consume(TokenType.WORD).Text, chameleon));
                }
                else if (match("if"))
                {
                    Expression condition = exp();
                    consume("then");
                    String label = consume(TokenType.WORD).Text;
                    statements.Add(new IfThenStatement(condition, label, chameleon));
                }
                else if(match("exit"))
                {
                    statements.Add(new ExitStatement());
                }
                else if(match("clear"))
                {
                    statements.Add(new ClearStatement());
                }
                else if(match("wait"))
                {
                    statements.Add(new WaitStatement());
                }
                else if(match("title"))
                {
                    statements.Add(new TitleStatement(consume(TokenType.STRING).Text));
                }
                else break;
            }

            return statements;
        }

        private Expression exp()
        {
            return op();
        }

        private Expression op()
        {
            Expression expression = atomic();

            while (match(TokenType.OPERATOR) ||
                   match(TokenType.EQUALS))
            {
                char op = last(1).Text[0];
                Expression right = atomic();
                expression = new OperatorExpression(expression, op, right);
            }

            return expression;
        }

        private Expression atomic()
        {
            if (match(TokenType.WORD))
            {
                return new VariableExpression(last(1).Text, chameleon);
            }
            else if (match(TokenType.NUMBER))
            {
                return new NumberValue(double.Parse(last(1).Text));
            }
            else if (match(TokenType.STRING))
            {
                return new StringValue(last(1).Text);
            }
            else if (match(TokenType.LEFT_PAREN))
            {
                Expression expression = exp();
                consume(TokenType.RIGHT_PAREN);
                return expression;
            }
            throw new Exception("Couldn't parse.");
        }

        private bool match(TokenType type1, TokenType type2)
        {
            if (get(0).Type != type1) return false;
            if (get(1).Type != type2) return false;
            position += 2;
            return true;
        }

        private bool match(TokenType type)
        {
            if (get(0).Type != type) return false;
            position++;
            return true;
        }

        private bool match(string name)
        {
            if (get(0).Type != TokenType.WORD) return false;
            if (!get(0).Text.Equals(name)) return false;
            position++;
            return true;
        }

        private Token consume(TokenType type)
        {
            if (get(0).Type != type) throw new Exception("Expected " + type + ".");
            return tokens[position++];
        }

        private Token consume(String name)
        {
            if (!match(name)) throw new Exception("Expected " + name + ".");
            return last(1);
        }

        private Token last(int offset)
        {
            return tokens[position - offset];
        }

        private Token get(int offset)
        {
            if (position + offset >= tokens.Count)
            {
                return new Token("", TokenType.EOF);
            }
            return tokens[position + offset];
        }
    }
}
