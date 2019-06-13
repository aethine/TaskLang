using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using TaskLang.Tokens;

namespace TaskLang.Tokenizer
{
    public static class Tokenizer
    {
        public static readonly string[] Operators = { "+", "-", "/", "*", "and", "or", "xor", "not" };
        public static readonly Pattern WordPattern = s => Regex.IsMatch(s, "^[a-zA-Z][a-zA-Z0-9_]*$");
        public static readonly Pattern NumberPattern = s => double.TryParse(s, out _);
        public static readonly Pattern StringPattern = s => Regex.IsMatch(s, "^\"([^\"\\\\]+|\\\\.)*\"?$");
        public static readonly Pattern SymbolPattern = s => Regex.IsMatch(s, @"^[!@#$%^&*()\[\]{}\\|;:<>,/?`~\-=+]+$");
        public static readonly Pattern[] AllPatterns = { WordPattern, NumberPattern, StringPattern, SymbolPattern };

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
        private static string[] Format(string[] lines)
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
                if (!String.IsNullOrWhiteSpace(current)) result.Add(current);
                current = "";
            }
            return result.ToArray();
        }
        //Converts a single line into an array of tokens
        private static Token[] LineToTokens(string line)
        {
            List<Token> tokens = new List<Token>();
            string current = "";
            Pattern cpattern = null;

            for (int x = 0; x < line.Length; x++)
            {
                char c = line[x];

                if (cpattern == null)
                {
                    if (char.IsWhiteSpace(c)) continue;
                    foreach (Pattern p in AllPatterns)
                    {
                        if (p(c.ToString()))
                        {
                            cpattern = p;
                            break;
                        }
                    }
                    if (cpattern == null) tokens.Add(new Token(TokenType.Error, c.ToString()));
                    else current += c.ToString();
                }
                else if (!cpattern(current + c.ToString()))
                {
                    tokens.Add(WordToToken(current));
                    current = "";
                    cpattern = null;
                    x--;
                }
                else current += c.ToString();
            }

            tokens.Add(WordToToken(current));
            return tokens.ToArray();
        }
        //Converts single word into token
        private static Token WordToToken(string word)
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
            else if (WordPattern(word)) return new Token(TokenType.Word, word);
            else return new Token(TokenType.Error, word);
        }
    }
}
