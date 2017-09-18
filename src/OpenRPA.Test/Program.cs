using System;
using OpenRPA.Core;

namespace OpenRPA.Test
{
    class Program
    {
        static void Main()
        {
            var desktop = new Desktop();
            desktop.LaunchChrome("http://rpaaas.com");
            desktop.LaunchEdge("http://rpaaas.com");
            desktop.LaunchInternetExplorer("http://rpaaas.com");
            Console.ReadLine();
        }
    }
}
