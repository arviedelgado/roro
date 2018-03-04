
using Roro.Activities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Windows.Forms;

namespace Roro
{
    public sealed class ElementQuery : DataType<List<Condition>>, IEnumerable<Condition>
    {
        public override DataGridViewCell CellTemplate => new ElementPickerLinkCell();

        public ElementQuery() : this(new List<Condition>()) { }

        public ElementQuery(string xml): this(XmlSerializerHelper.ToObject<List<Condition>>(xml)) { }

        public ElementQuery(IEnumerable<Condition> conditions) : base(new List<Condition>(conditions)) { }

        public void Add(Condition condition) => this.Value.Add(condition);

        public void Clear() => this.Value.Clear();

        public int Count => this.Value.Count;

        public IEnumerator<Condition> GetEnumerator() => this.Value.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

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
            return XmlSerializerHelper.ToString(this.Value);
        }

        public static ElementQuery Get(Input<ElementQuery> input)
        {
            var value = input.Value;
            if (value == string.Empty) return null;

            var query = new ElementQuery(XmlSerializerHelper.ToObject<List<Condition>>(value));
            return query;
        }
    }
}
