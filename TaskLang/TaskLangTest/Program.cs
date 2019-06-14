using System;
using System.Text.RegularExpressions;
using System.Linq;
using TaskLang.Tokens;
using TaskLang.Tokenizer;
using TaskLang.Errors;
using TaskLang.Context;
using TaskLang.Compile;

namespace TaskLangTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines;
            args = new string[] { "#hi\nvar x = 0\nx = \"hello\" * true\nabc a ->\n| return a\nif false then\n|cmd \"echo hi\"\n else abc 0 + 5" };
            if (args.Length > 0) lines = Regex.Split(string.Join(' ', args), @"[\r\n]");
            else lines = new string[] { "exec \"server.jar\"", "cmd \"echo hello\"", "abc a t ->", "|return a+t" };

            Token[] tokens = Tokenizer.LinesToTokens(lines);
            var errors = ErrorChecking.FirstPass(tokens);
            if (errors.Count > 0)
            {
                Console.WriteLine("--ERRORS FOUND--");
                foreach (var kvp in errors.OrderBy(kvp => kvp.Key))
                {
                    Console.WriteLine($"LINE {kvp.Key}:");
                    foreach (string message in kvp.Value) Console.WriteLine($"    -{message}");
                }
                return;
            }
            else Console.WriteLine("Initial parsing SUCCESS!");
            Token[][] splitTokens = Contextualizer.SplitTokens(tokens);
            ExpressionContext[] contexts = Contextualizer.ClassifyExpressions(splitTokens, new Context());

            if (Array.Exists(contexts, c => c.Expression is ExpressionError))
            {
                foreach (ExpressionError err in contexts.Where(c => c.Expression is ExpressionError).Select(c => (ExpressionError)c.Expression)) Console.WriteLine($"- On line {err.LineNumber}: {err.Error}");
                return;
            }
            Console.WriteLine("Classification success!");
            foreach (string s in Compiler.Compile(contexts)) Console.WriteLine(s);
        }
    }
}
