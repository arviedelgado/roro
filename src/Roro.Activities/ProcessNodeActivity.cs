using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Roro.Activities
{
    // Do not use [DataContract], so derived class cannot.
    public abstract class ProcessNodeActivity : Activity
    {
        public abstract void Execute(ActivityContext context);
    }
}