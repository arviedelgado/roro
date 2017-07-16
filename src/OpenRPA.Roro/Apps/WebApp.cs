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
                        viewport = app.GetElement("pane", x => x.Class == "Chrome_RenderWidgetHostHWND");
                        break;
                    case WebAppType.IE:
                        viewport = app.GetElement("pane", x => x.Class == "Internet Explorer_Server" || x.Class == "NewTabWnd");
                        break;
                    case WebAppType.Edge:
                        viewport = app.GetElement("pane", x => x.Class == "Internet Explorer_Server" || x.Class == "NewTabPage");
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            return viewport.Rect;
        }

        public static WebApp StartChrome(string url = "about:blank")
        {
            return new WebApp(WebAppType.Chrome, url);
        }

        public static WebApp StartIE(string url = "about:blank")
        {
            return new WebApp(WebAppType.IE, url);
        }

        public static WebApp StartEdge(string url = "about:blank")
        {
            return new WebApp(WebAppType.Edge, url);
        }

        public static WebApp StartFirefox(string url = "about:blank")
        {
            return new WebApp(WebAppType.Firefox, url);
        }

        public static WebApp StartSafari(string url = "about:blank")
        {
            return new WebApp(WebAppType.Safari, url);
        }

        public static WebApp StartOpera(string url = "about:blank")
        {
            return new WebApp(WebAppType.Opera, url);
        }

        private WebApp(WebAppType webAppType, string url)
        {
            this.webAppType = webAppType;
            string instanceId = string.Format("openrpa-roro-{0}", DateTime.Now.Ticks);
            switch (webAppType)
            {
                case WebAppType.Chrome:
                    // service
                    ChromeDriverService chromeService = ChromeDriverService.CreateDefaultService();
                    chromeService.HideCommandPromptWindow = false;
                    
                    // options
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument(instanceId);
                    this.webDriver = new ChromeDriver(chromeService, chromeOptions);
                    break;

                case WebAppType.IE:
                    // ERROR: Unable to get browser
                    // REPLICATE: Start driver, navigate manually to other site
                    Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BFCACHE", "iexplore.exe", 0, RegistryValueKind.DWord);

                    // ERROR: WebDriverException: Unexpected error launching Internet Explorer. Protected Mode settings are not the same for all zones. Enable Protected Mode must be set to the same value (enabled or disabled) for all zones.
                    Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\0", "2500", 0, RegistryValueKind.DWord);
                    Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\1", "2500", 0, RegistryValueKind.DWord);
                    Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\2", "2500", 0, RegistryValueKind.DWord);
                    Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\3", "2500", 0, RegistryValueKind.DWord);
                    Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\4", "2500", 0, RegistryValueKind.DWord);

                    // service
                    InternetExplorerDriverService ieService = InternetExplorerDriverService.CreateDefaultService();
                    ieService.HideCommandPromptWindow = true;
                    
                    // options
                    InternetExplorerOptions ieOptions = new InternetExplorerOptions();
                    ieOptions.InitialBrowserUrl = string.Format("about:blank:{0}", instanceId);
                    this.webDriver = new InternetExplorerDriver(ieService, ieOptions);
                    break;

                case WebAppType.Edge:

                    // service
                    EdgeDriverService edgeService = EdgeDriverService.CreateDefaultService();
                    edgeService.HideCommandPromptWindow = false;

                    // options
                    EdgeOptions edgeOptions = new EdgeOptions();
                    this.webDriver = new EdgeDriver(edgeService, edgeOptions);
                    break;

                case WebAppType.Firefox:
                    // service
                    FirefoxDriverService firefoxService = FirefoxDriverService.CreateDefaultService();
                    firefoxService.HideCommandPromptWindow = false;
                
                    // options
                    FirefoxOptions firefoxOptions = new FirefoxOptions();
                    firefoxOptions.AddArgument(instanceId);
                    this.webDriver = new FirefoxDriver(firefoxService, firefoxOptions, new TimeSpan(0, 60, 0));
                    break;

                case WebAppType.Safari:
                    // service
                    SafariDriverService safariService = SafariDriverService.CreateDefaultService();
                    safariService.HideCommandPromptWindow = false;
                
                    // options
                    SafariOptions safariOptions = new SafariOptions();
                    this.webDriver = new SafariDriver(safariService, safariOptions);
                    break;

                case WebAppType.Opera:
                    // service
                    OperaDriverService operaService = OperaDriverService.CreateDefaultService();
                    operaService.HideCommandPromptWindow = false;
                
                    // options
                    OperaOptions operaOptions = new OperaOptions();
                    operaOptions.AddArgument(instanceId);
                    this.webDriver = new OperaDriver(operaService, operaOptions);
                    break;

                default:
                    throw new NotImplementedException();
            }

            this.GoTo("about:blank");

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
            var rawElement = (RemoteWebElement)webDriver.ExecuteScript("return document.activeElement");
            return rawElement == null ? null : new WebElement(this, rawElement);
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
