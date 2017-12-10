using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roro.Activities
{
    public abstract class ActivityContext
    {
        public abstract T Get<T>(InArgument<T> input) where T : DataType, new();

        public abstract T Get<T>(InOutArgument<T> input) where T : DataType, new();

        public abstract void Set<T>(OutArgument<T> output, T value) where T : DataType, new();

        public abstract void Set<T>(InOutArgument<T> output, T value) where T : DataType, new();
    }
}
