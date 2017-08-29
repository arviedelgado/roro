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
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace OpenRPA.Core
{
    public sealed class WinElement : Element
    {
        private readonly IntPtr rawElement;

        private WinElement(IntPtr rawElement)
        {
            this.rawElement = rawElement;
        }

        public string Id
        {
            get
            {
                this.Invoke(out string value, IntPtr.Zero);
                return value;
            }
        }

        public string Class
        {
            get
            {
                this.Invoke(out string value, IntPtr.Zero);
                return value;
            }
        }

        public string Name
        {
            get
            {
                this.Invoke(out string value, IntPtr.Zero);
                return value;
            }
        }

        public string Type
        {
            get
            {
                this.Invoke(out WinElementType value, IntPtr.Zero);
                return value.ToString().ToLower();
            }
        }

        public string Path
        {
            get
            {
                var parent = this.Parent;
                return string.Format("{0}/{1}", parent == null ? string.Empty : parent.Path, this.Type);
            }
        }

        public Rect Bounds
        {
            get
            {
                this.Invoke(out Rect value, IntPtr.Zero);
                return value;
            }
        }

        public int ProcessId
        {
            get
            {
                this.Invoke(out int value, IntPtr.Zero);
                return value;
            }
        }

        public WinElement MainWindow
        {
            get
            {
                var value = this;
                var root = WinElement.GetRoot();
                while (value != null && !value.Parent.Equals(root))
                {
                    value = value.Window;
                }
                return value;
            }
        }

        public WinElement Window
        {
            get
            {
                var value = this.Parent;
                while (value != null && value.Type != "window")
                {
                    value = value.Parent;
                }
                return value;
            }
        }

        public WinElement Parent
        {
            get
            {
                this.Invoke(out IntPtr value, IntPtr.Zero);
                return value == IntPtr.Zero ? null : new WinElement(value);
            }
        }

        public IEnumerable<WinElement> Children
        {
            get
            {
                var children = new List<WinElement>();
                this.Invoke(out IntPtr value, IntPtr.Zero);
                while (value != IntPtr.Zero)
                {
                    children.Add(new WinElement(value));
                    this.Invoke(out value, value);
                }
                return children;
            }
        }

        public WinElement GetElement(Predicate<WinElement> predicate)
        {
            var q = new Queue<WinElement>();
            q.Enqueue(this);
            while (q.Count > 0)
            {
                var e = q.Dequeue();
                if (predicate.Invoke(e))
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

        public override bool Equals(object obj)
        {
            WinElement other = obj as WinElement;
            if (other == null) return false;
            this.Invoke(out bool result, other.rawElement);
            return result;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static WinElement GetRoot()
        {
            new WinElement(IntPtr.Zero).Invoke(out IntPtr value, IntPtr.Zero);
            return value == null ? null : new WinElement(value);
        }

        public static WinElement GetFromFocus()
        {
            new WinElement(IntPtr.Zero).Invoke(out IntPtr value, IntPtr.Zero);
            return value == null ? null : new WinElement(value);
        }

        public static WinElement GetFromPoint(int screenX, int screenY)
        {
            new WinElement(IntPtr.Zero).Invoke(out IntPtr value, (Int64)screenY << 32 | (Int64)screenX);
            return value == null ? null : new WinElement(value);
        }

        [DllImport("WindowsDriver.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern bool InvokeElement(IntPtr handle, string name, IntPtr outParam, IntPtr inParam);

        private bool Invoke<Out, In>(out Out outParam, In inParam, [CallerMemberName] string name = null)
        {
            bool result;
            if (typeof(In) == typeof(string))
            {
                IntPtr inStrParam = Marshal.StringToBSTR(inParam == null ? string.Empty : inParam.ToString());
                result = Invoke(out outParam, inStrParam, name);
                Marshal.FreeBSTR(inStrParam);
                return result;
            }
            if (typeof(Out) == typeof(string))
            {
                result = Invoke(out IntPtr outStrParam, inParam, name);
                outParam = (Out)(outStrParam == IntPtr.Zero ? string.Empty : (object)Marshal.PtrToStringBSTR(outStrParam));
                Marshal.FreeBSTR(outStrParam);
                return result;
            }
            Type inParamType = typeof(In).IsEnum ? Enum.GetUnderlyingType(typeof(In)) : typeof(In);
            Type outParamType = typeof(Out).IsEnum ? Enum.GetUnderlyingType(typeof(Out)) : typeof(Out);
            IntPtr inParamPtr = Marshal.AllocHGlobal(Marshal.SizeOf(inParamType));
            IntPtr outParamPtr = Marshal.AllocHGlobal(Marshal.SizeOf(outParamType));
            Marshal.StructureToPtr(Convert.ChangeType(inParam, inParamType), inParamPtr, false);
            result = InvokeElement(this.rawElement, name, outParamPtr, inParamPtr);
            outParam = (Out)Marshal.PtrToStructure(outParamPtr, outParamType);
            Marshal.FreeHGlobal(inParamPtr);
            Marshal.FreeHGlobal(outParamPtr);
            return result;
        }
    }
}
