
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace Roro.Activities
{
    [DataContract]
    public partial class Page : IDisposable
    {
        [DataMember]
        public Guid Id { get; private set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        private List<Node> Nodes { get; set; }

        private HashSet<Node> SelectedNodes { get; set; }

        private Dictionary<Node, GraphicsPath> RenderedNodes { get; set; }

        internal List<VariableNode> VariableNodes => this.Nodes.Where(x => x is VariableNode).Cast<VariableNode>().ToList();

        private Panel Canvas { get; set; }

        private Page() => Initialize();

        [OnDeserializing]
        private void Initialize(StreamingContext context = default)
        {
            this.Id = Guid.NewGuid();
            this.Name = string.Format("My{0}_{1}", this.GetType().Name, System.DateTime.Now.Ticks);
            this.Nodes = new List<Node>();
            this.SelectedNodes = new HashSet<Node>();
            this.RenderedNodes = new Dictionary<Node, GraphicsPath>();

            this.AddNode(typeof(StartNodeActivity).FullName,
                PageRenderOptions.GridSize * 15,
                PageRenderOptions.GridSize * 5);

            this.AddNode(typeof(EndNodeActivity).FullName,
                PageRenderOptions.GridSize * 15,
                PageRenderOptions.GridSize * 15);

            this.Canvas = new Panel();
            this.Canvas.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(this.Canvas, true);
            this.Canvas.Paint += Canvas_Paint;
            this.Canvas.MouseDown += Canvas_MouseDown;
            this.Canvas.KeyDown += Canvas_KeyDown;
            this.Canvas.AllowDrop = true;
            this.Canvas.DragEnter += Canvas_DragEnter;
            this.Canvas.DragDrop += Canvas_DragDrop;

            this.Initialize_PageRunner();
        }

        private Node GetNodeById(Guid id)
        {
            return this.Nodes.FirstOrDefault(x => x.Id == id);
        }

        private Node GetNodeFromPoint(Point pt)
        {
            if (this.RenderedNodes.FirstOrDefault(x => x.Value.IsVisible(pt.X, pt.Y)) is KeyValuePair<Node, GraphicsPath> item)
            {
                return item.Key;
            }
            return null;
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
                while (this.VariableNodes.Exists(v => v.Name == "Variable " + variableIndex))
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
            bounds.Location = new Point(x, y);
            bounds.Offset(-bounds.Width / 2, -bounds.Height / 2);
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

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}