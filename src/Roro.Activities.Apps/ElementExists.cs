using System;
using System.Linq;

namespace Roro.Activities.Apps
{
    public sealed class ElementExists : DecisionNodeActivity
    {
        public Input<ElementQuery> Element { get; set; }

        public override bool Execute(ActivityContext context)
        {
            return context.CountElements(this.Element) == 1;
        }
    }
}
