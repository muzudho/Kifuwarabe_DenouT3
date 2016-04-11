using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P693_ShogiGui___.L___500_Gui;
using System.Windows.Forms;

namespace Grayscale.P693_ShogiGui___.L___081_Canvas
{
    public interface Shape_Canvas
    {

        /// <summary>
        /// 描画
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="shogiGui"></param>
        /// <param name="errH"></param>
        void Paint(
            object sender,
            PaintEventArgs e,
            MainGui_Csharp shogiGui,
            string windowName,
            KwErrorHandler errH
        );

    }
}
