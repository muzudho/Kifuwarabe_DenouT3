using Grayscale.P003Log.I500Struct;
using Grayscale.P693ShogiGui.I500Gui;
using System.Windows.Forms;

namespace Grayscale.P693ShogiGui.L081Canvas
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
            IKwErrorHandler errH
        );

    }
}
