using System;
using System.Drawing;
using System.Windows.Forms;

namespace Roro.Activities.Apps
{
    internal class ElementHighlighterForm : Form
    {
        private const int border = 3;

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Highlighter
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "Highlighter";
            this.Opacity = 0.8;
            this.BackColor = Color.Red;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Bounds = Rectangle.Empty;
            this.ShowInTaskbar = false;   // Hide in taskbar
            this.Text = string.Empty;     // Hide in task manager
            this.Visible = true;  // Set Visible (to true) before Enabled (to false) to hide in ALT+TAB windows
            this.Enabled = false; // Disable focus; mouse and keyboard events
            this.Load += new System.EventHandler(this.Highlighter_Load);
            this.ResumeLayout(false);

        }

        public ElementHighlighterForm()
        {
            this.InitializeComponent();
            this.Render(Screen.PrimaryScreen.Bounds, Color.Red);
        }

        public void Render(Rectangle rect, Color color)
        {
            Rectangle newRect = rect;
            newRect.Inflate(border, border);
            Region region = new Region(new Rectangle(0, 0, newRect.Width, newRect.Height));
            region.Exclude(new Rectangle(border, border, newRect.Width - border * 2, newRect.Height - border * 2));

            this.Invoke(new Action(() =>
            {
                this.DesktopBounds = newRect;
                this.Region = region;
                this.BackColor = color;
                this.TopMost = true;
                this.Show();
            }));
        }

        private void Highlighter_Load(object sender, EventArgs e)
        {

        }
    }
}
