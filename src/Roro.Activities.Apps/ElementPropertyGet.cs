using System;
using System.Linq;
using System.Reflection;

namespace Roro.Activities.Element
{
    public sealed class ElementPropertyGet : ProcessNodeActivity
    {
        public Input<ElementQuery> Element { get; set; }

        public Input<Text> PropertyName { get; set; }

        public Output<Text> PropertyValue { get; set; }

        public override void Execute(ActivityContext context)
        {
            var query = ElementQuery.Get(this.Element);

            var elements = WinContext.Shared.GetElementsFromQuery(query);

            if (elements.Count() == 0)
                throw new Exception("Element not found.");
            if (elements.Count() > 1)
                throw new Exception("Too many elements found.");

            var e = elements.First() as WinElement;

            var name = context.Get(this.PropertyName);

            if (e.GetType().GetProperty(name, BindingFlags.Public | BindingFlags.Instance) is PropertyInfo pi)
            {
                context.Set(this.PropertyValue, pi.GetValue(e)?.ToString() ?? string.Empty);
            }
            else
            {
                throw new Exception(string.Format("Element property '{0}' not found.", name));
            }
        }
    }
}
