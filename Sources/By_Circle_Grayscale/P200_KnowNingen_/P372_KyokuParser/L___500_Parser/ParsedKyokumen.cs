using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P324_KifuTree___.L___250_Struct;
using System.Collections.Generic;

namespace Grayscale.P372_KyokuParser.L___500_Parser
{
    public interface ParsedKyokumen
    {
        /// <summary>
        /// 初期局面の先後。
        /// </summary>
        Playerside FirstPside { get; set; }

        /// <summary>
        /// 棋譜ノード。
        /// </summary>
        KifuNode KifuNode { get; set; }

        /// <summary>
        /// 持ち駒リスト。
        /// </summary>
        List<MotiItem> MotiList { get; set; }

        SkyBuffer buffer_Sky { get; set; }

    }
}
