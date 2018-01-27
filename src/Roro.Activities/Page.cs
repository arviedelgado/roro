
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
    public partial class Page
    {
        [DataMember]
        public Guid Id { get; private set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        private StartNodeActivity StartNodeActivity { get; set; }

        [DataMember]
        private EndNodeActivity EndNodeActivity { get; set; }

        [DataMember]
        private List<Node> Nodes { get; set; }

        private HashSet<Node> SelectedNodes { get; set; }

        private Dictionary<Node, GraphicsPath> RenderedNodes { get; set; }

        internal List<VariableNode> VariableNodes => this.Nodes.Where(x => x is VariableNode).Cast<VariableNode>().ToList();

        public Page() => Initialize();

        [OnDeserializing]
        private void Initialize(StreamingContext context = default)
        {
            this.Id = Guid.NewGuid();
            this.Name = string.Format("My{0}_{1}", this.GetType().Name, System.DateTime.Now.Ticks);
            this.Nodes = new List<Node>();
            this.SelectedNodes = new HashSet<Node>();
            this.RenderedNodes = new Dictionary<Node, GraphicsPath>();

            this.StartNodeActivity = new StartNodeActivity();
            this.EndNodeActivity = new EndNodeActivity();

            this.AddNode(typeof(StartNodeActivity).FullName,
                PageRenderOptions.GridSize * 15,
                PageRenderOptions.GridSize * 5);

            this.AddNode(typeof(EndNodeActivity).FullName,
                PageRenderOptions.GridSize * 15,
                PageRenderOptions.GridSize * 15);
        }

        public Node GetNodeById(Guid id)
        {
            return this.Nodes.FirstOrDefault(x => x.Id == id);
        }

        public Node GetNodeFromPoint(Point pt)
        {
            if (this.RenderedNodes.FirstOrDefault(x => x.Value.IsVisible(pt.X, pt.Y)) is KeyValuePair<Node, GraphicsPath> item)
            {
                return item.Key;
            }
            return null;
        }

        public Node AddNode(string activityFullName, int x, int y)
        {
            if (this.Started)
            {
                MessageBox.Show("Cannot add activities while the robot is running.");
                return null;
            }

            Node node;
            var activity = Activity.CreateInstance(activityFullName);
            if (activity is StartNodeActivity)
            {
                node = new StartNode(this.StartNodeActivity)
                {
                    Name = "Start"
                };
            }
            else if (activity is EndNodeActivity)
            {
                node = new EndNode(this.EndNodeActivity)
                {
                    Name = "End"
                };
            }
            else if (activity is ProcessNodeActivity)
            {
                node = new ProcessNode(activity)
                {
                    Name = activityFullName.Split('.').Last().Humanize()
                };
            }
            else if (activity is DecisionNodeActivity)
            {
                node = new DecisionNode(activity)
                {
                    Name = activityFullName.Split('.').Last().Humanize()
                };
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

        public void RemoveNode(Node node)
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

        #region Attach/Detach Events

        private Control canvas;

        public void AttachEvents(Panel canvas)
        {
            this.canvas = canvas;
            this.canvas.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(this.canvas, true);
            this.canvas.Paint += OnPaint;
            this.canvas.MouseDown += Canvas_MouseDown;
            this.canvas.KeyDown += Canvas_KeyDown;
            this.canvas.AllowDrop = true;
            this.canvas.DragEnter += Canvas_DragEnter;
            this.canvas.DragDrop += Canvas_DragDrop;
        }

        #endregion
    }
}