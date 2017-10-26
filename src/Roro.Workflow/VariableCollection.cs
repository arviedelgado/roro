using Roro.Activities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Roro.Workflow
{
    public class VariableCollection
    {
        private readonly IList<Variable> variables = new List<Variable>();

        public T Get<T>(InArgument<T> input)
        {
            var expression = input.Value;
            return Resolve<T>(expression);
        }

        public void Set<T>(OutArgument<T> output, T value)
        {
            var variableName = output.Value;
            this.variables.First(x => x.Name == variableName).Value = value;
        }

        public T Resolve<T>(string expression)
        {
            return default(T);
        }
    }

}
