using System;
using TaskLang;

namespace TaskLangTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] abc = { "exec \"\"", "cmd \"\"", "abc a t ->", "|return a + t" };
            foreach (Token t in Tokenizer.LinesToTokens(abc)) Console.WriteLine(t);
        }
    }
}
