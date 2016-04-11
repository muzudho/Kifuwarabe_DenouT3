using Grayscale.P163_KifuCsa____.L___250_Struct;
using System.Collections.Generic;

namespace Grayscale.P163_KifuCsa____.L250____Struct
{

    /// <summary>
    /// .csa棋譜の、解析後のデータ。
    /// </summary>
    public class CsaKifuImpl : CsaKifu
    {
        /// <summary>
        /// 棋譜のバージョン
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// １プレイヤー名
        /// </summary>
        public string Player1Name { get; set; }

        /// <summary>
        /// ２プレイヤー名
        /// </summary>
        public string Player2Name { get; set; }

        /// <summary>
        /// 将棋盤。[9～0,〇～九]の１００升。0列と0段を使わずに８１升だけ使う。
        /// </summary>
        public string[,] Shogiban { get; set; }

        /// <summary>
        /// 先手なら「+」、後手なら「-」。
        /// </summary>
        public string FirstSengo { get; set; }

        /// <summary>
        /// 指し手データ。
        /// </summary>
        public List<CsaKifuSasite> SasiteList { get; set; }

        /// <summary>
        /// 対局終了の仕方の分類。
        /// </summary>
        public string FinishedStatus { get; set; }


        public CsaKifuImpl()
        {
            this.Player1Name = "";
            this.Player2Name = "";
            this.Shogiban = new string[10,10];
            this.FirstSengo = "+";
            this.SasiteList = new List<CsaKifuSasite>();
            this.FinishedStatus = "";
        }

    }
}
