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
        internal virtual string DataTypeId { get; set; }

        [DataMember]
        internal string Value { get; set; }

        protected Argument()
        {
            this.Name = string.Empty;
            this.Value = string.Empty;
        }
    }

    [DataContract]
    public class InArgument : Argument
    {
        public InArgument() => base.DataTypeId = new Text().Id;
    }

    [DataContract]
    public sealed class Input<T> : InArgument where T : DataType, new()
    {
        public Input() => base.DataTypeId = new T().Id;

        internal override string DataTypeId
        {
            get => base.DataTypeId;
            set => throw new Exception(string.Format("Property '{0}.DataTypeId' cannot be assigned to -- it is readonly", this.GetType().Name));
        }
    }

    [DataContract]
    public class OutArgument : Argument
    {
        public OutArgument() => base.DataTypeId = new Text().Id;
    }

    [DataContract]
    public sealed class Output<T> : OutArgument where T : DataType, new()
    {
        public Output() => base.DataTypeId = new T().Id;

        internal override string DataTypeId
        {
            get => base.DataTypeId;
            set => throw new Exception(string.Format("Property '{0}.DataTypeId' cannot be assigned to -- it is readonly", this.GetType().Name));
        }
    }
}