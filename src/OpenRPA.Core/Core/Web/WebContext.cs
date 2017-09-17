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
using System.Linq;
using OpenQA.Selenium.Remote;
using System.Management;
using OpenRPA.Queries;
using System.Collections.Generic;

namespace OpenRPA.Core
{
    public abstract class WebContext : Context
    {
        private const string defaultUrl = "about:blank";

        public int ProcessId { get; protected set; }

        protected RemoteWebDriver Driver { get; set; }

        public abstract WinElement Viewport { get; protected set; }

        public abstract bool UpdateViewport(WinElement target);

        public override Element GetElementFromFocus()
        {
            if (WinContext.Shared.GetElementFromFocus() is WinElement target
                && this.UpdateViewport(target)
                && this.Viewport is WinElement viewport)
            {
                if (this.ExecuteScript("return document.activeElement") is RemoteWebElement rawElement)
                {
                    return new WebElement(rawElement);
                }
            }
            return null;
        }

        public override Element GetElementFromPoint(int screenX, int screenY)
        {
            if (WinContext.Shared.GetElementFromPoint(screenX, screenY) is WinElement target
                && this.UpdateViewport(target)
                && this.Viewport is WinElement viewport)
            {
                if (this.ExecuteScript("return document.elementFromPoint(arguments[0], arguments[1])", screenX - viewport.Bounds.X, screenY - viewport.Bounds.Y) is RemoteWebElement rawElement)
                {
                    return new WebElement(rawElement);
                }
            }
            return null;
        }

        public override IReadOnlyList<Element> GetElementsFromQuery(Query query)
        {
            var result = new List<WebElement>();
            var targetPath = query.First(x => x.Name == "Path").Value.ToString().Remove(0, 1).Replace('/', '>');

            if (this.ExecuteScript("console.log(arguments[0]); return document.querySelectorAll(arguments[0])", targetPath) is IEnumerable<object> rawElements)
            {
                foreach (var rawElement in rawElements)
                {
                    var candidate = new WebElement(rawElement as RemoteWebElement);
                    if (candidate.TryQuery(query))
                    {
                        result.Add(candidate);
                    }
                }
            }
            return result;
        }

        protected int GetProcessIdFromSession(Guid session)
        {
            using (var searcher = new ManagementObjectSearcher(string.Format("SELECT ProcessId, CommandLine FROM Win32_Process WHERE CommandLine LIKE '%{0}%'", session)))
            {
                return (Convert.ToInt32(searcher.Get().Cast<ManagementObject>().First()["ProcessId"]));
            }
            throw new Exception(string.Format("{0} session {1} not found.", this.GetType().Name, session));
        }

        #region WebContext Common

        public void GoToUrl(string url)
        {
            this.Driver.Navigate().GoToUrl(url);
        }

        public void GoBack()
        {
            this.Driver.Navigate().Back();
        }

        public void GoForward()
        {
            this.Driver.Navigate().Forward();
        }

        public void Refresh()
        {
            this.Driver.Navigate().Refresh();
        }

        public object ExecuteScript(string script, params object[] args)
        {
            return this.Driver.ExecuteScript(script, args);
        }

        #endregion
    }
}
