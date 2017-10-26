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
using System.Drawing;

namespace Roro
{
    public sealed class WebElement : Element
    {
        internal readonly RemoteWebElement rawElement;

        internal WebElement(RemoteWebElement rawElement, int offsetX, int offsetY)
        {
            this.rawElement = rawElement;
            var script = @"
                var el = arguments[0];
                var getPath = function(elem) {
                    var path = '';
                    while (elem) {
                        path = '/' + elem.tagName + path;
                        elem = elem.parentElement || elem.ownerDocument.defaultView.frameElement; // element.document.window.iframe
                    }
                    return path;
                };
                var result = {
                    id:     el.getAttribute('id') || '',
                    class:  el.getAttribute('class') || '',
                    name:   el.getAttribute('name') || '',
                    type:   el.tagName.toLowerCase(),
                    path:   getPath(el).toLowerCase(),
                    left:   el.getBoundingClientRect().left,
                    top:    el.getBoundingClientRect().top,
                    width:  el.getBoundingClientRect().width,
                    height: el.getBoundingClientRect().height,
                    text:   el.innerText || '',
                    value:  el.value || '',
                    checked:el.checked || false,
                };
                return result;
            ";
            var driver = this.rawElement.WrappedDriver as RemoteWebDriver;
            var result = driver.ExecuteScript(script, this.rawElement) as IDictionary<string, object>;
            this.Id = result["id"].ToString();
            this.Class = Convert.ToString(result["class"]);
            this.Name = Convert.ToString(result["name"]);
            this.Type = Convert.ToString(result["type"]);
            this.Path = Convert.ToString(result["path"]);
            this.Bounds = new Rectangle(
                Convert.ToInt32(result["left"]) + offsetX,
                Convert.ToInt32(result["top"]) + offsetY,
                Convert.ToInt32(result["width"]),
                Convert.ToInt32(result["height"])
            );
            this.Text = Convert.ToString(result["text"]);
            this.Value = Convert.ToString(result["value"]);
            this.Checked = Convert.ToBoolean(result["checked"]);
        }

        [BotProperty]
        public string Id { get; }

        [BotProperty]
        public string Class { get; }

        [BotProperty]
        public string Name { get; }

        [BotProperty]
        public string Type { get; }

        [BotProperty]
        public override string Path { get; }
        
        public override Rectangle Bounds { get; }

        [BotProperty]
        public string Text { get; }

        [BotProperty]
        public string Value { get; }

        [BotProperty]
        public bool Checked { get; }
        
        public WebElement Parent
        {
            get
            {
                var driver = this.rawElement.WrappedDriver as RemoteWebDriver;
                if (driver.ExecuteScript("return arguments[0].parentElement", this.rawElement) is RemoteWebElement rawParent)
                {
                    return new WebElement(rawParent, 0, 0);
                }
                return null;
            }
        }

        public IEnumerable<WebElement> Children
        {
            get
            {
                var children = new List<WebElement>();
                var driver = this.rawElement.WrappedDriver as RemoteWebDriver;
                if (driver.ExecuteScript("return arguments[0].children", this.rawElement) is IEnumerable<object> rawChildren)
                {
                    foreach (RemoteWebElement rawChild in rawChildren)
                    {
                        children.Add(new WebElement(rawChild, 0, 0));
                    }
                }
                return children;
            }
        }
    }
}
