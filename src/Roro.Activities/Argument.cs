using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Runtime.Serialization;

namespace Roro.Activities
{
    [DataContract]
    [TypeConverter(typeof(ArgumentTypeConverter))]
    [Editor(typeof(ArgumentTypeEditor), typeof(UITypeEditor))]
    public abstract class Argument : ICloneable
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember(Name = "Type")]
        private string TypeName { get; set; }

        public Type Type
        {
            get => Type.GetType(this.TypeName);
            set => this.TypeName = value.ToString();
        }

        [DataMember]
        public string Value { get; set; }

        public object Clone() => this.MemberwiseClone();
    }

    [DataContract]
    public abstract class InArgument : Argument
    {
        protected InArgument(string name, Type type)
        {
            this.Name = name;
            this.Type = type;
        }
    }

    [DataContract]
    public sealed class InArgument<T> : InArgument
    {
        public InArgument() : this(string.Empty)
        {

        }

        public InArgument(string name) : base(name, typeof(T))
        {

        }
    }

    [DataContract]
    public abstract class OutArgument : Argument
    {
        protected OutArgument(string name, Type type)
        {
            this.Name = name;
            this.Type = type;
        }
    }

    [DataContract]
    public sealed class OutArgument<T> : OutArgument
    {
        public OutArgument() : this(string.Empty)
        {

        }

        public OutArgument(string name) : base(name, typeof(T))
        {

        }
    }
}
