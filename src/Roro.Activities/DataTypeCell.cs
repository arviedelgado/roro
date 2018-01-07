using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Roro.Activities
{
    public sealed class DataTypeCell : DataGridViewComboBoxCell
    {
        public void OnDataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            this.DataSource = new List<DataType>(this.DataSource as List<DataType>)
            {
                DataType.FromId(this.Value.ToString())
            };
            e.ThrowException = false;
        }
    }
}
