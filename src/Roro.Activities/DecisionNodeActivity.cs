using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Roro.Activities
{
    // Do not use [DataContract], so derived class cannot.
    public abstract class DecisionNodeActivity : Activity
    {
        public abstract bool Execute(ActivityContext context);
    }
}