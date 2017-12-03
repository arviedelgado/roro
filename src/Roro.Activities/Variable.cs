using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms;

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

    public class VariableColumn : DataGridViewComboBoxColumn
    {
        public VariableColumn()
        {
            this.CellTemplate = new VariableCell();
            this.ValueType = typeof(Variable);
            this.ValueMember = "Id";
            this.DisplayMember = "Name";
            this.DisplayStyleForCurrentCellOnly = true;
        }
    }

    public class VariableCell : DataGridViewComboBoxCell
    {
        public void OnDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            this.DataSource = new List<Variable>(this.DataSource as List<Variable>)
            {
                new Variable<Text>(Variable.Missing) { Id = (Guid)this.Value }
            };
            e.ThrowException = false;
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            if (formattedValue.ToString() == Variable.Missing)
            {
                cellStyle.ForeColor = Color.Red;
            }
            else if (formattedValue.ToString().Length > 0)
            {
                formattedValue = string.Format("[{0}]", formattedValue);
            }
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
        }
    }
}