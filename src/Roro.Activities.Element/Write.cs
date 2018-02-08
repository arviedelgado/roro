using System;
using System.Linq;

namespace Roro.Activities.Element
{
    public sealed class Write : ProcessNodeActivity
    {
        public Input<ElementQuery> Element { get; set; }

        public Input<Text> Text { get; set; }

        public override void Execute(ActivityContext context)
        {
            var query = ElementQuery.Get(this.Element);

            if (query == null)
                throw new Exception("Input 'Element' is required.");

            var elements = WinContext.Shared.GetElementsFromQuery(query);
            if (elements.Count() == 0)
                throw new Exception("Element not found.");
            if (elements.Count() > 1)
                throw new Exception("Too many elements found.");

            var text = context.Get(this.Text);

            using (var input = new InputDriver())
            {
                var e = elements.First() as WinElement;
                e.Focus();
                input.Write(text);
            }
        }
    }
}
