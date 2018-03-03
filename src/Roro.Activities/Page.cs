using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Roro.Activities
{
    public interface IPage
    {
        Guid Id { get; set; }

        string Name { get; set; }

        List<Node> Nodes { get; set; }

        event EventHandler OnStateChanged;

        void Run();

        void Pause();

        void Stop();
    }

    public partial class Page : IPage, IDisposable
    {
        [XmlAttribute]
        public Guid Id { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        public List<Node> Nodes { get; set; }

        public IList<T> GetNodes<T>() where T : Node
        {
            return this.Nodes.Where(x => x is T).Cast<T>().ToList();
        }

        public Page()
        {
            this.Initialize();
            this.Initialize_Events();
            this.Initialize_Runner();
        }

        private void Initialize()
        {
            this.Id = Guid.NewGuid();
            this.Name = string.Empty;
            this.Nodes = new List<Node>();

            this.AddNode(typeof(StartNodeActivity).FullName,
                PageRenderOptions.GridSize * 15,
                PageRenderOptions.GridSize * 5);

            this.AddNode(typeof(EndNodeActivity).FullName,
                PageRenderOptions.GridSize * 15,
                PageRenderOptions.GridSize * 15);
        }

        private Node GetNodeById(Guid id)
        {
            return this.Nodes.FirstOrDefault(x => x.Id == id);
        }

        private Node AddNode(string activityId, int x, int y)
        {
            if (this.State == PageState.Running)
            {
                MessageBox.Show("The robot is currently running.");
                return null;
            }

            Node node;
            var activity = Activity.CreateInstance(activityId);
            if (activity is StartNodeActivity)
            {
                node = new StartNode(activity)
                {
                    Name = "Start"
                };
            }
            else if (activity is EndNodeActivity)
            {
                node = new EndNode(activity)
                {
                    Name = "End"
                };
            }
            else if (activity is ProcessNodeActivity)
            {
                node = new ProcessNode(activity);
            }
            else if (activity is DecisionNodeActivity)
            {
                node = new DecisionNode(activity);
            }
            else if (activity is LoopStartNodeActivity)
            {
                node = new LoopStartNode(activity)
                {
                    Name = "Start Loop"
                };
                var loopStartNode = node as LoopStartNode;
                var loopEndNode = this.AddNode(typeof(LoopEndNodeActivity).FullName, x, y + PageRenderOptions.GridSize * 10) as LoopEndNode;
                loopStartNode.LoopEndNodeId = loopEndNode.Id;
                loopEndNode.LoopStartNodeId = loopStartNode.Id;
            }
            else if (activity is LoopEndNodeActivity)
            {
                node = new LoopEndNode(activity)
                {
                    Name = "End Loop"
                };
            }
            else if (activity is VariableNodeActivity)
            {
                var variableIndex = 1;
                while (this.GetNodes<VariableNode>().Count(v => v.Name == "Variable " + variableIndex) > 0)
                {
                    variableIndex++;
                }
                node = new VariableNode(activity)
                {
                    Name = "Variable " + variableIndex
                };
            }
            else
            {
                throw new NotSupportedException();
            }
            var bounds = node.Bounds;
            bounds.X = x - bounds.Width / 2;
            bounds.Y = y - bounds.Height / 2;
            node.SetBounds(bounds);
            this.Nodes.Add(node);
            return node;
        }

        private void RemoveNode(Node node)
        {
            if (node is StartNode)
            {
                MessageBox.Show("Start activity cannot be deleted.");
            }
            else if (node is LoopStartNode loopStartNode)
            {
                if (this.GetNodeById(loopStartNode.LoopEndNodeId) is Node relatedNode)
                {
                    this.Nodes.Remove(relatedNode);
                }
                this.Nodes.Remove(node);
            }
            else if (node is LoopEndNode loopEndNode)
            {
                if (this.GetNodeById(loopEndNode.LoopStartNodeId) is Node relatedNode)
                {
                    this.Nodes.Remove(relatedNode);
                }
                this.Nodes.Remove(node);
            }
            else
            {
                this.Nodes.Remove(node);
            }
        }

        public override string ToString()
        {
            return XmlSerializerHelper.ToString(this);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}