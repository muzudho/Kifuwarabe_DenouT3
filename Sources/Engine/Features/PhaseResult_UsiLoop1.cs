﻿
namespace Grayscale.P571KifuWarabe.L250UsiLoop
{
    public enum PhaseResult_UsiLoop1
    {

        /// <summary>
        /// 何もしません。
        /// </summary>
        None,

        /// <summary>
        /// ループを抜けます。
        /// </summary>
        Break,

        /// <summary>
        /// アプリケーションを終了します。
        /// </summary>
        Quit,

        /// <summary>
        /// タイムアウトによる強制終了。
        /// </summary>
        TimeoutShutdown,
    }
}