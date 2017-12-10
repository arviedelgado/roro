using System;
using System.Windows.Forms;

namespace Roro.Activities
{
    public sealed class VariableColumn : DataGridViewComboBoxColumn
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
}