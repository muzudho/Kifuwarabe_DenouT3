using Grayscale.P693ShogiGui.I500Gui;

namespace Grayscale.P693ShogiGui.I492Widgets
{
    /// <summary>
    /// ウィジェット読込みクラス。
    /// </summary>
    public interface WidgetsLoader
    {
        string FileName { get; set; }
        MainGui_Csharp ShogibanGui { get; set; }

        void Step1_ReadFile();
        void Step2_Compile_AllWidget(object obj_shogiGui);
        void Step3_SetEvent(object obj_shogiGui);

    }
}
