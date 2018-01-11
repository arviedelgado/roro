using System;
using System.Collections.Generic;
using System.Linq;

namespace Roro.Activities
{
    public sealed class ActivityContext
    {
        private IEnumerable<Variable> InVariables { get; }

        private IEnumerable<Variable> OutVariables { get; }

        public ActivityContext(IEnumerable<Variable> variables) : this(variables, variables)
        {
            ;
        }

        public ActivityContext(IEnumerable<Variable> inVariables, IEnumerable<Variable> outVariables)
        {
            this.InVariables = inVariables;
            this.OutVariables = outVariables;
        }

        public T Get<T>(InArgument<T> inArgument) where T : DataType, new()
        {
            return this.InternalGet(inArgument) as T;
        }

        internal object InternalGet(InArgument inArgument)
        {
            return Expression.Evaluate(inArgument.Value, this.InVariables);
        }

        public void Set<T>(OutArgument<T> outArgument, T value) where T : DataType, new()
        {
            this.InternalSet(outArgument, value);
        }

        internal void InternalSet(OutArgument outArgument, object value)
        {
            if (outArgument.Value.Length == 0)
            {
                return;
            }
            if (this.OutVariables.FirstOrDefault(x => x.Name == outArgument.Value) is Variable variable)
            {
                if (value is DataType dataType)
                {
                    value = dataType.GetValue();
                }
                variable.Value = value;
            }
            else
            {
                throw new Exception("Variable not found.");
            }
        }
    }
}
