
using Roro.Activities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;

namespace Roro
{
    [DataContract]
    public sealed class ElementQuery : DataType<List<Condition>>, IEnumerable<Condition>
    {
        public override string Name => "Element";

        public override DataGridViewCell CellTemplate => new ElementPickerLinkCell();

        public ElementQuery() : this(new List<Condition>()) { }

        public ElementQuery(string xml): this(DataContractSerializerHelper.ToObject<List<Condition>>(xml)) { }

        public ElementQuery(IEnumerable<Condition> conditions) : base(new List<Condition>(conditions)) { }

        public void Add(Condition condition)
        {
            this.Value.Add(condition);
        }

        public void Clear()
        {
            this.Value.Clear();
        }

        public IEnumerator<Condition> GetEnumerator()
        {
            return this.Value.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public override void SetValue(object value)
        {
            throw new NotImplementedException();
        }

        public override string ToExpression()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return DataContractSerializerHelper.ToString(this.Value);
        }

        public static ElementQuery Get(Input<ElementQuery> input)
        {
            var value = input.GetType().GetProperty("Value", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(input).ToString();
            if (value == string.Empty) return null;

            var query = new ElementQuery(DataContractSerializerHelper.ToObject<List<Condition>>(value));
            return query;
        }

    }
}
