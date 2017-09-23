using System;
using OpenRPA.Core;
using System.Linq;
using System.Diagnostics;

namespace OpenRPA.Test
{
    class Program
    {
        static void Main()
        {
            var desktop = new Desktop();
            var context = desktop.LaunchChrome("http://example.com");
            context.ExecuteScript(@"
                var iframe = document.body.appendChild(document.createElement('iframe'));
                iframe.src = 'http://example.com';
                iframe.style.cssText = 'position: fixed; top: 0; left: 0;';
            ");

            context.ExecuteScript(@"
                var iframe = document.body.appendChild(document.createElement('iframe'));
                iframe.src = 'http://example.com';
                iframe.style.cssText = 'position: fixed; bottom: 0; right: 0;';
            ");


            while (true)
            {
                var query = desktop.Inspect();
                var sw = Stopwatch.StartNew();
                var elements = desktop.Query(query);
                Console.WriteLine("Result: {0} elements found in {1} seconds", elements.Count(), sw.ElapsedMilliseconds / 1000.0);
                Console.WriteLine();
                foreach (var el in elements)
                {
                    Console.WriteLine("{0}\t{1}", el.Bounds, el.Path);
                }
            }
        }
    }
}
