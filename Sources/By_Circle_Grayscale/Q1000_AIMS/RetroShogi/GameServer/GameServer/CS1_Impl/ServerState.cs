using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.GameServer
{

    /// <summary>
    /// 状態。　名前付けはとりあえず適当☆ｗｗ
    /// </summary>
    public enum ServerState
    {
        // コンピューター・プレイヤーを起こせ☆ｗｗ
        CompAwake1,
        CompAwake2,
        CompAwake3,

        // ターミナルを起こせ☆ｗｗ
        TermAwake2,
        TermAwake3
    }
}
