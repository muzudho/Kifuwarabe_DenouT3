using Grayscale.P693_ShogiGui___.L___080_Shape;
using Grayscale.P693_ShogiGui___.L___500_Gui;
using System.Collections.Generic;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P693_ShogiGui___.P703_ShogiGui___.L101____Conv
{
    public abstract class Conv_Koma_InGui
    {

        /// <summary>
        /// FIXME: 使ってない？
        /// 
        /// ************************************************************************************************************************
        /// 駒のハンドル(*1)を元に、ボタンを返します。
        /// ************************************************************************************************************************
        /// 
        ///     *1…将棋の駒１つ１つに付けられた番号です。
        /// 
        /// </summary>
        /// <param name="hKomas"></param>
        /// <param name="shape_PnlTaikyoku"></param>
        /// <returns></returns>
        public static List<Shape_BtnKoma> HKomasToBtns(List<int> hKomas, MainGui_Csharp shogiGui)
        {
            List<Shape_BtnKoma> btns = new List<Shape_BtnKoma>();

            foreach (int handle in hKomas)
            {
                Shape_BtnKoma btn = shogiGui.Shape_PnlTaikyoku.Btn40Komas[handle];
                if (null != btn)
                {
                    btns.Add(btn);
                }
            }

            return btns;
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 駒のハンドル(*1)を元に、ボタンを返します。
        /// ************************************************************************************************************************
        /// 
        ///     *1…将棋の駒１つ１つに付けられた番号です。
        /// 
        /// </summary>
        /// <param name="hKoma"></param>
        /// <param name="shape_PnlTaikyoku"></param>
        /// <returns>なければヌル</returns>
        public static Shape_BtnKoma FingerToKomaBtn(Finger koma, MainGui_Csharp shogiGui)
        {
            Shape_BtnKoma found = null;

            int hKoma = (int)koma;

            if (0 <= hKoma && hKoma < shogiGui.Shape_PnlTaikyoku.Btn40Komas.Length)
            {
                found = shogiGui.Shape_PnlTaikyoku.Btn40Komas[hKoma];
            }

            return found;
        }

    }
}
