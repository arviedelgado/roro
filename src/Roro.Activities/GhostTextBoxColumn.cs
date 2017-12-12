using System;
using System.Windows.Forms;

namespace Roro.Activities
{
    public sealed class GhostTextBoxColumn : DataGridViewTextBoxColumn
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

        public GhostTextBoxColumn()
        {
            this.CellTemplate = new GhostTextBoxCell();
        }
    }
}