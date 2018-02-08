using System;
using System.Linq;

namespace Roro.Activities.Element
{
    public sealed class Exists : DecisionNodeActivity
    {
        public Input<ElementQuery> Element { get; set; }

        public override bool Execute(ActivityContext context)
        {
            var query = ElementQuery.Get(this.Element);

            if (query == null)
                throw new Exception("Input 'Element' is required.");

            var elements = WinContext.Shared.GetElementsFromQuery(query);

            if (elements.Count() == 0)
                return false;

            return true;
        }
    }
}
