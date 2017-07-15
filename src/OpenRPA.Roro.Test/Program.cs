using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenRPA.Roro;
using OpenRPA.Roro.Apps;

namespace OpenRPA.Roro.Test
{
    class Program
    {
        static void Main()
        {
            var app = App.Start("notepad");

            //var app = WebApp.StartChrome("http://www.roroscript.com");

            var moved = false;
            var point = default(InputEventArgs);
            Input.OnMouseMove += (ref InputEventArgs args) =>
            {
                moved = true;
                point = args;
            };
            while (true)
            {
                if (moved)
                {
                    moved = false;
                    var el = app.GetElementFromPoint(point.X, point.Y);
                    if (el == null) continue;
                    Console.Title = string.Format("Inspector [{0}, {1}]", point.X, point.Y);
                    Console.WriteLine("{0} Id=\"{1}\" Name=\"{2}\" Class=\"{3}\"", el.Path, el.Id, el.Name, el.Class);
                    Flasher.Flash(el.Rect);
                }
            }
        }
    }
}
