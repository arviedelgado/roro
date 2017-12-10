using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Roro.Activities
{
    public sealed class VariableCell : DataGridViewComboBoxCell
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