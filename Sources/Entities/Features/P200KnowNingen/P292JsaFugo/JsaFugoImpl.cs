﻿namespace Grayscale.Kifuwarakaku.Entities.Features
{

    /// <summary>
    /// ************************************************************************************************************************
    /// 符号テキスト(*1)の、データの持ち方をここに書いています。
    /// ************************************************************************************************************************
    /// 
    ///         *1…「▲５五角右引」など。筋、段、同、駒種類、成、右左、上引、成、打。
    /// 
    /// </summary>
    public class JsaFugoImpl
    {

        #region プロパティー類

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 駒種類
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public PieceType Syurui
        {
            get
            {
                return this.syurui;
            }
        }
        private PieceType syurui;


        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 右、左、直など
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public MigiHidari MigiHidari
        {
            get
            {
                return this.migiHidari;
            }
        }
        private MigiHidari migiHidari;


        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 上、引
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public AgaruHiku AgaruHiku
        {
            get
            {
                return this.agaruHiku;
            }
        }
        private AgaruHiku agaruHiku;


        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 成
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public NariNarazu Nari
        {
            get
            {
                return this.nari;
            }
        }
        private NariNarazu nari;


        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// “打”表示。必ずしも持ち駒を打つタイミングとは一致しない。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public DaHyoji DaHyoji
        {
            get
            {
                return this.daHyoji;
            }
        }
        private DaHyoji daHyoji;

        #endregion



        /// <summary>
        /// ************************************************************************************************************************
        /// コンストラクターです。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="syurui"></param>
        /// <param name="migiHidari"></param>
        /// <param name="agaruHiku"></param>
        /// <param name="nari"></param>
        /// <param name="daHyoji"></param>
        public JsaFugoImpl(
            PieceType syurui, MigiHidari migiHidari, AgaruHiku agaruHiku, NariNarazu nari, DaHyoji daHyoji)
        {
            this.syurui = syurui;
            this.migiHidari = migiHidari;
            this.agaruHiku = agaruHiku;
            this.nari = nari;
            this.daHyoji = daHyoji;
        }







    }
}
