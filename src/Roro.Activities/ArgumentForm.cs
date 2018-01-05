
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace Roro.Activities
{
    public sealed class ArgumentForm : Form
    {
        private Panel argumentPanel;
        private DataGridView argumentDataGridView;
        private Panel argumentButtonsPanel;
        private Button argumentMoveUpButton;
        private Button argumentMoveDownButton;
        private Button argumentRemoveButton;
        private Label argumentLabel;
        private Button argumentAddButton;

        private void InitializeComponent()
        {
            this.argumentPanel = new System.Windows.Forms.Panel();
            this.argumentDataGridView = new System.Windows.Forms.DataGridView();
            this.argumentLabel = new System.Windows.Forms.Label();
            this.argumentButtonsPanel = new System.Windows.Forms.Panel();
            this.argumentMoveUpButton = new System.Windows.Forms.Button();
            this.argumentMoveDownButton = new System.Windows.Forms.Button();
            this.argumentRemoveButton = new System.Windows.Forms.Button();
            this.argumentAddButton = new System.Windows.Forms.Button();
            this.argumentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.argumentDataGridView)).BeginInit();
            this.argumentButtonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // argumentPanel
            // 
            this.argumentPanel.BackColor = System.Drawing.Color.White;
            this.argumentPanel.Controls.Add(this.argumentDataGridView);
            this.argumentPanel.Controls.Add(this.argumentLabel);
            this.argumentPanel.Controls.Add(this.argumentButtonsPanel);
            this.argumentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.argumentPanel.Location = new System.Drawing.Point(0, 0);
            this.argumentPanel.Name = "argumentPanel";
            this.argumentPanel.Size = new System.Drawing.Size(344, 311);
            this.argumentPanel.TabIndex = 0;
            // 
            // argumentDataGridView
            // 
            this.argumentDataGridView.AllowUserToAddRows = false;
            this.argumentDataGridView.AllowUserToDeleteRows = false;
            this.argumentDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.argumentDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.argumentDataGridView.BackgroundColor = System.Drawing.Color.White;
            this.argumentDataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.argumentDataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.argumentDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.argumentDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.argumentDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.argumentDataGridView.EnableHeadersVisualStyles = false;
            this.argumentDataGridView.GridColor = System.Drawing.SystemColors.ControlDarkDark;
            this.argumentDataGridView.Location = new System.Drawing.Point(0, 25);
            this.argumentDataGridView.MultiSelect = false;
            this.argumentDataGridView.Name = "argumentDataGridView";
            this.argumentDataGridView.RowHeadersVisible = false;
            this.argumentDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.argumentDataGridView.Size = new System.Drawing.Size(344, 263);
            this.argumentDataGridView.TabIndex = 2;
            this.argumentDataGridView.TabStop = false;
            // 
            // argumentLabel
            // 
            this.argumentLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.argumentLabel.Font = new System.Drawing.Font("Segoe UI Light", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.argumentLabel.Location = new System.Drawing.Point(0, 0);
            this.argumentLabel.Name = "argumentLabel";
            this.argumentLabel.Size = new System.Drawing.Size(344, 25);
            this.argumentLabel.TabIndex = 3;
            this.argumentLabel.Text = "argumentLabel";
            // 
            // argumentButtonsPanel
            // 
            this.argumentButtonsPanel.Controls.Add(this.argumentMoveUpButton);
            this.argumentButtonsPanel.Controls.Add(this.argumentMoveDownButton);
            this.argumentButtonsPanel.Controls.Add(this.argumentRemoveButton);
            this.argumentButtonsPanel.Controls.Add(this.argumentAddButton);
            this.argumentButtonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.argumentButtonsPanel.Location = new System.Drawing.Point(0, 288);
            this.argumentButtonsPanel.Name = "argumentButtonsPanel";
            this.argumentButtonsPanel.Size = new System.Drawing.Size(344, 23);
            this.argumentButtonsPanel.TabIndex = 1;
            // 
            // argumentMoveUpButton
            // 
            this.argumentMoveUpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.argumentMoveUpButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.argumentMoveUpButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.argumentMoveUpButton.Location = new System.Drawing.Point(178, 0);
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
            this.argumentMoveDownButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.argumentMoveDownButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.argumentMoveDownButton.Location = new System.Drawing.Point(264, 0);
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
            this.argumentRemoveButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.argumentRemoveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.argumentRemoveButton.Location = new System.Drawing.Point(86, 0);
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
            this.argumentAddButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDarkDark;
            this.argumentAddButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.argumentAddButton.Location = new System.Drawing.Point(0, 0);
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
            this.ClientSize = new System.Drawing.Size(344, 311);
            this.Controls.Add(this.argumentPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ArgumentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.ArgumentMasterForm_Load);
            this.argumentPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.argumentDataGridView)).EndInit();
            this.argumentButtonsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private ArgumentForm() => this.InitializeComponent();

        public static TableLayoutPanel Create(Page page, Node node)
        {
            var activity = node.Activity;
            var variables = page.Variables;

            var argumentPanels = new List<Panel>();
            if (ArgumentForm.Create<InArgument>(page, node) is Panel inArgumentPanel) argumentPanels.Add(inArgumentPanel);
            if (ArgumentForm.Create<OutArgument>(page, node) is Panel outArgumentPanel) argumentPanels.Add(outArgumentPanel);
            if (ArgumentForm.Create<InOutArgument>(page, node) is Panel inOutArgumentPanel) argumentPanels.Add(inOutArgumentPanel);

            var argumentTableLayoutPanel = new TableLayoutPanel
            {
                ColumnCount = 1,
                RowCount = argumentPanels.Count,
                Dock = DockStyle.Fill,
                
            };

            argumentTableLayoutPanel.RowStyles.Clear();
            foreach (var argumentPanel in argumentPanels)
            {
                argumentTableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F / argumentPanels.Count));
                argumentTableLayoutPanel.Controls.Add(argumentPanel, 0, argumentTableLayoutPanel.RowStyles.Count - 1);
            }

            return argumentTableLayoutPanel;
        }

        public static Panel Create<T>(Page page, Node node) where T : Argument
        {
            var activity = node.Activity;
            var variables = page.Variables;
            var dataTypes = DataType.GetCommonTypes();
            var arguments = activity.GetArguments<T>();

            if (arguments == null) return null;

            var form = new ArgumentForm();
            form.CreateColumnsForArguments<T>(dataTypes, variables);
            form.argumentButtonsPanel.Visible = activity.AllowUserToEditArgumentRowList;

            var grid = form.argumentDataGridView;
            grid.Columns[0].ReadOnly = !activity.AllowUserToEditArgumentColumn1;
            grid.Columns[1].ReadOnly = !activity.AllowUserToEditArgumentColumn2;
            grid.Columns[2].ReadOnly = !activity.AllowUserToEditArgumentColumn3;

            grid.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(grid, true);

            grid.Tag = new BindingList<T>(arguments); // see AllowUserToEditArgumentRowList

            UpdateRowList(grid);

            grid.CellValueChanged += (sender, e) =>
            {
                var value = grid[e.ColumnIndex, e.RowIndex].Value;
                if (arguments[e.RowIndex] is InArgument inArgument)
                {
                    if (e.ColumnIndex == 0) inArgument.Name = (string)value;
                    if (e.ColumnIndex == 1) inArgument.DataTypeId = (string)value;
                    if (e.ColumnIndex == 2) inArgument.Expression = (string)value;
                }
                else if (arguments[e.RowIndex] is OutArgument outArgument)
                {
                    if (e.ColumnIndex == 0) outArgument.Name = (string)value;
                    if (e.ColumnIndex == 1) outArgument.DataTypeId = (string)value;
                    if (e.ColumnIndex == 2) outArgument.VariableId = (Guid)value;
                }
                else if (arguments[e.RowIndex] is InOutArgument inOutArgument)
                {
                    if (e.ColumnIndex == 0) inOutArgument.DataTypeId = (string)value;
                    if (e.ColumnIndex == 1) inOutArgument.VariableId = (Guid)value;
                    if (e.ColumnIndex == 2) inOutArgument.Expression = (string)value;
                }
                else
                {
                    throw new NotSupportedException();
                }
            };

            grid.CurrentCellDirtyStateChanged += (sender, e) =>
            {
                if (grid.IsCurrentCellDirty)
                {
                    grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            };

            return form.argumentPanel;
        }

        #region CreateColumnsForArguments

        private void CreateColumnsForArguments<T>(List<DataType> dataTypes, List<Variable> variables)
        {
            variables = new List<Variable>(variables);
            variables.Insert(0, new Variable<Text>()); // add blank for dropdown
            if (typeof(T) == typeof(InArgument))
            {
                this.argumentLabel.Text = "Inputs";
                this.CreateColumnsForInArguments(dataTypes);
            }
            else if (typeof(T) == typeof(OutArgument))
            {
                this.argumentLabel.Text = "Outputs";
                this.CreateColumnsForOutArguments(dataTypes, variables);
            }
            else if (typeof(T) == typeof(InOutArgument))
            {
                this.argumentLabel.Text = "Inputs";
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
            grid.Columns.Add(new GhostTextBoxColumn()
            {
                Name = "Name",
                FillWeight = 35,
            });
            grid.Columns.Add(new DataTypeColumn()
            {
                Name = "Type",
                FillWeight = 15,
                DataSource = dataTypes
            });
            grid.Columns.Add(new GhostTextBoxColumn()
            {
                Name = "Expression",
                FillWeight = 50,
            });
        }

        private void CreateColumnsForOutArguments(List<DataType> dataTypes, List<Variable> variables)
        {
            var grid = this.argumentDataGridView;
            grid.AutoGenerateColumns = false;
            grid.Columns.Clear();
            grid.Columns.Add(new GhostTextBoxColumn()
            {
                Name = "Name",
                FillWeight = 35,
            });
            grid.Columns.Add(new DataTypeColumn()
            {
                Name = "Type",
                FillWeight = 15,
                DataSource = dataTypes
            });
            grid.Columns.Add(new VariableColumn()
            {
                Name = "Variable",
                FillWeight = 50,
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
                DataSource = dataTypes
            });
            grid.Columns.Add(new VariableColumn()
            {
                Name = "Variable",
                FillWeight = 35,
                DataSource = variables
            });
            grid.Columns.Add(new GhostTextBoxColumn()
            {
                Name = "Expression",
                FillWeight = 50,
            });
        }

        #endregion

        #region ArgumentButtons

        private static void UpdateRowList(DataGridView grid)
        {
            var firstDisplayedScrollingRowIndex = grid.FirstDisplayedScrollingRowIndex;

            grid.Rows.Clear();
            foreach (var argument in grid.Tag as IBindingList)
            {
                if (argument is InArgument inArgument)
                {
                    grid.Rows.Add(inArgument.Name, inArgument.DataTypeId, inArgument.Expression);
                }
                else if (argument is OutArgument outArgument)
                {
                    grid.Rows.Add(outArgument.Name, outArgument.DataTypeId, outArgument.VariableId);
                }
                else if (argument is InOutArgument inOutArgument)
                {
                    grid.Rows.Add(inOutArgument.DataTypeId, inOutArgument.VariableId, inOutArgument.Expression);
                }
                else
                {
                    throw new NotSupportedException();
                }
            }

            if (grid.Rows.Count > 0)
            {
                grid.FirstDisplayedScrollingRowIndex = Math.Max(Math.Min(firstDisplayedScrollingRowIndex, grid.Rows.Count - 1), 0);
            }
        }

        private void ArgumentAddButton_Click(object sender, EventArgs e)
        {
            var grid = this.argumentDataGridView;
            var list = grid.Tag as IBindingList;
            if (grid.CurrentCell is DataGridViewCell cell && cell.Selected)
            {
                grid.ClearSelection();
                if (list is BindingList<InArgument> && new InArgument() is InArgument inArgument)
                {
                    list.Insert(cell.RowIndex + 1, inArgument);
                }
                else if (list is BindingList<OutArgument> && new OutArgument() is OutArgument outArgument)
                {
                    list.Insert(cell.RowIndex + 1, outArgument);
                }
                else if (list is BindingList<InOutArgument> && new InOutArgument() is InOutArgument inOutArgument)
                {
                    list.Insert(cell.RowIndex + 1, inOutArgument);
                }
                else
                {
                    throw new NotSupportedException();
                }
                var rowIndex = cell.RowIndex;
                var columnIndex = 0; // cell.ColumnIndex;
                UpdateRowList(grid);
                grid.CurrentCell = grid[columnIndex, rowIndex + 1];
                grid.Focus();
            }
            else
            {
                grid.ClearSelection();
                if (list is BindingList<InArgument>)
                {
                    list.Add(new InArgument());
                }
                else if (list is BindingList<OutArgument>)
                {
                    list.Add(new OutArgument());
                }
                else if (list is BindingList<InOutArgument>)
                {
                    list.Add(new InOutArgument());
                }
                else
                {
                    throw new NotSupportedException();
                }
                UpdateRowList(grid);
                grid.CurrentCell = grid[0, grid.Rows.Count - 1];
                grid.Focus();
            }
        }

        private void ArgumentRemoveButton_Click(object sender, EventArgs e)
        {
            var grid = this.argumentDataGridView;
            var list = grid.Tag as IBindingList;
            if (grid.CurrentCell is DataGridViewCell cell && cell.Selected)
            {
                grid.ClearSelection();
                var rowIndex = cell.RowIndex;
                var columnIndex = cell.ColumnIndex;
                list.RemoveAt(cell.RowIndex);
                UpdateRowList(grid);
                if (grid.Rows.Count > 0)
                {
                    grid.CurrentCell = grid[columnIndex, Math.Min(rowIndex, grid.Rows.Count - 1)];
                }
                grid.Focus();
            }
        }

        private void ArgumentMoveUpButton_Click(object sender, EventArgs e)
        {
            var grid = this.argumentDataGridView;
            var list = grid.Tag as IBindingList;
            if (grid.CurrentCell is DataGridViewCell cell && cell.Selected && cell.RowIndex > 0)
            {
                grid.ClearSelection();
                var rowIndex = cell.RowIndex;
                var columnIndex = cell.ColumnIndex;
                var item = list[cell.RowIndex];
                list.RemoveAt(rowIndex);
                list.Insert(rowIndex - 1, item);
                UpdateRowList(grid);
                grid.CurrentCell = grid[columnIndex, rowIndex - 1];
                grid.Focus();
            }
        }

        private void ArgumentMoveDownButton_Click(object sender, EventArgs e)
        {
            var grid = this.argumentDataGridView;
            var list = grid.Tag as IBindingList;
            if (grid.CurrentCell is DataGridViewCell cell && cell.Selected && cell.RowIndex < grid.Rows.Count - 1)
            {
                grid.ClearSelection();
                var rowIndex = cell.RowIndex;
                var columnIndex = cell.ColumnIndex;
                var item = list[cell.RowIndex];
                list.RemoveAt(rowIndex);
                list.Insert(rowIndex + 1, item);
                UpdateRowList(grid);
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

        private void ArgumentMasterForm_Load(object sender, EventArgs e)
        {

        }
    }
}