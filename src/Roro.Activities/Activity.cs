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
        private IEnumerable<PropertyInfo> GetInputProperties()
        {
            return this.GetType().GetProperties()
                    .Where(x => typeof(InArgument).IsAssignableFrom(x.PropertyType) &&
                        !x.PropertyType.IsAbstract && x.PropertyType.IsGenericType);
        }

        internal protected virtual List<InArgument> Inputs
        {
            get
            {
                var inputs = new List<InArgument>();
                foreach (var inputProp in this.GetInputProperties())
                {
                    inputs.Add(new InArgument()
                    {
                        Name = inputProp.Name,
                        DataTypeId = (Activator.CreateInstance(inputProp.PropertyType.GetGenericArguments().First()) as DataType).Id,
                        Value = string.Empty
                    });
                }
                return inputs;
            }
            set
            {
                var inputs = new List<InArgument>(value);
                foreach (var inputProp in this.GetInputProperties())
                {
                    var input = Activator.CreateInstance(inputProp.PropertyType) as InArgument;
                    input.Value = inputs.First(x => x.Name == input.Name && x.DataTypeId == input.DataTypeId).Value;
                }
            }
        }

        private IEnumerable<PropertyInfo> GetOutputProperties()
        {
            return this.GetType().GetProperties()
                    .Where(x => typeof(OutArgument).IsAssignableFrom(x.PropertyType) &&
                        !x.PropertyType.IsAbstract && x.PropertyType.IsGenericType);
        }

        internal protected virtual List<OutArgument> Outputs
        {
            get
            {
                var outputs = new List<OutArgument>();
                foreach (var outputProp in this.GetOutputProperties())
                {
                    outputs.Add(new OutArgument()
                    {
                        Name = outputProp.Name,
                        DataTypeId = (Activator.CreateInstance(outputProp.PropertyType.GetGenericArguments().First()) as DataType).Id,
                        Value = string.Empty
                    });
                }
                return outputs;
            }
            set
            {
                var outputs = new List<OutArgument>(value);
                foreach (var outputProp in this.GetOutputProperties())
                {
                    var output = Activator.CreateInstance(outputProp.PropertyType) as OutArgument;
                    output.Value = outputs.First(x => x.Name == output.Name && x.DataTypeId == output.DataTypeId).Value;
                }
            }
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