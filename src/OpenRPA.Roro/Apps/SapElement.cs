using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenRPA.Roro.Apps
{
    public sealed class SapElement : IElement
    {
        private SapApp.SapObject rawElement;

        internal SapElement(SapApp.SapObject rawElement)
        {
            this.rawElement = rawElement;
            var rawParent = this.rawElement.Get("Parent");
            if (rawParent != null)
            {
                this.Parent = new SapElement(rawParent);
                if (this.Parent.Type == "session")
                {
                    this.Parent = null;
                }
            }
        }

        internal SapElement(SapApp.SapObject rawElement, SapElement parent)
        {
            this.rawElement = rawElement;
            this.Parent = parent;
        }

        public string Id
        {
            get
            {
                return this.rawElement.Get("Id").Object.ToString();
            }
        }

        public string Name
        {
            get
            {
                return this.rawElement.Get("Name").Object.ToString();
            }
        }

        public string Class
        {
            get
            {
                return string.Empty; // Not supported.
            }
        }

        public string Type
        {
            get
            {
                return ((SapElementType)this.rawElement.Get("TypeAsNumber").Object).ToString().ToLower();
            }
        }

        public string Path
        {
            get
            {
                return string.Format("{0}/{1}", this.Parent == null ? string.Empty : this.Parent.Path, this.Type);
            }
        }

        public Rect Rect
        {
            get
            {
                return new Rect(
                    (int)this.rawElement.Get("ScreenLeft").Object,
                    (int)this.rawElement.Get("ScreenTop").Object,
                    (int)this.rawElement.Get("Width").Object,
                    (int)this.rawElement.Get("Height").Object);
            }
        }

        public IElement Parent
        {
            get;
        }

        public IElement[] Children
        {
            get
            {
                List<SapElement> children = new List<SapElement>();
                var rawChildren = rawElement.Get("Children");
                if (rawChildren != null)
                {
                    for (int index = 0, count = rawChildren.Count; index < count; index++)
                    {
                        children.Add(new SapElement(rawChildren[index], this));
                    }
                }
                return children.ToArray();
            }
        }
    }
}
