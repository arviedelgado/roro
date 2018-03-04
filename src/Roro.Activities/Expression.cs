
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;

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

            expression = Expression.Resolve(expression, variableNodes);

            var codeProvider = new CSharpCodeProvider();
            var compilerParameters = new CompilerParameters
            {
                GenerateInMemory = true,
                GenerateExecutable = false,
            };
            var code = @"
using System;
public class ExpressionClass { public static object ExpressionMethod() {
return
#line 1
" +  expression + @"
;}}";
            var compilerResults = codeProvider.CompileAssemblyFromSource(compilerParameters, code);
            if (compilerResults.Errors.HasErrors)
            {
                var errorText = string.Empty;
                foreach (CompilerError compilerError in compilerResults.Errors)
                {
                    errorText += string.Format("ERROR: Line {0}, Column {1} - {2}\n", compilerError.Line, compilerError.Column, compilerError.ErrorText);
                }
                throw new Exception(errorText);
            }

            var expressionObject = compilerResults.CompiledAssembly.GetType("ExpressionClass");
            var expressionResult = expressionObject.GetMethod("ExpressionMethod").Invoke(expressionObject, null);

            return expressionResult;
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