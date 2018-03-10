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
using OpenQA.Selenium.Edge;
using Microsoft.Win32;
using System.Linq;
using System.Drawing;

namespace Roro
{
    public sealed class EdgeContext : WebContext
    {
        public EdgeContext()
        {
            // session
            var session = "-ServerName:MicrosoftEdge";

            // service
            var service = EdgeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = false;

            // options
            var options = new EdgeOptions();

            // driver
            this.Driver = new EdgeDriver(service, options, this.Timeout);

            // process
            this.ProcessId = this.GetProcessIdFromSession(session);
        }

        protected override bool UpdateViewport(WinElement winElement)
        {
            this.Viewport = Rectangle.Empty;
            if (winElement != null && winElement.MainWindow is WinElement mainWindow
                && mainWindow.Class == "ApplicationFrameWindow"
                && mainWindow.Children.FirstOrDefault(x => x.Type == "window" && x.Name == "Microsoft Edge" && x.Class == "Windows.UI.Core.CoreWindow") is WinElement target
                && target.ProcessId == this.ProcessId)
            {
                if (target.GetElement(x => x.Class == "Internet Explorer_Server" || x.Class == "NewTabPage") is WinElement viewport)
                {
                    this.Viewport = viewport.Bounds;
                    return true;
                }
            }
            return false;
        }
    }
}
