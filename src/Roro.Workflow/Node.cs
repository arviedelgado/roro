using Roro.Activities;
using System;
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

        public abstract void SetNextTo(Guid id);

        public abstract Guid Execute();

        public Node()
        {
            this.Id = Guid.NewGuid();
            this.Name = string.Format("My{0}_{1}", this.GetType().Name, DateTime.Now.Ticks);
            this.Bounds = new Rectangle(
                PageRenderOptions.GridSize * Helper.Between(0, 20),
                PageRenderOptions.GridSize * Helper.Between(0, 20),
                PageRenderOptions.GridSize * 6,
                PageRenderOptions.GridSize * 4);
        }

        public abstract GraphicsPath Render(Graphics g, Rectangle r, DefaultNodeStyle o);
    }
}
