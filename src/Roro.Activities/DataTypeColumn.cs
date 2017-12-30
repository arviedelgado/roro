using System;
using System.Windows.Forms;

namespace Roro.Activities
{
    public sealed class DataTypeColumn : DataGridViewComboBoxColumn
    {
        public override bool ReadOnly
        {
            get => base.ReadOnly;
            set
            {
                if (!value) this.HeaderCell.Style.ForeColor = System.Drawing.Color.Blue;
                base.ReadOnly = value;
            }
        }

        public DataTypeColumn()
        {
            this.CellTemplate = new DataTypeCell();
            this.ValueMember = "Id";
            this.DisplayMember = "Name";
            this.DisplayStyleForCurrentCellOnly = true;
        }
    }
 }
