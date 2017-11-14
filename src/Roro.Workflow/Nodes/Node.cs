using Roro.Activities;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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

        [DataMember]
        public Rectangle Bounds { get; private set; }

        public void SetBounds(Rectangle rect)
        {
            foreach (var port in this.Ports)
            {
                port.UpdateBounds(rect);
            }
            this.Bounds = rect;
        }

        [DataMember]
        public Activity Activity { get; set; }

        [DataMember]
        [Browsable(false)]
        public List<Port> Ports { get; }

        [Browsable(false)]
        public Dictionary<Port, SKPath> RenderedPorts { get; }

        public Port GetPortById(Guid id)
        {
            return this.Ports.FirstOrDefault(x => x.Id == id);
        }

        public Port GetPortFromPoint(Point pt)
        {
            if (this.RenderedPorts.FirstOrDefault(x => x.Value.Contains(pt.X, pt.Y)) is KeyValuePair<Port, SKPath> item)
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
            this.Name = string.Format("{0} {1} {2}", this.GetType().Name, Helper.Between(0, 999), Helper.Between(0, 999));
            this.Bounds = new Rectangle(
                PageRenderOptions.GridSize * Helper.Between(4, 30),
                PageRenderOptions.GridSize * Helper.Between(4, 30),
                PageRenderOptions.GridSize * this.GetSize().Width,
                PageRenderOptions.GridSize * this.GetSize().Height);
            this.Ports = new List<Port>();
            this.RenderedPorts = new Dictionary<Port, SKPath>();
        }

        public abstract Size GetSize();

        public abstract SKPath Render(SKCanvas g, Rectangle r, NodeStyle o);

        public void RenderText(SKCanvas g, Rectangle r, NodeStyle o)
        {
            using (var p = new SKPaint() { IsAntialias = true })
            {
                p.Color = Color.Black.ToSKColor();
                p.Typeface = SKTypeface.FromFamilyName("Verdana");
                p.TextSize = 11;
                p.TextAlign = SKTextAlign.Center;

                var words = this.Name.Split(new char[] { ' ' });
                var phrase = string.Empty;
                var phrases = new List<string>();
                foreach (var word in words)
                {
                    if (p.MeasureText(phrase + ' ' + word) > r.Width)
                    {
                        phrases.Add(phrase);
                        phrase = word;
                    }
                    else
                    {
                        phrase += ' ' + word;
                    }
                }
                if (phrase.Length > 0)
                {
                    phrases.Add(phrase);
                }
                for (var i = 0; i < phrases.Count; i++)
                {
                    g.DrawText(phrases[i].Trim(),
                        r.Center().X,
                        r.Center().Y + p.TextSize / 4
                        - (phrases.Count - 1) * p.TextSize / 2
                        + i * p.TextSize,
                        p);
                }
            }
        }
    }
}
