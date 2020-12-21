namespace Grayscale.Kifuwarakaku.Engine
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;
    using Grayscale.Kifuwarakaku.Engine.Features;
    using Grayscale.Kifuwarakaku.Entities.Features;
    using Grayscale.Kifuwarakaku.Entities.Logging;
    using Grayscale.Kifuwarakaku.UseCases;
    using Nett;

    /// <summary>
    /// プログラムのエントリー・ポイントです。
    /// </summary>
    class Program
    {
        /// <summary>
        /// Ｃ＃のプログラムは、
        /// この Main 関数から始まり、 Main 関数を抜けて終わります。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // 将棋エンジン　きふわらべ
            ProgramSupport programSupport = new ProgramSupport();
            programSupport.AtBegin();
            bool isTimeoutShutdown = false;

            try
            {

                // 
                // 図.
                // 
                //     プログラムの開始：  ここの先頭行から始まります。
                //     プログラムの実行：  この中で、ずっと無限ループし続けています。
                //     プログラムの終了：  この中の最終行を終えたとき、
                //                         または途中で Environment.Exit(0); が呼ばれたときに終わります。
                //                         また、コンソールウィンドウの[×]ボタンを押して強制終了されたときも  ぶつ切り  で突然終わります。


                //************************************************************************************************************************
                // ループ（全体）
                //************************************************************************************************************************
                //
                // 図.
                //
                //      無限ループ（全体）
                //          │
                //          ├─無限ループ（１）
                //          │                      将棋エンジンであることが認知されるまで、目で訴え続けます(^▽^)
                //          │                      認知されると、無限ループ（２）に進みます。
                //          │
                //          └─無限ループ（２）
                //                                  対局中、ずっとです。
                //                                  対局が終わると、無限ループ（１）に戻ります。
                //
                // 無限ループの中に、２つの無限ループが入っています。
                //

                while (true)
                {
#if DEBUG_STOPPABLE
            MessageBox.Show("きふわらべのMainの無限ループでブレイク☆！", "デバッグ");
            System.Diagnostics.Debugger.Break();
#endif
                    //
                    // USIループ（１つ目）
                    //
                    UsiLoop1 usiLoop1 = new UsiLoop1(programSupport);

                    isTimeoutShutdown = false;
                    PhaseResult_UsiLoop1 result_UsiLoop1;

                    //
                    // サーバーに noop を送ってもよいかどうかは setoption コマンドがくるまで分からないので、
                    // 作ってしまっておきます。
                    // 1回も役に立たずに Loop2 に行くようなら、正常です。
#if NOOPABLE
            NoopTimerImpl noopTimer = new NoopTimerImpl();
            noopTimer._01_BeforeLoop();
#endif

                    while (true)
                    {
                        result_UsiLoop1 = PhaseResult_UsiLoop1.None;

                        // 将棋サーバーから何かメッセージが届いていないか、見てみます。
                        string line = Util_Message.Download_Nonstop();

                        // (2020-12-13 sun) ノン・ブロッキングなら このコードが意味あったんだが☆（＾～＾）
                        if (null == line)//次の行が無ければヌル。
                        {
                            // メッセージは届いていませんでした。
                            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
#if NOOPABLE
                    bool isTimeoutShutdown_temp;
                    noopTimer._03_AtEmptyMessage(this.Owner, out isTimeoutShutdown_temp);
                    if (isTimeoutShutdown_temp)
                    {
                        //MessageBox.Show("ループ１でタイムアウトだぜ☆！");
                        out_isTimeoutShutdown = isTimeoutShutdown_temp;
                        result_UsiLoop1 = PhaseResult_UsiLoop1.TimeoutShutdown;
                        goto end_loop1;
                    }
#endif

                            goto gt_NextTime1;
                        }

                        // 通信ログは必ず取ります。
                        Logger.WriteLineC(line);

#if NOOPABLE
                noopTimer._04_AtResponsed(this.Owner, line);
#endif




                        if ("usi" == line)
                        {
                            //------------------------------------------------------------
                            // あなたは USI ですか？
                            //------------------------------------------------------------
                            //
                            // 図.
                            //
                            //      log.txt
                            //      ┌────────────────────────────────────────
                            //      ～
                            //      │2014/08/02 1:31:35> usi
                            //      │
                            //
                            //
                            // 将棋所で [対局(G)]-[エンジン管理...]-[追加...] でファイルを選んだときに、
                            // 送られてくる文字が usi です。


                            //------------------------------------------------------------
                            // エンジン設定ダイアログボックスを作ります
                            //------------------------------------------------------------
                            //
                            // 図.
                            //
                            //      log.txt
                            //      ┌────────────────────────────────────────
                            //      ～
                            //      │2014/08/02 23:40:15< option name 子 type check default true
                            //      │2014/08/02 23:40:15< option name USI type spin default 2 min 1 max 13
                            //      │2014/08/02 23:40:15< option name 寅 type combo default tiger var マウス var うし var tiger var ウー var 龍 var へび var 馬 var ひつじ var モンキー var バード var ドッグ var うりぼー
                            //      │2014/08/02 23:40:15< option name 卯 type button default うさぎ
                            //      │2014/08/02 23:40:15< option name 辰 type string default DRAGON
                            //      │2014/08/02 23:40:15< option name 巳 type filename default スネーク.html
                            //      │
                            //
                            //
                            // 将棋所で [エンジン設定] ボタンを押したときに出てくるダイアログボックスに、
                            //      ・チェックボックス
                            //      ・スピン
                            //      ・コンボボックス
                            //      ・ボタン
                            //      ・テキストボックス
                            //      ・ファイル選択テキストボックス
                            // を置くことができます。
                            //
                            Playing.Send("option name 子 type check default true");
                            Playing.Send("option name USI type spin default 2 min 1 max 13");
                            Playing.Send("option name 寅 type combo default tiger var マウス var うし var tiger var ウー var 龍 var へび var 馬 var ひつじ var モンキー var バード var ドッグ var うりぼー");
                            Playing.Send("option name 卯 type button default うさぎ");
                            Playing.Send("option name 辰 type string default DRAGON");
                            Playing.Send("option name 巳 type filename default スネーク.html");


                            //------------------------------------------------------------
                            // USI です！！
                            //------------------------------------------------------------
                            //
                            // 図.
                            //
                            //      log.txt
                            //      ┌────────────────────────────────────────
                            //      ～
                            //      │2014/08/02 2:03:33< id name fugafuga 1.00.0
                            //      │2014/08/02 2:03:33< id author hogehoge
                            //      │2014/08/02 2:03:33< usiok
                            //      │
                            //
                            // プログラム名と、作者名を送り返す必要があります。
                            // オプションも送り返せば、受け取ってくれます。
                            // usi を受け取ってから、5秒以内に usiok を送り返して完了です。

                            var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
                            var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));
                            var engineName = toml.Get<TomlTable>("Engine").Get<string>("Name");
                            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                            var engineAuthor = toml.Get<TomlTable>("Engine").Get<string>("Author");

                            Playing.Send($"id name {engineName} {version.Major}.{version.Minor}.{version.Build}");
                            Playing.Send($"id author {engineAuthor}");
                            Playing.Send("usiok");
                        }
                        else if (line.StartsWith("setoption"))
                        {
                            //------------------------------------------------------------
                            // 設定してください
                            //------------------------------------------------------------
                            //
                            // 図.
                            //
                            //      log.txt
                            //      ┌────────────────────────────────────────
                            //      ～
                            //      │2014/08/02 8:19:36> setoption name USI_Ponder value true
                            //      │2014/08/02 8:19:36> setoption name USI_Hash value 256
                            //      │
                            //
                            // ↑ゲーム開始時には、[対局]ダイアログボックスの[エンジン共通設定]の２つの内容が送られてきます。
                            //      ・[相手の手番中に先読み] チェックボックス
                            //      ・[ハッシュメモリ  ★　MB] スピン
                            //
                            // または
                            //
                            //      log.txt
                            //      ┌────────────────────────────────────────
                            //      ～
                            //      │2014/08/02 23:47:35> setoption name 卯
                            //      │2014/08/02 23:47:35> setoption name 卯
                            //      │2014/08/02 23:48:29> setoption name 子 value true
                            //      │2014/08/02 23:48:29> setoption name USI value 6
                            //      │2014/08/02 23:48:29> setoption name 寅 value 馬
                            //      │2014/08/02 23:48:29> setoption name 辰 value DRAGONabcde
                            //      │2014/08/02 23:48:29> setoption name 巳 value C:\Users\Takahashi\Documents\新しいビットマップ イメージ.bmp
                            //      │
                            //
                            //
                            // 将棋所から、[エンジン設定] ダイアログボックスの内容が送られてきます。
                            // このダイアログボックスは、将棋エンジンから将棋所に  ダイアログボックスを作るようにメッセージを送って作ったものです。
                            //

                            //------------------------------------------------------------
                            // 設定を一覧表に変えます
                            //------------------------------------------------------------
                            //
                            // 上図のメッセージのままだと使いにくいので、
                            // あとで使いやすいように Key と Value の表に分けて持ち直します。
                            //
                            // 図.
                            //
                            //      setoptionDictionary
                            //      ┌──────┬──────┐
                            //      │Key         │Value       │
                            //      ┝━━━━━━┿━━━━━━┥
                            //      │USI_Ponder  │true        │
                            //      ├──────┼──────┤
                            //      │USI_Hash    │256         │
                            //      └──────┴──────┘
                            //
                            Regex regex = new Regex(@"setoption name ([^ ]+)(?: value (.*))?", RegexOptions.Singleline);
                            Match m = regex.Match(line);

                            if (m.Success)
                            {
                                string name = (string)m.Groups[1].Value;
                                string value = "";

                                if (3 <= m.Groups.Count)
                                {
                                    // 「value ★」も省略されずにありました。
                                    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                                    value = (string)m.Groups[2].Value;
                                }

                                if (programSupport.SetoptionDictionary.ContainsKey(name))
                                {
                                    // 設定を上書きします。
                                    programSupport.SetoptionDictionary[name] = value;
                                }
                                else
                                {
                                    // 設定を追加します。
                                    programSupport.SetoptionDictionary.Add(name, value);
                                }
                            }

                            if (programSupport.SetoptionDictionary.ContainsKey("USI_ponder"))
                            {
                                string value = programSupport.SetoptionDictionary["USI_ponder"];

                                bool result;
                                if (Boolean.TryParse(value, out result))
                                {
                                    programSupport.Option_enable_usiPonder = result;
                                }
                            }
                            else if (programSupport.SetoptionDictionary.ContainsKey("noopable"))
                            {
                                //
                                // 独自実装。
                                //
                                string value = programSupport.SetoptionDictionary["noopable"];

                                bool result;
                                if (Boolean.TryParse(value, out result))
                                {
                                    programSupport.Option_enable_serverNoopable = result;
                                }
                            }
                        }
                        else if ("isready" == line)
                        {
                            usiLoop1.AtLoop_OnIsready(line, ref result_UsiLoop1);
                        }
                        else if ("usinewgame" == line) { usiLoop1.AtLoop_OnUsinewgame(line, ref result_UsiLoop1); }
                        else if ("quit" == line) { usiLoop1.AtLoop_OnQuit(line, ref result_UsiLoop1); }
                        else
                        {
                            //------------------------------------------------------------
                            // ○△□×！？
                            //------------------------------------------------------------
                            //
                            // ／(＾×＾)＼
                            //

                            // 通信が届いていますが、このプログラムでは  聞かなかったことにします。
                            // USIプロトコルの独習を進め、対応／未対応を選んでください。
                            //
                            // ログだけ取って、スルーします。
                        }

                        switch (result_UsiLoop1)
                        {
                            case PhaseResult_UsiLoop1.Break:
                                goto end_loop1;

                            case PhaseResult_UsiLoop1.Quit:
                                goto end_loop1;

                            default:
                                break;
                        }

                    gt_NextTime1:
                        ;
                    }
                end_loop1:

                    if (isTimeoutShutdown)
                    {
                        //MessageBox.Show("ループ１で矯正終了するんだぜ☆！");
                        goto gt_EndMethod;
                    }
                    else if (result_UsiLoop1 == PhaseResult_UsiLoop1.Quit)
                    {
                        goto gt_EndMethod;//全体ループを抜けます。
                    }

                    //
                    // USIループ（２つ目）
                    //
                    UsiLoop2 usiLoop2 = new UsiLoop2(programSupport.shogisasi, programSupport);
                    usiLoop2.AtBegin();
                    usiLoop2.AtBody(out isTimeoutShutdown);
                    usiLoop2.AtEnd();
                    if (isTimeoutShutdown)
                    {
                        //MessageBox.Show("ループ２で矯正終了するんだぜ☆！");
                        isTimeoutShutdown = isTimeoutShutdown;
                        goto gt_EndMethod;//全体ループを抜けます。
                    }
                }

            }
            catch (Exception ex)
            {
                // エラーが起こりました。
                //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                // どうにもできないので  ログだけ取って無視します。
                Logger.Fatal($"(^ー^)「大外枠でキャッチ」{ex}");
                // TODO Playing.Send("bestmove resign");
            }

        gt_EndMethod:
            ;

            programSupport.AtEnd();
        }
    }
}
