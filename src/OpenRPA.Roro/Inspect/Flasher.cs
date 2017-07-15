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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace OpenRPA.Roro
{
    public class Flasher
    {
        private static readonly Flasher Instance = new Flasher();

        private readonly Form form;

        private Flasher()
        {
            this.form = new Form();
            this.form.FormBorderStyle = FormBorderStyle.None;
            this.form.BackColor = System.Drawing.Color.Red;
            Thread staThread = new Thread(() =>
            {
                const int border = 5;
                this.form.Opacity = 0.6;
                this.form.Bounds = this.rect = this.toRect = Rectangle.Empty;
                this.form.FormBorderStyle = FormBorderStyle.None;
                this.form.ShowInTaskbar = false;   // Hide in taskbar
                this.form.Text = string.Empty;     // Hide in task manager
                this.form.Visible = true;  // Set Visible (to true) before Enabled (to false) to hide in ALT+TAB windows
                this.form.Enabled = false; // Disable focus, mouse and keyboard events
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
                this.form.Invalidate();
                Application.Run(this.form);
            });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
        }

        private Rectangle rect;

        private Rectangle toRect;

        public static void Flash(Rect r)
        {
            Flasher.Instance.rect = Flasher.Instance.toRect = new Rectangle(r.X, r.Y, r.Width, r.Height);
            Flasher.Instance.form.Invalidate();
        }
    }
}
