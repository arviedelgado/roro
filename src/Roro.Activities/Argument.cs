using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace Roro.Activities
{
    [DataContract]
    public abstract class Input
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string DataTypeId { get; set; }

        [DataMember]
        public string Expression { get; set; }
    }

    [DataContract]
    public sealed class Input<T> : Input where T : DataType, new()
    {
        public Input() : this(string.Empty) { }

        public Input(string name)
        {
            this.Name = name;
            this.DataTypeId = new T().Id;
            this.Expression = string.Empty;
        }
    }

    [DataContract]
    public abstract class Output
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string DataTypeId { get; set; }

        [DataMember]
        public Guid VariableId { get; set; }
    }

    [DataContract]
    public sealed class Output<T> : Output where T : DataType, new()
    {
        public Output() : this(string.Empty) { }

        public Output(string name)
        {
            this.Name = name;
            this.DataTypeId = new T().Id;
            this.VariableId = Guid.Empty;
        }
    }

    public class ActivityGrid : DataGridView
    {
        private void InitializeComponent()
        {
            // grid
            this.AllowUserToAddRows = false;
            this.AllowUserToDeleteRows = false;
            this.AutoGenerateColumns = false;
            this.SelectionMode = DataGridViewSelectionMode.CellSelect;
            this.EnableHeadersVisualStyles = false;
            this.ShowEditingIcon = false;
            this.Dock = DockStyle.Fill;
            this.BorderStyle = BorderStyle.None;
            this.BackgroundColor = Color.White;
            this.MultiSelect = false;
            this.DefaultCellStyle.Padding = new Padding(1);

            // columns
            this.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // rows
            this.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            this.RowHeadersVisible = false;
            this.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            // cells
            this.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;
        }

        public ActivityGrid(List<Input> inputs)
        {
            this.InitializeComponent();

            this.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Name",
                DataPropertyName = "Name",
                FillWeight = 35
            });

            this.Columns.Add(new DataTypeColumn
            {
                Name = "Type",
                DataPropertyName = "DataTypeId",
                FillWeight = 15
            });

            this.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Value",
                DataPropertyName = "Expression",
                FillWeight = 50
            });

            this.DataSource = new BindingList<Input>(inputs)
            {
                AllowNew = true,
                AllowEdit = true,
                AllowRemove = true
            };
        }

        public ActivityGrid(List<Output> outputs)
        {
            this.InitializeComponent();

            var variables = new List<Variable>()
            {
                new Variable<Number>(),
                new Variable<Text>("Var1"),
                new Variable<Text>("Var2"),
                new Variable<Text>("Var3")
            };

            this.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Name",
                DataPropertyName = "Name",
                FillWeight = 35
            });

            this.Columns.Add(new DataTypeColumn
            {
                Name = "Type",
                DataPropertyName = "DataTypeId",
                FillWeight = 15
            });

            this.Columns.Add(new VariableColumn
            {
                Name = "Variable",
                DataPropertyName = "VariableId",
                FillWeight = 50,
                DataSource = variables
            });

            this.DataSource = new BindingList<Output>(outputs)
            {
                AllowNew = true,
                AllowEdit = true,
                AllowRemove = true
            };
        }

        protected override void OnDataError(bool displayErrorDialogIfNoHandler, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = true;
            if (this[e.ColumnIndex, e.RowIndex] is VariableCell variableCell)
            {
                variableCell.OnDataError(this, e);
            }
            if (this[e.ColumnIndex, e.RowIndex] is DataTypeCell dataTypeCell)
            {
                dataTypeCell.OnDataError(this, e);
            }
            if (e.ThrowException)
            {
                base.OnDataError(displayErrorDialogIfNoHandler, e);
            }
        }

        protected override void OnCellParsing(DataGridViewCellParsingEventArgs e)
        {
            if (this.CurrentCell.OwningColumn is DataGridViewComboBoxColumn)
            {
                var edit = this.EditingControl as DataGridViewComboBoxEditingControl;
                e.Value = edit.SelectedItem;
                e.ParsingApplied = true;
            }
            base.OnCellParsing(e);
        }

        internal void AddRow(object sender, EventArgs args)
        {
            var grid = this;
            var list = (grid.DataSource as IBindingList);
            if (grid.CurrentCell is DataGridViewCell cell && cell.Selected)
            {
                grid.ClearSelection();
                if (list is BindingList<Input>)
                {
                    list.Insert(cell.RowIndex + 1, new Input<Text>());
                }
                else
                {
                    list.Insert(cell.RowIndex + 1, new Output<Text>());
                }
                grid.CurrentCell = grid[0, cell.RowIndex + 1];
                grid.Focus();
            }
            else
            {
                grid.ClearSelection();
                if (list is BindingList<Input>)
                {
                    list.Add(new Input<Text>());
                }
                else
                {
                    list.Add(new Output<Text>());
                }
                grid.CurrentCell = grid[0, grid.Rows.Count - 1];
                grid.Focus();
            }
        }

        internal void RemoveRow(object sender, EventArgs args)
        {
            var grid = this;
            var list = (grid.DataSource as IBindingList);
            if (grid.CurrentCell is DataGridViewCell cell && cell.Selected)
            {
                grid.ClearSelection();
                list.RemoveAt(cell.RowIndex);
                grid.Focus();
            }
        }

        internal void MoveUpRow(object sender, EventArgs args)
        {
            var grid = this;
            var list = (grid.DataSource as IBindingList);
            if (grid.CurrentCell is DataGridViewCell cell && cell.Selected && cell.RowIndex > 0)
            {
                grid.ClearSelection();
                var rowIndex = cell.RowIndex;
                var columnIndex = cell.ColumnIndex;
                var item = list[cell.RowIndex];
                list.RemoveAt(rowIndex);
                list.Insert(rowIndex - 1, item);
                grid.CurrentCell = grid[columnIndex, rowIndex - 1];
                grid.Focus();
            }
        }

        internal void MoveDownRow(object sender, EventArgs args)
        {
            var grid = this;
            var list = (grid.DataSource as IBindingList);
            if (grid.CurrentCell is DataGridViewCell cell && cell.Selected && cell.RowIndex < grid.Rows.Count - 1)
            {
                grid.ClearSelection();
                var rowIndex = cell.RowIndex;
                var columnIndex = cell.ColumnIndex;
                var item = list[cell.RowIndex];
                list.RemoveAt(rowIndex);
                list.Insert(rowIndex + 1, item);
                grid.CurrentCell = grid[columnIndex, rowIndex + 1];
                grid.Focus();
            }
        }
    }
}
