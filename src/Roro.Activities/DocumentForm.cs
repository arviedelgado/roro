using System.Linq;
using System.Windows.Forms;

namespace Roro.Workflow
{
    public class DocumentForm : Form
    {

        private void InitializeComponent()
        {
            this.activityPanel = new System.Windows.Forms.Panel();
            this.pagePanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // activityPanel
            // 
            this.activityPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.activityPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.activityPanel.Location = new System.Drawing.Point(3, 69);
            this.activityPanel.Name = "activityPanel";
            this.activityPanel.Padding = new System.Windows.Forms.Padding(0, 20, 0, 0);
            this.activityPanel.Size = new System.Drawing.Size(186, 489);
            this.activityPanel.TabIndex = 0;
            // 
            // pagePanel
            // 
            this.pagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pagePanel.Location = new System.Drawing.Point(195, 69);
            this.pagePanel.Name = "pagePanel";
            this.pagePanel.Size = new System.Drawing.Size(786, 489);
            this.pagePanel.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.5122F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.48781F));
            this.tableLayoutPanel1.Controls.Add(this.activityPanel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pagePanel, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.94296F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.05704F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(984, 561);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // DocumentForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DocumentForm";
            this.Load += new System.EventHandler(this.DocumentForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private DocumentForm() => this.InitializeComponent();

        private Panel pagePanel;
        private Panel activityPanel;
        private TableLayoutPanel tableLayoutPanel1;
        private Document doc;

        public static DocumentForm Create()
        {
            return new DocumentForm();
        }

        private void DocumentForm_Load(object sender, System.EventArgs e)
        {
            this.doc = new Document();
            this.doc.Pages.First().AttachEvents(this.pagePanel);
            ActivityForm.Create().Parent = this.activityPanel;
        }
    }
}
