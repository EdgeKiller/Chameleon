using System;
using System.Collections.Generic;

namespace Chameleon.TokenPackage
{
    public static class Tokenizer
    {
        public static List<Token> Tokenize(string source)
        {
            List<Token> tokens = new List<Token>();

            string token = "";
            TokenizeState state = TokenizeState.DEFAULT;

            string charTokens = "\n=+-*/<>()";
            TokenType[] tokenTypes = { TokenType.LINE, TokenType.EQUALS,
                TokenType.OPERATOR, TokenType.OPERATOR, TokenType.OPERATOR,
                TokenType.OPERATOR, TokenType.OPERATOR, TokenType.OPERATOR,
                TokenType.LEFT_PAREN, TokenType.RIGHT_PAREN
            };

            char c, lastc = '\0';
            
            for (int i = 0; i < source.Length; i++)
            {
                c = source[i];
                switch (state)
                {
                    case TokenizeState.DEFAULT:
                        if (charTokens.IndexOf(c) != -1)
                        {
                            tokens.Add(new Token(Char.ToString(c),
                                tokenTypes[charTokens.IndexOf(c)]));
                        }
                        else if (char.IsLetter(c))
                        {
                            token += c;
                            state = TokenizeState.WORD;
                        }
                        else if (char.IsDigit(c) || (char.IsDigit(lastc) && c == ','))
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

                    case TokenizeState.WORD:
                        if (char.IsLetterOrDigit(c))
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
                            tokens.Add(new Token(token, TokenType.WORD));
                            token = "";
                            state = TokenizeState.DEFAULT;
                            i--;
                        }
                        break;

                    case TokenizeState.NUMBER:
                        if (char.IsDigit(c) || (char.IsDigit(lastc) && c == ','))
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
                        if (c == '"' && lastc != '\\')
                        {
                            tokens.Add(new Token(token, TokenType.STRING));
                            token = "";
                            state = TokenizeState.DEFAULT;
                        }
                        else
                        {
                            if (lastc == '\\')
                                token = token.Remove(token.Length - 1);
                            token += c;
                        }
                        break;

                    case TokenizeState.COMMENT:
                        if (c == '\n')
                        {
                            state = TokenizeState.DEFAULT;
                        }
                        break;
                }
                lastc = source[i];
            }

            return tokens;
        }


    }
}
