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
using OpenRPA.Queries;
using System.Linq;
using System.Diagnostics;

namespace OpenRPA.Core
{
    public sealed class WinElement : Element
    {
        private readonly IntPtr rawElement;

        private WinElement(IntPtr rawElement)
        {
            this.rawElement = rawElement;
        }

        [BotProperty]
        public string Id => this.Invoke<string>();

        [BotProperty]
        public string Class => this.Invoke<string>();

        [BotProperty]
        public string Name => this.Invoke<string>();

        [BotProperty]
        public string Type => this.Invoke<WinElementType>().ToString().ToLower();

        [BotProperty]
        public string Path => string.Format("{0}/{1}", this.Parent == null ? string.Empty : this.Parent.Path, this.Type);

        public Rect Bounds => this.Invoke<Rect>();

        [BotProperty]
        public int Index => (this.Parent is WinElement parent ? parent.Children.ToList().IndexOf(this) : 0);

        public int ProcessId => this.Invoke<int>();

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
                IntPtr value = this.Invoke<IntPtr>();
                return value == IntPtr.Zero ? null : new WinElement(value);
            }
        }

        public IEnumerable<WinElement> Children
        {
            get
            {
                var children = new List<WinElement>();
                IntPtr value = this.Invoke<IntPtr>();
                while (value != IntPtr.Zero)
                {
                    children.Add(new WinElement(value));
                    value = this.Invoke<IntPtr>(value);
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

        public override bool Equals(object obj) => (obj is WinElement other && this.Invoke<bool>(other.rawElement));

        public override int GetHashCode() => base.GetHashCode();

        public static WinElement GetRoot()
        {
            IntPtr value = new WinElement(IntPtr.Zero).Invoke<IntPtr>();
            return value == null ? null : new WinElement(value);
        }
        
        public static WinElement GetFromFocus()
        {
            IntPtr value = new WinElement(IntPtr.Zero).Invoke<IntPtr>();
            return value == null ? null : new WinElement(value);
        }

        public static WinElement GetFromPoint(int screenX, int screenY)
        {
            IntPtr value = new WinElement(IntPtr.Zero).Invoke<IntPtr>((Int64)screenY << 32 | (Int64)screenX);
            return value == null ? null : new WinElement(value);
        }

        public static IReadOnlyList<WinElement> GetFromQuery(Query query)
        {
            var sw = Stopwatch.StartNew();
            var result = new List<WinElement>();
            var candidates = new Queue<WinElement>();
            var targetPath = query.First(x => x.Name == "Path").Value.ToString();

            candidates.Enqueue(WinElement.GetRoot());
            while (candidates.Count > 0)
            {
                var candidate = candidates.Dequeue();
                var candidatePath = candidate.Path;
                if (targetPath.StartsWith(candidatePath))
                {
                    Console.WriteLine(candidatePath);
                    if (targetPath.Equals(candidatePath))
                    {
                        if (candidate.TryQuery(query))
                        {
                            result.Add(candidate);
                        }
                    }
                    else
                    {
                        foreach (var child in candidate.Children)
                        {
                            candidates.Enqueue(child);
                        }
                    }
                }
            }
            Console.WriteLine("Matches: {0} in {1} seconds", result.Count, sw.ElapsedMilliseconds / 1000.0);
            return result;
        }

        [DllImport("WinDriver.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern void InvokeElement(IntPtr handle, string name, IntPtr result, IntPtr param);

        private T Invoke<T>(object param = null, [CallerMemberName] string name = null)
        {
            T result;

            param = param ?? IntPtr.Zero;

            var paramType = param.GetType();
            if (paramType.IsEnum) paramType = Enum.GetUnderlyingType(paramType);

            var resultType = typeof(T);
            if (resultType.IsEnum) resultType = Enum.GetUnderlyingType(resultType);

            if (paramType == typeof(string))
            {
                IntPtr strParam = Marshal.StringToBSTR(param.ToString());
                result = Invoke<T>(strParam, name);
                Marshal.FreeBSTR(strParam);
                return result;
            }

            if (resultType == typeof(string))
            {
                IntPtr strResult = Invoke<IntPtr>(param, name);
                result = (T)(strResult == IntPtr.Zero ? string.Empty : (object)Marshal.PtrToStringBSTR(strResult));
                Marshal.FreeBSTR(strResult);
                return result;
            }

            IntPtr paramPtr = Marshal.AllocHGlobal(Marshal.SizeOf(paramType));
            IntPtr resultPtr = Marshal.AllocHGlobal(Marshal.SizeOf(resultType));

            Marshal.StructureToPtr(Convert.ChangeType(param, paramType), paramPtr, false);
            InvokeElement(this.rawElement, name, resultPtr, paramPtr);
            result = (T)Marshal.PtrToStructure(resultPtr, resultType);

            Marshal.FreeHGlobal(paramPtr);
            Marshal.FreeHGlobal(resultPtr);

            return result;
        }


        //#region BotProperty Extensions

        //[BotProperty]
        //public int Width => this.Bounds.Width;

        //[BotProperty]
        //public int Height => this.Bounds.Height;

        //[BotProperty]
        //public string Window_Title => this.Type == "window" ? this.Name : this.Window.Name;

        //[BotProperty]
        //public string Parent_Name => this.Parent.Name;

        //[BotProperty]
        //public int Parent_Index => this.Parent.Index;

        //[BotProperty]
        //public int Parent_Width => this.Parent.Width;

        //[BotProperty]
        //public int Parent_Height => this.Parent.Height;

        //#endregion
    }
}
