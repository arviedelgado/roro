using System;
using System.Runtime.Serialization;

namespace Roro.Activities
{
    [DataContract]
    public class Variable
    {
        public const string Missing = "MISSING";

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string DataTypeId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        private object Value { get; set; }

        public void SetValue(object value)
        {
            var dataType = DataType.FromId(this.DataTypeId);
            dataType.SetValue(value);
            this.Value = dataType.GetValue();
        }

        public object GetValue()
        {
            return this.Value;
        }
    }
}