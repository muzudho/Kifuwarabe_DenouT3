﻿#if DEBUG
using Grayscale.Kifuwarakaku.Entities.Features;
#endif

namespace Grayscale.P551Tansaku.I500Tansaku
{

    /// <summary>
    /// 探索が終わるまで、途中で変更されない設定。
    /// </summary>
    public interface Tansaku_Args
    {

        bool IsHonshogi { get; }
        int[] YomuLimitter { get; }

#if DEBUG
        KaisetuBoards LogF_moveKiki { get; }
#endif
    }
}
