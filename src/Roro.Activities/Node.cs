
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.Serialization;

namespace Roro.Activities
{
    [DataContract]
    public abstract class Node
    {
        [DataMember]
        public Guid Id { get; private set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Rectangle Bounds { get; private set; }

        [DataMember]
        public string ActivityId { get; private set; }

        [DataMember]
        public List<Input> ActivityInputs { get; internal set; }

        [DataMember]
        public List<Output> ActivityOutputs { get; internal set; }

        [DataMember]
        public List<Port> Ports { get; private set; }

        public Dictionary<Port, GraphicsPath> RenderedPorts { get; set; }

        public virtual bool CanStartLink => true;

        public virtual bool CanEndLink => true;

        public Node(Activity activity)
        {
            this.Id = Guid.NewGuid();
            this.Name = activity.GetType().Name.Humanize();
            this.Bounds = new Rectangle(
                PageRenderOptions.GridSize * 0,
                PageRenderOptions.GridSize * 0,
                PageRenderOptions.GridSize * 4,
                PageRenderOptions.GridSize * 2);
            this.Ports = new List<Port>();
            this.ActivityId = activity.GetType().FullName;
            this.ActivityInputs = activity.Inputs;
            this.ActivityOutputs = activity.Outputs;
            this.Initialize();
        }

        [OnDeserializing]
        private void Initialize(StreamingContext context = default)
        {
            this.RenderedPorts = new Dictionary<Port, GraphicsPath>();
        }

        public void SetBounds(Rectangle rect)
        {
            rect.X = Math.Max(0, (rect.X / PageRenderOptions.GridSize) * PageRenderOptions.GridSize);
            rect.Y = Math.Max(0, (rect.Y / PageRenderOptions.GridSize) * PageRenderOptions.GridSize);
            foreach (var port in this.Ports)
            {
                port.UpdateBounds(rect);
            }
            this.Bounds = rect;
        }

        public Port GetPortById(Guid id)
        {
            return this.Ports.FirstOrDefault(x => x.Id == id);
        }

        public Port GetPortFromPoint(Point pt)
        {
            if (this.RenderedPorts.FirstOrDefault(x => x.Value.IsVisible(pt.X, pt.Y)) is KeyValuePair<Port, GraphicsPath> item)
            {
                return item.Key;
            }
            return null;
        }

        public abstract Guid Execute(IEnumerable<VariableNode> variableNodes);

        public abstract GraphicsPath Render(Graphics g, Rectangle r, NodeStyle o);

        public virtual void RenderText(Graphics g, Rectangle r, NodeStyle o)
        {
            g.DrawString(this.Name, o.Font, o.FontBrush, r, o.StringFormat);
        }
    }
}
