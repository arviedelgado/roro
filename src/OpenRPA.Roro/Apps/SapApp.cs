// BSD 2-Clause License

// Copyright(c) 2017, Arvie Delgado
// All rights reserved.

// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:

// * Redistributions of source code must retain the above copyright notice, this
//   list of conditions and the following disclaimer.

// * Redistributions in binary form must reproduce the above copyright notice,
//   this list of conditions and the following disclaimer in the documentation
//   and/or other materials provided with the distribution.

// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
// AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED.IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
// OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

using System;
using System.Collections.Generic;

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
