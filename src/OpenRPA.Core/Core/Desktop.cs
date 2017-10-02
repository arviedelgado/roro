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
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace OpenRPA.Core
{
    public sealed class Desktop
    {
        private readonly Highligher highligter;

        private readonly IList<Context> contexts;

        private InputEventArgs pointEvent;

        private readonly Timer pointTimer;

        private bool hasPending = false;

        private bool isBusy = false;

        public Desktop()
        {
            this.highligter = new Highligher();
            this.contexts = new List<Context>();
            this.contexts.Add(WinContext.Shared);
            this.contexts.Add(SapContext.Shared);
            this.pointTimer = new Timer(GetElementFromPoint, null, Timeout.Infinite, Timeout.Infinite);

            Console.WindowHeight = Convert.ToInt32(Console.LargestWindowHeight * 0.8);
        }

        private Query Result = new Query();

        public Query Inspect()
        {
            this.Result = null;
            Input.OnMouseMove += Input_OnMouseMove;
            Input.OnKeyUp += Input_OnKeyUp;
            while (this.Result == null) { }
            return this.Result;
        }

        public IEnumerable<Element> Query(Query query)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("GET_ELEMENTS_FROM_QUERY");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();

            var result = new List<Element>();
            foreach (var context in this.contexts)
            {
                var elements = context.GetElementsFromQuery(query);
                if (elements.Count() > 0)
                {
                    result.AddRange(elements);
                    break;
                }
            }

            return result;
        }

        private void Input_OnKeyUp(InputEventArgs e)
        {
            if (e.Key == KeyboardKey.LeftCtrl || e.Key == KeyboardKey.RightCtrl)
            {
                Input.OnMouseMove -= Input_OnMouseMove;
                Input.OnKeyUp -= Input_OnKeyUp;
                this.pointEvent = e;
                this.pointTimer.Change(0, Timeout.Infinite);
            }
        }

        private void Input_OnMouseMove(InputEventArgs e)
        {
            this.hasPending = true;
            this.pointEvent = e;
            this.pointTimer.Change(500, Timeout.Infinite);
        }

        private void GetElementFromPoint(object state)
        {
            if (this.isBusy)
            {
                return;
            }

            this.isBusy = true;
            this.hasPending = false;

            try
            {
                var e = this.pointEvent;

                Console.Clear();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("GET_QUERY_FROM_ELEMENT_FROM_POINT {0} {1}", e.X, e.Y);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine();

                Context context = this.contexts.First();
                Element element = context.GetElementFromPoint(e.X, e.Y);

                this.highligter.Invoke(element.Bounds, Color.Red);

                foreach (var ctx in this.contexts.Skip(1))
                {
                    if (ctx.GetElementFromPoint(e.X, e.Y) is Element elem)
                    {
                        context = ctx;
                        element = elem;
                        continue;
                    }
                }

                var query = element.GetQuery();

                Console.WriteLine(query);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Press the CTRL key to get the elements using the query.");
                Console.WriteLine();

                if (e.Type == InputEventType.KeyUp)
                {
                    this.Result = query;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: {0}", ex);
            }
            finally
            {
                this.isBusy = false;
                if (this.hasPending)
                {
                    this.pointTimer.Change(0, Timeout.Infinite);
                }
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

        private WebContext LaunchWeb<T>(string url) where T: WebContext, new()
        {
            WebContext context;
            try
            {
                context = new T();
            }
            catch
            {
                context = new InternetExplorerContext();
            }
            context.GoToUrl(url);
            this.contexts.Add(context);
            return context;
        }

        public WebContext LaunchChrome(string url = null)
        {
            return this.LaunchWeb<ChromeContext>(url);
        }

        public WebContext LaunchInternetExplorer(string url = null)
        {
            return this.LaunchWeb<InternetExplorerContext>(url);
        }

        public WebContext LaunchEdge(string url = null)
        {
            return this.LaunchWeb<EdgeContext>(url);
        }
    }
}