using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P324_KifuTree___.L___250_Struct;
using Grayscale.P372_KyokuParser.L___500_Parser;
using System.Collections.Generic;

namespace Grayscale.P372_KyokuParser.L500____Parser
{
    /// <summary>
    /// 解析された局面
    /// </summary>
    public class ParsedKyokumenImpl : ParsedKyokumen
    {

        /// <summary>
        /// 初期局面の先後。
        /// </summary>
        public Playerside FirstPside { get; set; }

        /// <summary>
        /// 棋譜ノード。
        /// </summary>
        public KifuNode KifuNode { get; set; }

        /// <summary>
        /// 持ち駒リスト。
        /// </summary>
        public List<MotiItem> MotiList { get; set; }

        public SkyBuffer buffer_Sky { get; set; }

        public ParsedKyokumenImpl()
        {
            this.MotiList = new List<MotiItem>();
        }

    }
}
