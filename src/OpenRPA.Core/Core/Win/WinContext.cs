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
using OpenRPA.Queries;
using System.Linq;

namespace OpenRPA.Core
{
    public sealed class WinContext : Context
    {
        public static readonly WinContext Shared = new WinContext();

        public override Element GetElementFromFocus()
        {
            return WinElement.GetFromFocus();
        }

        public override Element GetElementFromPoint(int screenX, int screenY)
        {
            return WinElement.GetFromPoint(screenX, screenY);
        }

        public override IReadOnlyList<Element> GetElementsFromQuery(Query query)
        {
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
            return result;
        }
    }
}