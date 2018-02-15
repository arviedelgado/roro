
using System;
using System.Collections.Generic;
using System.Linq;
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
        private TabPage inputsTab;
        private TabPage outputsTab;
        private DataGridView inputGrid;
        private DataGridView outputGrid;
        private TabPage valuesTab;
        private TextBox currentValueTextBox;
        private TextBox initialValueTextBox;
        private Label currentValueLabel;
        private Label initialValueLabel;
        private ComboBox typeComboBox;
        private Label typeLabel;
        private LabelColumn inputNameColumn;
        private DataTypeColumn inputTypeColumn;
        private GhostTextBoxColumn inputValueColumn;
        private LabelColumn outputNameColumn;
        private DataTypeColumn outputTypeColumn;
        private VariableColumn outputValueColumn;
        private Label nameLabel;

        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.typeLabel = new System.Windows.Forms.Label();
            this.typeComboBox = new System.Windows.Forms.ComboBox();
            this.cancelButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.inputsTab = new System.Windows.Forms.TabPage();
            this.inputGrid = new System.Windows.Forms.DataGridView();
            this.inputNameColumn = new Roro.Activities.LabelColumn();
            this.inputTypeColumn = new Roro.Activities.DataTypeColumn();
            this.inputValueColumn = new Roro.Activities.GhostTextBoxColumn();
            this.outputsTab = new System.Windows.Forms.TabPage();
            this.outputGrid = new System.Windows.Forms.DataGridView();
            this.outputNameColumn = new Roro.Activities.LabelColumn();
            this.outputTypeColumn = new Roro.Activities.DataTypeColumn();
            this.outputValueColumn = new Roro.Activities.VariableColumn();
            this.valuesTab = new System.Windows.Forms.TabPage();
            this.currentValueLabel = new System.Windows.Forms.Label();
            this.initialValueLabel = new System.Windows.Forms.Label();
            this.initialValueTextBox = new System.Windows.Forms.TextBox();
            this.currentValueTextBox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.inputsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputGrid)).BeginInit();
            this.outputsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputGrid)).BeginInit();
            this.valuesTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(235)))), ((int)(((byte)(240)))));
            this.panel1.Controls.Add(this.nameTextBox);
            this.panel1.Controls.Add(this.typeLabel);
            this.panel1.Controls.Add(this.typeComboBox);
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.okButton);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Controls.Add(this.nameLabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(584, 361);
            this.panel1.TabIndex = 0;
            // 
            // nameTextBox
            // 
            this.nameTextBox.AcceptsReturn = true;
            this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nameTextBox.Location = new System.Drawing.Point(12, 33);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(338, 23);
            this.nameTextBox.TabIndex = 0;
            // 
            // typeLabel
            // 
            this.typeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.typeLabel.AutoSize = true;
            this.typeLabel.Location = new System.Drawing.Point(353, 12);
            this.typeLabel.Margin = new System.Windows.Forms.Padding(3);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(32, 15);
            this.typeLabel.TabIndex = 5;
            this.typeLabel.Text = "Type";
            this.typeLabel.Visible = false;
            // 
            // typeComboBox
            // 
            this.typeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeComboBox.FormattingEnabled = true;
            this.typeComboBox.Location = new System.Drawing.Point(356, 33);
            this.typeComboBox.Name = "typeComboBox";
            this.typeComboBox.Size = new System.Drawing.Size(216, 23);
            this.typeComboBox.TabIndex = 4;
            this.typeComboBox.TabStop = false;
            this.typeComboBox.Visible = false;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.Location = new System.Drawing.Point(497, 326);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 4;
            this.cancelButton.TabStop = false;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.okButton.Location = new System.Drawing.Point(416, 326);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.TabStop = false;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.inputsTab);
            this.tabControl1.Controls.Add(this.outputsTab);
            this.tabControl1.Controls.Add(this.valuesTab);
            this.tabControl1.Location = new System.Drawing.Point(12, 62);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(560, 258);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 2;
            this.tabControl1.TabStop = false;
            // 
            // inputsTab
            // 
            this.inputsTab.BackColor = System.Drawing.Color.White;
            this.inputsTab.Controls.Add(this.inputGrid);
            this.inputsTab.Location = new System.Drawing.Point(4, 24);
            this.inputsTab.Name = "inputsTab";
            this.inputsTab.Padding = new System.Windows.Forms.Padding(3);
            this.inputsTab.Size = new System.Drawing.Size(552, 230);
            this.inputsTab.TabIndex = 0;
            this.inputsTab.Text = "Inputs";
            // 
            // inputGrid
            // 
            this.inputGrid.AllowUserToAddRows = false;
            this.inputGrid.AllowUserToDeleteRows = false;
            this.inputGrid.AllowUserToResizeRows = false;
            this.inputGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.inputGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.inputGrid.Size = new System.Drawing.Size(540, 208);
            this.inputGrid.TabIndex = 0;
            this.inputGrid.TabStop = false;
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
            // outputsTab
            // 
            this.outputsTab.BackColor = System.Drawing.Color.White;
            this.outputsTab.Controls.Add(this.outputGrid);
            this.outputsTab.Location = new System.Drawing.Point(4, 24);
            this.outputsTab.Name = "outputsTab";
            this.outputsTab.Padding = new System.Windows.Forms.Padding(3);
            this.outputsTab.Size = new System.Drawing.Size(552, 230);
            this.outputsTab.TabIndex = 1;
            this.outputsTab.Text = "Outputs";
            // 
            // outputGrid
            // 
            this.outputGrid.AllowUserToAddRows = false;
            this.outputGrid.AllowUserToDeleteRows = false;
            this.outputGrid.AllowUserToResizeRows = false;
            this.outputGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.outputGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.outputGrid.Size = new System.Drawing.Size(540, 208);
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
            this.outputValueColumn.DisplayStyleForCurrentCellOnly = true;
            this.outputValueColumn.FillWeight = 50F;
            this.outputValueColumn.HeaderText = "Variable";
            this.outputValueColumn.Name = "outputValueColumn";
            this.outputValueColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // valuesTab
            // 
            this.valuesTab.BackColor = System.Drawing.Color.White;
            this.valuesTab.Controls.Add(this.currentValueLabel);
            this.valuesTab.Controls.Add(this.initialValueLabel);
            this.valuesTab.Controls.Add(this.initialValueTextBox);
            this.valuesTab.Controls.Add(this.currentValueTextBox);
            this.valuesTab.Location = new System.Drawing.Point(4, 24);
            this.valuesTab.Name = "valuesTab";
            this.valuesTab.Padding = new System.Windows.Forms.Padding(3);
            this.valuesTab.Size = new System.Drawing.Size(552, 230);
            this.valuesTab.TabIndex = 2;
            this.valuesTab.Text = "Values";
            // 
            // currentValueLabel
            // 
            this.currentValueLabel.AutoSize = true;
            this.currentValueLabel.Location = new System.Drawing.Point(6, 66);
            this.currentValueLabel.Margin = new System.Windows.Forms.Padding(3);
            this.currentValueLabel.Name = "currentValueLabel";
            this.currentValueLabel.Size = new System.Drawing.Size(78, 15);
            this.currentValueLabel.TabIndex = 3;
            this.currentValueLabel.Text = "Current Value";
            // 
            // initialValueLabel
            // 
            this.initialValueLabel.AutoSize = true;
            this.initialValueLabel.Location = new System.Drawing.Point(6, 11);
            this.initialValueLabel.Margin = new System.Windows.Forms.Padding(3);
            this.initialValueLabel.Name = "initialValueLabel";
            this.initialValueLabel.Size = new System.Drawing.Size(67, 15);
            this.initialValueLabel.TabIndex = 2;
            this.initialValueLabel.Text = "Initial Value";
            // 
            // initialValueTextBox
            // 
            this.initialValueTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.initialValueTextBox.Location = new System.Drawing.Point(6, 32);
            this.initialValueTextBox.Name = "initialValueTextBox";
            this.initialValueTextBox.Size = new System.Drawing.Size(328, 23);
            this.initialValueTextBox.TabIndex = 0;
            this.initialValueTextBox.TabStop = false;
            // 
            // currentValueTextBox
            // 
            this.currentValueTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.currentValueTextBox.Location = new System.Drawing.Point(6, 87);
            this.currentValueTextBox.Name = "currentValueTextBox";
            this.currentValueTextBox.ReadOnly = true;
            this.currentValueTextBox.Size = new System.Drawing.Size(328, 23);
            this.currentValueTextBox.TabIndex = 1;
            this.currentValueTextBox.TabStop = false;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(12, 12);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(3);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(39, 15);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Name";
            // 
            // NodeForm
            // 
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "NodeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Activity Properties";
            this.Load += new System.EventHandler(this.NodeForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.inputsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.inputGrid)).EndInit();
            this.outputsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.outputGrid)).EndInit();
            this.valuesTab.ResumeLayout(false);
            this.valuesTab.PerformLayout();
            this.ResumeLayout(false);

        }

        private NodeForm() => this.InitializeComponent();

        private Page targetPage;

        private Node targetNode;

        public NodeForm(Page page, Node node) : this()
        {
            this.targetPage = page;
            this.targetNode = node;

            this.AcceptButton = this.okButton;        
            this.CancelButton = this.cancelButton;

            this.nameTextBox.Text = node.Name;
            this.nameTextBox.Validating += (sender, e) =>
            {
                if (this.nameTextBox.Text.Length == 0)
                {
                    e.Cancel = true;
                    MessageBox.Show("'Name' should not be blank", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            this.InputsTab_Initialize();
            this.OutputsTab_Initialize(page);
            this.ValuesTab_Initialize(page);
      
        }

        private void InputsTab_Initialize()
        {
            if (this.targetNode.ActivityInputs is List<Input> inputs && inputs.Count > 0)
            {
                this.inputTypeColumn.DataSource = DataType.GetCommonTypes();
                foreach (var input in inputs)
                {
                    this.inputGrid.Rows.Add(input.Name, input.Type, input.Value);
                    this.inputGrid[this.inputGrid.ColumnCount - 1, this.inputGrid.RowCount - 1] = DataType.GetFromId(input.Type).CellTemplate;
                    this.inputGrid[this.inputGrid.ColumnCount - 1, this.inputGrid.RowCount - 1].Value = input.Value;
                }

                this.inputGrid.DataError += (sender, e) =>
                {
                    if (this.inputGrid[e.ColumnIndex, e.RowIndex] is VariableCell variableCell)
                    {
                        variableCell.OnDataError(sender, e);
                    }
                    if (this.inputGrid[e.ColumnIndex, e.RowIndex] is DataTypeCell dataTypeCell)
                    {
                        dataTypeCell.OnDataError(sender, e);
                    }
                };
                this.inputsTab.Text = this.inputsTab.Text + " (" + inputs.Count + ")";
            }
            else
            {
                this.tabControl1.TabPages.Remove(this.inputsTab);
            }
        }

        private void OutputsTab_Initialize(Page page)
        {
            if (this.targetNode.ActivityOutputs is List<Output> outputs && outputs.Count > 0)
            {
                // Type ComboBox
                this.outputTypeColumn.DataSource = DataType.GetCommonTypes();
                // Value ComboBOx
                var variableNames = new List<string>() { string.Empty };
                variableNames.AddRange(page.VariableNodes.OrderBy(x => x.Name).Select(x => x.Name).ToList());
                this.outputValueColumn.DataSource = variableNames;
                //
                foreach (var output in outputs)
                {
                    this.outputGrid.Rows.Add(output.Name, output.Type, output.Value);
                }
                this.outputGrid.DataError += (sender, e) =>
                {
                    if (this.outputGrid[e.ColumnIndex, e.RowIndex] is VariableCell variableCell)
                    {
                        variableCell.OnDataError(sender, e);
                    }
                    if (this.outputGrid[e.ColumnIndex, e.RowIndex] is DataTypeCell dataTypeCell)
                    {
                        dataTypeCell.OnDataError(sender, e);
                    }
                };
                this.outputsTab.Text = this.outputsTab.Text + " (" + outputs.Count + ")";
            }
            else
            {
                this.tabControl1.TabPages.Remove(this.outputsTab);
            }
        }

        private void ValuesTab_Initialize(Page page)
        {
            if (this.targetNode is VariableNode variableNode)
            {
                this.nameTextBox.Validating += (sender, e) =>
                {
                    if (page.VariableNodes.FirstOrDefault(x => x.Name == this.nameTextBox.Text && x != variableNode) != null)
                    {
                        e.Cancel = true;
                        MessageBox.Show("Variable name '" + this.nameTextBox.Text + "' already exists");
                    }
                    else if (this.nameTextBox.Text.Contains(VariableNode.StartToken))
                    {
                        e.Cancel = true;
                        MessageBox.Show("Variable name should not contain " + VariableNode.StartToken, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (this.nameTextBox.Text.Contains(VariableNode.EndToken))
                    {
                        e.Cancel = true;
                        MessageBox.Show("Variable name should not contain " + VariableNode.EndToken, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                };

                this.typeLabel.Visible = true;
                this.typeComboBox.Visible = true;

                this.typeComboBox.DataSource = DataType.GetCommonTypes();
                this.typeComboBox.ValueMember = "Id";
                this.typeComboBox.DisplayMember = "Name";
                this.typeComboBox.SelectedValue = variableNode.Type;

                this.initialValueTextBox.Text = variableNode.InitialValue?.ToString();
                this.initialValueTextBox.Validating += (sender, e) =>
                {
                    var data = DataType.GetFromId(this.typeComboBox.SelectedValue.ToString());
                    if (this.initialValueTextBox.Text == string.Empty)
                    {
                        this.initialValueTextBox.Text = data.GetValue().ToString();
                    }
                    try
                    {
                        data.SetValue(this.initialValueTextBox.Text);
                    }
                    catch
                    {
                        e.Cancel = true;
                        var useDefaultValue = DialogResult.Yes == MessageBox.Show("Variable value should be a valid " + this.typeComboBox.Text + ".\n\nDo you want to set the default value?", string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                        if (useDefaultValue)
                        {
                            this.initialValueTextBox.Text = data.GetValue().ToString();
                        }
                    }
                };

                this.currentValueTextBox.Text = variableNode.CurrentValue?.ToString();
            }
            else
            {
                this.tabControl1.TabPages.Remove(this.valuesTab);
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                this.targetNode.Name = this.nameTextBox.Text;
                if (this.targetNode is VariableNode variableNode)
                {
                    variableNode.Type = this.typeComboBox.SelectedValue.ToString();
                    variableNode.InitialValue = this.initialValueTextBox.Text;
                    variableNode.CurrentValue = this.currentValueTextBox.Text;
                }
                else
                {
                    var inputs = new List<Input>();
                    foreach (DataGridViewRow row in this.inputGrid.Rows)
                    {
                        inputs.Add(new Input()
                        {
                            Name = row.Cells[0].Value.ToString(),
                            Type = row.Cells[1].Value.ToString(),
                            Value = row.Cells[2].Value?.ToString() ?? string.Empty
                        });
                    }
                    this.targetNode.ActivityInputs = inputs;

                    var outputs = new List<Output>();
                    foreach (DataGridViewRow row in this.outputGrid.Rows)
                    {
                        outputs.Add(new Output()
                        {
                            Name = row.Cells[0].Value.ToString(),
                            Type = row.Cells[1].Value.ToString(),
                            Value = row.Cells[2].Value?.ToString() ?? string.Empty
                        });
                    }
                    this.targetNode.ActivityOutputs = outputs;
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.None;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void NodeForm_Load(object sender, EventArgs e)
        {

        }
    }
}
