namespace Grayscale.Kifuwarakaku.GuiOfCsharp.Features
{
    using System;

    public delegate void DELEGATE_Form1_Load(MainGui_Csharp shogiGui, object sender, EventArgs e);

    public interface Form1_Shogiable
    {
        Uc_Form1Mainable Uc_Form1Main { get; }

        DELEGATE_Form1_Load Delegate_Form1_Load { get; set; }
    }
}
