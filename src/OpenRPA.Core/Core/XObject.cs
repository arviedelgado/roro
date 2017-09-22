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

namespace OpenRPA.Core
{
    internal class XObject
    {
        private readonly object Object;

        public XObject(object obj)
        {
            if (obj is null)
                throw new ArgumentNullException("obj");

            if (obj.GetType() == this.GetType())
                throw new ArgumentException("The parameter'obj' cannot be an instance of XObject.");

            this.Object = obj;

        }

        public XObject Get(string name)
        {
            if (this.Object.GetType().InvokeMember(name, System.Reflection.BindingFlags.GetProperty, null, Object, null) is object result)
            {
                return new XObject(result);
            }
            return null;
        }

        public T Get<T>(string name)
        {
            if (this.Get(name) is XObject obj)
            {
                return (T)obj.Object;
            }
            return default(T);
        }

        public void Set(string name, object value)
        {
            this.Object.GetType().InvokeMember(name, System.Reflection.BindingFlags.SetProperty, null, this.Object, new object[] { value });
        }

        public XObject Invoke(string name, params object[] args)
        {
            if (this.Object.GetType().InvokeMember(name, System.Reflection.BindingFlags.InvokeMethod, null, Object, args) is object result)
            {
                return new XObject(result);
            }
            return null;
        }

        public T Invoke<T>(string name, params object[] args)
        {
            if (this.Invoke(name, args) is XObject obj)
            {
                return (T)obj.Object;
            }
            return default(T);
        }

        public override string ToString()
        {
            return this.Object.ToString();
        }
    }
}
