using System;
using System.Linq;

namespace Roro.Activities.Element
{
    public sealed class ElementExists : DecisionNodeActivity
    {
        public Input<ElementQuery> Element { get; set; }

        public override bool Execute(ActivityContext context)
        {
            var query = ElementQuery.Get(this.Element);

            var elements = WinContext.Shared.GetElementsFromQuery(query);

            if (elements.Count() == 0)
                return false;

            return true;
        }
    }
}
