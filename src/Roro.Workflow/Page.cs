using Roro.Activities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
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
        private List<Node> Nodes { get; set; }

        private HashSet<Node> SelectedNodes { get; set; }

        private Dictionary<Node, GraphicsPath> RenderedNodes { get; }

        public Page()
        {
            this.Id = Guid.NewGuid();
            this.Name = string.Format("My{0}_{1}", this.GetType().Name, DateTime.Now.Ticks);
            this.Nodes = new List<Node>();
            this.AddNode(typeof(StartNodeActivity).FullName);
            this.AddNode(typeof(EndNodeActivity).FullName);
            this.SelectedNodes = new HashSet<Node>();
            this.RenderedNodes = new Dictionary<Node, GraphicsPath>();
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
                node = new StartNode();
                node.Name = "Start";
            }
            else if (activity is EndNodeActivity)
            {
                node = new EndNode();
                node.Name = "End";
            }
            else if (activity is LoopStartNodeActivity)
            {
                node = new LoopStartNode();
                node.Name = "Loop Start";
            }
            else if (activity is LoopEndNodeActivity)
            {
                node = new LoopEndNode();
                node.Name = "Loop End";
            }
            else if (activity is PreparationNodeActivity)
            {
                node = new PreparationNode();
                node.Name = "Preparation";
            }
            else
            {
                node = new ProcessNode();
                node.Name = Regex.Replace(activityFullName.Split('.').Last(), "([a-z](?=[A-Z0-9])|[A-Z](?=[A-Z][a-z]))", "$1 ");
            }
            node.Activity = activity;
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