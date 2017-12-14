using Roro.Workflow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Roro.Activities
{
    public sealed class ArgumentForm : Form
    {
        private Panel argumentPanel;
        private DataGridView argumentDataGridView;
        private TabControl argumentTabControl;
        private Panel argumentButtonsPanel;
        private Button argumentMoveUpButton;
        private Button argumentMoveDownButton;
        private Button argumentRemoveButton;
        private Button argumentAddButton;
        private TabPage argumentTabPage;

        private void InitializeComponent()
        {
            this.argumentPanel = new System.Windows.Forms.Panel();
            this.argumentDataGridView = new System.Windows.Forms.DataGridView();
            this.argumentTabControl = new System.Windows.Forms.TabControl();
            this.argumentTabPage = new System.Windows.Forms.TabPage();
            this.argumentAddButton = new System.Windows.Forms.Button();
            this.argumentRemoveButton = new System.Windows.Forms.Button();
            this.argumentMoveDownButton = new System.Windows.Forms.Button();
            this.argumentMoveUpButton = new System.Windows.Forms.Button();
            this.argumentButtonsPanel = new System.Windows.Forms.Panel();
            this.argumentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.argumentDataGridView)).BeginInit();
            this.argumentTabControl.SuspendLayout();
            this.argumentTabPage.SuspendLayout();
            this.argumentButtonsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // argumentPanel
            // 
            this.argumentPanel.BackColor = System.Drawing.Color.White;
            this.argumentPanel.Controls.Add(this.argumentTabControl);
            this.argumentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.argumentPanel.Location = new System.Drawing.Point(0, 0);
            this.argumentPanel.Name = "argumentPanel";
            this.argumentPanel.Size = new System.Drawing.Size(434, 311);
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
            this.argumentDataGridView.Location = new System.Drawing.Point(3, 3);
            this.argumentDataGridView.Margin = new System.Windows.Forms.Padding(10, 10, 3, 3);
            this.argumentDataGridView.MultiSelect = false;
            this.argumentDataGridView.Name = "argumentDataGridView";
            this.argumentDataGridView.RowHeadersVisible = false;
            this.argumentDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.argumentDataGridView.Size = new System.Drawing.Size(420, 251);
            this.argumentDataGridView.TabIndex = 2;
            this.argumentDataGridView.TabStop = false;
            // 
            // argumentTabControl
            // 
            this.argumentTabControl.Controls.Add(this.argumentTabPage);
            this.argumentTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.argumentTabControl.Location = new System.Drawing.Point(0, 0);
            this.argumentTabControl.Name = "argumentTabControl";
            this.argumentTabControl.SelectedIndex = 0;
            this.argumentTabControl.Size = new System.Drawing.Size(434, 311);
            this.argumentTabControl.TabIndex = 4;
            this.argumentTabControl.TabStop = false;
            // 
            // argumentTabPage
            // 
            this.argumentTabPage.Controls.Add(this.argumentDataGridView);
            this.argumentTabPage.Controls.Add(this.argumentButtonsPanel);
            this.argumentTabPage.Location = new System.Drawing.Point(4, 24);
            this.argumentTabPage.Name = "argumentTabPage";
            this.argumentTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.argumentTabPage.Size = new System.Drawing.Size(426, 283);
            this.argumentTabPage.TabIndex = 0;
            this.argumentTabPage.Text = "argumenTabPage";
            this.argumentTabPage.UseVisualStyleBackColor = true;
            // 
            // argumentAddButton
            // 
            this.argumentAddButton.Location = new System.Drawing.Point(0, 3);
            this.argumentAddButton.Name = "argumentAddButton";
            this.argumentAddButton.Size = new System.Drawing.Size(80, 23);
            this.argumentAddButton.TabIndex = 3;
            this.argumentAddButton.TabStop = false;
            this.argumentAddButton.Text = "Add";
            this.argumentAddButton.UseVisualStyleBackColor = true;
            this.argumentAddButton.Click += new System.EventHandler(this.ArgumentAddButton_Click);
            // 
            // argumentRemoveButton
            // 
            this.argumentRemoveButton.Location = new System.Drawing.Point(86, 3);
            this.argumentRemoveButton.Name = "argumentRemoveButton";
            this.argumentRemoveButton.Size = new System.Drawing.Size(80, 23);
            this.argumentRemoveButton.TabIndex = 2;
            this.argumentRemoveButton.TabStop = false;
            this.argumentRemoveButton.Text = "Remove";
            this.argumentRemoveButton.UseVisualStyleBackColor = true;
            this.argumentRemoveButton.Click += new System.EventHandler(this.ArgumentRemoveButton_Click);
            // 
            // argumentMoveDownButton
            // 
            this.argumentMoveDownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.argumentMoveDownButton.Location = new System.Drawing.Point(340, 3);
            this.argumentMoveDownButton.Name = "argumentMoveDownButton";
            this.argumentMoveDownButton.Size = new System.Drawing.Size(80, 23);
            this.argumentMoveDownButton.TabIndex = 1;
            this.argumentMoveDownButton.TabStop = false;
            this.argumentMoveDownButton.Text = "Move Down";
            this.argumentMoveDownButton.UseVisualStyleBackColor = true;
            this.argumentMoveDownButton.Click += new System.EventHandler(this.ArgumentMoveDownButton_Click);
            // 
            // argumentMoveUpButton
            // 
            this.argumentMoveUpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.argumentMoveUpButton.Location = new System.Drawing.Point(254, 3);
            this.argumentMoveUpButton.Name = "argumentMoveUpButton";
            this.argumentMoveUpButton.Size = new System.Drawing.Size(80, 23);
            this.argumentMoveUpButton.TabIndex = 0;
            this.argumentMoveUpButton.TabStop = false;
            this.argumentMoveUpButton.Text = "Move Up";
            this.argumentMoveUpButton.UseVisualStyleBackColor = true;
            this.argumentMoveUpButton.Click += new System.EventHandler(this.ArgumentMoveUpButton_Click);
            // 
            // argumentButtonsPanel
            // 
            this.argumentButtonsPanel.Controls.Add(this.argumentMoveUpButton);
            this.argumentButtonsPanel.Controls.Add(this.argumentMoveDownButton);
            this.argumentButtonsPanel.Controls.Add(this.argumentRemoveButton);
            this.argumentButtonsPanel.Controls.Add(this.argumentAddButton);
            this.argumentButtonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.argumentButtonsPanel.Location = new System.Drawing.Point(3, 254);
            this.argumentButtonsPanel.Name = "argumentButtonsPanel";
            this.argumentButtonsPanel.Size = new System.Drawing.Size(420, 26);
            this.argumentButtonsPanel.TabIndex = 1;
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
            ((System.ComponentModel.ISupportInitialize)(this.argumentDataGridView)).EndInit();
            this.argumentTabControl.ResumeLayout(false);
            this.argumentTabPage.ResumeLayout(false);
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
                        
            form.argumentPanel.ParentChanged += (sender, e) =>
            {
                grid.DataSource = new BindingList<T>(arguments); // perfect.
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
                this.argumentTabPage.Text = "Inputs";
                this.CreateColumnsForInArguments(dataTypes);
            }
            else if (typeof(T) == typeof(OutArgument))
            {
                this.argumentTabPage.Text = "Outputs";
                this.CreateColumnsForOutArguments(dataTypes, variables);
            }
            else if (typeof(T) == typeof(InOutArgument))
            {
                this.argumentTabPage.Text = "Inputs";
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
                DataPropertyName = "Name",
            });
            grid.Columns.Add(new DataTypeColumn()
            {
                Name = "Type",
                FillWeight = 15,
                DataPropertyName = "DataTypeId",
                DataSource = dataTypes
            });
            grid.Columns.Add(new GhostTextBoxColumn()
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
            grid.Columns.Add(new GhostTextBoxColumn()
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
            grid.Columns.Add(new GhostTextBoxColumn()
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

        private void ArgumentMasterForm_Load(object sender, EventArgs e)
        {

        }
    }
}