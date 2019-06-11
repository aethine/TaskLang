using System;
using System.Linq;
using TaskLang;

namespace TaskLangTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] abc = { "exec \"server.jar\"", "cmd \"echo\"", "abc a t ->", "|return a + t" };
            Token[] tokens = Tokenizer.LinesToTokens(abc);
            var errors = ErrorChecking.FirstPass(tokens);
            if (errors.Count > 0)
            {
                Console.WriteLine("--ERRORS FOUND--");
                foreach (var kvp in errors.OrderBy(kvp => kvp.Key))
                {
                    Console.WriteLine($"LINE {kvp.Key}:");
                    foreach (string message in kvp.Value) Console.WriteLine($"    -{message}");
                }
            }
            else
            {
                Console.WriteLine("SUCCESS!");
                foreach (Token t in tokens) Console.WriteLine(t);
            }
        }
    }
}
