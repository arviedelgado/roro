using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Roro.Activities
{
    public sealed class ActivityContext
    {
        private CancellationToken CancellationToken { get; }

        private IEnumerable<VariableNode> InVariables { get; }

        private IEnumerable<VariableNode> OutVariables { get; }

        public ActivityContext(CancellationToken token, IEnumerable<VariableNode> variables) : this(token, variables, variables)
        {
            ;
        }

        public ActivityContext(CancellationToken token, IEnumerable<VariableNode> inVariables, IEnumerable<VariableNode> outVariables)
        {
            this.InVariables = inVariables;
            this.OutVariables = outVariables;
        }

        public void ThrowIfCancellationRequested()
        {
            this.CancellationToken.ThrowIfCancellationRequested();
        }

        public T Get<T>(Input<T> input) where T : DataType, new()
        {
            if (this.InternalGet(input) is object value)
            {
                var t = new T();
                t.SetValue(value);
                return t;
            }
            return null;
        }

        private object InternalGet(Input input)
        {
            if (string.IsNullOrWhiteSpace(input.Value))
            {
                return null;
            }
            return Expression.Evaluate(input.Value, this.InVariables);
        }

        public void Set<T>(Output<T> output, T value) where T : DataType, new()
        {
            this.InternalSet(output, value);
        }

        private void InternalSet(Output output, object value)
        {
            if (string.IsNullOrWhiteSpace(output.Value))
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
