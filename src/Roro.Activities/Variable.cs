using System;
using System.Runtime.Serialization;

namespace Roro.Activities
{
    [DataContract]
    public sealed class Variable
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string DataTypeId { get; set; }

        [DataMember(Name = "Value")]
        private object _Value { get; set; }

        public object Value
        {
            get
            {
                var dataType = DataType.FromId(this.DataTypeId);
                dataType.SetValue(this._Value);
                return this._Value;
            }
            set
            {
                var dataType = DataType.FromId(this.DataTypeId);
                dataType.SetValue(value);
                this._Value = dataType.GetValue();
            }
        }
 
            
        public Variable()
        {
            this.Name = "<Enter name>";
            this.DataTypeId = new Text().Id;
            this.Value = string.Empty;
        }

        public static readonly Variable Empty = new Variable() { Name = string.Empty };
    }
}