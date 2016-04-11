using Grayscale.P693_ShogiGui___.L___500_Gui;
using System;

namespace Grayscale.P693_ShogiGui___.L___510_Form
{
    public delegate void DELEGATE_Form1_Load(MainGui_Csharp shogiGui, object sender, EventArgs e);

    public interface Form1_Shogiable
    {
        Uc_Form1Mainable Uc_Form1Main { get; }

        DELEGATE_Form1_Load Delegate_Form1_Load { get; set; }
    }
}
