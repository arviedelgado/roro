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
        public virtual List<InArgument> Inputs
        {
            get => this.Get<InArgument>();
            protected set { /* For DataContractSerializer */ }
        }

        [DataMember]
        public virtual List<OutArgument> Outputs
        {
            get => this.Get<OutArgument>();
            protected set { /* For DataContractSerializer */ }
        }

        private List<T> Get<T>() where T : Argument
        {
            var args = new List<T>();
            var props = this.GetType().GetProperties().Where(p => p.PropertyType.BaseType == typeof(T));
            foreach (var prop in props)
            {
                T arg = prop.GetValue(this) as T;
                if (arg == null)
                {
                    arg = Activator.CreateInstance(prop.PropertyType) as T;
                    arg.Name = prop.Name;
                    arg.Type = arg.GetType().GetGenericArguments().First();
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