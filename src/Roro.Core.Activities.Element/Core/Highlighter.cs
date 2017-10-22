// BSD 2-Clause License

// Copyright(c) 2017, Arvie Delgado
// All rights reserved.

// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:

// * Redistributions of source code must retain the above copyright notice, this
//   list of conditions and the following disclaimer.

// * Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution.

// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED.IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace Roro.Core
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
            new Thread(() =>
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
            }).Start();
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
