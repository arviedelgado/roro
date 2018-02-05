
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Automation;

namespace Roro
{
    public sealed class WinElement : Element
    {
        private readonly AutomationElement rawElement;

        private WinElement(AutomationElement rawElement)
        {
            this.rawElement = rawElement;
        }

        [Property]
        public string Id => this.rawElement.Current.AutomationId;

        [Property]
        public string Class => this.rawElement.Current.ClassName;

        [Property]
        public string Name => this.rawElement.Current.Name;

        [Property]
        public string Type => this.rawElement.Current.ControlType.ProgrammaticName.Split('.').Last().ToLower();

        [Property]
        public override string Path => string.Format("{0}/{1}", this.Parent == null ? string.Empty : this.Parent.Path, this.Type);

        public override Rectangle Bounds
        {
            get
            {
                var r = this.rawElement.Current.BoundingRectangle;
                return new Rectangle((int)r.X, (int)r.Y, (int)r.Width, (int)r.Height);
            }
        }

        [Property]
        public int Index => (this.Parent is WinElement parent ? parent.Children.ToList().IndexOf(this) : 0);

        public int ProcessId => this.rawElement.Current.ProcessId;

        public WinElement MainWindow
        {
            get
            {
                var value = this;
                var root = WinElement.GetRoot();
                while (value != null && !value.Parent.Equals(root))
                {
                    value = value.Window;
                }
                return value;
            }
        }

        public WinElement Window
        {
            get
            {
                var value = this.Parent;
                while (value != null && value.Type != "window")
                {
                    value = value.Parent;
                }
                return value;
            }
        }

        public WinElement Parent
        {
            get
            {
                if (TreeWalker.RawViewWalker.GetParent(this.rawElement) is AutomationElement rawParent)
                {
                    return new WinElement(rawParent);
                }
                return null;
            }
        }

        public IEnumerable<WinElement> Children
        {
            get
            {
                var children = new List<WinElement>();
                var rawChild = TreeWalker.RawViewWalker.GetFirstChild(this.rawElement);
                while (rawChild != null)
                {
                    children.Add(new WinElement(rawChild));
                    rawChild = TreeWalker.RawViewWalker.GetNextSibling(rawChild);
                }
                return children;
            }
        }

        public WinElement GetElement(Predicate<WinElement> predicate)
        {
            var q = new Queue<WinElement>();
            q.Enqueue(this);
            while (q.Count > 0)
            {
                var e = q.Dequeue();
                if (predicate.Invoke(e))
                {
                    return e;
                }
                foreach (var c in e.Children)
                {
                    q.Enqueue(c);
                }
            }
            return null;
        }

        public override bool Equals(object obj) => (obj is WinElement other && this.rawElement.Equals(other.rawElement));

        public override int GetHashCode() => this.rawElement.GetHashCode();

        internal static WinElement GetRoot()
        {
            return new WinElement(AutomationElement.RootElement);
        }

        internal static WinElement GetFromFocus()
        {
            if (AutomationElement.FocusedElement is AutomationElement rawElement)
            {
                return new WinElement(rawElement);
            }
            return null;
        }

        internal static WinElement GetFromPoint(int screenX, int screenY)
        {
            if (AutomationElement.FromPoint(new System.Windows.Point(screenX, screenY)) is AutomationElement rawElement)
            {
                return new WinElement(rawElement);
            }
            return null;
        }

        //#region BotProperty Extensions

        //[BotProperty]
        //public int Width => this.Bounds.Width;

        //[BotProperty]
        //public int Height => this.Bounds.Height;

        //[BotProperty]
        //public string Window_Title => this.Type == "window" ? this.Name : this.Window.Name;

        //[BotProperty]
        //public string Parent_Name => this.Parent.Name;

        //[BotProperty]
        //public int Parent_Index => this.Parent.Index;

        //[BotProperty]
        //public int Parent_Width => this.Parent.Width;

        //[BotProperty]
        //public int Parent_Height => this.Parent.Height;

        //#endregion
    }
}
