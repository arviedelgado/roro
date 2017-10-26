using System;
using System.Runtime.Serialization;

namespace Roro.Activities
{
    [DataContract]
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

    public abstract class InArgument : Argument
    {

    }

    public sealed class InArgument<T> : InArgument
    {

    }

    public abstract class OutArgument : Argument
    {

    }

    public sealed class OutArgument<T> : OutArgument
    {
 
    }
}
