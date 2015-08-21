using System;
using System.Collections.Generic;

namespace Chameleon.TokenPackage
{
    public static class Tokenizer
    {
        public static List<Token> Tokenize(string source)
        {
            List<Token> tokens = new List<Token>();

            source += " ";

            string token = "";
            TokenizeState state = TokenizeState.DEFAULT;

            string charTokens = "\n=+-*/%^<>()";
            TokenType[] tokenTypes = { TokenType.LINE, TokenType.ASSIGN,
                TokenType.OPERATOR, TokenType.OPERATOR, TokenType.OPERATOR,
                TokenType.OPERATOR, TokenType.OPERATOR, TokenType.OPERATOR,
                TokenType.COMP_OPERATOR, TokenType.COMP_OPERATOR,
                TokenType.LEFT_P, TokenType.RIGHT_P
            };


            char c = '\0', nc = '\0', pc = '\0';

            for (int i = 0; i < source.Length; i++)
            {
                //Actual char
                c = source[i];
                //Previous char
                if (i > 0)
                    pc = source[i - 1];
                else
                    pc = '\0';
                //Next char
                if (i < source.Length - 1)
                    nc = source[i + 1];
                else
                    nc = '\0';

                switch (state)
                {
                    case TokenizeState.DEFAULT:
                        int indexC = charTokens.IndexOf(c);

                        if (indexC != -1)
                        {
                            if (c == '=' && (nc == '=' || nc == '>' || nc == '<' || nc == '!'))
                            {
                                tokens.Add(new Token(c.ToString() + nc.ToString(), TokenType.COMP_OPERATOR));
                                i++;
                            }
                            else if ((c == '+' || c == '-' || c == '/' || c == '*') && nc == '=')
                            {
                                tokens.Add(new Token(c.ToString() + nc.ToString(), TokenType.ASSIGN));
                                i++;
                            }
                            else if ((c == '+' && nc == '+') || (c == '-' && nc == '-'))
                            {
                                tokens.Add(new Token(c.ToString() + nc.ToString(), TokenType.SPE_OPERATOR));
                                i++;
                            }
                            else
                            {
                                tokens.Add(new Token(Char.ToString(c), tokenTypes[charTokens.IndexOf(c)]));
                            }
                        }
                        else if (char.IsLetter(c))
                        {
                            token += c;
                            state = TokenizeState.IDENTIFIER;
                        }
                        else if (char.IsDigit(c))
                        {
                            token += c;
                            state = TokenizeState.NUMBER;
                        }
                        else if (c == '"')
                        {
                            state = TokenizeState.STRING;
                        }
                        else if (c == '\'')
                        {
                            state = TokenizeState.COMMENT;
                        }
                        break;
                    case TokenizeState.IDENTIFIER:

                        if(char.IsLetterOrDigit(c))
                        {
                            token += c;
                        }
                        else if (c == ':')
                        {
                            tokens.Add(new Token(token, TokenType.LABEL));
                            token = "";
                            state = TokenizeState.DEFAULT;
                        }
                        else
                        {
                            tokens.Add(new Token(token, TokenType.IDENTIFIER));
                            token = "";
                            state = TokenizeState.DEFAULT;
                            i--;
                        }
                        break;

                    case TokenizeState.NUMBER:
                        if(char.IsDigit(c) || (c == ',' && !token.Contains(",")))
                        {
                            token += c;
                        }
                        else
                        {
                            tokens.Add(new Token(token, TokenType.NUMBER));
                            token = "";
                            state = TokenizeState.DEFAULT;
                            i--;
                        }
                        break;
                    case TokenizeState.STRING:
                        if(c == '"' && pc != '\\')
                        {
                            tokens.Add(new Token(token, TokenType.STRING));
                            token = "";
                            state = TokenizeState.DEFAULT;
                        }
                        else
                        {
                            if (pc == '\\')
                                token = token.Remove(token.Length - 1);
                            token += c;
                        }
                        break;
                    case TokenizeState.COMMENT:
                        if(c == '\n' || c == '\'')
                        {
                            state = TokenizeState.DEFAULT;
                        }
                        break;
                }
            }
            return tokens;
        }

    }
}
