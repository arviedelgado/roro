using Roro.Activities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;

namespace Roro.Workflow
{
    [DataContract]
    public abstract class Node
    {
        [DataMember]
        public Guid Id { get; private set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Rectangle Bounds { get; set; }

        [DataMember]
        public Activity Activity { get; set; }

        [DataMember]
        public Guid Next { get; protected set; }

        public virtual void SetNextTo(Guid id)
        {
            this.Next = id;
        }

        public virtual Guid Execute()
        {
            return this.Next;
        }

        public Node()
        {
            this.Id = Guid.NewGuid();
            this.Name = string.Format("My{0}_{1}", this.GetType().Name, DateTime.Now.Ticks);
            this.Bounds = new Rectangle(
                PageRenderOptions.GridSize * Helper.Between(4, 30),
                PageRenderOptions.GridSize * Helper.Between(4, 30),
                PageRenderOptions.GridSize * this.GetSize().Width,
                PageRenderOptions.GridSize * this.GetSize().Height);
        }

        public abstract Size GetSize();

        public abstract GraphicsPath RenderNode(Graphics g, Rectangle r, NodeStyle o);

        public GraphicsPath RenderPort(Graphics g, Rectangle r, NodeStyle o)
        {
            var portPoint = new Point(r.CenterX(), r.Bottom);
            portPoint.Offset(-PageRenderOptions.GridSize / 2, -PageRenderOptions.GridSize / 2);
            var portSize = new Size(PageRenderOptions.GridSize, PageRenderOptions.GridSize);
            var portRect = new Rectangle(portPoint, portSize);
            g.FillEllipse(o.PortBackBrush, portRect);
            var portPath = new GraphicsPath();
            portPath.AddEllipse(portRect);
            return portPath;
        }
    }
}
