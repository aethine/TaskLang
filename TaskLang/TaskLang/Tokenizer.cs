using System;
using System.Collections.Generic;
using System.Text;

namespace TaskLang
{
    static class Tokenizer
    {
        public static Token[] LinesToTokens(string[] lines)
        {
            List<Token> tokens = new List<Token>();
            lines = Format(lines);

            foreach (string line in lines)
            {
                tokens.AddRange(LineToTokens(line));
                tokens.Add(new Token(TokenType.NewLine));
            }
            return tokens.ToArray();
        }
        //Removes comments and whitespace
        static string[] Format(string[] lines)
        {
            List<string> result = new List<string>();
            string current = "";
            foreach (string line in lines)
            {
                foreach (char c in line)
                {
                    if (c == '#') break; //comment
                    else current += c.ToString();
                }
                if(!String.IsNullOrWhiteSpace(current)) result.Add(current);
                current = "";
            }
            return result.ToArray();
        }
        //Converts a single line into an array of tokens
        static Token[] LineToTokens(string line)
        {
            List<Token> tokens = new List<Token>();


            return tokens.ToArray();
        }
    }
}
