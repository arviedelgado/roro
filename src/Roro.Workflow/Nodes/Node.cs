using Roro.Activities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
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

        private Rectangle bounds;

        [DataMember]
        public Rectangle Bounds
        {
            get
            {
                foreach (var port in this.Ports)
                {
                    port.UpdateBounds(this.bounds);
                }
                return this.bounds;
            }
            set => this.bounds = value;
        }

        [DataMember]
        public Activity Activity { get; set; }

        [DataMember]
        public List<Port> Ports { get; }

        public Dictionary<Port, GraphicsPath> RenderedPorts { get; }

        public Port GetPortById(Guid id)
        {
            return this.Ports.FirstOrDefault(x => x.Id == id);
        }

        public Port GetPortFromPoint(Point pt)
        {
            if (this.RenderedPorts.FirstOrDefault(x => x.Value.IsVisible(pt)) is KeyValuePair<Port, GraphicsPath> item)
            {
                return item.Key;
            }
            return null;
        }

        public virtual Guid Execute()
        {
            return Guid.Empty;
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
            this.Ports = new List<Port>();
            this.RenderedPorts = new Dictionary<Port, GraphicsPath>();
        }

        public abstract Size GetSize();

        public abstract GraphicsPath Render(Graphics g, Rectangle r, NodeStyle o);
    }
}
