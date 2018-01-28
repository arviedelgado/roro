using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roro.Activities
{
    public partial class Page
    {
        private Node DebugNode { get; set; }

        private bool Started { get; set; }

        private bool Stopping { get; set; }

        public void Start()
        {
            if (this.Started)
            {
                MessageBox.Show("The robot is already running.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                this.Started = true;
                Console.Clear();
                Console.WriteLine("INFO: Start.");
                foreach (var variableNode in this.VariableNodes)
                {
                    variableNode.CurrentValue = variableNode.InitialValue;
                }
                this.StepNext(this.Nodes.Find(x => x is StartNode));
            }
        }

        public void Stop()
        {
            if (this.Started && this.Stopping)
            {
                MessageBox.Show("The robot will stop after completing the current activity.. please wait.");
            }
            else if (this.Started)
            {
                this.Stopping = true;
            }
        }

        private void StepNext(Node node)
        {
            Task.Run(() =>
            {
                try
                {
                    Console.WriteLine("Executing {0} - {1}", node.Id, node.Name);
                    this.DebugNode = node;
                    this.Canvas.Invalidate();
                    var nextNodeId = node.Execute(this.VariableNodes);
                    if (this.GetNodeById(nextNodeId) is Node nextNode)
                    {
                        if (this.Stopping)
                        {
                            throw new Exception("Stopped by user.");
                        }
                        Thread.Sleep(500);
                        StepNext(nextNode);
                    }
                    else if (node is EndNode endNode)
                    {
                        this.Started = false;
                        this.Stopping = false;
                        Console.WriteLine("INFO: End.");
                        MessageBox.Show("The robot completed the activities successfully.", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        throw new Exception("Next activity not found.");
                    }
                }
                catch (Exception ex)
                {
                    this.Started = false;
                    this.Stopping = false;
                    MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }
    }
}
