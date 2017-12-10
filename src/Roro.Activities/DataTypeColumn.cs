using System;
using System.Windows.Forms;

namespace Roro.Activities
{
    public sealed class DataTypeColumn : DataGridViewComboBoxColumn
    {
        public DataTypeColumn()
        {
            this.CellTemplate = new DataTypeCell();
            this.ValueType = typeof(DataType);
            this.ValueMember = "Id";
            this.DisplayMember = "Name";
            this.DisplayStyleForCurrentCellOnly = true;
        }
    }
 }
