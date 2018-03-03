using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Roro.Activities
{
    public abstract class Activity
    {
        protected Activity()
        {
            this.Inputs = this.Inputs;
            this.Outputs = this.Outputs;
        }

        private IEnumerable<PropertyInfo> GetPropertyInfos<T>()
        {
            return this.GetType().GetProperties()
                    .Where(x =>
                        typeof(T).IsAssignableFrom(x.PropertyType)
                        && x.PropertyType.IsGenericType);
        }

        internal protected virtual List<Input> Inputs
        {
            get
            {
                var args = new List<Input>();
                foreach (var prop in this.GetPropertyInfos<Input>())
                {
                    args.Add(new Input()
                    {
                        Name = prop.Name,
                        Type = prop.PropertyType.GetGenericArguments().First().FullName,
                        Value = prop.GetValue(this) is Input arg ? arg.Value : string.Empty
                    });
                }
                return args;
            }
            set
            {
                var args = new List<Input>(value);
                foreach (var prop in this.GetPropertyInfos<Input>())
                {
                    var genArgs = prop.PropertyType.GetGenericArguments();
                    var genType = typeof(Input<>).MakeGenericType(genArgs);
                    var arg = Activator.CreateInstance(genType) as Input;
                    arg.Name = prop.Name;
                    arg.Type = genArgs.First().FullName;
                    arg.Value = args.First(x => x.Name == prop.Name).Value;
                    prop.SetValue(this, arg);
                }
            }
        }

        internal protected virtual List<Output> Outputs
        {
            get
            {
                var args = new List<Output>();
                foreach (var prop in this.GetPropertyInfos<Output>())
                {
                    args.Add(new Output()
                    {
                        Name = prop.Name,
                        Type = prop.PropertyType.GetGenericArguments().First().FullName,
                        Value = prop.GetValue(this) is Output arg ? arg.Value : string.Empty
                    });
                }
                return args;
            }
            set
            {
                var args = new List<Output>(value);
                foreach (var prop in this.GetPropertyInfos<Output>())
                {
                    var genArgs = prop.PropertyType.GetGenericArguments();
                    var genType = typeof(Output<>).MakeGenericType(genArgs);
                    var arg = Activator.CreateInstance(genType) as Output;
                    arg.Name = prop.Name;
                    arg.Type = genArgs.First().FullName;
                    arg.Value = args.First(x => x.Name == prop.Name).Value;
                    prop.SetValue(this, arg);
                }
            }
        }

        public static IEnumerable<Type> GetExternalActivities()
        {
            foreach (var file in Directory.GetFiles(Environment.CurrentDirectory, "Roro.Activities.*.dll"))
            {
                Assembly.LoadFrom(file);
            }
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var refasm in asm.GetReferencedAssemblies())
                {
                    Console.WriteLine(refasm.FullName);
                }
            }


            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => !x.IsAbstract
                    && (typeof(ProcessNodeActivity).IsAssignableFrom(x)
                        || typeof(DecisionNodeActivity).IsAssignableFrom(x)))
                .OrderBy(x => x.FullName);
        }

        private static IEnumerable<Type> GetAllActivities()
        {
            Activity.GetExternalActivities();
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => !x.IsAbstract)
                .OrderBy(x => x.FullName);
        }

        public static Activity CreateInstance(string activityId)
        {
            if (Activity.GetAllActivities().Where(x => x.FullName == activityId).FirstOrDefault() is Type activityType)
            {
                return Activator.CreateInstance(activityType) as Activity;
            }
            else
            {
                throw new TypeLoadException(string.Format("Cannot create instance of '{0}' activity", activityId));
            }
        }
    }
}