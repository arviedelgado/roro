namespace Roro.Workflow.UI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.skWorkspaceParent = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // skWorkspaceParent
            // 
            this.skWorkspaceParent.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.skWorkspaceParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.skWorkspaceParent.Location = new System.Drawing.Point(0, 0);
            this.skWorkspaceParent.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.skWorkspaceParent.Name = "skWorkspaceParent";
            this.skWorkspaceParent.Size = new System.Drawing.Size(850, 388);
            this.skWorkspaceParent.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 388);
            this.Controls.Add(this.skWorkspaceParent);
            this.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel skWorkspaceParent;
    }
}

