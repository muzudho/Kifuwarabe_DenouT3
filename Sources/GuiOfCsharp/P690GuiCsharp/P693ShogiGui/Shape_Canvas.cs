﻿using System.Windows.Forms;
using Grayscale.Kifuwarakaku.Entities.Logger;
using Grayscale.P693ShogiGui.I500Gui;

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
        /// <param name="logTag"></param>
        void Paint(
            object sender,
            PaintEventArgs e,
            MainGui_Csharp shogiGui,
            string windowName,
            ILogTag logTag
        );

    }
}
