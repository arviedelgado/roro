using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Roro.Activities
{
    public sealed class ArgumentForm : Form
    {
        private Panel argumentPanel;
        private TabControl argumentTabControl;
        private DataGridView argumentDataGridView;
        private Panel argumentButtonsPanel;
        private Button argumentMoveUpButton;
        private Button argumentMoveDownButton;
        private Button argumentRemoveButton;
        private Button argumentAddButton;
        private TabPage argumentTabPage;

        private void InitializeComponent()
        {
            this.argumentPanel = new System.Windows.Forms.Panel();
            this.argumentTabControl = new System.Windows.Forms.TabControl();
            this.argumentTabPage = new System.Windows.Forms.TabPage();
            this.argumentDataGridView = new System.Windows.Forms.DataGridView();
            this.argumentButtonsPanel = new System.Windows.Forms.Panel();
            this.argumentMoveUpButton = new System.Windows.Forms.Button();
            this.argumentMoveDownButton = new System.Windows.Forms.Button();
            this.argumentRemoveButton = new System.Windows.Forms.Button();
            this.argumentAddButton = new System.Windows.Forms.Button();
            this.argumentPanel.SuspendLayout();
            this.argumentTabControl.SuspendLayout();
            this.argumentTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.argumentDataGridView)).BeginInit();
            this.argumentButtonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // argumentPanel
            // 
            this.argumentPanel.Controls.Add(this.argumentTabControl);
            this.argumentPanel.Controls.Add(this.argumentButtonsPanel);
            this.argumentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.argumentPanel.Location = new System.Drawing.Point(0, 0);
            this.argumentPanel.Name = "argumentPanel";
            this.argumentPanel.Size = new System.Drawing.Size(434, 311);
            this.argumentPanel.TabIndex = 0;
            // 
            // argumentTabControl
            // 
            this.argumentTabControl.Controls.Add(this.argumentTabPage);
            this.argumentTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.argumentTabControl.Location = new System.Drawing.Point(0, 0);
            this.argumentTabControl.Name = "argumentTabControl";
            this.argumentTabControl.SelectedIndex = 0;
            this.argumentTabControl.Size = new System.Drawing.Size(434, 282);
            this.argumentTabControl.TabIndex = 0;
            this.argumentTabControl.TabStop = false;
            // 
            // argumentTabPage
            // 
            this.argumentTabPage.Controls.Add(this.argumentDataGridView);
            this.argumentTabPage.Location = new System.Drawing.Point(4, 24);
            this.argumentTabPage.Name = "argumentTabPage";
            this.argumentTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.argumentTabPage.Size = new System.Drawing.Size(426, 254);
            this.argumentTabPage.TabIndex = 0;
            this.argumentTabPage.Text = "argumentTabPage";
            this.argumentTabPage.UseVisualStyleBackColor = true;
            // 
            // argumentDataGridView
            // 
            this.argumentDataGridView.AllowUserToAddRows = false;
            this.argumentDataGridView.AllowUserToDeleteRows = false;
            this.argumentDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.argumentDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.argumentDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.argumentDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.argumentDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.argumentDataGridView.EnableHeadersVisualStyles = false;
            this.argumentDataGridView.Location = new System.Drawing.Point(3, 3);
            this.argumentDataGridView.MultiSelect = false;
            this.argumentDataGridView.Name = "argumentDataGridView";
            this.argumentDataGridView.RowHeadersVisible = false;
            this.argumentDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.argumentDataGridView.Size = new System.Drawing.Size(420, 248);
            this.argumentDataGridView.TabIndex = 0;
            this.argumentDataGridView.TabStop = false;
            this.argumentDataGridView.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.ArgumentDataGridView_CellParsing);
            this.argumentDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.ArgumentDataGridView_DataError);
            // 
            // argumentButtonsPanel
            // 
            this.argumentButtonsPanel.Controls.Add(this.argumentMoveUpButton);
            this.argumentButtonsPanel.Controls.Add(this.argumentMoveDownButton);
            this.argumentButtonsPanel.Controls.Add(this.argumentRemoveButton);
            this.argumentButtonsPanel.Controls.Add(this.argumentAddButton);
            this.argumentButtonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.argumentButtonsPanel.Location = new System.Drawing.Point(0, 282);
            this.argumentButtonsPanel.Name = "argumentButtonsPanel";
            this.argumentButtonsPanel.Size = new System.Drawing.Size(434, 29);
            this.argumentButtonsPanel.TabIndex = 1;
            // 
            // argumentMoveUpButton
            // 
            this.argumentMoveUpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.argumentMoveUpButton.Location = new System.Drawing.Point(265, 3);
            this.argumentMoveUpButton.Name = "argumentMoveUpButton";
            this.argumentMoveUpButton.Size = new System.Drawing.Size(80, 23);
            this.argumentMoveUpButton.TabIndex = 0;
            this.argumentMoveUpButton.TabStop = false;
            this.argumentMoveUpButton.Text = "Move Up";
            this.argumentMoveUpButton.UseVisualStyleBackColor = true;
            this.argumentMoveUpButton.Click += new System.EventHandler(this.ArgumentMoveUpButton_Click);
            // 
            // argumentMoveDownButton
            // 
            this.argumentMoveDownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.argumentMoveDownButton.Location = new System.Drawing.Point(351, 3);
            this.argumentMoveDownButton.Name = "argumentMoveDownButton";
            this.argumentMoveDownButton.Size = new System.Drawing.Size(80, 23);
            this.argumentMoveDownButton.TabIndex = 1;
            this.argumentMoveDownButton.TabStop = false;
            this.argumentMoveDownButton.Text = "Move Down";
            this.argumentMoveDownButton.UseVisualStyleBackColor = true;
            this.argumentMoveDownButton.Click += new System.EventHandler(this.ArgumentMoveDownButton_Click);
            // 
            // argumentRemoveButton
            // 
            this.argumentRemoveButton.Location = new System.Drawing.Point(90, 3);
            this.argumentRemoveButton.Name = "argumentRemoveButton";
            this.argumentRemoveButton.Size = new System.Drawing.Size(80, 23);
            this.argumentRemoveButton.TabIndex = 2;
            this.argumentRemoveButton.TabStop = false;
            this.argumentRemoveButton.Text = "Remove";
            this.argumentRemoveButton.UseVisualStyleBackColor = true;
            this.argumentRemoveButton.Click += new System.EventHandler(this.ArgumentRemoveButton_Click);
            // 
            // argumentAddButton
            // 
            this.argumentAddButton.Location = new System.Drawing.Point(4, 3);
            this.argumentAddButton.Name = "argumentAddButton";
            this.argumentAddButton.Size = new System.Drawing.Size(80, 23);
            this.argumentAddButton.TabIndex = 3;
            this.argumentAddButton.TabStop = false;
            this.argumentAddButton.Text = "Add";
            this.argumentAddButton.UseVisualStyleBackColor = true;
            this.argumentAddButton.Click += new System.EventHandler(this.ArgumentAddButton_Click);
            // 
            // ArgumentForm
            // 
            this.ClientSize = new System.Drawing.Size(434, 311);
            this.Controls.Add(this.argumentPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ArgumentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.ArgumentMasterForm_Load);
            this.argumentPanel.ResumeLayout(false);
            this.argumentTabControl.ResumeLayout(false);
            this.argumentTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.argumentDataGridView)).EndInit();
            this.argumentButtonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private ArgumentForm() => this.InitializeComponent();

        public static Panel Create<T>(Activity activity, List<T> arguments, List<Variable> variables) where T : Argument
        {
            var dataTypes = DataType.GetCommonTypes();
            variables = new List<Variable>()
            {
                new Variable<Text>("Text1"),
                new Variable<Text>("Text2"),
                new Variable<Number>("Num1"),
                new Variable<Number>("Num2"),
                new Variable<Number>("Num3"),
                new Variable<Flag>("Flag1"),
                new Variable<DateTime>("DateTime1"),
                new Variable<Collection>("Collection1"),
            };

            variables.Insert(0, new Variable<Text>());
           
            var form = new ArgumentForm();

            form.CreateColumnsForArguments<T>(dataTypes, variables);
            form.argumentButtonsPanel.Visible = activity.AllowUserToEditArgumentRowList;

            var grid = form.argumentDataGridView;
            grid.Columns[0].ReadOnly = !activity.AllowUserToEditArgumentColumn1;
            grid.Columns[1].ReadOnly = !activity.AllowUserToEditArgumentColumn2;
            grid.Columns[2].ReadOnly = !activity.AllowUserToEditArgumentColumn3;
            form.argumentPanel.ParentChanged += (sender, e) =>
            {
                grid.DataSource = new BindingList<T>(arguments); // perfect.
            };
            
            form.argumentTabPage.Text = typeof(T).Name;
            
            return form.argumentPanel;
        }

        private void ArgumentMasterForm_Load(object sender, EventArgs e)
        {

        }

        #region CreateColumnsForArguments

        private void CreateColumnsForArguments<T>(List<DataType> dataTypes, List<Variable> variables)
        {
            if (typeof(T) == typeof(InArgument))
            {
                this.CreateColumnsForInArguments(dataTypes);
            }
            else if (typeof(T) == typeof(OutArgument))
            {
                this.CreateColumnsForOutArguments(dataTypes, variables);
            }
            else if (typeof(T) == typeof(InOutArgument))
            {
                this.CreateColumnsForInOutArguments(dataTypes, variables);
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        private void CreateColumnsForInArguments(List<DataType> dataTypes)
        {
            var grid = this.argumentDataGridView;
            grid.AutoGenerateColumns = false;
            grid.Columns.Clear();
            grid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Name",
                FillWeight = 35,
                DataPropertyName = "Name",
            });
            grid.Columns.Add(new DataTypeColumn()
            {
                Name = "Type",
                FillWeight = 15,
                DataPropertyName = "DataTypeId",
                DataSource = dataTypes
            });
            grid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Expression",
                FillWeight = 50,
                DataPropertyName = "Expression",
            });
        }

        private void CreateColumnsForOutArguments(List<DataType> dataTypes, List<Variable> variables)
        {
            var grid = this.argumentDataGridView;
            grid.AutoGenerateColumns = false;
            grid.Columns.Clear();
            grid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Name",
                FillWeight = 35,
                DataPropertyName = "Name",
            });
            grid.Columns.Add(new DataTypeColumn()
            {
                Name = "Type",
                FillWeight = 15,
                DataPropertyName = "DataTypeId",
                DataSource = dataTypes
            });
            grid.Columns.Add(new VariableColumn()
            {
                Name = "Variable",
                FillWeight = 50,
                DataPropertyName = "VariableId",
                DataSource = variables
            });
        }

        private void CreateColumnsForInOutArguments(List<DataType> dataTypes, List<Variable> variables)
        {
            var grid = this.argumentDataGridView;
            grid.AutoGenerateColumns = false;
            grid.Columns.Clear();
            grid.Columns.Add(new DataTypeColumn()
            {
                Name = "Type",
                FillWeight = 15,
                DataPropertyName = "DataTypeId",
                DataSource = dataTypes
            });
            grid.Columns.Add(new VariableColumn()
            {
                Name = "Variable",
                FillWeight = 35,
                DataPropertyName = "VariableId",
                DataSource = variables
            });
            grid.Columns.Add(new DataGridViewTextBoxColumn()
            {
                Name = "Expression",
                FillWeight = 50,
                DataPropertyName = "Expression",
            });
        }

        #endregion

        #region ArgumentButtons

        private void ArgumentAddButton_Click(object sender, EventArgs e)
        {
            var grid = this.argumentDataGridView;
            var list = (grid.DataSource as IBindingList);
            if (grid.CurrentCell is DataGridViewCell cell && cell.Selected)
            {
                grid.ClearSelection();
                if (list is BindingList<InArgument>)
                {
                    list.Insert(cell.RowIndex + 1, new InArgument<Text>());
                }
                else if (list is BindingList<OutArgument>)
                {
                    list.Insert(cell.RowIndex + 1, new OutArgument<Text>());
                }
                else if (list is BindingList<InOutArgument>)
                {
                    list.Insert(cell.RowIndex + 1, new InOutArgument<Text>());
                }
                else
                {
                    throw new NotSupportedException();
                }
                grid.CurrentCell = grid[0, cell.RowIndex + 1];
                grid.Focus();
            }
            else
            {
                grid.ClearSelection();
                if (list is BindingList<InArgument>)
                {
                    list.Add(new InArgument<Text>());
                }
                else if (list is BindingList<OutArgument>)
                {
                    list.Add(new OutArgument<Text>());
                }
                else if (list is BindingList<InOutArgument>)
                {
                    list.Add(new InOutArgument<Text>());
                }
                else
                {
                    throw new NotSupportedException();
                }
                grid.CurrentCell = grid[0, grid.Rows.Count - 1];
                grid.Focus();
            }
        }

        private void ArgumentRemoveButton_Click(object sender, EventArgs e)
        {
            var grid = this.argumentDataGridView;
            var list = (grid.DataSource as IBindingList);
            if (grid.CurrentCell is DataGridViewCell cell && cell.Selected)
            {
                grid.ClearSelection();
                list.RemoveAt(cell.RowIndex);
                grid.Focus();
            }
        }

        private void ArgumentMoveUpButton_Click(object sender, EventArgs e)
        {
            var grid = this.argumentDataGridView;
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

        private void ArgumentMoveDownButton_Click(object sender, EventArgs e)
        {
            var grid = this.argumentDataGridView;
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

        #endregion

        #region ArgumentDataGridView

        private void ArgumentDataGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            var grid = sender as DataGridView;
            if (grid.CurrentCell.OwningColumn is DataGridViewComboBoxColumn)
            {
                var edit = grid.EditingControl as DataGridViewComboBoxEditingControl;
                e.Value = edit.SelectedItem;
                e.ParsingApplied = true;
            }
        }

        private void ArgumentDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            var grid = sender as DataGridView;
            if (grid[e.ColumnIndex, e.RowIndex] is VariableCell variableCell)
            {
                variableCell.OnDataError(this, e);
            }
            if (grid[e.ColumnIndex, e.RowIndex] is DataTypeCell dataTypeCell)
            {
                dataTypeCell.OnDataError(this, e);
            }
        }

        #endregion

    }
}