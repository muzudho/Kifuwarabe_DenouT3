﻿using System.Collections.Generic;
using Grayscale.Kifuwarakaku.Entities.Features;

namespace Grayscale.P372KyokuParser.I500Parser
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
