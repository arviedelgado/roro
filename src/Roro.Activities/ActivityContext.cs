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

        public T Get<T>(Input<T> inArgument) where T : DataType, new()
        {
            return this.InternalGet(inArgument) as T;
        }

        internal object InternalGet(InArgument inArgument)
        {
            return Expression.Evaluate(inArgument.Value, this.InVariables);
        }

        public void Set<T>(Output<T> outArgument, T value) where T : DataType, new()
        {
            this.InternalSet(outArgument, value);
        }

        internal void InternalSet(OutArgument outArgument, object value)
        {
            if (outArgument.Value.Length == 0)
            {
                return;
            }
            if (this.OutVariables.FirstOrDefault(x => x.Name == outArgument.Value) is VariableNode variable)
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
