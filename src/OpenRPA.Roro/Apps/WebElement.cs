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
using OpenQA.Selenium.Remote;

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
            this.RefreshProperties();
            var driver = (RemoteWebDriver)this.rawElement.WrappedDriver;
            //var rawParent = (RemoteWebElement)driver.ExecuteScript("return arguments[0].parentElement", this.rawElement);
            //if (rawParent != null)
            //{
            //    this.Parent = new WebElement(ownerApp, rawParent);
            //}
        }

        internal WebElement(WebApp ownerApp, RemoteWebElement rawElement, WebElement parent)
        {
            this.ownerApp = ownerApp;
            this.rawElement = rawElement;
            this.RefreshProperties();
            this.Parent = parent;
        }

        private void RefreshProperties()
        {
            var script = @"
                var el = arguments[0];
                var getPath = function(elem)
                {
                    var path = elem.tagName;
                    while (elem)
                    {
                        path = elem.tagName + '/' + path;
                        elem = elem.parentElement || elem.ownerDocument.defaultView.frameElement; // element.document.window.iframe
                    }
                    return path;
                };
                return {
                    id:     el.getAttribute('id') || '',
                    class:  el.getAttribute('class') || '',
                    name:   el.getAttribute('name') || '',
                    type:   el.tagName || '',
                    path:   getPath(el),
                    left:   el.getBoundingClientRect().left,
                    top:    el.getBoundingClientRect().top,
                    width:  el.getBoundingClientRect().width,
                    height: el.getBoundingClientRect().height                    
                };
            ";
            var driver = (RemoteWebDriver)this.rawElement.WrappedDriver;
            var result = (IDictionary<string, object>)driver.ExecuteScript(script, this.rawElement);
            this.Id = result["id"].ToString();
            this.Class = result["class"].ToString();
            this.Name = result["name"].ToString();
            this.Type = result["type"].ToString().ToLower();
            this.Path = result["path"].ToString().ToLower();
            var viewportRect = this.ownerApp.GetViewportRect();
            this.Rect = new Rect(
                Convert.ToInt32(result["left"]) + viewportRect.X,
                Convert.ToInt32(result["top"])  + viewportRect.Y,
                Convert.ToInt32(result["width"]),
                Convert.ToInt32(result["height"])
            ); 
        }

        public string Id
        {
            get;
            private set;
        }

        public string Class
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public string Type
        {
            get;
            private set;
        }

        public string Path
        {
            get;
            private set;
            //{
            //    return string.Format("{0}/{1}", this.Parent == null ? string.Empty : this.Parent.Path, this.Type);
            //}
        }

        public Rect Rect
        {
            get;
            private set;
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
