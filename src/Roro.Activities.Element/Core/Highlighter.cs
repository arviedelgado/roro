using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roro
{
    internal class Highligher
    {
        private Rectangle rect;

        private Rectangle toRect;

        private readonly Form form;

        private const int border = 3;

        public Highligher()
        {
            this.form = new Form();
            Task.Run(() =>
            {
                this.form.Opacity = 0.8;
                this.form.BackColor = Color.Red;
                this.form.FormBorderStyle = FormBorderStyle.None;
                this.form.Bounds = this.rect = this.toRect = Rectangle.Empty;
                this.form.ShowInTaskbar = false;   // Hide in taskbar
                this.form.Text = string.Empty;     // Hide in task manager
                this.form.Visible = true;  // Set Visible (to true) before Enabled (to false) to hide in ALT+TAB windows
                this.form.Enabled = false; // Disable focus; mouse and keyboard events
                this.form.Invalidate();
                this.form.Paint += (object sender, PaintEventArgs args) =>
                {
                    Rectangle newRect = this.rect;
                    newRect.Inflate(border, border);
                    Region region = new Region(new Rectangle(0, 0, newRect.Width, newRect.Height));
                    region.Exclude(new Rectangle(border, border, newRect.Width - border * 2, newRect.Height - border * 2));
                    this.form.SetDesktopBounds(newRect.X, newRect.Y, newRect.Width, newRect.Height);
                    this.form.Region = region;
                    this.form.TopMost = true;
                };
                Application.Run(this.form);
            });
        }

        public void Invoke(Rectangle rect, Color color)
        {
            this.rect = this.toRect = rect;
            this.form.BackColor = color;
            this.form.Invalidate();
            this.form.Show();
        }
    }
}
