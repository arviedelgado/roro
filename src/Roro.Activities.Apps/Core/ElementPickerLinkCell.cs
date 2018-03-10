using System.Drawing;
using System.Windows.Forms;

namespace Roro.Activities.Apps
{
    public sealed class ElementPickerLinkCell : DataGridViewLinkCell
    {
        public ElementPickerLinkCell()
        {
            this.TrackVisitedState = false;
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            if (formattedValue.ToString().Length == 0)
            {
                formattedValue = "Pick element";
                cellStyle.Font = new Font(cellStyle.Font, FontStyle.Italic);
            }
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
        }

        protected override void OnClick(DataGridViewCellEventArgs e)
        {
            using (var f = new ElementPickerForm(this.Value))
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    this.Value = f.Query.ToString();
                }
                this.DataGridView.FindForm().Activate();
            }
        }
    }
}