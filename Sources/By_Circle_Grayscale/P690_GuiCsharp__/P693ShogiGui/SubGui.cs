using Grayscale.P693ShogiGui.L081Canvas;

namespace Grayscale.P693ShogiGui.I500Gui
{
    /// <summary>
    /// コンソール・ウィンドウに対応。
    /// </summary>
    public interface SubGui
    {

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// グラフィックを描くツールは全部この中です。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        Shape_Canvas Shape_Canvas { get; }


        /// <summary>
        /// 入力欄の文字列。
        /// </summary>
        string InputString99 { get; }
        void AddInputString99(string line);
        void SetInputString99(string line);
        void ClearInputString99();

    }
}
