using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace TaskLang
{
    public static class Tokenizer
    {
        public static readonly string[] Operators = { "+", "-", "*", "/", "not", "or", "and", "xor"};
        public static readonly Regex WordPattern = new Regex("[a-zA-Z][a-zA-Z0-9_]*");
        public static readonly char[] SpecialChars = { '|', '(', ')', '=', ';' };

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
            string current = "";

            foreach(char c in line)
            {
                if (Char.IsWhiteSpace(c))
                {
                    if (!string.IsNullOrWhiteSpace(current)) tokens.Add(WordToToken(current));
                    current = "";
                }
                else if (SpecialChars.Contains(c))
                {
                    if (!string.IsNullOrWhiteSpace(current)) tokens.Add(WordToToken(current));
                    tokens.Add(WordToToken(c.ToString()));
                    current = "";
                }
                else current += c.ToString();
            }

            tokens.Add(WordToToken(current));
            return tokens.ToArray();
        }
        static Token WordToToken(string word)
        {
            if (double.TryParse(word, out double d)) return new Token(TokenType.Number, d.ToString());
            else if (word.StartsWith("\"") && word.EndsWith("\"")) //TODO: better string parsing
            {
                //remove surrounding quotes
                return new Token(TokenType.String, word.Remove(word.Length - 1).Remove(0, 1));
            }
            else if (Operators.Contains(word)) return new Token(TokenType.Operator, word);
            else if (word == "->") return new Token(TokenType.Arrow);
            else if (word == "|") return new Token(TokenType.Group);
            else if (word == "=") return new Token(TokenType.Assign);
            else if (word == ";") return new Token(TokenType.Semicolon);
            else if (word == "(") return new Token(TokenType.LeftParen);
            else if (word == ")") return new Token(TokenType.RightParen);
            else if (word == "return") return new Token(TokenType.ReturnKey);
            else if (word == "while") return new Token(TokenType.WhileKey);
            else if (word == "for") return new Token(TokenType.ForKey);
            else if (word == "in") return new Token(TokenType.InKey);
            else if (word == "break") return new Token(TokenType.BreakKey);
            else if (word == "var") return new Token(TokenType.VarKey);
            else if (word == "if") return new Token(TokenType.IfKey);
            else if (word == "then") return new Token(TokenType.ThenKey);
            else if (word == "elif") return new Token(TokenType.ElifKey);
            else if (word == "else") return new Token(TokenType.ElseKey);
            else if (word == "true") return new Token(TokenType.TrueKey);
            else if (word == "false") return new Token(TokenType.FalseKey);
            else if (WordPattern.IsMatch(word)) return new Token(TokenType.Word, word);
            else return new Token(TokenType.Error, word);
        }
    }
}
