
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace Roro.Activities
{
    public class Expression
    {
        public static object Evaluate(string expression, IEnumerable<Variable> variables)
        {
            if (String.IsNullOrWhiteSpace(expression))
            {
                return null;
            }

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
                foreach (CompilerError compilerError in compilerResults.Errors)
                {
                    Console.WriteLine("ERROR: Line {0}, Column {1} - {2}", compilerError.Line, compilerError.Column, compilerError.ErrorText);
                }
                throw new Exception();
            }

            var expressionObject = compilerResults.CompiledAssembly.GetType("ExpressionClass");
            var expressionResult = expressionObject.GetMethod("ExpressionMethod").Invoke(expressionObject, null);

            return expressionResult;
        }
    }
}