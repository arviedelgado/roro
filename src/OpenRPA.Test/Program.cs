using System;
using OpenRPA.Core;

namespace OpenRPA.Test
{
    class Program
    {
        static void Main()
        {
            var desktop = new Desktop();
            //var context = desktop.LaunchChrome("http://example.com");
            //context.ExecuteScript(@"
            //    var iframe = document.body.appendChild(document.createElement('iframe'));
            //    iframe.src = 'http://example.com';
            //    iframe.style.cssText = 'position: fixed; top: 0; left: 0;';
            //");

            //context.ExecuteScript(@"
            //    var iframe = document.body.appendChild(document.createElement('iframe'));
            //    iframe.src = 'http://example.com';
            //    iframe.style.cssText = 'position: fixed; bottom: 0; right: 0;';
            //");

            //            desktop.LaunchEdge("http://rpaaas.com");
            //            desktop.LaunchInternetExplorer("http://rpaaas.com");
            Console.ReadLine();
        }
    }
}
