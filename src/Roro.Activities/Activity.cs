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
        public virtual bool AllowUserToEditArgumentRowList => false;

        public virtual bool AllowUserToEditArgumentColumn1 => false;

        public virtual bool AllowUserToEditArgumentColumn2 => false;

        public virtual bool AllowUserToEditArgumentColumn3 => false;

        protected virtual List<InArgument> GetInArguments() => null;

        protected virtual List<OutArgument> GetOutArguments() => null;

        protected virtual List<InOutArgument> GetInOutArguments() => null;

        public Activity()
        {
            this.GetInArguments();
            this.GetOutArguments();
            this.GetInOutArguments();
        }

        public List<T> GetArguments<T>() where T : Argument
        {
            if (typeof(T) == typeof(InArgument)) return this.GetInArguments() as List<T>;
            if (typeof(T) == typeof(OutArgument)) return this.GetOutArguments() as List<T>;
            if (typeof(T) == typeof(InOutArgument)) return this.GetInOutArguments() as List<T>;
            throw new NotSupportedException();
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