using System;
using OpenRPA.Roro.Apps;
using OpenRPA.Roro.Inspect;

namespace OpenRPA.Roro.Test
{
    class Program
    {
        static void Main()
        {

            IApp app;

            // app = App.Start("notepad");

            try
            {
                app = WebApp.StartChrome("http://www.example.com");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                app = WebApp.StartIE("http://www.example.com");
            }

            var inspector = new Inspector(app);

            while (true)
            {
                inspector.PrintAndFlash();
            }
        }
    }

    public class Inspector
    {
        private readonly IApp app;

        private Func<IElement> cachedFunc;

        private InputEventArgs cachedArgs;

        public Inspector(IApp app)
        {
            this.app = app;
            this.cachedFunc = () => { return null; };
            Input.OnMouseMove += GetFromPoint;
            Input.OnMouseUp += GetFromFocus;
            Input.OnKeyUp += GetFromFocus;
        }

        private void GetFromFocus(ref InputEventArgs args)
        {
            this.cachedArgs = args;
            this.cachedFunc = () => { return app.GetElementFromFocus(); };
        }

        private void GetFromPoint(ref InputEventArgs args)
        {
            this.cachedArgs = args;
            this.cachedFunc = () => { return app.GetElementFromPoint(this.cachedArgs.X, this.cachedArgs.Y); };
        }

        public void PrintAndFlash()
        {
            try
            {
                var sw = System.Diagnostics.Stopwatch.StartNew();
                var el = this.cachedFunc.Invoke();
                if (el == null) return;
                Console.WriteLine("{0} Id=\"{1}\" Name=\"{2}\" Class=\"{3}\"", el.Path, el.Id, el.Name, el.Class);
                Console.WriteLine(sw.ElapsedMilliseconds / 1000.0);
                Flasher.Flash(el.Rect);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: {0}", ex.Message);
            }
        }
    }
}
