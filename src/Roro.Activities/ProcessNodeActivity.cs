using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Roro.Activities
{
    // Do not use [DataContract], so derived class cannot.
    public abstract class ProcessNodeActivity : Activity
    {
        public override bool AllowUserToEditArgumentColumn3 => true;

        protected override List<InArgument> GetInArguments()
        {
            var list = new List<InArgument>();
            var props = this.GetType().GetProperties()
                .Where(x => typeof(InArgument).IsAssignableFrom(x.PropertyType) &&
                    !x.PropertyType.IsAbstract && x.PropertyType.IsGenericType);
            foreach (var prop in props)
            {
                var item = prop.GetValue(this) as InArgument;
                if (item == null)
                {
                    item = Activator.CreateInstance(prop.PropertyType) as InArgument;
                    item.Name = prop.Name.Humanize();
                    prop.SetValue(this, item);
                }
                list.Add(item);
            }
            return list;
        }

        protected override List<OutArgument> GetOutArguments()
        {
            var list = new List<OutArgument>();
            var props = this.GetType().GetProperties()
                .Where(x => typeof(OutArgument).IsAssignableFrom(x.PropertyType) &&
                    !x.PropertyType.IsAbstract && x.PropertyType.IsGenericType);
            foreach (var prop in props)
            {
                var item = prop.GetValue(this) as OutArgument;
                if (item == null)
                {
                    item = Activator.CreateInstance(prop.PropertyType) as OutArgument;
                    item.Name = prop.Name.Humanize();
                    prop.SetValue(this, item);
                }
                list.Add(item);
            }
            return list;
        }

        public abstract void Execute(ActivityContext context);
    }
}