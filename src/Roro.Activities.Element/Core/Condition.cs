
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Runtime.Serialization;

namespace Roro
{
    [DataContract]
    public sealed class Condition
    {
        public bool Use
        {
            get => this.Required || this.Enabled;
            set => this.Enabled = value;
        }

        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public object Value { get; set; }

        [DataMember]
        public bool Enabled { get; private set; }

        [DataMember]
        public bool Required { get; private set; }

        public Condition(string name, object value, bool enabled, bool required)
        {
            this.Name = name;
            this.Value = value;
            this.Enabled = enabled;
            this.Required = required;
        }

        public bool Compare(object otherValue, Type otherType)
        {
            var value = this.Value;
            if (otherType == typeof(string))
            {
                return LikeOperator.LikeString(
                    value.ToString(),
                    otherValue.ToString().Replace("[", "[[]").Replace("#", "[#]").Replace("?", "[?]"),
                    Microsoft.VisualBasic.CompareMethod.Binary);
            }
            else
            {
                return otherValue.Equals(value);
            }
        }
    }
}
