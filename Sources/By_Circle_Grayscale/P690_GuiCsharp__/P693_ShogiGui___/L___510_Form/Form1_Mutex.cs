using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grayscale.P693_ShogiGui___.L___510_Form
{
    public enum Form1_Mutex
    {
        /// <summary>
        /// ロックがかかっていない
        /// </summary>
        Empty,

        /// <summary>
        /// タイマー
        /// </summary>
        Timer,

        /// <summary>
        /// マウス操作
        /// </summary>
        MouseOperation,

        /// <summary>
        /// 棋譜再生
        /// </summary>
        Saisei,

        /// <summary>
        /// 起動時
        /// </summary>
        Launch
    }
}
