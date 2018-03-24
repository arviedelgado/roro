
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Roro.Activities
{
    internal class Expression
    {
        public static object Evaluate(string expression, IEnumerable<VariableNode> variableNodes)
        {
            if (String.IsNullOrWhiteSpace(expression))
            {
                return null;
            }

            try
            {
                expression = Expression.Resolve(expression, variableNodes);
                var result = Task.Run(async () =>
                {
                    return await CSharpScript.EvaluateAsync(expression, ScriptOptions.Default.WithImports("System"));
                }).Result;
                return result;
            }
            catch (CompilationErrorException)
            {
                throw;
                //                string.Join(Environment.NewLine, e.Diagnostics)
            }
        }

        private static string Resolve(string expression, IEnumerable<VariableNode> variableNodes)
        {
            var resolvedExpression = string.Empty;

            var currentIndex = 0;
            while (currentIndex < expression.Length)
            {
                var startIndex = expression.IndexOf(VariableNode.StartToken, currentIndex);
                if (startIndex < 0)
                {
                    resolvedExpression += expression.Substring(currentIndex);
                    break;
                }
                var endIndex = expression.IndexOf(VariableNode.EndToken, startIndex + 1);
                if (endIndex < 0)
                {
                    throw new FormatException(string.Format("The variable at char {0} has missing '{1}' in expression:\n\n{2}", startIndex, VariableNode.EndToken, expression));
                }
                var variableName = expression.Substring(startIndex + 1, endIndex - startIndex - 1);
                if (variableName == VariableNode.StartToken)
                {
                    resolvedExpression += expression.Substring(currentIndex, endIndex - currentIndex + 1);
                    currentIndex = endIndex + 1;
                    continue;
                }
                var variableNode = variableNodes.FirstOrDefault(x => x.Name == variableName);
                if (variableNode == null)
                {
                    throw new FormatException(string.Format("Variable {0}{1}{2} not found.", VariableNode.StartToken, variableName, VariableNode.EndToken));
                }
                var variable = DataType.CreateInstance(variableNode.Type);
                variable.SetValue(variableNode.CurrentValue);
                resolvedExpression += variable.ToExpression();
                currentIndex = endIndex + 1;
            }

            return resolvedExpression;
        }
    }
}