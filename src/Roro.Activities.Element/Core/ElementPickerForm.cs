
using System;
using System.Collections;
using System.Collections.Generic;
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(12, 286);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.TabStop = false;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(93, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(419, 297);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.TabStop = false;
            // 
            // okButton
            // 
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(12, 257);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 3;
            this.okButton.TabStop = false;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // ElementPickerForm
            // 
            this.AcceptButton = this.okButton;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(524, 321);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "ElementPickerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Click \"Pick\" button, then press CTRL key to select element";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ElementPickerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
        private DataGridView dataGridView1;
        private Button okButton;
        private CancellationTokenSource cts;

        private void InitializeVariables()
        {
            this.inputEvent = null;
            this.inputDriver = new InputDriver();
            this.highlighter = new ElementHighlighterForm();
            inputDriver.OnMouseMove += Input_OnMouseMove;
            inputDriver.OnKeyUp += Input_OnKeyUp;
            this.cts = new CancellationTokenSource();
            this.Query = new ElementQuery();
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