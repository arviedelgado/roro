using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roro.Activities
{
    public abstract class ActivityContext
    {
        public abstract T Get<T>(Input<T> input) where T : DataType, new();

        public abstract void Set<T>(Output<T> output, T value) where T : DataType, new();
    }
}
