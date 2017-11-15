using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace Roro.Activities
{
    [DataContract]
    [TypeConverter(typeof(ActivityTypeConverter))]
    public abstract class Activity
    {
        [DataMember]
        public virtual List<InArgument> Inputs => this.Get<InArgument>();

        [DataMember]
        public virtual List<OutArgument> Outputs => this.Get<OutArgument>();

        private List<T> Get<T>() where T : Argument
        {
            var args = new List<T>();
            var props = this.GetType().GetProperties().Where(p => p.PropertyType.BaseType == typeof(T));
            foreach (var prop in props)
            {
                T arg = prop.GetValue(this) as T;
                // create a new instance if null.
                if (arg == null)
                {
                    var gArgs = prop.PropertyType.GetGenericArguments();
                    var gType = prop.PropertyType.GetGenericTypeDefinition().MakeGenericType(gArgs);
                    arg = Activator.CreateInstance(gType) as T;
                    arg.Name = prop.Name;
                    arg.Type = gArgs.First();
                    arg.Value = string.Empty;
                    prop.SetValue(this, arg);
                }
                args.Add(arg);
            }
            return args;
        }

        public abstract void Execute(Context context);
    }
}