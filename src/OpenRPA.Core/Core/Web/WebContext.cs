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
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Opera;
using OpenQA.Selenium.Remote;
using System.Management;
using Microsoft.Win32;
using System.Collections.Generic;

namespace OpenRPA.Core
{
    public sealed class WebContext
    {
        private RemoteWebDriver driver;

        private IDictionary<int, RemoteWebDriver> drivers;

        private const string defaultUrl = "about:blank";

        public WebContext()
        {
            this.drivers = new Dictionary<int, RemoteWebDriver>();
        }

        private void Start<T>(string url) where T : RemoteWebDriver
        {
            int processId;
            RemoteWebDriver driver;
            string opaqueSessionId = Guid.NewGuid().ToString();

            //
            // CHROME
            //

            if (typeof(T) == typeof(ChromeDriver))
            {
                // service
                ChromeDriverService chromeService = ChromeDriverService.CreateDefaultService();

                // options
                ChromeOptions chromeOptions = new ChromeOptions();
                chromeOptions.AddArgument("--force-renderer-accessibility");
                chromeOptions.AddArgument(opaqueSessionId);

                // driver
                driver = new ChromeDriver(chromeService, chromeOptions);
            }

            //
            // IE
            //

            else if (typeof(T) == typeof(InternetExplorerDriver))
            {
                //// ERROR: Unable to get browser
                //// REPLICATE: Start driver, navigate manually to other site
                Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BFCACHE", "iexplore.exe", 0, RegistryValueKind.DWord);

                //// ERROR: WebDriverException: Unexpected error launching Internet Explorer. Protected Mode settings are not the same for all zones. Enable Protected Mode must be set to the same value (enabled or disabled) for all zones.
                Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\0", "2500", 0, RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\1", "2500", 0, RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\2", "2500", 0, RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\3", "2500", 0, RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\4", "2500", 0, RegistryValueKind.DWord);

                // service
                InternetExplorerDriverService ieService = InternetExplorerDriverService.CreateDefaultService();
                ieService.HideCommandPromptWindow = false;

                // options
                InternetExplorerOptions ieOptions = new InternetExplorerOptions();
                ieOptions.InitialBrowserUrl = string.Format("{0}:{1}", WebContext.defaultUrl, opaqueSessionId);

                // driver
                driver = new InternetExplorerDriver(ieService, ieOptions);
            }

            //
            // EDGE
            //

            else if (typeof(T) == typeof(EdgeDriver))
            {
                // service
                EdgeDriverService edgeService = EdgeDriverService.CreateDefaultService();
                edgeService.HideCommandPromptWindow = false;
                
                // options
                EdgeOptions edgeOptions = new EdgeOptions();
                opaqueSessionId = "-ServerName:MicrosoftEdge";

                // driver
                driver = new EdgeDriver(edgeService, edgeOptions);
            }

            //
            // OTHERS
            //

            else
            {
                throw new NotImplementedException();
            }

            using (var searcher = new ManagementObjectSearcher(string.Format("SELECT ProcessId, CommandLine FROM Win32_Process WHERE CommandLine LIKE '%{0}%'", opaqueSessionId)))
            {
                processId = (Convert.ToInt32(searcher.Get().Cast<ManagementObject>().First()["ProcessId"]));
                this.drivers.Add(processId, driver);
                this.driver = driver;
                this.GoTo(WebContext.defaultUrl);
                this.GoTo(url ?? WebContext.defaultUrl);
                return;
            }

            throw new Exception("ProcessId not found.");
        }

        public void StartIE(string url = null)
        {
            Start<InternetExplorerDriver>(url);
        }

        public void StartEdge(string url = null)
        {
            Start<EdgeDriver>(url);
        }

        public void StartChrome(string url = null)
        {
            Start<ChromeDriver>(url);
        }

        public void GoTo(string url)
        {
            this.driver.Navigate().GoToUrl(url);
        }

        public void GoBack()
        {
            this.driver.Navigate().Back();
        }

        public void GoForward()
        {
            this.driver.Navigate().Forward();
        }

        public void Refresh()
        {
            this.driver.Navigate().Refresh();
        }

        public object ExecuteScript(string script, params object[] args)
        {
            return this.driver.ExecuteScript(script, args);
        }

        public WinElement GetViewport(WinElement mainWindow)
        {
            var driverType = this.driver.GetType();
            if (driverType == typeof(ChromeDriver))
            {
                return mainWindow.GetElement(x => x.Class == "Chrome_RenderWidgetHostHWND");
            }
            if (driverType == typeof(InternetExplorerDriver))
            {
                return mainWindow.GetElement(x => x.Class == "Internet Explorer_Server" || x.Class == "NewTabWnd");
            }
            if (driverType == typeof(EdgeDriver))
            {
                return mainWindow.GetElement(x => x.Class == "Internet Explorer_Server" || x.Class == "NewTabPage");
            }
            throw new Exception("Viewport not found");
        }

        public WebElement GetElementFromFocus(WinElement winElement)
        {
            if (winElement == null)
            {
                return null;
            }

            var mainWindow = winElement.MainWindow;
            if (mainWindow == null)
            {
                return null;
            }

            var processId = mainWindow.ProcessId;
            if (mainWindow.Class == "ApplicationFrameWindow")
            {
                var edgeWindow = mainWindow.Children.FirstOrDefault(x =>
                    x.Type == "window" &&
                    x.Name == "Microsoft Edge" &&
                    x.Class == "Windows.UI.Core.CoreWindow");
                if (edgeWindow != null)
                {
                    processId = edgeWindow.ProcessId;
                }
            }

            if (this.drivers.TryGetValue(processId, out RemoteWebDriver driver))
            {
                this.driver = driver;
                var viewportRect = GetViewport(mainWindow).Bounds;
                if (this.ExecuteScript("return document.activeElement") is RemoteWebElement rawElement)
                {
                    var element = new WebElement(rawElement);
                    element.Bounds = new Rect(
                        element.Bounds.X + viewportRect.X,
                        element.Bounds.Y + viewportRect.Y,
                        element.Bounds.Width,
                        element.Bounds.Height);
                    return element;
                }
            }

            return null;
        }

        public WebElement GetElementFromPoint(int screenX, int screenY, WinElement winElement)
        {
            if (winElement == null)
            {
                return null;
            }

            var mainWindow = winElement.MainWindow;
            if (mainWindow == null)
            {
                return null;
            }

            var processId = mainWindow.ProcessId;
            if (mainWindow.Class == "ApplicationFrameWindow")
            {
                var edgeWindow = mainWindow.Children.FirstOrDefault(x =>
                    x.Type == "window" &&
                    x.Name == "Microsoft Edge" &&
                    x.Class == "Windows.UI.Core.CoreWindow");
                if (edgeWindow != null)
                {
                    processId = edgeWindow.ProcessId;
                }
            }

            if (this.drivers.TryGetValue(processId, out RemoteWebDriver driver))
            {
                this.driver = driver;
                var viewportRect = GetViewport(mainWindow).Bounds;
                if (this.ExecuteScript("return document.elementFromPoint(arguments[0], arguments[1])", screenX - viewportRect.X, screenY - viewportRect.Y) is RemoteWebElement rawElement)
                {
                    var element = new WebElement(rawElement);
                    element.Bounds = new Rect(
                        element.Bounds.X + viewportRect.X,
                        element.Bounds.Y + viewportRect.Y,
                        element.Bounds.Width,
                        element.Bounds.Height);
                    return element;
                }
            }

            return null;
        }
    }
}
