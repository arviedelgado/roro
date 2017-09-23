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
using System.Threading;

namespace OpenRPA.Inputs
{
    public class Input
    {
        [DllImport("WinDriver.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void SetInputState(InputEventArgs e);

        [DllImport("WinDriver.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void OnInputStateChanged(InputEventHandler handler);

        public static event InputEventHandler OnKeyUp = delegate { };

        public static event InputEventHandler OnKeyDown = delegate { };

        public static event InputEventHandler OnMouseUp = delegate { };

        public static event InputEventHandler OnMouseDown = delegate { };

        public static event InputEventHandler OnMouseMove = delegate { };

        private static readonly InputEventHandler inputEventHandler;

        private static void InputEventHandler(InputEventArgs e)
        {
            switch (e.Type)
            {
                case InputEventType.KeyUp: OnKeyUp.Invoke(e); break;
                case InputEventType.KeyDown: OnKeyDown.Invoke(e); break;
                case InputEventType.MouseUp: OnMouseUp.Invoke(e); break;
                case InputEventType.MouseDown: OnMouseDown.Invoke(e); break;
                case InputEventType.MouseMove: OnMouseMove.Invoke(e); break;
            }
        }

        static Input()
        {
            OnInputStateChanged(inputEventHandler = InputEventHandler);
        }

        public static void KeyUp(KeyboardKey key)
        {
            SetInputState(new InputEventArgs() { Type = InputEventType.KeyUp, Key = key });
        }

        public static void KeyDown(KeyboardKey key)
        {
            SetInputState(new InputEventArgs() { Type = InputEventType.KeyDown, Key = key });
        }

        public static void MouseUp(MouseButton button)
        {
            SetInputState(new InputEventArgs() { Type = InputEventType.MouseUp, Button = button });
        }

        public static void MouseDown(MouseButton button)
        {
            SetInputState(new InputEventArgs() { Type = InputEventType.MouseDown, Button = button });
        }

        public static void MouseMove(int x, int y)
        {
            SetInputState(new InputEventArgs() { Type = InputEventType.MouseMove, X = x, Y = y });
        }

        public static void Click(MouseButton button)
        {
            Input.MouseDown(button);
            Input.MouseUp(button);
        }

        public static void Press(params KeyboardKey[] keys)
        {
            var modKeys = new Dictionary<KeyboardKey, bool>
            {
                { KeyboardKey.LeftAlt, false },
                { KeyboardKey.LeftCtrl, false },
                { KeyboardKey.LeftShift, false },
                { KeyboardKey.LeftWin, false },
                { KeyboardKey.RightAlt, false },
                { KeyboardKey.RightCtrl, false },
                { KeyboardKey.RightShift, false },
                { KeyboardKey.RightWin, false }
            };
            foreach (KeyboardKey key in keys)
            { 
                Input.KeyDown(key);
                if (modKeys.ContainsKey(key))
                {
                    modKeys[key] = true;
                }
                else
                {
                    Input.KeyUp(key);
                }
            }
            foreach (KeyboardKey key in modKeys.Keys)
            {
                if (modKeys[key] == true)
                {
                    Input.KeyUp(key);
                }
            }
        }

        public static void Write(string text)
        {
            foreach (char c in text ?? string.Empty)
            {
                Input.KeyDown((KeyboardKey)(-c));
                Input.KeyUp((KeyboardKey)(-c));
            }
        }
    }
}
