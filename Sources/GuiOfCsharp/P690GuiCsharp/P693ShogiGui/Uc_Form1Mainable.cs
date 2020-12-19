﻿using System.Drawing;
using Grayscale.Kifuwarakaku.Entities.Logging;
using Grayscale.P027Settei.L510Xml;
using Grayscale.P693ShogiGui.I500Gui;

namespace Grayscale.P693ShogiGui.I510Form
{
    public interface Uc_Form1Mainable
    {
        Color BackColor { get; set; }

        Form1_Mutex MutexOwner { get; set; }

        void Solute_RepaintRequest(
            Form1_Mutex mutex, MainGui_Csharp mainGui, ILogTag logTag);

        MainGui_Csharp MainGui { get; }

        SetteiXmlFile SetteiXmlFile { get; }
    }
}