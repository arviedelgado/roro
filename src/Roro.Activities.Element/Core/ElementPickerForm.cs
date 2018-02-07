
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;

namespace Roro
{
    public sealed class ElementPickerForm : Form
    {
        private Button button1;

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.okButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.conditionEnabledColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.conditionNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.conditionValueColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.TabStop = false;
            this.button1.Text = "Pick";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.PickButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(437, 12);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.TabStop = false;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(230)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.conditionEnabledColumn,
            this.conditionNameColumn,
            this.conditionValueColumn});
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(12, 41);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.Size = new System.Drawing.Size(500, 268);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.TabStop = false;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(356, 12);
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
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.okButton);
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(524, 321);
            this.panel1.TabIndex = 4;
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
            // 
            // conditionValueColumn
            // 
            this.conditionValueColumn.DataPropertyName = "Value";
            this.conditionValueColumn.FillWeight = 60F;
            this.conditionValueColumn.HeaderText = "Value";
            this.conditionValueColumn.Name = "conditionValueColumn";
            this.conditionValueColumn.ReadOnly = true;
            // 
            // ElementPickerForm
            // 
            this.AcceptButton = this.okButton;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(524, 321);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ElementPickerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Click \"Pick\" button, then press CTRL key to select element";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ElementPickerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
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
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = this.Query.GetValue();
        }

        private void PickButton_Click(object sender, EventArgs e)
        {
            this.Pick();
        }

        public ElementQuery Query { get; private set; }

        private ElementHighlighterForm highlighter;

        private InputDriver inputDriver;

        private InputEventArgs inputEvent;
        private Button cancelButton;
        private Button okButton;
        private Panel panel1;
        private DataGridView dataGridView1;
        private DataGridViewCheckBoxColumn conditionEnabledColumn;
        private DataGridViewTextBoxColumn conditionNameColumn;
        private DataGridViewTextBoxColumn conditionValueColumn;
        private CancellationTokenSource cts;

        private void InitializeVariables()
        {
            this.inputEvent = null;
            this.inputDriver = new InputDriver();
            this.highlighter = new ElementHighlighterForm();
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
                        this.dataGridView1.Invoke(new Action(() =>
                        {
                            this.dataGridView1.DataSource = q.GetValue();
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

        }
    }
}