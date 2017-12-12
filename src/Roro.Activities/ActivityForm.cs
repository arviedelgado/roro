using Roro.Workflow;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Roro.Activities
{
    public class ActivityForm : Form
    {
        private SplitContainer splitContainer1;

        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Size = new System.Drawing.Size(734, 411);
            this.splitContainer1.SplitterDistance = 511;
            this.splitContainer1.TabIndex = 4;
            this.splitContainer1.TabStop = false;
            // 
            // ActivityForm
            // 
            this.ClientSize = new System.Drawing.Size(734, 411);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "ActivityForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.ActivityForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private ActivityForm() => this.InitializeComponent();

        public ActivityForm(Page page, Node node) : this()
        {
            ArgumentForm.Create(page, node).Parent = this.splitContainer1.Panel1;
            VariableForm.Create(page).Parent = this.splitContainer1.Panel2;
        }

        private void ActivityForm_Load(object sender, EventArgs e)
        {

        }
    }
}
