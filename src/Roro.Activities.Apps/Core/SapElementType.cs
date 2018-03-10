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

namespace Roro
{
    // As of SAP GUI 7.40 patch 5

    internal enum SapElementType
    {
        Application = 10,
        Box = 62,
        Button = 40,
        CheckBox = 42,
        Collection = 120,
        ComboBox = 34,
        Component = 0,
        ComponentCollection = 128,
        Connection = 11,
        Container = 70,
        ContainerShell = 51,
        ContextMenu = 127,
        CTextField = 32,
        CustomControl = 50,
        DialogShell = 125,
        DockShell = 126,
        FrameWindow = 20,
        GOSShell = 123,
        Label = 30,
        ListContainer = 73,
        MainWindow = 21,
        Menu = 110,
        Menubar = 111,
        MessageWindow = 23,
        ModalWindow = 22,
        OkCodeField = 35,
        PasswordField = 33,
        RadioButton = 41,
        Scrollbar = 100,
        ScrollContainer = 72,
        Session = 12,
        SessionInfo = 121,
        Shell = 122,
        SimpleContainer = 71,
        SplitterContainer = 75,
        SplitterShell = 124,
        Statusbar = 103,
        StatusPane = 43,
        Tab = 91,
        TableColumn = 81,
        TableControl = 80,
        TableRow = 82,
        TabStrip = 90,
        TextField = 31,
        Titlebar = 102,
        Toolbar = 101,
        Unknown = -1,
        UserArea = 74,
        VComponent = 1,
        VContainer = 2
    }
}
