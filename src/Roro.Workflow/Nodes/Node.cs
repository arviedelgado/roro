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

        public List<Port> Ports { get; set; }

        public abstract void SetNextTo(Guid id);

        public abstract Guid Execute();

        public Node()
        {
            this.Id = Guid.NewGuid();
            this.Name = string.Format("My{0}_{1}", this.GetType().Name, DateTime.Now.Ticks);
            this.Bounds = new Rectangle(
                PageRenderOptions.GridSize * Helper.Between(3, 30),
                PageRenderOptions.GridSize * Helper.Between(3, 30),
                PageRenderOptions.GridSize * 4,
                PageRenderOptions.GridSize * 2);
            this.Ports = new List<Port>();
        }

        public abstract Size GetSize();

        public abstract GraphicsPath Render(Graphics g, Rectangle r, NodeStyle o);

        public void RenderPorts(Graphics g, Rectangle r, NodeStyle o)
        {
            foreach (var port in this.Ports)
            {
                port.Render(g, r, o);
            }
        }
    }
}
