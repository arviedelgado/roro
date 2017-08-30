using System;
using OpenRPA.Core;

namespace OpenRPA.Test
{
    class Program
    {
        static void Main()
        {
            var desktop = new Desktop();
            //     desktop.LaunchChrome();
            //     desktop.LaunchEdge();
            desktop.LaunchIE("http://www.google.com/");
            Console.ReadLine();
        }
    }
}
