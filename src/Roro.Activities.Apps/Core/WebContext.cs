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
using System.Collections.Generic;

namespace Roro
{
    public abstract class WebContext : Context
    {
        protected readonly TimeSpan Timeout = TimeSpan.FromSeconds(5);

        protected const string DefaultUrl = "about:blank";

        protected RemoteWebDriver Driver { get; set; }

        protected abstract bool UpdateViewport(WinElement target);

        public override Element GetElementFromPoint(int screenX, int screenY)
        {
            if (WinContext.Target is WinElement target && this.UpdateViewport(target))
            {
                var script = @"
                    var x = arguments[0];
                    var y = arguments[1];
                    return document.elementFromPoint(x, y);
                ";
                var frameScreenX = this.Viewport.X;
                var frameScreenY = this.Viewport.Y;
                this.Driver.SwitchTo().DefaultContent();
                while (true)
                {
                    if (this.ExecuteScript(script, screenX - frameScreenX, screenY - frameScreenY) is RemoteWebElement rawElement)
                    {
                        var element = new WebElement(rawElement, frameScreenX, frameScreenY);
                        if (element.Type == "iframe")
                        {
                            frameScreenX = element.Bounds.X;
                            frameScreenY = element.Bounds.Y;
                            this.Driver.SwitchTo().Frame(rawElement);
                            continue;
                        }
                        this.Driver.SwitchTo().DefaultContent();
                        return element;
                    }
                    break;
                }
            }
            return null;
        }

        public override IEnumerable<Element> GetElementsFromQuery(Query query)
        {
            var path = query.First(x => x.Name == "Path").Value.ToString()
                        .Substring(1).Replace('/', '>').Replace(">iframe>", ">iframe#");

            return GetElementsFromFrame(null, this.Viewport.X, this.Viewport.Y, path).Where(x => x.TryQuery(query)).ToList();
        }


        private IEnumerable<WebElement> GetElementsFromFrame(WebElement frame, int frameScreenX, int frameScreenY, string path)
        {
            var pathPrefix = path.Split('#').First();
            var pathSuffix = String.Join(string.Empty, path.Split('#').Skip(1));

            var result = new List<WebElement>();

            if (frame == null)
            {
                this.Driver.SwitchTo().DefaultContent();
            }
            else
            {
                this.Driver.SwitchTo().Frame(frame.rawElement);
            }

            if (this.ExecuteScript("return document.querySelectorAll(arguments[0])", pathPrefix) is IEnumerable<object> rawElements)
            {
                foreach (RemoteWebElement rawElement in rawElements)
                {
                    var element = new WebElement(rawElement, frameScreenX, frameScreenY);
                    if (element.Type == "iframe")
                    {
                        result.AddRange(GetElementsFromFrame(element, element.Bounds.X, element.Bounds.Y, pathSuffix));
                    }
                    else
                    {
                        result.Add(element);
                    }
                }
            }

            this.Driver.SwitchTo().ParentFrame();

            return result;
        }

        protected int GetProcessIdFromSession(string session)
        {
            using (var searcher = new ManagementObjectSearcher(string.Format("SELECT ProcessId, CommandLine FROM Win32_Process WHERE CommandLine LIKE '%{0}%'", session)))
            {
                this.GoToUrl(WebContext.DefaultUrl);
                return (Convert.ToInt32(searcher.Get().Cast<ManagementObject>().First()["ProcessId"]));
            }
            throw new Exception(string.Format("{0} session {1} not found.", this.GetType().Name, session));
        }

        #region WebContext Common

        public void GoToUrl(string url)
        {
            this.Driver.Navigate().GoToUrl(url ?? WebContext.DefaultUrl);
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
