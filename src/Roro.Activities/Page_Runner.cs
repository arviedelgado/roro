using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Roro.Activities
{
    public enum PageState
    {
        Stopped = 0,
        Running,
        Paused,
        Completed
    }

    public partial class Page
    {
        public PageState State { get; private set; }

        public event EventHandler OnStateChanged;

        private Node currentNode = null;

        private CancellationTokenSource ctsPause;

        private CancellationTokenSource ctsStop;

        private void Initialize_PageRunner()
        {
            this.State = PageState.Stopped;
            this.OnStateChanged = delegate { };
            this.currentNode = null;
            this.ctsPause = new CancellationTokenSource();
            this.ctsStop = new CancellationTokenSource();
        }

        public void Run()
        {
            switch (this.State)
            {
                case PageState.Running:
                    MessageBox.Show("The robot is already running.");
                    Console.WriteLine("The robot is already running.");
                    break;

                case PageState.Paused:
                    this.ctsPause = new CancellationTokenSource();
                    this.ctsStop = new CancellationTokenSource();
                    this.State = PageState.Running;
                    this.OnStateChanged.Invoke(null, null);
                    this.RunNextAsync();
                    break;

                case PageState.Stopped:
                case PageState.Completed:
                    this.currentNode = this.Nodes.First(x => x is StartNode);
                    this.ctsPause = new CancellationTokenSource();
                    this.ctsStop = new CancellationTokenSource();
                    this.State = PageState.Running;
                    this.OnStateChanged.Invoke(null, null);
                    this.RunNextAsync();
                    break;
            }
        }

        public void Pause()
        {
            switch (this.State)
            {
                case PageState.Running:
                    if (this.ctsPause.IsCancellationRequested)
                    {
                        MessageBox.Show("The robot is already pausing.");
                        Console.WriteLine("The robot is already pausing.");
                    }
                    else
                    {
                        this.ctsPause.Cancel();
                    }
                    break;

                case PageState.Paused:
                    MessageBox.Show("The robot is not running.");
                    Console.WriteLine("The robot is not running.");
                    break;

                case PageState.Stopped:
                case PageState.Completed:
                    MessageBox.Show("The robot is not running.");
                    Console.WriteLine("The robot is not running.");
                    break;
            }
        }

        public void Stop()
        {
            switch (this.State)
            {
                case PageState.Running:
                    if (this.ctsStop.IsCancellationRequested)
                    {
                        MessageBox.Show("The robot is already stopping.");
                        Console.WriteLine("The robot is already stopping.");
                    }
                    else
                    {
                        this.ctsStop.Cancel();
                    }
                    break;

                case PageState.Paused:
                    this.State = PageState.Stopped;
                    this.OnStateChanged.Invoke(null, null);
                    break;

                case PageState.Stopped:
                case PageState.Completed:
                    MessageBox.Show("The robot is not running.");
                    Console.WriteLine("The robot is not running.");
                    break;
            }
        }

        private void RunNextAsync()
        {
            Task.Run(() =>
            {
                try
                {
                    this.Canvas.Invalidate();
                    this.ctsStop.Token.ThrowIfCancellationRequested();
                    this.ctsPause.Token.ThrowIfCancellationRequested();
                    if (this.currentNode == null)
                    {
                        MessageBox.Show("The robot cannot find the current activity.");
                        throw new OperationCanceledException("The robot cannot find the current activity.", this.ctsPause.Token);
                    }
                    var nextNodeId = this.currentNode.Execute(this.VariableNodes);
                    if (this.currentNode is EndNode endNode)
                    {
                        throw new OperationCanceledException("The robot completed successfully.", null);
                    }
                    if (this.GetNodeById(nextNodeId) is Node nextNode)
                    {
                        this.currentNode = nextNode;
                        this.RunNextAsync();
                    }
                    else
                    {
                        MessageBox.Show("The robot cannot find the next activity.");
                        throw new OperationCanceledException("The robot cannot find the next activity.", this.ctsPause.Token);
                    }
                }
                catch (OperationCanceledException opex)
                {
                    if (opex.CancellationToken == this.ctsPause.Token)
                    {
                        this.State = PageState.Paused;
                        this.OnStateChanged.Invoke(null, null);
                    }
                    else if (opex.CancellationToken == this.ctsStop.Token)
                    {
                        this.State = PageState.Stopped;
                        this.OnStateChanged.Invoke(null, null);
                    }
                    else
                    {
                        this.State = PageState.Completed;
                        this.OnStateChanged.Invoke(null, null);
                    }
                }
                catch (Exception ex)
                {
                    this.State = PageState.Paused;
                    this.OnStateChanged.Invoke(null, null);
                    Console.WriteLine(ex.Message);
                    MessageBox.Show(ex.Message);
                }
            });
        }
    }
}
