
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Runtime.Serialization;

namespace Roro
{
    [DataContract]
    public sealed class Condition
    {
        [DataMember]
        public bool Enabled { get; set; }

        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public object Value { get; set; }

        public Condition(string name, object value)
        {
            this.Name = name;
            this.Value = value;
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
