
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace Roro
{
    public sealed class ElementBot
    {
        private readonly Highligher highligter;

        private readonly IList<Context> contexts;

        private InputEventArgs pointEvent;

        private readonly Timer pointTimer;

        private bool hasPending = false;

        private bool isBusy = false;

        public ElementBot()
        {
            this.highligter = new Highligher();
            this.contexts = new List<Context>();
            this.contexts.Add(WinContext.Shared);
            //this.contexts.Add(SapContext.Shared);
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

                //Console.WriteLine(query);
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
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: {1} - {0}", ex.Message, typeof(T).Name);
                context = new IEContext();
            }
            context.GoToUrl(url);
            this.contexts.Add(context);
            return context;
        }

        public WebContext LaunchChrome(string url = null)
        {
            return this.LaunchWeb<ChromeContext>(url);
        }

        public WebContext LaunchIE(string url = null)
        {
            return this.LaunchWeb<IEContext>(url);
        }

        public WebContext LaunchEdge(string url = null)
        {
            return this.LaunchWeb<EdgeContext>(url);
        }
    }
}