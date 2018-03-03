using System;
using System.Linq;

namespace Roro.Activities.Element
{
    public sealed class ElementValueGet : ProcessNodeActivity
    {
        public Input<ElementQuery> Element { get; set; }

        public Output<Text> Value { get; set; }

        public override void Execute(ActivityContext context)
        {
            var query = ElementQuery.Get(this.Element);

            var elements = WinContext.Shared.GetElementsFromQuery(query);
            if (elements.Count() == 0)
                throw new Exception("Element not found.");
            if (elements.Count() > 1)
                throw new Exception("Too many elements found.");

            var e = elements.First() as WinElement;

            e.Focus();

            context.Set(this.Value, e.Value);

        }
    }
}
