using System.Collections.Generic;

namespace Grayscale.P163_KifuCsa____.L___250_Struct
{
    /// <summary>
    /// .csa棋譜の、解析後のデータ。
    /// </summary>
    public interface CsaKifu
    {
        /// <summary>
        /// 棋譜のバージョン
        /// </summary>
        string Version { get; set; }

        /// <summary>
        /// １プレイヤー名
        /// </summary>
        string Player1Name { get; set; }

        /// <summary>
        /// ２プレイヤー名
        /// </summary>
        string Player2Name { get; set; }

        /// <summary>
        /// 将棋盤。[１～９,一～九]の８１升。
        /// </summary>
        string[,] Shogiban { get; set; }

        /// <summary>
        /// 先手なら「+」、後手なら「-」。
        /// </summary>
        string FirstSengo { get; set; }

        /// <summary>
        /// 指し手データ。
        /// </summary>
        List<CsaKifuSasite> SasiteList { get; set; }

        /// <summary>
        /// 対局終了の仕方の分類。
        /// </summary>
        string FinishedStatus { get; set; }
    }
}
