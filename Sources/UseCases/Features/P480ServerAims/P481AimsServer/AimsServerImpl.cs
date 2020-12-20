    using Grayscale.Kifuwarakaku.Entities.Logging;
using Grayscale.P224Sky.L500Struct;
using Grayscale.P461Server.L498Server;
using Grayscale.P481AimsServer.I060Phase;
using Grayscale.P481AimsServer.I070ServerBase;
using Grayscale.P481AimsServer.L060Phase;
using Grayscale.P481AimsServer.L125Receiver;
using System;

namespace Grayscale.P481AimsServer.L500Server
{

    /// <summary>
    /// AIMSクライアントと標準入出力通信するサーバー。
    /// 
    /// AIMS「usi」→　こちら　：　ゲームの開始です。将棋エンジンを起動してください。
    /// 
    /// </summary>
    public class AimsServerImpl : Server_Impl, AimsServerBase
    {
        /// <summary>
        /// 将棋エンジンへのファイルパス。
        /// </summary>
        public string ShogiEngineFilePath { get; set; }

        /// <summary>
        /// フェーズ。
        /// </summary>
        public Phase_AimsServer Phase_AimsServer { get { return this.phase_AimsServer; } }
        public void SetPhase_AimsServer(Phase_AimsServer phase_AimsServer)
        {
            this.phase_AimsServer = phase_AimsServer;
        }
        private Phase_AimsServer phase_AimsServer;

        public AimsServerImpl(SkyConst src_Sky, int temezumi)
            : base(src_Sky, temezumi, new Receiver_ForAimsImpl())
        {
            // AIMSサーバー用の特別処理。
            ((Receiver_ForAimsImpl)this.EngineClient.Receiver).SetOwner_AimsServer(this);

            // 最初の状態。
            this.phase_AimsServer = Phase_AimsServer._01_Server_Booted;
        }

        public void AtBegin()
        {
        }

        public void AtBody()
        {
            PhaseResult_AimsServer phaseResult = PhaseResult_AimsServer.None;

            while (true)
            {
                switch(phase_AimsServer)
                {
                    case Phase_AimsServer._01_Server_Booted:
                        {
                            //----------------------------------------
                            // AIMS GUI から起動をかけられた直後の状態です。
                            //----------------------------------------

                            // コマンドを送る前に、先にフェーズ変更
                            //MessageBox.Show("サーバー「先に _01_First_B フェーズに変更するぜ☆」");
                            this.SetPhase_AimsServer(Phase_AimsServer._02_WaitAimsUsiok);

                            //
                            // AIMS GUI                         擬似将棋サーバー
                            // --------                         -------------
                            //  待機中     ←送信コマンド「usi」←       送信
                            //
                            //MessageBox.Show("サーバー「AIMS GUI に usi コマンドを送るぜ☆");
                            Console.Out.WriteLine("usi");
                        }
                        break;
                    case Phase_AimsServer._02_WaitAimsUsiok:
                        {
                            //
                            // AIMS GUI                         擬似将棋サーバー
                            // --------                         -------------
                            //  準備中     →予想コマンド「usiok」→     待機中
                            //

                            string input_fromAims = Console.In.ReadLine();

                            if (input_fromAims == "usiok")
                            {
                                //
                                // AIMS GUI                         擬似将棋サーバー
                                // --------                         -------------
                                //  送信済     →コマンド「usiok」→       着信
                                //
                                //MessageBox.Show("サーバー「AIMS GUIから、usiok コマンドが届いたぜ☆」");

                                // コマンドを送る前に、先にフェーズ変更
                                //MessageBox.Show("サーバー「先に WaitEngineLive フェーズに変更するぜ☆」");
                                this.SetPhase_AimsServer(Phase_AimsServer._03_WaitEngineLive);

                                // 将棋エンジンを起動するぜ☆
                                //MessageBox.Show("サーバー「将棋エンジンを起動するぜ☆」");
                                this.EngineClient.Start(this.ShogiEngineFilePath);
                            }
                        }
                        break;

                    case Phase_AimsServer._03_WaitEngineLive:
                        {
                            //----------------------------------------
                            // 将棋サーバーが起ち上がっていることを確認したいぜ☆！
                            //----------------------------------------

                            if (this.EngineClient.ShogiEngineProcessWrapper.IsLive_ShogiEngine())
                            {

                                // コマンドを送る前に、先にフェーズ変更
                                //MessageBox.Show("サーバー「将棋エンジンのプロセスが起動していることを確認したぜ☆！\n"+
                                //    "先に WaitEngineUsiok フェーズに変更するぜ☆」");
                                this.SetPhase_AimsServer(Phase_AimsServer._04_WaitEngineUsiok);


                                // 将棋エンジンに usi コマンドを送るぜ☆
                                //MessageBox.Show("サーバー「このあと、将棋エンジンにusiコマンドを送るぜ☆」");
                                this.EngineClient.ShogiEngineProcessWrapper.Send_Usi();
                            }
                            else
                            {
                                // まだ起動してないなあ。
                            }

                        }
                        break;

                    case Phase_AimsServer._04_WaitEngineUsiok:
                        {
                            // thru
                        }
                        break;

                    case Phase_AimsServer._05_WaitAimsReadyok:
                        {
                            string input_fromAims = Console.In.ReadLine();

                            if (input_fromAims == "readyok")
                            {
                                //MessageBox.Show("サーバー「AIMS GUIから readyok の返信があったぜ☆！\n" +
                                //    "先に WaitEngineReadyok フェーズに変更するぜ☆」");
                                this.SetPhase_AimsServer(Phase_AimsServer._05_WaitEngineReadyok);

                                //MessageBox.Show("サーバー「将棋エンジンに、setoption（注文） と、isready（準備できたか返せ） コマンドを送るぜ☆」");

                                //------------------------------------------------------------
                                // 将棋エンジンへ：setoption
                                //------------------------------------------------------------

                                // 将棋エンジンへ：　「私は将棋サーバーですが、USIプロトコルのponderコマンドには対応していませんので、送ってこないでください」
                                this.EngineClient.ShogiEngineProcessWrapper.Send_Setoption("setoption name USI_Ponder value false");

                                // 将棋エンジンへ：　「私は将棋サーバーです。noop コマンドを送ってくれば、すぐに ok コマンドを返します。1分間を空けてください」
                                this.EngineClient.ShogiEngineProcessWrapper.Send_Setoption("setoption name noopable value true");

                                //------------------------------------------------------------
                                // 将棋エンジンへ：　「準備はいいですか？」
                                //------------------------------------------------------------
                                this.EngineClient.ShogiEngineProcessWrapper.Send_Isready();

                            }
                        }
                        break;

                    case Phase_AimsServer._05_WaitEngineReadyok:
                        {
                        }
                        break;
                }


                switch(phaseResult)
                {
                    case PhaseResult_AimsServer.Break:
                        goto gt_EndLoop;
                }
            }
        gt_EndLoop:
            ;
        }

        public void AtEnd()
        {
        }

    }
}
