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
    public class Element : IElement
    {
        private readonly IntPtr rawElement;

        internal Element(IntPtr rawElement)
        {
            this.rawElement = rawElement;
            IntPtr rawParent;
            App.InvokeElement(this.rawElement, "GetParent", out rawParent, IntPtr.Zero);
            if (rawParent != IntPtr.Zero)
            {
                this.Parent = new Element(rawParent);
                if (this.Type == "window" && this.Parent.Type == "pane" && this.Parent.Parent == null)
                {
                    this.Parent = null;
                }
            }
        }

        internal Element(IntPtr rawElement, Element parent)
        {
            this.rawElement = rawElement;
            this.Parent = parent;
        }

        public string Id
        {
            get
            {
                string value;
                App.InvokeElement(this.rawElement, "GetId", out value, IntPtr.Zero);
                return value;
            }
        }

        public string Class
        {
            get
            {
                string value;
                App.InvokeElement(this.rawElement, "GetClass", out value, IntPtr.Zero);
                return value;
            }
        }

        public string Name
        {
            get
            {
                string value;
                App.InvokeElement(this.rawElement, "GetName", out value, IntPtr.Zero);
                return value;
            }
        }

        public string Type
        {
            get
            {
                ElementType value;
                App.InvokeElement(this.rawElement, "GetType", out value, IntPtr.Zero);
                return value.ToString().ToLower();
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
                Rect value;
                App.InvokeElement(this.rawElement, "GetRect", out value, IntPtr.Zero);
                return value;
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
                IntPtr value;
                List<Element> children = new List<Element>();
                App.InvokeElement(this.rawElement, "GetChildren", out value, IntPtr.Zero);
                while (value != IntPtr.Zero)
                {
                    children.Add(new Element(value, this));
                    App.InvokeElement(this.rawElement, "GetChildren", out value, value);
                }
                return children.ToArray();
            }
        }
    }
}
