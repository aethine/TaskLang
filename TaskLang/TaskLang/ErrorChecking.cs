using System;
using System.Collections.Generic;
using System.Text;

namespace TaskLang
{
    public static class ErrorChecking
    {
        //run this before contextualizer
        public static Dictionary<int, string[]> FirstPass(Token[] tokens)
        {
            Dictionary<int, string[]> errors = new Dictionary<int, string[]>();
            int line = 0;
            List<string> currentErrors = new List<string>();

            foreach (Token t in tokens)
            {
                if (t.Type == TokenType.NewLine)
                {
                    if (currentErrors.Count > 0)
                    {
                        errors.Add(line, currentErrors.ToArray());
                        currentErrors.Clear();
                    }
                    line++;
                }
                else if (t.Type == TokenType.Error)
                {
                    currentErrors.Add($"Unrecognized sequence \"{t.Info}\"");
                }
            }
            if (currentErrors.Count > 0) errors.Add(line, currentErrors.ToArray());
            return errors;
        }
    }
}
