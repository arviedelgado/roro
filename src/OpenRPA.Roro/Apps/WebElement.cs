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
using System.Linq;
using System.Text;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Remote;
using System.Management;

namespace OpenRPA.Roro.Apps
{
    public class WebElement : IElement
    {
        private readonly WebApp ownerApp;

        private readonly RemoteWebElement rawElement;
        
        internal WebElement(WebApp ownerApp, RemoteWebElement rawElement)
        {
            this.ownerApp = ownerApp;
            this.rawElement = rawElement;
            var driver = (RemoteWebDriver)this.rawElement.WrappedDriver;
            var rawParent = (RemoteWebElement)driver.ExecuteScript("return arguments[0].parentElement", this.rawElement);
            if (rawParent != null)
            {
                this.Parent = new WebElement(ownerApp, rawParent);
            }
        }

        internal WebElement(WebApp ownerApp, RemoteWebElement rawElement, WebElement parent)
        {
            this.ownerApp = ownerApp;
            this.rawElement = rawElement;
            this.Parent = parent;
        }

        public string Id
        {
            get
            {
                return this.rawElement.GetAttribute("id");
            }
        }

        public string Class
        {
            get
            {
                return this.rawElement.GetAttribute("class");
            }
        }

        public string Name
        {
            get
            {
                return this.rawElement.GetAttribute("name") ?? string.Empty;
            }
        }

        public string Type
        {
            get
            {
                return this.rawElement.TagName.ToLower();
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
                var rect = new Rect();
                var viewportRect = this.ownerApp.GetViewportRect();
                var driver = (RemoteWebDriver)this.rawElement.WrappedDriver;
                var rawRect = (IDictionary<string, object>)driver.ExecuteScript("return arguments[0].getBoundingClientRect()", this.rawElement);
                rect.X = Convert.ToInt32(rawRect["left"]) + viewportRect.X;
                rect.Y = Convert.ToInt32(rawRect["top"]) + +viewportRect.Y;
                rect.Width = Convert.ToInt32(rawRect["width"]);
                rect.Height = Convert.ToInt32(rawRect["height"]);
                return rect;
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
                var children = new List<IElement>();
                var driver = (RemoteWebDriver)this.rawElement.WrappedDriver;
                var rawChildren = (IEnumerable<object>)driver.ExecuteScript("return arguments[0].children", this.rawElement);
                foreach (var rawChild in rawChildren)
                {
                    children.Add(new WebElement(this.ownerApp, (RemoteWebElement)rawChild, this));
                }
                return children.ToArray();
            }
        }
    }
}
