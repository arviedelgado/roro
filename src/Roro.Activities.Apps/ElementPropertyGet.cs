using System;
using System.Linq;
using System.Reflection;

namespace Roro.Activities.Apps
{
    public sealed class ElementPropertyGet : ProcessNodeActivity
    {
        public Input<ElementQuery> Element { get; set; }

        public Input<Text> PropertyName { get; set; }

        public Output<Text> PropertyValue { get; set; }

        public override void Execute(ActivityContext context)
        {
            var e = context.GetElement(this.Element);

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