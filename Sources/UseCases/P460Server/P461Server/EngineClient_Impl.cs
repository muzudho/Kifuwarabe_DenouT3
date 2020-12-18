using Grayscale.Kifuwarakaku.Entities.Logger;
using Grayscale.P005Tushin.L500Util;
using Grayscale.P211WordShogi.L500Word;
using Grayscale.P324KifuTree.I250Struct;
using Grayscale.P341Ittesasu.L250OperationA;
using Grayscale.P461Server.I125Receiver;
using Grayscale.P461Server.I496EngineWrapper;
using Grayscale.P461Server.I497EngineClient;
using Grayscale.P461Server.L496EngineWrapper;
using System;
using System.Diagnostics;

namespace Grayscale.P461Server.L497EngineClient
{
    /// <summary>
    ///  プロセスラッパー
    /// 
    ///     １つの将棋エンジンと通信します。１対１の関係になります。
    ///     このクラスを、将棋エンジンのコンソールだ、と想像して使います。
    /// 
    /// </summary>
    public class EngineClient_Impl : EngineClient
    {
        /// <summary>
        /// オーナー・サーバー
        /// </summary>
        public object Owner_Server { get { return this.ownerServer; } }//Server型
        public void SetOwner_Server(object owner)//Server型
        {
            this.ownerServer = owner;
        }
        private object ownerServer;//Server型

        /// <summary>
        /// レシーバー
        /// </summary>
        public Receiver Receiver { get { return this.receiver; } }
        private Receiver receiver;

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 将棋エンジンと会話できるオブジェクトです。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public EngineProcessWrapper ShogiEngineProcessWrapper { get; set; }

        public EngineClient_Impl(Receiver receiver)
        {
            this.receiver = receiver;
            this.receiver.SetOwner_EngineClient(this);

            this.ShogiEngineProcessWrapper = new EngineProcessWrapperImpl();

#if DEBUG
            this.ShogiEngineProcessWrapper.SetDelegate_ShogiServer_ToEngine( (string line, ILogTag logTag) =>
            {
                //
                // USIコマンドを将棋エンジンに送ったタイミングで、なにかすることがあれば、
                // ここに書きます。
                //
                Logger.WriteLineC(logTag, line);
            });
#endif
        }

        /// <summary>
        /// 将棋エンジンを起動します。
        /// </summary>
        public void Start(string shogiEngineFilePath)
        {
            try
            {
                if (this.ShogiEngineProcessWrapper.IsLive_ShogiEngine())
                {
                    Util_Message.Show("将棋エンジンサービスは終了していません。");
                    goto gt_EndMethod;
                }

                //------------------------------
                // ログファイルを削除します。
                //------------------------------
                Logger.RemoveAllLogFiles();


                ProcessStartInfo startInfo = new ProcessStartInfo();

                startInfo.FileName = shogiEngineFilePath; // 実行するファイル名
                //startInfo.CreateNoWindow = true; // コンソール・ウィンドウを開かない
                startInfo.UseShellExecute = false; // シェル機能を使用しない
                startInfo.RedirectStandardInput = true;//標準入力をリダイレクト
                startInfo.RedirectStandardOutput = true; // 標準出力をリダイレクト

                this.ShogiEngineProcessWrapper.SetShogiEngine(Process.Start(startInfo)); // アプリの実行開始

                //  OutputDataReceivedイベントハンドラを追加
                this.ShogiEngineProcessWrapper.ShogiEngine.OutputDataReceived += this.Receiver.OnListenUpload_Async;
                this.ShogiEngineProcessWrapper.ShogiEngine.Exited += this.OnExited;

                // 非同期受信スタート☆！
                this.ShogiEngineProcessWrapper.ShogiEngine.BeginOutputReadLine();
            }
            catch (Exception ex)
            {
                Util_Message.Show(ex.GetType().Name + "：" + ex.Message);
            }

        gt_EndMethod:
            ;
        }


        /// <summary>
        /// ************************************************************************************************************************
        /// この将棋サーバーを終了したときにする挙動を、ここに書きます。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnExited(object sender, System.EventArgs e)
        {
            ILogTag logTag = LogTags.EngineDefault;
            this.ShogiEngineProcessWrapper.Send_Shutdown(logTag);
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 手番が替わったときの挙動を、ここに書きます。
        /// ************************************************************************************************************************
        /// </summary>
        public void OnChangedTurn(KifuTree kifu, ILogTag logTag)
        {
            if (!this.ShogiEngineProcessWrapper.IsLive_ShogiEngine())
            {
                goto gt_EndMethod;
            }

            // FIXME:
            Playerside pside = kifu.CurNode.Value.KyokumenConst.KaisiPside;
            switch (pside)
            {
                case Playerside.P2:
                    // 仮に、コンピューターが後手番とします。

                    //------------------------------------------------------------
                    // とりあえず、コンピューターが後手ということにしておきます。
                    //------------------------------------------------------------

                    // 例：「position startpos moves 7g7f」
                    this.ShogiEngineProcessWrapper.Send_Position(Util_KirokuGakari.ToSfen_PositionCommand(kifu), logTag);

                    this.ShogiEngineProcessWrapper.Send_Go(logTag);

                    break;
                default:
                    break;
            }

        gt_EndMethod:
            ;
        }


        /// <summary>
        /// 将棋エンジンに、終了するように促します。
        /// </summary>
        public void Send_Shutdown(ILogTag logTag)
        {
            this.ShogiEngineProcessWrapper.Send_Shutdown(logTag);
        }

        /// <summary>
        /// 将棋エンジンに、ログを出すように促します。
        /// </summary>
        public void Send_Logdase(ILogTag logTag)
        {
            this.ShogiEngineProcessWrapper.Send_Logdase(logTag);
        }

        ///// <summary>
        ///// 将棋エンジンを先手にするために、go を出します。
        ///// </summary>
        //public void Send_Go(IErrorController logTag)
        //{
        //    this.ShogiEngineProcessWrapper.Send_Go(logTag);
        //}

    }


}
