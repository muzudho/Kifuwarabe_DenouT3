﻿namespace Grayscale.Kifuwarakaku.Entities.Features
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
        public PieceType Komasyurui { get { return this.komasyurui; } }
        private PieceType komasyurui;

        public Komahaiyaku185 Haiyaku
        {
            get
            {
                return Data_KomahaiyakuTransition.ToHaiyaku(this.Komasyurui, this.Masu, this.Pside);
            }
        }


        public RO_Star(Playerside pside, SyElement masu, PieceType syurui)
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
        public PieceType ToNarazuCase()
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
