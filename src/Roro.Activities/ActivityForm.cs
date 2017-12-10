using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roro.Activities
{
    public class ActivityForm : Form
    {
        private SplitContainer argumentSplitContainer;

        private void InitializeComponent()
        {
            this.argumentSplitContainer = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.argumentSplitContainer)).BeginInit();
            this.argumentSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // argumentSplitContainer
            // 
            this.argumentSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.argumentSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.argumentSplitContainer.Name = "argumentSplitContainer";
            this.argumentSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.argumentSplitContainer.Size = new System.Drawing.Size(734, 411);
            this.argumentSplitContainer.SplitterDistance = 162;
            this.argumentSplitContainer.TabIndex = 0;
            // 
            // ActivityForm
            // 
            this.ClientSize = new System.Drawing.Size(734, 411);
            this.Controls.Add(this.argumentSplitContainer);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.Name = "ActivityForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.ActivityForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.argumentSplitContainer)).EndInit();
            this.argumentSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        public ActivityForm(Activity activity)
        {
            this.InitializeComponent();

            if (activity.GetArguments<InArgument>() is List<InArgument> inArguments)
            {
                ArgumentForm.Create(activity, inArguments, null).Parent = this.argumentSplitContainer.Panel1;
            }

            if (activity.GetArguments<OutArgument>() is List<OutArgument> outArguments)
            {
                ArgumentForm.Create(activity, outArguments, null).Parent = this.argumentSplitContainer.Panel2;
            }

            if (activity.GetArguments<InOutArgument>() is List<InOutArgument> inOutArguments)
            {
                ArgumentForm.Create(activity, inOutArguments, null).Parent = this.argumentSplitContainer.Panel1;
            }
        }

        private void ActivityForm_Load(object sender, EventArgs e)
        {

        }
    }
}
