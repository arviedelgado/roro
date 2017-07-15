using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenRPA.Roro.Apps
{
    public sealed class SapApp : IApp
    {
        internal class SapObject
        {
            public readonly object Object;

            public SapObject(object obj)
            {
                this.Object = obj;
            }

            public SapObject this[int index]
            {
                get
                {
                    return Invoke("Item", index);
                }
            }

            public int Count
            {
                get
                {
                    return (int)Get("Count").Object;
                }
            }

            public SapObject Invoke(string name, params object[] args)
            {
                return new SapObject(Object.GetType().InvokeMember(name, System.Reflection.BindingFlags.InvokeMethod, null, Object, args));
            }

            public SapObject Get(string name)
            {
                try
                {
                    return new SapObject(Object.GetType().InvokeMember(name, System.Reflection.BindingFlags.GetProperty, null, Object, null));
                }
                catch
                {
                    return null;
                }
            }

            public void Set(string name, object value)
            {
                this.Object.GetType().InvokeMember(name, System.Reflection.BindingFlags.SetProperty, null, this.Object, new object[] { value });
            }

            public override string ToString()
            {
                return this.Object.ToString();
            }
        }

        private readonly SapObject engine;

        public SapApp()
        {
            var sapType = Type.GetTypeFromProgID("SapROTWr.SapROTWrapper");
            if (sapType == null) throw new NullReferenceException("SAP is not installed.");

            var sapROTWrapper = new SapObject(Activator.CreateInstance(sapType));
            if (sapROTWrapper == null) throw new NullReferenceException("SAP is installed. Failed to create ROTWrapper.");

            var sapROTEntry = sapROTWrapper.Invoke("GetROTEntry", "SAPGUI");
            if (sapROTWrapper == null) throw new NullReferenceException("SAP is installed. Failed to get ROTEntry.");

            var sapScriptingEngine = sapROTEntry.Invoke("GetScriptingEngine");
            if (sapScriptingEngine == null) throw new NullReferenceException("SAP is installed. Failed to get ScriptingEngine.");

            this.engine = sapScriptingEngine;
        }

        public IElement[] Children
        {
            get
            {
                List<IElement> children = new List<IElement>();
                var app = new SapElement(new SapObject(this.engine));
                foreach (IElement session in app.Children)
                {
                    children.AddRange(session.Children);
                }
                return children.ToArray();
            }
        }

        public IElement GetElementFromFocus()
        {
            try
            {
                var rawElement = this.engine.Get("ActiveSession").Get("ActiveWindow").Get("GuiFocus");
                return rawElement == null ? null : new SapElement(rawElement);
            }
            catch
            {
                return null;
            }
        }

        public IElement GetElementFromPoint(int x, int y)
        {
            try
            {
                var rawElement = this.engine.Get("ActiveSession").Invoke("FindByPosition", x, y, false);
                return rawElement == null ? null : new SapElement(rawElement);
            }
            catch
            {
                return null;
            }
        }
    }
}
