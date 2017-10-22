using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roro.Core.Activities
{
    public abstract class ActivityContext
    {
        public abstract T Get<T>(InArgument<T> input);

        public abstract void Set<T>(OutArgument<T> output, T value);
    }
}
