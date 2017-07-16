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
