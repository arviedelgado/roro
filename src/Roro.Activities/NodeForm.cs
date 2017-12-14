using Roro.Workflow;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Roro.Activities
{
    public class NodeForm : Form
    {
        private TextBox nodeNameTextBox;
        private SplitContainer splitContainer1;

        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.nodeNameTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 41);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Size = new System.Drawing.Size(710, 358);
            this.splitContainer1.SplitterDistance = 494;
            this.splitContainer1.TabIndex = 4;
            this.splitContainer1.TabStop = false;
            // 
            // nodeNameTextBox
            // 
            this.nodeNameTextBox.BackColor = System.Drawing.Color.White;
            this.nodeNameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nodeNameTextBox.Location = new System.Drawing.Point(12, 12);
            this.nodeNameTextBox.Name = "nodeNameTextBox";
            this.nodeNameTextBox.Size = new System.Drawing.Size(710, 23);
            this.nodeNameTextBox.TabIndex = 6;
            // 
            // NodeForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(734, 411);
            this.Controls.Add(this.nodeNameTextBox);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "NodeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.ActivityForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private NodeForm() => this.InitializeComponent();

        public NodeForm(Page page, Node node) : this()
        {
            ArgumentForm.Create(page, node).Parent = this.splitContainer1.Panel1;
            VariableForm.Create(page).Parent = this.splitContainer1.Panel2;
            this.nodeNameTextBox.DataBindings.Add("Text", node, "Name", false, DataSourceUpdateMode.OnPropertyChanged);
            this.nodeNameTextBox.PreviewKeyDown += NodeNameTextBox_PreviewKeyDown;
        }

        private void NodeNameTextBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) this.Close();
        }

        private void ActivityForm_Load(object sender, EventArgs e)
        {

        }
    }
}
