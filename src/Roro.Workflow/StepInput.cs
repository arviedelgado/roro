using Roro.Actions;
using Roro.Workflow.Framework;
using System;

namespace Roro.Workflow
{
    public class StepInput : ObservableObject
    {
        public string Name { get; set; }

        public Type DataType { get; set; }

        public string Expression { get; set; }

        public object RuntimeValue { get; set; }
    }

    public class StepInput<T> : StepInput, Input<T>
    {
        public new T RuntimeValue
        {
            get => (T)base.RuntimeValue;
        }
    }
}