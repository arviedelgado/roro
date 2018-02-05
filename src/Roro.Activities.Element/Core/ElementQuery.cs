
using Roro.Activities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

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

        public void Add(string name, object value)
        {
            this.Value.Add(new Condition(name, value));
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

    }
}
