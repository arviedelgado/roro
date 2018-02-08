using System;
using System.Windows.Forms;

namespace Roro.Activities
{
    public sealed class LabelColumn : DataGridViewTextBoxColumn
    {
        public override bool ReadOnly => true;

        public LabelColumn()
        {
            this.CellTemplate = new LabelCell();
            this.SortMode = DataGridViewColumnSortMode.NotSortable;
        }
    }
}