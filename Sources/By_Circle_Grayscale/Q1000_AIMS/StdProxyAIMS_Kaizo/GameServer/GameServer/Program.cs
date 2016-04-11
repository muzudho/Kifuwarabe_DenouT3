using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sample.GameServer
{
    static class Program
    {

        /// <summary>
        /// 状態
        /// </summary>
        private static ServerState State { get; set; }

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {

            Program.State = ServerState.CompAwake1;
            AiteImpl comp = null; // コンピューター・プレイヤー
            AiteImpl term = null; // ターミナル

            string aimsSaid; // AIMS からのメッセージ。
            while(true)
            {
                switch(Program.State)
                {
                    case ServerState.CompAwake1:
                        {
                            aimsSaid = Console.ReadLine();

                            if (aimsSaid != "") // しゃべったあ☆！
                            {
                                if (aimsSaid == "おいっ☆起きろ☆ｗｗ")
                                {
                                    // 書き出せば、AIMS に届きます。
                                    Console.WriteLine("「" + aimsSaid + "」だって☆ｗｗ？");
                                    System.Threading.Thread.Sleep(1000);

                                    Console.WriteLine("コンピューターを起こすんだぜ☆ｗｗ");
                                    System.Threading.Thread.Sleep(1000);

                                    // コンピューター・プレイヤー作成
                                    {
                                        comp = new AiteImpl();
                                        comp.CreateAite("./ComputerPlayer/ComputerPlayer/bin/Release/Sample.ComputerPlayer.exe");
                                        comp.Aite.OutputDataReceived += Program.ListenComp_Async;
                                        comp.Aite.Exited += Program.OnCompExited_Async;
                                        comp.Start();
                                        Program.State = ServerState.CompAwake2;
                                    }
                                }
                            }
                        }
                        break;

                    case ServerState.CompAwake2:
                        {
                            // コンピューター・プレイヤーからのメッセージを待っています。
                        }
                        break;

                    case ServerState.CompAwake3:
                        {
                            // コンピューター・プレイヤーからのメッセージを受け取りました。
                            Console.WriteLine("コンピューターが起きたんだぜ☆ｗｗｗｗ");
                            System.Threading.Thread.Sleep(1000);

                            Console.WriteLine("次は、ターミナルも出すんだぜ☆ｗｗ");
                            System.Threading.Thread.Sleep(1000);

                            // ターミナル作成
                            {
                                term = new AiteImpl();
                                term.CreateAite("./Terminal/Terminal/bin/Release/Sample.Terminal.exe");
                                term.Aite.OutputDataReceived += Program.ListenTerm_Async;
                                term.Aite.Exited += Program.OnTermExited_Async;
                                term.Start();
                                Program.State = ServerState.TermAwake2;
                            }
                        }
                        break;

                    case ServerState.TermAwake2:
                        {
                            // コンピューター・プレイヤーからのメッセージを待っています。
                        }
                        break;

                    case ServerState.TermAwake3:
                        {
                            // コンピューター・プレイヤーからのメッセージを受け取りました。
                            Console.WriteLine("ターミナルが起きたんだぜ☆ｗｗｗｗ");
                            System.Threading.Thread.Sleep(1000);

                            goto gt_EndLoop;
                        }

                    default:
                        break;
                }

            }
        gt_EndLoop:
            ;

            Console.WriteLine("サーバー☆　ｵﾜﾀなんだぜ☆ｗｗ");
            System.Threading.Thread.Sleep(1000);

        }


        #region コンピューター・プレイヤー

        /// <summary>
        /// コンピューター・プレイヤーからの書込みを非同期受信(*1)します。
        /// 
        ///         *1…こっちの都合に合わせず、データが飛んできます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void ListenComp_Async(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            string compSaid = e.Data; // コンピューター・プレイヤーからのメッセージ。

            if (null == compSaid)
            {
                // 無視
            }
            else
            {
                //>>>>>>>>>> メッセージを受け取りました。

                if (compSaid.StartsWith("むくり☆ｗｗ"))
                {
                    // コンピューターが起きたんだぜ☆ｗｗｗｗ
                    Program.State = ServerState.CompAwake3;
                }
                else
                {
                }
            }
        }

        /// <summary>
        /// コンピューター・プレイヤーが終了したときにする挙動を、ここに書きます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void OnCompExited_Async(object sender, System.EventArgs e)
        {
        }

        #endregion




        #region ターミナル

        /// <summary>
        /// ターミナルからの書込みを非同期受信(*1)します。
        /// 
        ///         *1…こっちの都合に合わせず、データが飛んできます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void ListenTerm_Async(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            string termSaid = e.Data; // ターミナルからのメッセージ。

            if (null == termSaid)
            {
                // 無視
            }
            else
            {
                //>>>>>>>>>> メッセージを受け取りました。

                if (termSaid.StartsWith("むくり☆ｗｗ"))
                {
                    // ターミナルが起きたんだぜ☆ｗｗｗｗ
                    Program.State = ServerState.TermAwake3;
                }
                else
                {
                }
            }
        }

        /// <summary>
        /// ターミナルが終了したときにする挙動を、ここに書きます。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void OnTermExited_Async(object sender, System.EventArgs e)
        {
        }

        #endregion


    }
}
