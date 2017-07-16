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
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace OpenRPA.Roro.Apps
{
    public class App : IApp
    {
        private readonly int processId;

        private App(int processId)
        {
            this.processId = processId;
        }

        public static App Attach(int processId)
        {
            return new App(processId);
        }

        public static App Start(string appPath)
        {
            Process process = Process.Start(appPath);
            while (process.MainWindowHandle == IntPtr.Zero)
            {
                process.WaitForInputIdle(1000);
                process.Refresh();
            }
            return App.Attach(process.Id);
        }

        public IElement[] Children
        {
            get
            {
                IntPtr element;
                App.InvokeElement(IntPtr.Zero, "GetFromHandle", out element, Process.GetProcessById(processId).MainWindowHandle);
                return new Element[]
                {
                    new Element(element, null)
                };
            }
        }

        public IElement GetElementFromFocus()
        {
            IntPtr element;
            App.InvokeElement(IntPtr.Zero, "GetFromFocus", out element, IntPtr.Zero);
            if (element == IntPtr.Zero) return null;
            int elementProcessId;
            App.InvokeElement(element, "GetProcessId", out elementProcessId, IntPtr.Zero);
            if (elementProcessId != this.processId) return null;
            return new Element(element);
        }

        public IElement GetElementFromPoint(int x, int y)
        {

            IntPtr element;
            App.InvokeElement(IntPtr.Zero, "GetFromPoint", out element, (Int64)y << 32 | (Int64)x);
            if (element == IntPtr.Zero) return null;
            int elementProcessId;
            App.InvokeElement(element, "GetProcessId", out elementProcessId, IntPtr.Zero);
            if (elementProcessId != this.processId) return null;
            return new Element(element);
        }

        public IElement GetElement(string type, Predicate<IElement> predicate)
        {
            var q = new Queue<IElement>();
            q.Enqueue(this.Children.First());
            while (q.Count > 0)
            {
                var e = q.Dequeue();
                if (e.Type == type && predicate.Invoke(e))
                {
                    return e;
                }
                foreach (var c in e.Children)
                {
                    q.Enqueue(c);
                }
            }
            return null;
        }

        [DllImport("WindowsDriver.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern bool InvokeElement(IntPtr handle, string name, IntPtr outParam, IntPtr inParam);

        internal static bool InvokeElement<Out, In>(IntPtr handle, string name, out Out outParam, In inParam)
        {
            bool success;
            if (typeof(In) == typeof(string))
            {
                IntPtr inStrParam = Marshal.StringToBSTR(inParam == null ? string.Empty : inParam.ToString());
                success = InvokeElement(handle, name, out outParam, inStrParam);
                Marshal.FreeBSTR(inStrParam);
                return success;
            }
            if (typeof(Out) == typeof(string))
            {
                IntPtr outStrParam;
                success = InvokeElement(handle, name, out outStrParam, inParam);
                outParam = (Out)(outStrParam == IntPtr.Zero ? string.Empty : (object)Marshal.PtrToStringBSTR(outStrParam));
                Marshal.FreeBSTR(outStrParam);
                return success;
            }
            Type inParamType = typeof(In).IsEnum ? Enum.GetUnderlyingType(typeof(In)) : typeof(In);
            Type outParamType = typeof(Out).IsEnum ? Enum.GetUnderlyingType(typeof(Out)) : typeof(Out);
            IntPtr inParamPtr = Marshal.AllocHGlobal(Marshal.SizeOf(inParamType));
            IntPtr outParamPtr = Marshal.AllocHGlobal(Marshal.SizeOf(outParamType));
            Marshal.StructureToPtr(Convert.ChangeType(inParam, inParamType), inParamPtr, false);
            success = InvokeElement(handle, name, outParamPtr, inParamPtr);
            outParam = (Out)Marshal.PtrToStructure(outParamPtr, outParamType);
            Marshal.FreeHGlobal(inParamPtr);
            Marshal.FreeHGlobal(outParamPtr);
            return success;
        }
    }
}
