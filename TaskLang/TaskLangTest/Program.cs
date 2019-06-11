using System;
using TaskLang;

namespace TaskLangTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] abc = { @"var x = (100 + 5) * 6" };
            foreach (Token t in Tokenizer.LinesToTokens(abc)) Console.WriteLine(t);
        }
    }
}
