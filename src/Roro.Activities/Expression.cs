using Microsoft.CSharp;
using Roro.Workflow;
using System;
using System.CodeDom.Compiler;

namespace Roro.Activities
{
    public class Expression
    {
        private string expression;

        public Expression(Page page, string expression)
        {
            this.expression = expression;
        }

        public T Evaluate<T>() where T: DataType, new()
        {
            if (String.IsNullOrWhiteSpace(expression))
            {
                return new T();
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
" +  this.expression + @"
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

            var exprObject = compilerResults.CompiledAssembly.GetType("ExpressionClass");
            var exprResult = exprObject.GetMethod("ExpressionMethod").Invoke(exprObject, null);

            if (new T() is T t && t.TrySetValue(exprResult))
            {
                return t;
            }
            else
            {
                throw new FormatException();
            }
        }
    }
}