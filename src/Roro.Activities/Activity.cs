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
                    .Where(x => typeof(Input).IsAssignableFrom(x.PropertyType) &&
                        !x.PropertyType.IsAbstract && x.PropertyType.IsGenericType);
        }

        internal protected virtual List<Input> Inputs
        {
            get
            {
                var inputs = new List<Input>();
                foreach (var inputProp in this.GetInputProperties())
                {
                    inputs.Add(new Input()
                    {
                        Name = inputProp.Name,
                        Type = (Activator.CreateInstance(inputProp.PropertyType.GetGenericArguments().First()) as DataType).Id,
                        Value = string.Empty
                    });
                }
                return inputs;
            }
            set
            {
                var inputs = new List<Input>(value);
                foreach (var inputProp in this.GetInputProperties())
                {
                    var input = Activator.CreateInstance(inputProp.PropertyType) as Input;
                    input.Value = inputs.First(x => x.Name == inputProp.Name && x.Type == input.Type).Value;
                    inputProp.SetValue(this, input);
                }
            }
        }

        private IEnumerable<PropertyInfo> GetOutputProperties()
        {
            return this.GetType().GetProperties()
                    .Where(x => typeof(Output).IsAssignableFrom(x.PropertyType) &&
                        !x.PropertyType.IsAbstract && x.PropertyType.IsGenericType);
        }

        internal protected virtual List<Output> Outputs
        {
            get
            {
                var outputs = new List<Output>();
                foreach (var outputProp in this.GetOutputProperties())
                {
                    outputs.Add(new Output()
                    {
                        Name = outputProp.Name,
                        Type = (Activator.CreateInstance(outputProp.PropertyType.GetGenericArguments().First()) as DataType).Id,
                        Value = string.Empty
                    });
                }
                return outputs;
            }
            set
            {
                var outputs = new List<Output>(value);
                foreach (var outputProp in this.GetOutputProperties())
                {
                    var output = Activator.CreateInstance(outputProp.PropertyType) as Output;
                    output.Value = outputs.First(x => x.Name == outputProp.Name && x.Type == output.Type).Value;
                    outputProp.SetValue(this, output);
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