using System;
using System.Runtime.Serialization;

namespace Roro.Activities
{
    [DataContract]
    public abstract class Variable
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string DataTypeId { get; set; }

        [DataMember]
        public string Name { get; set; }

        public const string Missing = "MISSING";
    }

    public sealed class Variable<T> : Variable where T : DataType, new()
    {
        public Variable()
        {
            this.Id = Guid.Empty;
            this.DataTypeId = new T().Id;
            this.Name = string.Empty;
        }

        public Variable(string name)
        {
            this.Id = Guid.NewGuid();
            this.DataTypeId = new T().Id;
            this.Name = name;
        }
    }
}