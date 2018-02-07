using System;
using System.Collections.Generic;
using System.Linq;

namespace Roro.Activities
{
    public sealed class ActivityContext
    {
        private IEnumerable<VariableNode> InVariables { get; }

        private IEnumerable<VariableNode> OutVariables { get; }

        public ActivityContext(IEnumerable<VariableNode> variables) : this(variables, variables)
        {
            ;
        }

        public ActivityContext(IEnumerable<VariableNode> inVariables, IEnumerable<VariableNode> outVariables)
        {
            this.InVariables = inVariables;
            this.OutVariables = outVariables;
        }

        public T Get<T>(Input<T> input) where T : DataType, new()
        {
            var t = new T();
            t.SetValue(this.InternalGet(input));
            return t;
        }

        private object InternalGet(Input input)
        {
            return Expression.Evaluate(input.Value, this.InVariables);
        }

        public void Set<T>(Output<T> output, T value) where T : DataType, new()
        {
            this.InternalSet(output, value);
        }

        private void InternalSet(Output output, object value)
        {
            if (output.Value.Length == 0)
            {
                return;
            }
            if (this.OutVariables.FirstOrDefault(x => x.Name == output.Value) is VariableNode variable)
            {
                if (value is DataType dataType)
                {
                    value = dataType.GetValue();
                }
                variable.CurrentValue = value;
            }
            else
            {
                throw new Exception("Variable not found.");
            }
        }
    }
}
