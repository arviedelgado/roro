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
using System.Drawing;

namespace Roro
{
    public sealed class SapElement : Element
    {
        private readonly XObject rawElement;

        internal SapElement(XObject rawElement)
        {
            this.rawElement = rawElement;
        }

        [BotProperty]
        public string Id => this.rawElement.Get<string>("Id");

        [BotProperty]
        public string Name => this.rawElement.Get<string>("Name");

        [BotProperty]
        public string Type => (this.rawElement.Get<SapElementType>("TypeAsNumber")).ToString().ToLower();

        [BotProperty]
        public override string Path => string.Format("{0}/{1}", this.Parent == null ? string.Empty : this.Parent.Path, this.Type);

        public override Rectangle Bounds => new Rectangle(
            this.rawElement.Get<int>("ScreenLeft"),
            this.rawElement.Get<int>("ScreenTop"),
            this.rawElement.Get<int>("Width"),
            this.rawElement.Get<int>("Height"));

        public SapElement Parent => this.rawElement.Get("Parent") is XObject rawParent ? new SapElement(rawParent) : null;

        public IEnumerable<SapElement> Children
        {
            get
            {
                var children = new List<SapElement>();
                var rawChildren = this.rawElement.Get("Children");
                foreach (var rawChild in rawChildren)
                {
                    children.Add(new SapElement(rawChild));
                }
                return children;
            }
        }
    }
}
