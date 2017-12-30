using System;
using System.Runtime.Serialization;

namespace Roro.Activities
{
    [DataContract]
    public abstract class Argument : ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    [DataContract]
    public abstract class InArgument : Argument
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string DataTypeId { get; set; }

        [DataMember]
        public string Expression { get; set; }
    }

    [DataContract]
    public sealed class InArgument<T> : InArgument where T : DataType, new()
    {
        public InArgument()
        {
            this.Name = string.Empty;
            this.DataTypeId = new T().Id;
            this.Expression = string.Empty;
        }
    }

    [DataContract]
    public abstract class OutArgument : Argument
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string DataTypeId { get; set; }

        [DataMember]
        public Guid VariableId { get; set; }
    }

    [DataContract]
    public sealed class OutArgument<T> : OutArgument where T : DataType, new()
    {
        public OutArgument()
        {
            this.Name = string.Empty;
            this.DataTypeId = new T().Id;
            this.VariableId = Guid.Empty;
        }
    }

    [DataContract]
    public abstract class InOutArgument : Argument
    {
        [DataMember]
        public Guid VariableId { get; set; }

        [DataMember]
        public string DataTypeId { get; set; }

        [DataMember]
        public string Expression { get; set; }
    }

    [DataContract]
    public sealed class InOutArgument<T> : InOutArgument where T : DataType, new()
    {
        public InOutArgument()
        {
            this.VariableId = Guid.Empty;
            this.DataTypeId = new T().Id;
            this.Expression = string.Empty;
        }
    }
}