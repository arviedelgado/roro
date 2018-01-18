
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Roro.Activities
{
    public class NodeForm : Form
    {
        private Panel panel1;
        private TabControl tabControl1;
        private TextBox nameTextBox;
        private Button cancelButton;
        private Button okButton;
        private TabPage inputTab;
        private TabPage outputTab;
        private DataGridView inputGrid;
        private GhostTextBoxColumn inputNameColumn;
        private DataTypeColumn inputTypeColumn;
        private GhostTextBoxColumn inputValueColumn;
        private DataGridView outputGrid;
        private GhostTextBoxColumn outputNameColumn;
        private DataTypeColumn outputTypeColumn;
        private GhostTextBoxColumn outputValueColumn;
        private Label nameLabel;

        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.inputTab = new System.Windows.Forms.TabPage();
            this.inputGrid = new System.Windows.Forms.DataGridView();
            this.outputTab = new System.Windows.Forms.TabPage();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.outputGrid = new System.Windows.Forms.DataGridView();
            this.outputNameColumn = new Roro.Activities.GhostTextBoxColumn();
            this.outputTypeColumn = new Roro.Activities.DataTypeColumn();
            this.outputValueColumn = new Roro.Activities.GhostTextBoxColumn();
            this.inputNameColumn = new Roro.Activities.GhostTextBoxColumn();
            this.inputTypeColumn = new Roro.Activities.DataTypeColumn();
            this.inputValueColumn = new Roro.Activities.GhostTextBoxColumn();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.inputTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputGrid)).BeginInit();
            this.outputTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(235)))), ((int)(((byte)(240)))));
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.okButton);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.nameTextBox);
            this.panel1.Controls.Add(this.nameLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(584, 361);
            this.panel1.TabIndex = 0;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(497, 326);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.TabStop = false;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(416, 326);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.TabStop = false;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.inputTab);
            this.tabControl1.Controls.Add(this.outputTab);
            this.tabControl1.Location = new System.Drawing.Point(12, 56);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(560, 264);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 2;
            this.tabControl1.TabStop = false;
            // 
            // inputTab
            // 
            this.inputTab.Controls.Add(this.inputGrid);
            this.inputTab.Location = new System.Drawing.Point(4, 24);
            this.inputTab.Name = "inputTab";
            this.inputTab.Padding = new System.Windows.Forms.Padding(3);
            this.inputTab.Size = new System.Drawing.Size(552, 236);
            this.inputTab.TabIndex = 0;
            this.inputTab.Text = "Inputs";
            this.inputTab.UseVisualStyleBackColor = true;
            // 
            // inputGrid
            // 
            this.inputGrid.AllowUserToAddRows = false;
            this.inputGrid.AllowUserToDeleteRows = false;
            this.inputGrid.AllowUserToResizeRows = false;
            this.inputGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.inputGrid.BackgroundColor = System.Drawing.Color.White;
            this.inputGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.inputGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.inputGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.inputNameColumn,
            this.inputTypeColumn,
            this.inputValueColumn});
            this.inputGrid.EnableHeadersVisualStyles = false;
            this.inputGrid.Location = new System.Drawing.Point(6, 6);
            this.inputGrid.Name = "inputGrid";
            this.inputGrid.Size = new System.Drawing.Size(540, 224);
            this.inputGrid.TabIndex = 0;
            this.inputGrid.TabStop = false;
            // 
            // outputTab
            // 
            this.outputTab.Controls.Add(this.outputGrid);
            this.outputTab.Location = new System.Drawing.Point(4, 24);
            this.outputTab.Name = "outputTab";
            this.outputTab.Padding = new System.Windows.Forms.Padding(3);
            this.outputTab.Size = new System.Drawing.Size(552, 236);
            this.outputTab.TabIndex = 1;
            this.outputTab.Text = "Outputs";
            this.outputTab.UseVisualStyleBackColor = true;
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(12, 27);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(350, 23);
            this.nameTextBox.TabIndex = 1;
            this.nameTextBox.TabStop = false;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(12, 9);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(39, 15);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Name";
            // 
            // outputGrid
            // 
            this.outputGrid.AllowUserToAddRows = false;
            this.outputGrid.AllowUserToDeleteRows = false;
            this.outputGrid.AllowUserToResizeRows = false;
            this.outputGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.outputGrid.BackgroundColor = System.Drawing.Color.White;
            this.outputGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.outputGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.outputGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.outputNameColumn,
            this.outputTypeColumn,
            this.outputValueColumn});
            this.outputGrid.EnableHeadersVisualStyles = false;
            this.outputGrid.Location = new System.Drawing.Point(6, 6);
            this.outputGrid.Name = "outputGrid";
            this.outputGrid.Size = new System.Drawing.Size(540, 224);
            this.outputGrid.TabIndex = 1;
            this.outputGrid.TabStop = false;
            // 
            // outputNameColumn
            // 
            this.outputNameColumn.FillWeight = 35F;
            this.outputNameColumn.HeaderText = "Name";
            this.outputNameColumn.Name = "outputNameColumn";
            this.outputNameColumn.ReadOnly = true;
            this.outputNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.outputNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // outputTypeColumn
            // 
            this.outputTypeColumn.DisplayMember = "Name";
            this.outputTypeColumn.DisplayStyleForCurrentCellOnly = true;
            this.outputTypeColumn.FillWeight = 15F;
            this.outputTypeColumn.HeaderText = "Type";
            this.outputTypeColumn.Name = "outputTypeColumn";
            this.outputTypeColumn.ReadOnly = true;
            this.outputTypeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.outputTypeColumn.ValueMember = "Id";
            // 
            // outputValueColumn
            // 
            this.outputValueColumn.FillWeight = 50F;
            this.outputValueColumn.HeaderText = "Variable";
            this.outputValueColumn.Name = "outputValueColumn";
            this.outputValueColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // inputNameColumn
            // 
            this.inputNameColumn.FillWeight = 35F;
            this.inputNameColumn.HeaderText = "Name";
            this.inputNameColumn.Name = "inputNameColumn";
            this.inputNameColumn.ReadOnly = true;
            this.inputNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.inputNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // inputTypeColumn
            // 
            this.inputTypeColumn.DisplayMember = "Name";
            this.inputTypeColumn.DisplayStyleForCurrentCellOnly = true;
            this.inputTypeColumn.FillWeight = 15F;
            this.inputTypeColumn.HeaderText = "Type";
            this.inputTypeColumn.Name = "inputTypeColumn";
            this.inputTypeColumn.ReadOnly = true;
            this.inputTypeColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.inputTypeColumn.ValueMember = "Id";
            // 
            // inputValueColumn
            // 
            this.inputValueColumn.FillWeight = 50F;
            this.inputValueColumn.HeaderText = "Value";
            this.inputValueColumn.Name = "inputValueColumn";
            this.inputValueColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // NodeForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "NodeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Activity Properties";
            this.Load += new System.EventHandler(this.ActivityForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.inputTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.inputGrid)).EndInit();
            this.outputTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.outputGrid)).EndInit();
            this.ResumeLayout(false);

        }

        private NodeForm() => this.InitializeComponent();

        public NodeForm(Page page, Node node) : this()
        {
            this.nameTextBox.DataBindings.Add("Text", node, "Name", false, DataSourceUpdateMode.OnPropertyChanged);
            this.nameTextBox.PreviewKeyDown += NodeNameTextBox_PreviewKeyDown;

            this.InputGrid_Initialize(node);
            this.OutputGrid_Initialize(node);
      
        }

        private void InputGrid_Initialize(Node node)
        {
            var grid = this.inputGrid;
            if (node.Activity.GetArguments<InArgument>() is List<InArgument> inputs && inputs.Count > 0)
            {
                this.inputTypeColumn.DataSource = DataType.GetCommonTypes();
                foreach (var input in inputs)
                {
                    grid.Rows.Add(input.Name, input.DataTypeId, input.Value);
                }

                this.inputTab.Text = this.inputTab.Text + " (" + inputs.Count + ")";

                grid.CellValueChanged += (sender, e) =>
                {
                    var value = grid[e.ColumnIndex, e.RowIndex].Value;
                    if (inputs[e.RowIndex] is InArgument input)
                    {
                        if (e.ColumnIndex == 2) input.Value = (string)value;
                    }
                };

                grid.CurrentCellDirtyStateChanged += (sender, e) =>
                {
                    if (grid.IsCurrentCellDirty)
                    {
                        grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }
                };
            }
            else
            {
                this.tabControl1.TabPages.Remove(this.inputTab);
            }
        }

        private void OutputGrid_Initialize(Node node)
        {
            var grid = this.outputGrid;
            if (node.Activity.GetArguments<OutArgument>() is List<OutArgument> outputs && outputs.Count > 0)
            {
                this.outputTypeColumn.DataSource = DataType.GetCommonTypes();
                foreach (var output in outputs)
                {
                    grid.Rows.Add(output.Name, output.DataTypeId, output.Value);
                }

                this.outputTab.Text = this.outputTab.Text + " (" + outputs.Count + ")";

                grid.CellValueChanged += (sender, e) =>
                {
                    var value = grid[e.ColumnIndex, e.RowIndex].Value;
                    if (outputs[e.RowIndex] is OutArgument output)
                    {
                        if (e.ColumnIndex == 2) output.Value = (string)value;
                    }
                };

                grid.CurrentCellDirtyStateChanged += (sender, e) =>
                {
                    if (grid.IsCurrentCellDirty)
                    {
                        grid.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }
                };
            }
            else
            {
                this.tabControl1.TabPages.Remove(this.outputTab);
            }
        }

        private void NodeNameTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) this.okButton.PerformClick();
        }

        private void ActivityForm_Load(object sender, EventArgs e)
        {
            this.nameTextBox.TabStop = true;
            this.nameTextBox.Focus();
        }

        private void nodeNamePanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
