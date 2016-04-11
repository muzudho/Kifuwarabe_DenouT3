using Grayscale.P222_Log_Kaisetu.L250____Struct;
using Grayscale.P551_Tansaku____.L___500_Tansaku;

namespace Grayscale.P551_Tansaku____.L500____Struct
{

    /// <summary>
    /// 探索が終わるまで、途中で変更されない設定。
    /// </summary>
    public class Tansaku_ArgsImpl : Tansaku_Args
    {
        /// <summary>
        /// 本将棋なら真。
        /// </summary>
        public bool IsHonshogi { get { return this.isHonshogi; } }
        private bool isHonshogi;

        /// <summary>
        /// 読みの上限の様々な設定☆（深さ優先探索で使用☆）
        /// </summary>
        public int[] YomuLimitter { get { return this.yomuLimitter; } }
        private int[] yomuLimitter;

        /// <summary>
        /// ログ用☆
        /// </summary>
        public KaisetuBoards LogF_moveKiki { get { return this.logF_moveKiki; } }
        private KaisetuBoards logF_moveKiki;

        public Tansaku_ArgsImpl(
            bool isHonshogi,
            int[] yomuLimitter,
            KaisetuBoards logF_moveKiki
            )
        {
            this.isHonshogi = isHonshogi;
            this.yomuLimitter = yomuLimitter;
            this.logF_moveKiki = logF_moveKiki;
        }
    }

}
