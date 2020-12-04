using Grayscale.P003Log.I500Struct;
using Grayscale.P027Settei.L510Xml;
using Grayscale.P693ShogiGui.I500Gui;
using System.Drawing;

namespace Grayscale.P693ShogiGui.I510Form
{
    public interface Uc_Form1Mainable
    {
        Color BackColor { get; set; }

        Form1_Mutex MutexOwner { get; set; }

        void Solute_RepaintRequest(
            Form1_Mutex mutex, MainGui_Csharp mainGui, KwErrorHandler errH);

        MainGui_Csharp MainGui { get; }

        SetteiXmlFile SetteiXmlFile { get; }
    }
}
