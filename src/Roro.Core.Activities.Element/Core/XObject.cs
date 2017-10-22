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
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Roro.Core
{
    internal class XObject : IEnumerable<XObject>
    {
        private readonly object item;

        private IList<XObject> items;

        public int Count => this.Get<int>("Count");

        public XObject(object obj)
        {
            if (obj is null)
                throw new ArgumentNullException("obj");

            if (obj.GetType() == this.GetType())
                throw new ArgumentException("The parameter'obj' cannot be an instance of XObject.");

            this.item = obj;

        }

        public XObject Get(string name)
        {
            if (this.item.GetType().InvokeMember(name, BindingFlags.GetProperty, null, this.item, null) is object result)
            {
                return new XObject(result);
            }
            return null;
        }

        public T Get<T>(string name)
        {
            if (this.Get(name) is XObject obj)
            {
                return (T)obj.item;
            }
            return default(T);
        }

        public void Set(string name, object value)
        {
            this.item.GetType().InvokeMember(name, BindingFlags.SetProperty, null, this.item, new object[] { value });
        }

        public XObject Invoke(string name, params object[] args)
        {
            if (this.item.GetType().InvokeMember(name, BindingFlags.InvokeMethod, null, this.item, args) is object result)
            {
                return new XObject(result);
            }
            return null;
        }

        public T Invoke<T>(string name, params object[] args)
        {
            if (this.Invoke(name, args) is XObject obj)
            {
                return (T)obj.item;
            }
            return default(T);
        }

        public IEnumerator<XObject> GetEnumerator()
        {
            if (this.items == null)
            {
                this.items = new List<XObject>();
                for (int i = 0, c = this.Count; i < c ; i++)
                {
                    this.items.Add(this.Invoke("Item", i));
                }
            }
            return this.items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public override string ToString() => this.item.ToString();
    }
}
