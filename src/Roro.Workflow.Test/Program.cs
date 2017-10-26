using Roro;
using Roro.Activities.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Roro.Workflow.Test
{
    public class Program
    {
        public static void Main()
        {
                        TestWorkflowSerialization();

            Console.WriteLine("Press any key to run Element Inspector . . . ");
            Console.ReadKey();
            TestElementActivities();

        }

        public static void TestElementActivities()
        {
            Console.WriteLine();
            Console.Clear();

            var desktop = new ElementBot();
            desktop.LaunchChrome("https://example.com");
            desktop.LaunchEdge("https://example.com");
            while (true)
            {
                var query = desktop.Inspect();
                var els = desktop.Query(query);
                Console.WriteLine("{0} match found..", els.Count());
                Console.WriteLine();
                foreach (var el in els)
                {
                    Console.WriteLine("{0} - {1}", el.Path, el.Bounds);
                }
            }
        }

        public static void TestWorkflowSerialization()
        {
            var page = new Page();
            page.Add<StartNode>();
            page.Add<ProcessNode>();
            page.Add<DecisionNode>();
            page.Add<ProcessNode>();
            page.Add<ProcessNode>();
            page.Add<EndNode>();
            Console.WriteLine("Page run started.");
            var next = page.Execute();
            while (next != Guid.Empty)
            {
                next = page.GetNodeById(next).Execute();
                Console.WriteLine("Next: {0}", next);
                Console.ReadLine();
            }
            Console.WriteLine("Page run ended.");
            Console.WriteLine();

            Console.Clear();
            Console.WriteLine("Serializing Object to XML . . .");
            Console.WriteLine();
            var a = PageSerializer.ToString(page);
            Console.WriteLine(a);
            Console.WriteLine();
            Console.WriteLine("Serializing Object to XML completed.");
            Console.WriteLine("Deserializing XML to Object . . .");
            var b = PageSerializer.ToString(PageSerializer.ToObject<Page>(a));
            Console.WriteLine("Deserializing XML to Object completed.");
            Console.WriteLine("Matched: {0}", a == b);
            Console.WriteLine();
            var activities = AppDomain.CurrentDomain.GetAssemblies()
    .SelectMany(x => x.GetTypes())
    .Where(x => x.BaseType == typeof(Roro.Activities.Activity));
            foreach (var act in activities)
            {
                Console.WriteLine(act);
            }

        }
    }
}
