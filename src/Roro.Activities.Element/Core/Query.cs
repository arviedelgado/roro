
using Roro.Activities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Roro
{
    [DataContract]
    public sealed class Query : DataType, IEnumerable<Condition>
    {
        [DataMember]
        private IList<Condition> Conditions { get; set; }

        public Query()
        {
            this.Conditions = new List<Condition>();
        }

        public IEnumerator<Condition> GetEnumerator()
        {
            return this.Conditions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public Query Append(string name, object value)
        {
            this.Conditions.Add(new Condition(name, value));
            return this;
        }

        public override object GetValue()
        {
            return this.Conditions;
        }

        public override void SetValue(object value)
        {

        }

        public override string ToExpression()
        {
            throw new NotImplementedException();
        }
    }
}
