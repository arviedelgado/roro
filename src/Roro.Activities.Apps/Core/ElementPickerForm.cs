
using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;

namespace Roro.Activities.Apps
{
    public sealed class ElementPickerForm : Form
    {
        private Button pickButton;
        private Button okButton;
        private Button cancelButton;
        private DataGridView conditionGrid;
        private Button clearButton;
        private Button testButton;
        private SplitContainer splitContainer1;
        private DataGridViewCheckBoxColumn conditionEnabledColumn;
        private LabelColumn conditionNameColumn;
        private DataGridViewTextBoxColumn conditionValueColumn;
        private Label label1;

        private void InitializeComponent()
        {
            this.pickButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.conditionGrid = new System.Windows.Forms.DataGridView();
            this.okButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label1 = new System.Windows.Forms.Label();
            this.testButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.conditionEnabledColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.conditionNameColumn = new Roro.Activities.LabelColumn();
            this.conditionValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.conditionGrid)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pickButton
            // 
            this.pickButton.Location = new System.Drawing.Point(12, 12);
            this.pickButton.Name = "pickButton";
            this.pickButton.Size = new System.Drawing.Size(75, 23);
            this.pickButton.TabIndex = 0;
            this.pickButton.TabStop = false;
            this.pickButton.Text = "Pick";
            this.pickButton.UseVisualStyleBackColor = true;
            this.pickButton.Click += new System.EventHandler(this.PickButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(447, 12);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.TabStop = false;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // conditionGrid
            // 
            this.conditionGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.conditionGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.conditionGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.conditionGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.conditionGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.conditionGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.conditionEnabledColumn,
            this.conditionNameColumn,
            this.conditionValueColumn});
            this.conditionGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conditionGrid.EnableHeadersVisualStyles = false;
            this.conditionGrid.Location = new System.Drawing.Point(0, 0);
            this.conditionGrid.Name = "conditionGrid";
            this.conditionGrid.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.conditionGrid.Size = new System.Drawing.Size(510, 220);
            this.conditionGrid.TabIndex = 2;
            this.conditionGrid.TabStop = false;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(366, 12);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.TabStop = false;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(235)))), ((int)(((byte)(240)))));
            this.panel1.Controls.Add(this.splitContainer1);
            this.panel1.Controls.Add(this.testButton);
            this.panel1.Controls.Add(this.clearButton);
            this.panel1.Controls.Add(this.pickButton);
            this.panel1.Controls.Add(this.okButton);
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(534, 311);
            this.panel1.TabIndex = 4;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(12, 41);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.conditionGrid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(510, 258);
            this.splitContainer1.SplitterDistance = 220;
            this.splitContainer1.TabIndex = 6;
            this.splitContainer1.TabStop = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Yellow;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(510, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "Press CTRL key to pick the highlighted element";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(93, 12);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(75, 23);
            this.testButton.TabIndex = 5;
            this.testButton.TabStop = false;
            this.testButton.Text = "Test";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(174, 12);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(75, 23);
            this.clearButton.TabIndex = 4;
            this.clearButton.TabStop = false;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // conditionEnabledColumn
            // 
            this.conditionEnabledColumn.DataPropertyName = "Use";
            this.conditionEnabledColumn.FillWeight = 10F;
            this.conditionEnabledColumn.HeaderText = "Use";
            this.conditionEnabledColumn.Name = "conditionEnabledColumn";
            this.conditionEnabledColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.conditionEnabledColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // conditionNameColumn
            // 
            this.conditionNameColumn.DataPropertyName = "Name";
            this.conditionNameColumn.FillWeight = 30F;
            this.conditionNameColumn.HeaderText = "Name";
            this.conditionNameColumn.Name = "conditionNameColumn";
            this.conditionNameColumn.ReadOnly = true;
            this.conditionNameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.conditionNameColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // conditionValueColumn
            // 
            this.conditionValueColumn.DataPropertyName = "Value";
            this.conditionValueColumn.FillWeight = 60F;
            this.conditionValueColumn.HeaderText = "Value";
            this.conditionValueColumn.Name = "conditionValueColumn";
            // 
            // ElementPickerForm
            // 
            this.AcceptButton = this.okButton;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(534, 311);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ElementPickerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Element Picker";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ElementPickerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.conditionGrid)).EndInit();
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        public ElementPickerForm(object gridCellValue)
        {
            this.InitializeComponent();
            this.InitializeVariables();
            //
            if (gridCellValue is string xml && xml.Length > 0
                && new ElementQuery(xml) is ElementQuery query)
            {
                this.Query = query;
            }
            else
            {
                this.Query = new ElementQuery();
            }
            this.conditionGrid.AutoGenerateColumns = false;
            this.conditionGrid.DataSource = this.Query.GetValue();
        }

        private void PickButton_Click(object sender, EventArgs e)
        {
            this.splitContainer1.Panel2Collapsed = false;
            this.pickButton.Enabled = false;
            this.testButton.Enabled = false;
            this.clearButton.Enabled = false;
            this.cancelButton.Enabled = false;
            this.okButton.Enabled = false;
            this.Pick();
        }

        public ElementQuery Query { get; private set; }

        private ElementHighlighterForm highlighter;

        private InputDriver inputDriver;

        private InputEventArgs inputEvent;

        private Panel panel1;

        private CancellationTokenSource cts;

        private void InitializeVariables()
        {
            this.inputEvent = null;
            this.inputDriver = new InputDriver();
            this.highlighter = new ElementHighlighterForm();
            this.splitContainer1.Panel2Collapsed = true;
            this.cts = new CancellationTokenSource();
            this.Query = new ElementQuery();
            this.FormClosing += (sender, e) =>
            {
                this.cts.Cancel();
                this.inputDriver.Dispose();
                this.highlighter.Close();
            };
            inputDriver.OnMouseMove += Input_OnMouseMove;
            inputDriver.OnKeyUp += Input_OnKeyUp;
        }

        private void Pick()
        {
            this.cts.Cancel();
            this.cts = new CancellationTokenSource();
            this.InspectAsync(cts.Token);
        }

        private void InspectAsync(CancellationToken token)
        {
            Task.Run(() =>
            {
                var e = this.inputEvent;
                while (true)
                {
                    try
                    {
                        if (token.IsCancellationRequested)
                        {
                            break;
                        }
                        if (e == this.inputEvent)
                        {
                            continue;
                        }
                        e = this.inputEvent;
                        var x = WinContext.Shared.GetElementFromPoint(e.X, e.Y);
                        var q = x.GetQuery();
                        if (token.IsCancellationRequested)
                        {
                            break;
                        }
                        this.Query = q;
                        this.conditionGrid.Invoke(new Action(() =>
                        {
                            this.conditionGrid.DataSource = q.GetValue();
                        }));
                        this.highlighter.Invoke(new Action(() =>
                        {
                            this.highlighter.Render(x.Bounds, Color.Red);
                        }));
                        if (this.WindowState == FormWindowState.Minimized)
                        {
                            this.FormBorderStyle = FormBorderStyle.None;
                            this.WindowState = FormWindowState.Normal;
                            this.FormBorderStyle = FormBorderStyle.FixedSingle;
                        }
                    }
                    catch (ElementNotAvailableException)
                    {
                        continue;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                this.Invoke(new Action(() =>
                {
                    this.splitContainer1.Panel2Collapsed = true;
                    this.pickButton.Enabled = true;
                    this.testButton.Enabled = true;
                    this.clearButton.Enabled = true;
                    this.cancelButton.Enabled = true;
                    this.okButton.Enabled = true;
                }));
            });
        }

        private void Input_OnKeyUp(InputEventArgs e)
        {
            if (e.Key == KeyboardKey.LeftCtrl || e.Key == KeyboardKey.RightCtrl)
            {
                this.cts.Cancel();
            }
            else
            {
                this.inputEvent = e;
            }
        }

        private void Input_OnMouseMove(InputEventArgs e)
        {
            this.inputEvent = e;
        }

        private void ElementPickerForm_Load(object sender, EventArgs e)
        {
            if (this.Query.Count == 0)
            {
                this.pickButton.PerformClick();
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            this.Query = new ElementQuery();
            this.conditionGrid.DataSource = this.Query.GetValue();
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            var elements = WinContext.Shared.GetElementsFromQuery(this.Query);
            if (elements.Count() == 1)
            {
                this.highlighter.Render(elements.First().Bounds, Color.Yellow);
            }
            MessageBox.Show(
                string.Format("The query matched {0} element{1}.",
                elements.Count(),
                elements.Count() == 1 ? string.Empty : "s"));
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}