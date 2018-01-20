using System;
using System.Runtime.Serialization;

namespace Roro.Activities
{
    [DataContract]
    public abstract class Argument
    {
        [DataMember]
        internal string Name { get; set; }

        [DataMember]
        internal string DataTypeId { get; set; }

        [DataMember]
        internal string Value { get; set; }

        protected Argument()
        {
            this.Name = string.Empty;
            this.DataTypeId = DataType.GetDefault().Id;
            this.Value = string.Empty;
        }
    }

    [DataContract]
    public class InArgument : Argument
    {
        internal InArgument() { }
    }

    [DataContract]
    public sealed class Input<T> : InArgument where T : DataType, new()
    {
        public Input() => this.DataTypeId = new T().Id;
    }

    [DataContract]
    public class OutArgument : Argument
    {
        internal OutArgument() { }
    }

    [DataContract]
    public sealed class Output<T> : OutArgument where T : DataType, new()
    {
        public Output() => this.DataTypeId = new T().Id;
    }
}