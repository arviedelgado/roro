using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace Roro.Activities
{
    [DataContract]
    public abstract class Activity
    {
        [DataMember]
        public virtual List<Input> Inputs
        {
            get => this.Get<Input>(); protected set { /* For DataContractSerializer */ }
        }

        [DataMember]
        public virtual List<Output> Outputs
        {
            get => this.Get<Output>(); protected set { /* For DataContractSerializer */ }
        }

        private List<T> Get<T>() where T : class
        {
            var props = new List<T>();
            var propInfos = this.GetType().GetProperties()
                .Where(x => typeof(T).IsAssignableFrom(x.PropertyType) && !x.PropertyType.IsAbstract && x.PropertyType.IsGenericType);
            foreach (var propInfo in propInfos)
            {
                T prop = propInfo.GetValue(this) as T;
                if (prop == null)
                {
                    prop = Activator.CreateInstance(propInfo.PropertyType) as T;
                    if (prop is Input input)
                    {
                        input.Name = Regex.Replace(propInfo.Name, "([a-z](?=[A-Z0-9])|[A-Z](?=[A-Z][a-z]))", "$1 ");
                    }
                    if (prop is Output output)
                    {
                        output.Name = Regex.Replace(propInfo.Name, "([a-z](?=[A-Z0-9])|[A-Z](?=[A-Z][a-z]))", "$1 ");
                    }
                    propInfo.SetValue(this, prop);
                }
                props.Add(prop);
            }
            return props;
        }

        public abstract void Execute(ActivityContext context);
    }
}