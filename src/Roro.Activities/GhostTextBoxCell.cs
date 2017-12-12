using System;
using System.Drawing;
using System.Windows.Forms;

namespace Roro.Activities
{
    public sealed class GhostTextBoxCell : DataGridViewTextBoxCell
    {
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            if (formattedValue.ToString().Length == 0 && !elementState.HasFlag(DataGridViewElementStates.Selected))
            {
                formattedValue = "Enter " + this.OwningColumn.HeaderText.ToLower();
                cellStyle.Font = new Font(cellStyle.Font, FontStyle.Italic);
                cellStyle.ForeColor = Color.FromArgb(150, 150, 150);
            }
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
        }
    }
}