using System.Diagnostics;

namespace Grayscale.Kifuwarakaku.UseCases.Features
{

    /// <summary>
    /// 将棋エンジンのプロセスをラッピングしています。
    /// </summary>
    public class EngineProcessWrapperImpl : EngineProcessWrapper
    {

        public DELEGATE_ShogiServer_ToEngine Delegate_ShogiServer_ToEngine { get { return this.delegate_ShogiServer_ToEngine; } }
        public void SetDelegate_ShogiServer_ToEngine(DELEGATE_ShogiServer_ToEngine delegateMethod)
        {
            this.delegate_ShogiServer_ToEngine = delegateMethod;
        }
        private DELEGATE_ShogiServer_ToEngine delegate_ShogiServer_ToEngine;

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// これが、将棋エンジン（プロセス）です。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public Process ShogiEngine { get { return this.shogiEngine; } }
        public void SetShogiEngine(Process shogiEngine)
        {
            this.shogiEngine = shogiEngine;
        }
        private Process shogiEngine;


        /// <summary>
        /// 将棋エンジンに向かって、ok コマンドを送信する要求。
        /// </summary>
        public bool Requested_SendOk { get { return this.requested_SendOk; } }
        public void SetRequested_SendOk(bool requested)
        {
            requested_SendOk = requested;
        }
        private bool requested_SendOk;


        /// <summary>
        /// 将棋エンジンが起動しているか否かです。
        /// </summary>
        /// <returns></returns>
        public bool IsLive_ShogiEngine()
        {
            return null != this.ShogiEngine && !this.ShogiEngine.HasExited;
        }


        /// <summary>
        /// 生成後、Pr_ofShogiEngine をセットしてください。
        /// </summary>
        public EngineProcessWrapperImpl()
        {
            this.SetDelegate_ShogiServer_ToEngine((string line) =>
            {
                // デフォルトでは何もしません。
            });
        }


        /// <summary>
        /// 将棋エンジンの標準入力へ、メッセージを送ります。
        /// 
        /// 二度手間なんだが、メソッドを１箇所に集約するためにこれを使う☆
        /// </summary>
        private void Download(string message)
        {
            //IErrorController logTag = OwataMinister.SERVER_NETWORK;

            this.ShogiEngine.StandardInput.WriteLine(message);

            if (null != this.Delegate_ShogiServer_ToEngine)
            {
                this.Delegate_ShogiServer_ToEngine(message);
            }
        }

        /// <summary>
        /// 将棋エンジンに、"position ～略～"を送信します。
        /// </summary>
        public void Send_Position(string position)
        {
            // 将棋エンジンの標準入力へ、メッセージを送ります。
            this.Download(position);
        }

        /// <summary>
        /// 将棋エンジンに、"setoption ～略～"を送信します。
        /// </summary>
        public void Send_Setoption(string setoption)
        {
            // 将棋エンジンの標準入力へ、メッセージを送ります。
            this.Download(setoption);
        }

        /// <summary>
        /// 将棋エンジンに、"usi"を送信します。
        /// </summary>
        public void Send_Usi()
        {
            // 将棋エンジンの標準入力へ、メッセージを送ります。
            this.Download("usi");
        }

        /// <summary>
        /// 将棋エンジンに、"isready"を送信します。
        /// </summary>
        public void Send_Isready()
        {
            this.Download("isready");
        }

        /// <summary>
        /// 将棋エンジンに、"usinewgame"を送信します。
        /// </summary>
        public void Send_Usinewgame()
        {
            // 将棋エンジンの標準入力へ、メッセージを送ります。
            this.Download("usinewgame");
        }

        /// <summary>
        /// 将棋エンジンに、"gameover lose"を送信します。
        /// </summary>
        public void Send_Gameover_lose()
        {
            // 将棋エンジンの標準入力へ、メッセージを送ります。
            this.Download("gameover lose");
        }

        /// <summary>
        /// 将棋エンジンに、"quit"を送信します。
        /// </summary>
        public void Send_Quit()
        {
            // 将棋エンジンの標準入力へ、メッセージを送ります。
            this.Download("quit");
        }

        /// <summary>
        /// 将棋エンジンに、"ok"を送信します。"noop"への返事です。
        /// </summary>
        public void Send_Noop_from_server()
        {
            // 将棋エンジンの標準入力へ、メッセージを送ります。
            this.Download("noop from server");
        }

        /// <summary>
        /// 将棋エンジンに、"go"を送信します。
        /// </summary>
        public void Send_Go()
        {
            // 将棋エンジンの標準入力へ、メッセージを送ります。
            this.Download("go");
        }

        /// <summary>
        /// 将棋エンジンに、終了するように促します。
        /// </summary>
        public void Send_Shutdown()
        {
            if (this.IsLive_ShogiEngine())
            {
                // 将棋エンジンの標準入力へ、メッセージを送ります。
                this.Download("quit");
            }
        }

        /// <summary>
        /// 将棋エンジンに、ログを出すように促します。
        /// </summary>
        public void Send_Logdase()
        {
            if (this.IsLive_ShogiEngine())
            {
                // 将棋エンジンの標準入力へ、メッセージを送ります。
                this.Download("logdase");
            }
        }

    }
}
