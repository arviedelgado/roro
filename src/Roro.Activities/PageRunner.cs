using System;
using System.Threading;
using System.Threading.Tasks;

namespace Roro.Activities
{
    public class PageRunner
    {
        private Page CurrentPage { get; set; }
        
        public PageRunner(Page page)
        {
            this.CurrentPage = page;
        }

        public void Run()
        {
            if (this.CurrentPage.DebugNode == null)
            {
                this.CurrentPage.DebugNode = this.CurrentPage.FirstNode;
            }
            Console.WriteLine("Run started.");
            this.RunNext();
        }

        private void RunNext()
        {
            Task.Run(() =>
            {
                if (this.CurrentPage.DebugNode is Node node)
                {
                    Console.WriteLine("Execute {0} - {1}", node.Id, node.Name);
                    this.CurrentPage.Render();
                    Thread.Sleep(500);
                    this.CurrentPage.DebugNode = this.CurrentPage.GetNodeById(node.Execute());
                    this.RunNext();
                }
                else
                {
                    Console.WriteLine("Run completed.");
                }
            });
        }
    }
}
