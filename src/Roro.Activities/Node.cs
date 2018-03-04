
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Xml.Serialization;

namespace Roro.Activities
{
    public abstract class Node
    {
        [XmlAttribute]
        public Guid Id { get; set; }

        [XmlAttribute]
        public string Name { get; set; }

        public Rect Bounds { get; set; }

        public Activity Activity { get; set; }

        public List<Port> Ports { get; set; }

        protected Node()
        {
            this.Ports = new List<Port>();
            this.RenderedPorts = new Dictionary<Port, GraphicsPath>();
        }

        public Node(Activity activity) : this()
        {
            this.Activity = activity;
            this.Id = Guid.NewGuid();
            this.Name = activity.GetType().Name.Humanize();
            this.Bounds = new Rectangle(
                PageRenderOptions.GridSize * 0,
                PageRenderOptions.GridSize * 0,
                PageRenderOptions.GridSize * 4,
                PageRenderOptions.GridSize * 2);
        }

        internal Dictionary<Port, GraphicsPath> RenderedPorts { get; set; }

        public virtual bool CanStartLink => true;

        public virtual bool CanEndLink => true;

        public void SetBounds(Rect rect)
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

        public abstract Guid Execute(ActivityContext context);

        public abstract GraphicsPath Render(Graphics g, Rect r, NodeStyle o);

        public virtual void RenderText(Graphics g, Rect r, NodeStyle o)
        {
            g.DrawString(this.Name, o.Font, o.FontBrush, r, o.StringFormat);
        }
    }
}
