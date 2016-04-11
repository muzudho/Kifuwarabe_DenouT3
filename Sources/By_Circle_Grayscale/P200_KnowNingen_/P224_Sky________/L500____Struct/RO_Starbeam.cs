using Grayscale.P218_Starlight__.L___500_Struct;
using System;

namespace Grayscale.P224_Sky________.L500____Struct
{

    /// <summary>
    /// ************************************************************************************************************************
    /// 流れ星の光
    /// ************************************************************************************************************************
    /// 
    /// 升目の場所、または　駒がどこからどこへ動いたかの一手、を表すデータです。
    /// リードオンリーな、盤上用の升目符号。「元」→「現」の形。
    /// 
    /// 「先後」、
    /// 「置き場」、
    /// 「筋、段」（明記がない場合、dstの意）、
    /// 「src筋、src段」（オプション）。
    /// 
    /// ・“同”
    /// 棋譜用の升目符号。“同”が使えます。
    /// “同”にする場合は、UNKNOWN_SUJI 筋、UNKNOWN_DAN 段に設定してください。毎回再計算します。
    /// 
    /// ・前の升：「7g7f」などにも使えるよう、src筋、src段も補助で用意。
    /// ・前の手：また、遡れるように、１つ前の升目符号へのリンクも補助で容易。
    /// 
    /// ・駒種類：「歩」「と金」など。補助で容易。
    /// </summary>
    [Serializable]
    public class RO_Starbeam : RO_Starlight, Starbeamable
    {


        #region プロパティー類


        ///// <summary>
        ///// ------------------------------------------------------------------------------------------------------------------------
        ///// 手得ヒストリー
        ///// ------------------------------------------------------------------------------------------------------------------------
        ///// 
        ///// 升を動いたら、１つ増やす。
        ///// ２５６手指せば、初期局面４０要素、先手１２８要素、後手１２８要素の　２９６サイズ程度メモリを圧迫する予定。
        ///// </summary>
        //public List<SyElement> TedokuHistory { get { return this.tedokuHistory; } }
        //private List<SyElement> tedokuHistory;

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 先後、升、配役
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public Starlightable LongTimeAgo { get { return this.longTimeAgo; } }
        protected Starlightable longTimeAgo;


        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// あれば、取った駒。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public object/*Ks14*/ FoodKomaSyurui { get { return this.tottaKomaSyurui; } }
        private object/*Ks14*/ tottaKomaSyurui;

        #endregion


        /// <summary>
        /// ************************************************************************************************************************
        /// 棋譜用。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="okiba"></param>
        /// <param name="srcOkiba"></param>
        /// <param name="suji"></param>
        /// <param name="dan"></param>
        /// <param name="srcSuji"></param>
        /// <param name="srcDan"></param>
        /// <param name="dstSyurui"></param>
        /// <param name="srcSyurui"></param>
        /// <param name="previousTe"></param>
        public RO_Starbeam(Starlightable longTimeAgo, Starlightable now, object/*Ks14*/ tottaKomaSyurui)
            : base(now)
        {
            this.longTimeAgo = longTimeAgo;
            this.tottaKomaSyurui = tottaKomaSyurui;
        }

    }

}
