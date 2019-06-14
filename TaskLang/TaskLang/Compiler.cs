using System;
using TaskLang.Tokens;
using TaskLang.Context;
using System.Collections.Generic;

namespace TaskLang.Compile
{
    //Converts output from contextualizer into Tasker-readable format
    public static class Compiler
    {
        public static string[] Compile(ExpressionContext[] input)
        {
            List<string> output = new List<string>();

            foreach (ExpressionContext ec in input)
            {
                switch (ec.Expression.Type)
                {
                    case ExpressionType.VarDeclaration:
                        output.Add("#var_decl");
                        break;
                    case ExpressionType.VarAssignment:
                        output.Add("#var_assn");
                        break;
                    case ExpressionType.FunctionDefinition:
                        output.Add("#fun_decl");
                        break;
                    case ExpressionType.FunctionCall:
                        output.Add("#fun_call");
                        break;
                    case ExpressionType.Return:
                        output.Add("#fun_retn");
                        break;
                    case ExpressionType.Control:
                        output.Add("#ctrl_exp");
                        break;
                }
                foreach (Token t in ec.Expression.Tokens)
                {
                    switch (t.Type)
                    {
                        case TokenType.Number:
                            output.Add($"num {t.Info}");
                            break;
                        case TokenType.Word:
                            output.Add($"word {t.Info}");
                            break;
                        case TokenType.String:
                            output.Add($"string {t.Info}");
                            break;
                        case TokenType.Operator:
                            output.Add($"operator {t.Info}");
                            break;
                        case TokenType.Arrow:
                        case TokenType.Group:
                        case TokenType.Assign:
                        case TokenType.Semicolon:
                        case TokenType.LeftParen:
                        case TokenType.RightParen:
                        case TokenType.Unit:
                        case TokenType.Return:
                        case TokenType.While:
                        case TokenType.For:
                        case TokenType.In:
                        case TokenType.Break:
                        case TokenType.Var:
                        case TokenType.If:
                        case TokenType.Then:
                        case TokenType.Elif:
                        case TokenType.Else:
                        case TokenType.True:
                        case TokenType.False:
                        case TokenType.Nil:
                            output.Add(t.Type.ToString().ToLower());
                            break;
                        default: break;
                    }
                }
                output.Add(";");
            }
            return output.ToArray();
        }
    }
}