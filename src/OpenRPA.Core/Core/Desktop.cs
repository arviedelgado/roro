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

using OpenRPA.Inputs;
using OpenRPA.Queries;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;

namespace OpenRPA.Core
{
    public sealed class Desktop
    {
        private readonly Highligher highligter;

        //private readonly SapContext sapContext;

        private readonly WebContext webContext;

        private readonly Timer focusTimer;

        private readonly Timer pointTimer;

        private InputEventArgs pointEvent;

        public Desktop()
        {
            this.highligter = new Highligher();
            //this.sapContext = new SapContext();
            this.webContext = new ChromeContext();
            this.focusTimer = new Timer(GetElementFromFocus, null, Timeout.Infinite, Timeout.Infinite);
            this.pointTimer = new Timer(GetElementFromPoint, null, Timeout.Infinite, Timeout.Infinite);

            Input.OnMouseMove += Input_OnMouseMove;
            Input.OnMouseUp += Input_OnMouseUp;
            Input.OnKeyUp += Input_OnKeyUp;
        }

        private void Input_OnMouseMove(InputEventArgs e)
        {
            this.pointEvent = e;
            this.pointTimer.Change(500, Timeout.Infinite);
        }

        private void Input_OnMouseUp(InputEventArgs e)
        {
            this.pointTimer.Change(Timeout.Infinite, Timeout.Infinite);
            this.focusTimer.Change(0, Timeout.Infinite);
        }

        private void Input_OnKeyUp(InputEventArgs e)
        {
            this.pointTimer.Change(Timeout.Infinite, Timeout.Infinite);
            this.focusTimer.Change(0, Timeout.Infinite);
        }

        private void GetElementFromFocus(object state)
        {
            //            try
            //            {
            //                WinElement winElement = WinElement.GetFromFocus();

            //                //WebElement webElement = this.webContext.GetElementFromFocus(winElement);

            //                //Element element = webElement ?? winElement as Element;

            //                //Console.WriteLine("FOCUS: {0}", element.Path);

            //                this.highligter.Invoke(winElement.Bounds);
            ////                Console.Write(winElement.Serialize());
            //            }
            //            catch (Exception ex)
            //            {
            //                Console.WriteLine("ERROR: {0}", ex);
            //            }
        }

        private void GetElementFromPoint(object state)
        {
            try
            {
                var e = pointEvent;

                Context context = WinContext.Shared;
                Element element = context.GetElementFromPoint(e.X, e.Y);
                Rect offset = new Rect();

                this.highligter.Invoke(element.Bounds, Color.Red);

                if (this.webContext.GetElementFromPoint(e.X, e.Y) is WebElement webElement)
                {
                    context = this.webContext;
                    element = webElement;
                    offset = this.webContext.Viewport.Bounds;
                }

                var query = element.GetQuery();

                Console.Title = element.Path;
                Console.Clear();
                Console.WriteLine(query);
                Console.WriteLine();

                var sw = Stopwatch.StartNew();
                var elements = context.GetElementsFromQuery(query);
                Console.WriteLine("Matches: {0} in {1} seconds", elements.Count, sw.ElapsedMilliseconds / 1000.0);

                foreach (var el in elements)
                {
                    var bounds = el.Bounds;
                    bounds.X += offset.X;
                    bounds.Y += offset.Y;
                    Console.WriteLine(bounds);
                    this.highligter.Invoke(bounds, Color.Blue);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: {0}", ex);
            }
        }

        public void Launch(string path, string args = null)
        {
            var p = Process.Start(path, args ?? string.Empty);
            while (p.MainWindowHandle == IntPtr.Zero)
            {
                p.WaitForInputIdle(1000);
                p.Refresh();
            }
        }
    }
}