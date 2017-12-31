using System;
using System.Runtime.Serialization;

namespace Roro.Activities
{
    [DataContract]
    public abstract class Argument
    {
        [DataMember]
        internal virtual string DataTypeId { get; set; }

        protected Argument()
        {
            ;
        }
    }

    [DataContract]
    public class InArgument : Argument
    {
        [DataMember]
        internal string Name { get; set; }

        [DataMember]
        internal string Expression { get; set; }

        internal InArgument()
        {
            this.Name = string.Empty;
            base.DataTypeId = new Text().Id;
            this.Expression = string.Empty;
        }
    }

    [DataContract]
    public sealed class InArgument<T> : InArgument where T : DataType, new()
    {
        internal override string DataTypeId
        {
            get => base.DataTypeId;
            set => throw new Exception(string.Format("Property '{0}.DataTypeId' cannot be assigned to -- it is readonly", this.GetType().Name));
        }

        public InArgument() => base.DataTypeId = new T().Id;
    }

    [DataContract]
    public class OutArgument : Argument
    {
        [DataMember]
        internal string Name { get; set; }

        [DataMember]
        internal Guid VariableId { get; set; }

        internal OutArgument()
        {
            this.Name = string.Empty;
            base.DataTypeId = new Text().Id;
            this.VariableId = Guid.Empty;
        }
    }

    [DataContract]
    public sealed class OutArgument<T> : OutArgument where T : DataType, new()
    {
        internal override string DataTypeId
        {
            get => base.DataTypeId;
            set => throw new Exception(string.Format("Property '{0}.DataTypeId' cannot be assigned to -- it is readonly", this.GetType().Name));
        }

        public OutArgument() => base.DataTypeId = new T().Id;
    }

    [DataContract]
    public class InOutArgument : Argument
    {
        [DataMember]
        internal Guid VariableId { get; set; }

        [DataMember]
        internal string Expression { get; set; }

        internal InOutArgument()
        {
            this.VariableId = Guid.Empty;
            base.DataTypeId = new Text().Id;
            this.Expression = string.Empty;
        }
    }

    [DataContract]
    public sealed class InOutArgument<T> : InOutArgument where T : DataType, new()
    {
        internal override string DataTypeId
        {
            get => base.DataTypeId;
            set => throw new Exception(string.Format("Property '{0}.DataTypeId' cannot be assigned to -- it is readonly", this.GetType().Name));
        }

        public InOutArgument() => base.DataTypeId = new T().Id;
    }
}