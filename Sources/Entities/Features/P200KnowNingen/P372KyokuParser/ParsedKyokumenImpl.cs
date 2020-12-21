using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.P324KifuTree.I250Struct;
using Grayscale.P372KyokuParser.I500Parser;
using System.Collections.Generic;

namespace Grayscale.P372KyokuParser.L500Parser
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
