using Grayscale.P056Syugoron.I250Struct;
using Grayscale.P211WordShogi.L500Word;
using Grayscale.P213Komasyurui.L250Word;
using Grayscale.P213Komasyurui.L500Util;
using Grayscale.P218Starlight.I500Struct;
using Grayscale.P234Komahaiyaku.L250Word;
using Grayscale.P234Komahaiyaku.L500Util;
using Grayscale.P236KomahaiyaTr.L500Table;

namespace Grayscale.P238Seiza.L250Struct
{

    /// <summary>
    /// 星。
    /// 
    /// 盤上に置けるものです。
    /// 駒、矢印、色付きマスなどを想定していますが、現状では　駒　しかありません。
    /// </summary>
    public class RO_Star : IMoveSource
    {
        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 星の型
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public BoardItem Startype { get { return this.startype; } }
        protected BoardItem startype;

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 現・駒の向き
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public Playerside Pside { get { return this.pside; } }
        protected Playerside pside;

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 現・マス
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public SyElement Masu { get { return this.masu; } }
        protected SyElement masu;


        /// <summary>
        /// 駒種類１４
        /// </summary>
        public Komasyurui14 Komasyurui { get { return this.komasyurui; } }
        private Komasyurui14 komasyurui;

        public Komahaiyaku185 Haiyaku
        {
            get
            {
                return Data_KomahaiyakuTransition.ToHaiyaku(this.Komasyurui, this.Masu, this.Pside);
            }
        }


        public RO_Star(Playerside pside, SyElement masu, Komasyurui14 syurui)
        {
            this.startype = BoardItem.Koma;
            this.pside = pside;
            this.masu = masu;
            this.komasyurui = syurui;
        }

        public RO_Star(Playerside pside, SyElement masu, Komahaiyaku185 haiyaku)
        {
            this.startype = BoardItem.Koma;
            this.pside = pside;
            this.masu = masu;
            this.komasyurui = Util_Komahaiyaku184.Syurui(haiyaku);
        }

        public RO_Star(RO_Star src)
        {
            this.startype = BoardItem.Koma;
            this.pside = src.Pside;
            this.masu = src.Masu;
            this.komasyurui = src.Komasyurui;
        }

        /// <summary>
        /// 不成ケース
        /// </summary>
        /// <returns></returns>
        public Komasyurui14 ToNarazuCase()
        {
            return Util_Komasyurui14.NarazuCaseHandle(this.komasyurui);// Haiyaku184Array.Syurui(this.Haiyaku)
        }

        /// <summary>
        /// 駒の表面の文字。
        /// </summary>
        public string Text_Label
        {
            get
            {
                string result;

                result = Util_Komasyurui14.Ichimoji[(int)this.komasyurui];//(int)Haiyaku184Array.Syurui(this.Haiyaku)

                return result;
            }
        }


    }
}
