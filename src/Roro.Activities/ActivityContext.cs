using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roro.Activities
{
    public sealed class ActivityContext
    {
        public T Get<T>(InArgument<T> argument) where T : DataType, new()
        {
            return new Expression(null, argument.Expression).Evaluate<T>();
        }

        public T Get<T>(InOutArgument<T> argument) where T : DataType, new()
        {
            return new Expression(null, argument.Expression).Evaluate<T>();
        }

        public void Set<T>(OutArgument<T> argument, T value) where T : DataType, new()
        {

        }

        public void Set<T>(InOutArgument<T> argument, T value) where T : DataType, new()
        {

        }
    }
}
