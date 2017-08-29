using System;
using OpenRPA.Core;

namespace OpenRPA.Test
{
    class Program
    {
        static void Main()
        {
            var inspector = new Desktop();
       //     inspector.LaunchChrome();
       //     inspector.LaunchEdge();
            inspector.LaunchIE("http://www.google.com/");
            Console.ReadLine();
        }
    }
}
