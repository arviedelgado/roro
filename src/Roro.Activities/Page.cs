using Roro.Activities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace Roro.Workflow
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
        internal List<Variable> Variables { get; private set; }

        [DataMember]
        private List<Node> Nodes { get; set; }

        private HashSet<Node> SelectedNodes { get; set; }

        private Dictionary<Node, GraphicsPath> RenderedNodes { get; }

        public Page()
        {
            this.Id = Guid.NewGuid();
            this.Name = string.Format("My{0}_{1}", this.GetType().Name, System.DateTime.Now.Ticks);
            this.Nodes = new List<Node>();
            this.StartNodeActivity = new StartNodeActivity();
            this.EndNodeActivity = new EndNodeActivity();
            this.SelectedNodes = new HashSet<Node>();
            this.RenderedNodes = new Dictionary<Node, GraphicsPath>();

            // test variables
            this.Variables = new List<Variable>()
            {
                new Variable<Text>("Text1"),
                new Variable<Text>("Text2"),
                new Variable<Number>("Num1"),
                new Variable<Number>("Num2"),
                new Variable<Number>("Num3"),
                new Variable<Flag>("Flag1"),
                new Variable<Activities.DateTime>("DateTime1"),
                new Variable<Collection>("Collection1"),
            };
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

        public Node AddNode(string activityFullName)
        {
            Node node;
            var activity = Activity.CreateInstance(activityFullName);
            if (activity is StartNodeActivity)
            {
                node = new StartNode(this.StartNodeActivity);
            }
            else if (activity is EndNodeActivity)
            {
                node = new EndNode(this.EndNodeActivity);
            }
            else if (activity is ProcessNodeActivity)
            {
                node = new ProcessNode(activity);
            }
            else if (activity is DecisionNodeActivity)
            {
                node = new DecisionNode(activity);
            }
            else if (activity is PreparationNodeActivity)
            {
                node = new PreparationNode(activity);
            }
            else if (activity is LoopStartNodeActivity)
            {
                node = new LoopStartNode(activity);
            }
            else if (activity is LoopEndNodeActivity)
            {
                node = new LoopEndNode(activity);
            }
            else
            {
                throw new NotSupportedException();
            }
            node.Name = activityFullName.Split('.').Last().Humanize();
            this.Nodes.Add(node);
            return node;
        }

        #region Attach/Detach Events

        private Control canvas;

        public void AttachEvents(Panel canvas)
        {
            this.canvas = canvas;
            this.canvas.Dock = DockStyle.Fill;
            this.canvas.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(this.canvas, true);
            this.canvas.Paint += OnPaint;
            this.canvas.MouseDown += MouseEvents;
            this.canvas.KeyDown += KeyEvents;
            this.canvas.DragEnter += DragEnterEvent;
            this.canvas.DragDrop += DragDropEvent;
        }

        public void DetachEvents()
        {
            this.canvas.Paint -= OnPaint;
            this.canvas.MouseDown -= MouseEvents;
            this.canvas.KeyDown -= KeyEvents;
        }

        #endregion
    }
}