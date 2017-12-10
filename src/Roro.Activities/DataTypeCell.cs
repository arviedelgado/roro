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
                DataType.GetTypeById(this.Value.ToString())
            };
            e.ThrowException = false;
        }

        protected override void OnDataGridViewChanged()
        {
            // Weird.

            // A NullReferenceException occur
            // when calling ArgumentForm.Create<T>(Activity activity, ...) method
            // where activity is an instance of ProcessNodeActivity or DecisionNodeActivity.
            // Other activities (Start-, End-, Preparation-, ...) does not throw this error.

            // To handle, this for now, override OnDataGridViewChanged (this) method.

            // System.NullReferenceException: Object reference not set to an instance of an object.
            // at System.Windows.Forms.DataGridViewComboBoxCell.InitializeDisplayMemberPropertyDescriptor(String displayMember)
            // at System.Windows.Forms.DataGridViewComboBoxCell.OnDataGridViewChanged()
            // at System.Windows.Forms.DataGridViewRowCollection.get_Item(Int32 index)
            // at System.Windows.Forms.DataGridView.AutoResizeRowInternal(Int32 rowIndex, DataGridViewAutoSizeRowMode autoSizeRowMode, Boolean fixedWidth, Boolean internalAutosizing)
            // at System.Windows.Forms.DataGridView.AdjustShrinkingRows(DataGridViewAutoSizeRowsMode autoSizeRowsMode, Boolean fixedWidth, Boolean internalAutosizing)
            // at System.Windows.Forms.DataGridView.PerformLayoutPrivate(Boolean useRowShortcut, Boolean computeVisibleRows, Boolean invalidInAdjustFillingColumns, Boolean repositionEditingControl)
            // at System.Windows.Forms.DataGridView.OnHandleCreated(EventArgs e)
            // at System.Windows.Forms.Control.WmCreate(Message & m)
            // at System.Windows.Forms.Control.WndProc(Message & m)
            // at System.Windows.Forms.DataGridView.WndProc(Message & m)
            // at System.Windows.Forms.Control.ControlNativeWindow.OnMessage(Message & m)
            // at System.Windows.Forms.Control.ControlNativeWindow.WndProc(Message & m)
            // at System.Windows.Forms.NativeWindow.Callback(IntPtr hWnd, Int32 msg, IntPtr wparam, IntPtr lparam)
        }
    }
}
