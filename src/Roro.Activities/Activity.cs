using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Roro.Activities
{
    [DataContract]
    public abstract class Activity
    {
        public virtual List<InArgument> Inputs
        {
            get
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
            set
            {

            }
        }

        public virtual List<OutArgument> Outputs
        {
            get
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
            set
            {

            }
        }

        public Activity()
        {
            ;
        }

        public static IEnumerable<Type> GetExternalActivities()
        {
            foreach (var file in Directory.GetFiles(Environment.CurrentDirectory, "Roro.Activities.*.dll"))
            {
                Assembly.LoadFrom(file);
            }
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => !x.IsAbstract
                    && (typeof(ProcessNodeActivity).IsAssignableFrom(x)
                        || typeof(DecisionNodeActivity).IsAssignableFrom(x)));
        }

        private static IEnumerable<Type> GetAllActivities()
        {
            Activity.GetExternalActivities();
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => !x.IsAbstract);
        }

        public static Activity CreateInstance(string name)
        {
            var activityType = Activity.GetAllActivities().Where(x => x.FullName == name).First();
            return Activator.CreateInstance(activityType) as Activity;
        }
    }
}