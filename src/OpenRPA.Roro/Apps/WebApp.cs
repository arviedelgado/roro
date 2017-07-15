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
    public class WebApp : IApp
    {
        private readonly App app;

        private readonly WebAppType webAppType;

        private readonly RemoteWebDriver webDriver;

        private IElement viewport;

        internal Rect GetViewportRect()
        {
            if (viewport == null)
            {
                switch (this.webAppType)
                {
                    case WebAppType.Chrome:
                        viewport = app.GetElement("pane", x => x.Name == "Chrome Legacy Window") ?? app.GetElement("document", x => x.Name == string.Empty);
                        break;
                    case WebAppType.IE:
                        viewport = app.GetElement("pane", x => x.Class == "Shell DocObject View" || x.Class == "NewTabWnd");
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            return viewport.Rect;
        }

        public static WebApp StartIE(string url = "about:blank")
        {
            return new WebApp(WebAppType.IE, url);
        }

        public static WebApp StartChrome(string url = "about:blank")
        {
            return new WebApp(WebAppType.Chrome, url);
        }

        private WebApp(WebAppType driverType, string url)
        {
            this.webAppType = driverType;
            string instanceId = string.Format("openrpa-roro-{0}", DateTime.Now.Ticks);
            switch (driverType)
            {
                case WebAppType.Chrome:
                    // service
                    ChromeDriverService chromeService = ChromeDriverService.CreateDefaultService();
                    //chromeService.HideCommandPromptWindow = true;
                    // options
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument(instanceId);
                    this.webDriver = new ChromeDriver(chromeService, chromeOptions);
                    break;

                case WebAppType.IE:
                    // service
                    InternetExplorerDriverService ieService = InternetExplorerDriverService.CreateDefaultService();
                    //ieService.HideCommandPromptWindow = true;
                    // options
                    InternetExplorerOptions ieOptions = new InternetExplorerOptions();
                    ieOptions.InitialBrowserUrl = string.Format("about:blank:{0}", instanceId);
                    this.webDriver = new InternetExplorerDriver(ieService, ieOptions);
                    break;
                case WebAppType.Edge:
                    // service
                    EdgeDriverService edgeService = EdgeDriverService.CreateDefaultService();
                    edgeService.HideCommandPromptWindow = true;
                    // options
                    EdgeOptions edgeOptions = new EdgeOptions();
                    this.webDriver = new EdgeDriver(edgeService, edgeOptions);
                    break;
                //case WebAppDriver.Firefox:
                //    this.driver = new FirefoxDriver();
                //    break;
                //case WebAppDriver.Safari:
                //    this.driver = new SafariDriver();
                //    break;
                //case WebAppDriver.Opera:
                //    this.driver = new OperaDriver();
                //    break;
                default:
                    throw new NotImplementedException();
            }

            using (var searcher = new ManagementObjectSearcher(string.Format("SELECT ProcessId, CommandLine FROM Win32_Process WHERE CommandLine LIKE '%{0}%'", instanceId)))
            {
                this.app = App.Attach(Convert.ToInt32(searcher.Get().Cast<ManagementObject>().First()["ProcessId"]));
            }

            this.GoTo(url);
        }

        public void GoTo(string url)
        {
            this.webDriver.Navigate().GoToUrl(url);
        }

        public void GoBack()
        {
            this.webDriver.Navigate().Back();
        }

        public void GoForward()
        {
            this.webDriver.Navigate().Forward();
        }

        public void Refresh()
        {
            this.webDriver.Navigate().Refresh();
        }

        public IElement[] Children
        {
            get
            {
                return new IElement[]
                {
                    new WebElement(this, (RemoteWebElement)webDriver.ExecuteScript("return document.documentElement"), null)
                };
            }
        }

        public IElement GetElementFromFocus()
        {
            return new WebElement(this, (RemoteWebElement)webDriver.ExecuteScript("return document.activeElement"));
        }

        public IElement GetElementFromPoint(int screenX, int screenY)
        {
            var viewportRect = this.GetViewportRect();
            var viewportX = screenX - viewportRect.X;
            var viewportY = screenY - viewportRect.Y;
            var rawElement = (RemoteWebElement)webDriver.ExecuteScript("return document.elementFromPoint(arguments[0], arguments[1])", viewportX, viewportY);
            return rawElement == null ? null : new WebElement(this, rawElement);
        }
    }
}
