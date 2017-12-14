using System.Windows.Forms;

namespace Roro.Workflow
{
    public class PageForm : Form
    {
        private Panel page;

        private void InitializeComponent()
        {
            this.page = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // page
            // 
            this.page.Dock = System.Windows.Forms.DockStyle.Fill;
            this.page.Location = new System.Drawing.Point(0, 0);
            this.page.Name = "page";
            this.page.Size = new System.Drawing.Size(284, 261);
            this.page.TabIndex = 0;
            // 
            // PageForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.page);
            this.Name = "PageForm";
            this.ResumeLayout(false);

        }

        private PageForm() => this.InitializeComponent();
    }
}
