﻿using Grayscale.P003Log.I500Struct;
using Grayscale.P542Scoreing.I005UsiLoop;
using System.Diagnostics;

namespace Grayscale.P571KifuWarabe.L249Noop
{
    /// <summary>
    /// 
    /// </summary>
    public class NoopTimerImpl
    {
        private Stopwatch sw_forNoop;
        private NoopPhase noopPhase;

        public NoopTimerImpl()
        {
            this.noopPhase = NoopPhase.None;
        }

        /// <summary>
        /// ループに入る前。
        /// </summary>
        public void _01_BeforeLoop()
        {
            this.sw_forNoop = new Stopwatch();
            this.sw_forNoop.Start();
        }

        /// <summary>
        /// メッセージが届いていないとき。
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="isTimeoutShutdown"></param>
        public void _02_AtEmptyMessage(ShogiEngine owner, out bool isTimeoutShutdown, IErrorController errH)
        {
            isTimeoutShutdown = false;
            //errH.Logger.WriteLineAddMemo("メッセージは届いていませんでした。this.sw_forNoop.Elapsed.Seconds=[" + this.sw_forNoop.Elapsed.Seconds + "]");

            if (owner.Option_enable_serverNoopable && 10 < this.sw_forNoop.Elapsed.Seconds)//0 < this.sw_forNoop.Elapsed.Se.Minutes
            {
                // 1分以上、サーバーからメッセージが届いていない場合。
                switch (this.noopPhase)
                {
                    case NoopPhase.NoopThrew:
                        {
                            //MessageBox.Show("20秒ほど経過しても、this.Option_threw_noop が偽だぜ☆！");

                            // noop を投げて 1分過ぎていれば。
#if DEBUG
                            errH.Logger.WriteLineAddMemo("計20秒ほど、サーバーからの応答がなかったぜ☆ (^-^)ﾉｼ");
#endif

                            // このプログラムを終了します。
                            isTimeoutShutdown = true;
                        }
                        break;
                    default:
                    case NoopPhase.None:
                        {
#if DEBUG
                            errH.Logger.WriteLineAddMemo("noopを投げるぜ☆");
#endif
                            // まだ noop を投げていないなら
                            owner.Send("noop");// サーバーが生きていれば、"ok" と返してくるはず。（独自実装）
                            this.noopPhase = NoopPhase.NoopThrew;
                            this.sw_forNoop.Restart();//時間計測をリセット。
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 応答があったとき。
        /// </summary>
        public void _03_AtResponsed(ShogiEngine owner, string command, IErrorController errH)
        {
            //System.Windows.Forms.MessageBox.Show("メッセージが届いています [" + line + "]");

            // noop リセット処理。
            //if (this.Option_threw_noop)
            //{
                // noopを投げてなくても、毎回ストップウォッチはリスタートさせます。
//#if DEBUG
                errH.Logger.WriteLineC("サーバーから応答[" + command + "]があったのでタイマーをリスタートさせるぜ☆");
//#endif
                this.noopPhase = NoopPhase.None;
                this.sw_forNoop.Restart();
            //}
        }
    }
}