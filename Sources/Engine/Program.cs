﻿namespace Grayscale.Kifuwarakaku.Engine
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text.RegularExpressions;
    using Grayscale.Kifuwarakaku.Engine.Features;
    using Grayscale.Kifuwarakaku.Entities.Features;
    using Grayscale.Kifuwarakaku.Entities.Logging;
    using Grayscale.Kifuwarakaku.UseCases;
    using Grayscale.Kifuwarakaku.UseCases.Features;
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

                    // USIループ（１つ目）
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
                            //------------------------------------------------------------
                            // それでは定刻になりましたので……
                            //------------------------------------------------------------
                            //
                            // 図.
                            //
                            //      log.txt
                            //      ┌────────────────────────────────────────
                            //      ～
                            //      │2014/08/02 1:31:35> isready
                            //      │
                            //
                            //
                            // 対局開始前に、将棋所から送られてくる文字が isready です。


                            //------------------------------------------------------------
                            // 将棋エンジン「おっおっ、設定を終わらせておかなければ（汗、汗…）」
                            //------------------------------------------------------------
#if DEBUG
            Logger.EngineDefault.Logger.WriteLineAddMemo("┏━━━━━設定━━━━━┓");
            foreach (KeyValuePair<string, string> pair in this.Owner.SetoptionDictionary)
            {
                // ここで将棋エンジンの設定を済ませておいてください。
                Logger.EngineDefault.Logger.WriteLineAddMemo(pair.Key + "=" + pair.Value);
            }
            Logger.EngineDefault.Logger.WriteLineAddMemo("┗━━━━━━━━━━━━┛");
#endif

                            //------------------------------------------------------------
                            // よろしくお願いします(^▽^)！
                            //------------------------------------------------------------
                            //
                            // 図.
                            //
                            //      log.txt
                            //      ┌────────────────────────────────────────
                            //      ～
                            //      │2014/08/02 2:03:33< readyok
                            //      │
                            //
                            //
                            // いつでも対局する準備が整っていましたら、 readyok を送り返します。
                            Playing.Send("readyok");
                        }
                        else if ("usinewgame" == line)
                        {
                            //------------------------------------------------------------
                            // 対局時計が ポチッ とされました
                            //------------------------------------------------------------
                            //
                            // 図.
                            //
                            //      log.txt
                            //      ┌────────────────────────────────────────
                            //      ～
                            //      │2014/08/02 2:03:33> usinewgame
                            //      │
                            //
                            //
                            // 対局が始まったときに送られてくる文字が usinewgame です。


                            // 無限ループ（１つ目）を抜けます。無限ループ（２つ目）に進みます。
                            result_UsiLoop1 = PhaseResult_UsiLoop1.Break;
                        }
                        else if ("quit" == line)
                        {
                            //------------------------------------------------------------
                            // おつかれさまでした
                            //------------------------------------------------------------
                            //
                            // 図.
                            //
                            //      log.txt
                            //      ┌────────────────────────────────────────
                            //      ～
                            //      │2014/08/02 1:31:38> quit
                            //      │
                            //
                            //
                            // 将棋エンジンを止めるときに送られてくる文字が quit です。


                            //------------------------------------------------------------
                            // ﾉｼ
                            //------------------------------------------------------------
                            //
                            // 図.
                            //
                            //      log.txt
                            //      ┌────────────────────────────────────────
                            //      ～
                            //      │2014/08/02 3:08:34> (^-^)ﾉｼ
                            //      │
                            //
                            //
#if DEBUG
            Logger.EngineDefault.Logger.WriteLineAddMemo("(^-^)ﾉｼ");
#endif


                            // このプログラムを終了します。
                            result_UsiLoop1 = PhaseResult_UsiLoop1.Quit;
                        }
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

                    programSupport.shogisasi.OnTaikyokuKaisi();//対局開始時の処理。

                    isTimeoutShutdown = false;
                    //PerformanceMetrics performanceMetrics = new PerformanceMetrics();//使ってない？

#if NOOPABLE
            // サーバーに noop を送ってもよい場合だけ有効にします。
            NoopTimerImpl noopTimer = null;
            if(this.owner.Option_enable_serverNoopable)
            {
                noopTimer = new NoopTimerImpl();
                noopTimer._01_BeforeLoop();
            }
#endif

                    while (true)
                    {

                        PhaseResult_UsiLoop2 result_UsiLoop2 = PhaseResult_UsiLoop2.None;

                        //ノンストップ版
                        //string line = TimeoutReader.ReadLine(1000);//指定ミリ秒だけブロック。  (2020-12-13 sun) そのあと抜ける。頼んで作ってもらった関数、入力を取りこぼす不具合がある☆（＾～＾）？

                        //通常版
                        string line = System.Console.In.ReadLine();

                        if (null == line)//次の行が無ければヌル。
                        {
                            // メッセージは届いていませんでした。
                            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

#if NOOPABLE
                    if (this.owner.Option_enable_serverNoopable)
                    {
                        bool isTimeoutShutdown_temp;
                        noopTimer._02_AtEmptyMessage(this.owner, out isTimeoutShutdown_temp,logTag);
                        if (isTimeoutShutdown_temp)
                        {
                            //MessageBox.Show("ループ２でタイムアウトだぜ☆！");
                            out_isTimeoutShutdown = isTimeoutShutdown_temp;
                            result_UsiLoop2 = PhaseResult_UsiLoop2.TimeoutShutdown;
                            goto end_loop2;
                        }
                    }
#endif

                            goto gt_NextLine_loop2;
                        }

                        // 通信ログは必ず取ります。
                        Logger.WriteLineC(line);

#if NOOPABLE
                if (this.owner.Option_enable_serverNoopable)
                {
                    noopTimer._03_AtResponsed(this.owner, line);
                }
#endif


                        if (line.StartsWith("position"))
                        {
                            //------------------------------------------------------------
                            // これが棋譜です
                            //------------------------------------------------------------
                            //
                            // 図.
                            //
                            //      log.txt
                            //      ┌────────────────────────────────────────
                            //      ～
                            //      │2014/08/02 2:03:35> position startpos moves 2g2f
                            //      │
                            //
                            // ↑↓この将棋エンジンは後手で、平手初期局面から、先手が初手  ▲２六歩  を指されたことが分かります。
                            //
                            //        ９  ８  ７  ６  ５  ４  ３  ２  １                 ９  ８  ７  ６  ５  ４  ３  ２  １
                            //      ┌─┬─┬─┬─┬─┬─┬─┬─┬─┐             ┌─┬─┬─┬─┬─┬─┬─┬─┬─┐
                            //      │香│桂│銀│金│玉│金│銀│桂│香│一           │ｌ│ｎ│ｓ│ｇ│ｋ│ｇ│ｓ│ｎ│ｌ│ａ
                            //      ├─┼─┼─┼─┼─┼─┼─┼─┼─┤             ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
                            //      │  │飛│  │  │  │  │  │角│  │二           │  │ｒ│  │  │  │  │  │ｂ│  │ｂ
                            //      ├─┼─┼─┼─┼─┼─┼─┼─┼─┤             ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
                            //      │歩│歩│歩│歩│歩│歩│歩│歩│歩│三           │ｐ│ｐ│ｐ│ｐ│ｐ│ｐ│ｐ│ｐ│ｐ│ｃ
                            //      ├─┼─┼─┼─┼─┼─┼─┼─┼─┤             ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
                            //      │  │  │  │  │  │  │  │  │  │四           │  │  │  │  │  │  │  │  │  │ｄ
                            //      ├─┼─┼─┼─┼─┼─┼─┼─┼─┤             ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
                            //      │  │  │  │  │  │  │  │  │  │五           │  │  │  │  │  │  │  │  │  │ｅ
                            //      ├─┼─┼─┼─┼─┼─┼─┼─┼─┤             ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
                            //      │  │  │  │  │  │  │  │歩│  │六           │  │  │  │  │  │  │  │Ｐ│  │ｆ
                            //      ├─┼─┼─┼─┼─┼─┼─┼─┼─┤             ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
                            //      │歩│歩│歩│歩│歩│歩│歩│  │歩│七           │Ｐ│Ｐ│Ｐ│Ｐ│Ｐ│Ｐ│Ｐ│  │Ｐ│ｇ
                            //      ├─┼─┼─┼─┼─┼─┼─┼─┼─┤             ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
                            //      │  │角│  │  │  │  │  │飛│  │八           │  │Ｂ│  │  │  │  │  │Ｒ│  │ｈ
                            //      ├─┼─┼─┼─┼─┼─┼─┼─┼─┤             ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
                            //      │香│桂│銀│金│玉│金│銀│桂│香│九           │Ｌ│Ｎ│Ｓ│Ｇ│Ｋ│Ｇ│Ｓ│Ｎ│Ｌ│ｉ
                            //      └─┴─┴─┴─┴─┴─┴─┴─┴─┘             └─┴─┴─┴─┴─┴─┴─┴─┴─┘
                            //
                            // または
                            //
                            //      log.txt
                            //      ┌────────────────────────────────────────
                            //      ～
                            //      │2014/08/02 2:03:35> position sfen lnsgkgsnl/9/ppppppppp/9/9/9/PPPPPPPPP/1B5R1/LNSGKGSNL w - 1 moves 5a6b 7g7f 3a3b
                            //      │
                            //
                            // ↑↓将棋所のサンプルによると、“２枚落ち初期局面から△６二玉、▲７六歩、△３二銀と進んだ局面”とのことです。
                            //
                            //                                           ＜初期局面＞    ９  ８  ７  ６  ５  ４  ３  ２  １
                            //                                                         ┌─┬─┬─┬─┬─┬─┬─┬─┬─┐
                            //                                                         │ｌ│ｎ│ｓ│ｇ│ｋ│ｇ│ｓ│ｎ│ｌ│ａ  ←lnsgkgsnl
                            //                                                         ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
                            //                                                         │  │  │  │  │  │  │  │  │  │ｂ  ←9
                            //                                                         ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
                            //                                                         │ｐ│ｐ│ｐ│ｐ│ｐ│ｐ│ｐ│ｐ│ｐ│ｃ  ←ppppppppp
                            //                                                         ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
                            //                                                         │  │  │  │  │  │  │  │  │  │ｄ  ←9
                            //                                                         ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
                            //                                                         │  │  │  │  │  │  │  │  │  │ｅ  ←9
                            //                                                         ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
                            //                                                         │  │  │  │  │  │  │  │  │  │ｆ  ←9
                            //                                                         ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
                            //                                                         │Ｐ│Ｐ│Ｐ│Ｐ│Ｐ│Ｐ│Ｐ│Ｐ│Ｐ│ｇ  ←PPPPPPPPP
                            //                                                         ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
                            //                                                         │  │Ｂ│  │  │  │  │  │Ｒ│  │ｈ  ←1B5R1
                            //                                                         ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
                            //                                                         │Ｌ│Ｎ│Ｓ│Ｇ│Ｋ│Ｇ│Ｓ│Ｎ│Ｌ│ｉ  ←LNSGKGSNL
                            //                                                         └─┴─┴─┴─┴─┴─┴─┴─┴─┘
                            //
                            //        ９  ８  ７  ６  ５  ４  ３  ２  １   ＜３手目＞    ９  ８  ７  ６  ５  ４  ３  ２  １
                            //      ┌─┬─┬─┬─┬─┬─┬─┬─┬─┐             ┌─┬─┬─┬─┬─┬─┬─┬─┬─┐
                            //      │香│桂│銀│金│  │金│  │桂│香│一           │ｌ│ｎ│ｓ│ｇ│  │ｇ│  │ｎ│ｌ│ａ
                            //      ├─┼─┼─┼─┼─┼─┼─┼─┼─┤             ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
                            //      │  │  │  │玉│  │  │銀│  │  │二           │  │  │  │ｋ│  │  │ｓ│  │  │ｂ
                            //      ├─┼─┼─┼─┼─┼─┼─┼─┼─┤             ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
                            //      │歩│歩│歩│歩│歩│歩│歩│歩│歩│三           │ｐ│ｐ│ｐ│ｐ│ｐ│ｐ│ｐ│ｐ│ｐ│ｃ
                            //      ├─┼─┼─┼─┼─┼─┼─┼─┼─┤             ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
                            //      │  │  │  │  │  │  │  │  │  │四           │  │  │  │  │  │  │  │  │  │ｄ
                            //      ├─┼─┼─┼─┼─┼─┼─┼─┼─┤             ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
                            //      │  │  │  │  │  │  │  │  │  │五           │  │  │  │  │  │  │  │  │  │ｅ
                            //      ├─┼─┼─┼─┼─┼─┼─┼─┼─┤             ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
                            //      │  │  │歩│  │  │  │  │  │  │六           │  │  │Ｐ│  │  │  │  │  │  │ｆ
                            //      ├─┼─┼─┼─┼─┼─┼─┼─┼─┤             ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
                            //      │歩│歩│  │歩│歩│歩│歩│歩│歩│七           │Ｐ│Ｐ│  │Ｐ│Ｐ│Ｐ│Ｐ│Ｐ│Ｐ│ｇ
                            //      ├─┼─┼─┼─┼─┼─┼─┼─┼─┤             ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
                            //      │  │角│  │  │  │  │  │飛│  │八           │  │Ｂ│  │  │  │  │  │Ｒ│  │ｈ
                            //      ├─┼─┼─┼─┼─┼─┼─┼─┼─┤             ├─┼─┼─┼─┼─┼─┼─┼─┼─┤
                            //      │香│桂│銀│金│玉│金│銀│桂│香│九           │Ｌ│Ｎ│Ｓ│Ｇ│Ｋ│Ｇ│Ｓ│Ｎ│Ｌ│ｉ
                            //      └─┴─┴─┴─┴─┴─┴─┴─┴─┘             └─┴─┴─┴─┴─┴─┴─┴─┴─┘
                            //

                            // 手番になったときに、“まず”、将棋所から送られてくる文字が position です。
                            // このメッセージを読むと、駒の配置が分かります。
                            //
                            // “が”、まだ指してはいけません。
#if DEBUG
                this.Log1("（＾△＾）positionきたｺﾚ！");
#endif
                            // 入力行を解析します。
                            KifuParserA_Result result = new KifuParserA_ResultImpl();
                            KifuParserA_Impl kifuParserA = new KifuParserA_Impl();
                            Model_Taikyoku model_Taikyoku = new Model_TaikyokuImpl(usiLoop2.Kifu);//FIXME:  この棋譜を委譲メソッドで修正するはず。 ShogiGui_Warabeは？
                            KifuParserA_Genjo genjo = new KifuParserA_GenjoImpl(line);
                            kifuParserA.Execute_All(
                                ref result,
                                model_Taikyoku,
                                genjo
                                );
                            if (null != genjo.StartposImporter_OrNull)
                            {
                                // SFENの解析結果を渡すので、
                                // その解析結果をどう使うかは、委譲します。
                                Util_InClient.OnChangeSky_Im_Client(
                                    model_Taikyoku,
                                    genjo
                                    );
                            }


#if DEBUG
                this.Log2_Png_Tyokkin(line, (KifuNode)result.Out_newNode_OrNull);
#endif

                            //------------------------------------------------------------
                            // じっとがまん
                            //------------------------------------------------------------
                            //
                            // 応答は無用です。
                            // 多分、将棋所もまだ準備ができていないのではないでしょうか（？）
                            //
                        }
                        else if (line.StartsWith("go ponder"))
                        {
                            //------------------------------------------------------------
                            // 将棋所が次に呼びかけるまで、考えていてください
                            //------------------------------------------------------------
                            //
                            // 図.
                            //
                            //      log.txt
                            //      ┌────────────────────────────────────────
                            //      ～
                            //      │2014/08/02 2:03:35> go ponder
                            //      │
                            //

                            // 先読み用です。
                            // 今回のプログラムでは対応しません。
                            //
                            // 将棋エンジンが  将棋所に向かって  「bestmove ★ ponder ★」といったメッセージを送ったとき、
                            // 将棋所は「go ponder」というメッセージを返してくると思います。
                            //
                            // 恐らく  このメッセージを受け取っても、将棋エンジンは気にせず  考え続けていればいいのではないでしょうか。


                            //------------------------------------------------------------
                            // じっとがまん
                            //------------------------------------------------------------
                            //
                            // まだ指してはいけません。
                            // 指したら反則です。相手はまだ指していないのだ☆ｗ
                            //
                        }
                        // 「go ponder」「go mate」「go infinite」とは区別します。
                        else if (line.StartsWith("go"))
                        {
                            //------------------------------------------------------------
                            // あなたの手番です
                            //------------------------------------------------------------
                            //
                            // 図.
                            //
                            //      log.txt
                            //      ┌────────────────────────────────────────
                            //      ～
                            //      │2014/08/02 2:36:19> go btime 599000 wtime 600000 byoyomi 60000
                            //      │
                            //
                            // もう指していいときに、将棋所から送られてくる文字が go です。
                            //


                            //------------------------------------------------------------
                            // 先手 3:00  後手 0:00  記録係「50秒ぉ～」
                            //------------------------------------------------------------
                            //
                            // 上図のメッセージのままだと使いにくいので、
                            // あとで使いやすいように Key と Value の表に分けて持ち直します。
                            //
                            // 図.
                            //
                            //      goDictionary
                            //      ┌──────┬──────┐
                            //      │Key         │Value       │
                            //      ┝━━━━━━┿━━━━━━┥
                            //      │btime       │599000      │
                            //      ├──────┼──────┤
                            //      │wtime       │600000      │
                            //      ├──────┼──────┤
                            //      │byoyomi     │60000       │
                            //      └──────┴──────┘
                            //      単位はミリ秒ですので、599000 は 59.9秒 です。
                            //
                            Regex regex = new Regex(@"go btime (\d+) wtime (\d+) byoyomi (\d+)", RegexOptions.Singleline);
                            Match m = regex.Match(line);

                            if (m.Success)
                            {
                                usiLoop2.GoProperties["btime"] = (string)m.Groups[1].Value;
                                usiLoop2.GoProperties["wtime"] = (string)m.Groups[2].Value;
                                usiLoop2.GoProperties["byoyomi"] = (string)m.Groups[3].Value;
                            }
                            else
                            {
                                usiLoop2.GoProperties["btime"] = "";
                                usiLoop2.GoProperties["wtime"] = "";
                                usiLoop2.GoProperties["byoyomi"] = "";
                            }



                            //----------------------------------------
                            // 棋譜ツリー、局面データは、position コマンドで先に与えられているものとします。
                            //----------------------------------------

                            // ┏━━━━プログラム━━━━┓

                            int latestTemezumi = usiLoop2.Kifu.CurNode.Value.KyokumenConst.Temezumi;//現・手目済

                            //#if DEBUG
                            // MessageBox.Show("["+latestTemezumi+"]手目済　["+this.owner.PlayerInfo.Playerside+"]の手番");
                            //#endif

                            SkyConst src_Sky = usiLoop2.Kifu.NodeAt(latestTemezumi).Value.KyokumenConst;//現局面

                            //logTag.Logger.WriteLineAddMemo("将棋サーバー「" + latestTemezumi + "手目、きふわらべ　さんの手番ですよ！」　" + line);


                            //----------------------------------------
                            // 王の状態を調べます。
                            //----------------------------------------
                            Result_KingState result_kingState;
                            {
                                result_kingState = Result_KingState.Empty;

                                RO_Star king1p = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(Finger_Honshogi.SenteOh).Now);
                                RO_Star king2p = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(Finger_Honshogi.GoteOh).Now);
                                //OwataMinister.WARABE_ENGINE.Logger.WriteLineAddMemo("将棋サーバー「ではここで、王さまがどこにいるか確認してみましょう」");
                                //OwataMinister.WARABE_ENGINE.Logger.WriteLineAddMemo("▲王の置き場＝" + Conv_SyElement.Masu_ToOkiba(koma1.Masu));
                                //OwataMinister.WARABE_ENGINE.Logger.WriteLineAddMemo("△王の置き場＝" + Conv_SyElement.Masu_ToOkiba(koma2.Masu));

                                if (Conv_SyElement.ToOkiba(king1p.Masu) != Okiba.ShogiBan)
                                {
                                    // 先手の王さまが将棋盤上にいないとき☆
                                    result_kingState = Result_KingState.Lost_SenteOh;
                                }
                                else if (Conv_SyElement.ToOkiba(king2p.Masu) != Okiba.ShogiBan)
                                {
                                    // または、後手の王さまが将棋盤上にいないとき☆
                                    result_kingState = Result_KingState.Lost_GoteOh;
                                }
                                else
                                {
                                    result_kingState = Result_KingState.Empty;
                                }
                            }

                            //------------------------------------------------------------
                            // わたしの手番のとき、王様が　将棋盤上からいなくなっていれば、投了します。
                            //------------------------------------------------------------
                            //
                            //      将棋ＧＵＩ『きふならべ』用☆　将棋盤上に王さまがいないときに、本将棋で　go　コマンドが送られてくることは無いのでは☆？
                            //
                            switch (result_kingState)
                            {
                                case Result_KingState.Lost_SenteOh:// 先手の王さまが将棋盤上にいないとき☆
                                case Result_KingState.Lost_GoteOh:// または、後手の王さまが将棋盤上にいないとき☆
                                    {
                                        //------------------------------------------------------------
                                        // 投了
                                        //------------------------------------------------------------
                                        //
                                        // 図.
                                        //
                                        //      log.txt
                                        //      ┌────────────────────────────────────────
                                        //      ～
                                        //      │2014/08/02 2:36:21< bestmove resign
                                        //      │
                                        //

                                        // この将棋エンジンは、後手とします。
                                        // ２０手目、投了  を決め打ちで返します。
                                        Playing.Send("bestmove resign");//投了
                                    }
                                    break;
                                default:// どちらの王さまも、まだまだ健在だぜ☆！
                                    {
                                        List<KifuNode> bestKifuNodeList = new List<KifuNode>();

                                        //------------------------------------------------------------
                                        // 指し手のチョイス
                                        //------------------------------------------------------------
                                        bool isHonshogi = true;



                                        //------------------------------------------------------------
                                        // MultiPV のテスト中☆
                                        //------------------------------------------------------------
                                        //
                                        // 指し手を決めます。
                                        // TODO: その指し手の評価値がいくらだったのか調べたい。
                                        //
                                        // FIXME: ログがＭｕｌｔｉＰＶ別になっていないので、混ざって、同じ手を２度指しているみたいに見えてしまう☆
                                        //
                                        int multiPV_Count = 1;// 2;
                                        {
                                            // 最善手、次善手、三次善手、四次善手、五次善手
                                            for (int iMultiPV = 0; iMultiPV < multiPV_Count; iMultiPV++)
                                            {
                                                bestKifuNodeList.Add(usiLoop2.shogisasi.WA_Bestmove(
                                                    isHonshogi,
                                                    usiLoop2.Kifu)
                                                    );
                                            }


#if DEBUG
                                //// 内容をログ出力
                                //// 最善手、次善手、三次善手、四次善手、五次善手
                                //StringBuilder sb = new StringBuilder();
                                //for (int iMultiPV = 0; iMultiPV < 5; iMultiPV++)
                                //{
                                //    string sfenText = Util_Sky.ToSfenMoveText(bestMoveList[iMultiPV]);
                                //    sb.AppendLine("[" + iMultiPV + "]" + sfenText);
                                //}
                                //System.Windows.Forms.MessageBox.Show(sb.ToString());
#endif
                                        }

                                        KifuNode bestKifuNode = null;
                                        // 最善手、次善手、三次善手、四次善手、五次善手
                                        float bestScore = float.MinValue;
                                        for (int iMultiPV = 0; iMultiPV < bestKifuNodeList.Count; iMultiPV++)
                                        {
                                            KifuNode node = bestKifuNodeList[iMultiPV];

                                            if (null != node && null != node.KyHyokaSheet_Mutable && bestScore <= node.Score)
                                            {
                                                bestScore = node.Score;
                                                bestKifuNode = node;
                                            }
                                        }

                                        IMove bestMove2;
                                        if (null == bestKifuNode)
                                        {
                                            // 投了
                                            bestMove2 = Util_Sky258A.NullObjectMove;
                                        }
                                        else
                                        {
                                            bestMove2 = bestKifuNode.Key;
                                        }

                                        if (Util_Sky_BoolQuery.isEnableSfen(bestMove2))
                                        {
                                            string sfenText = ConvMoveStrSfen.ToMoveStrSfen(bestMove2);

                                            // ログが重過ぎる☆！
                                            //OwataMinister.WARABE_ENGINE.Logger.WriteLineAddMemo("(Warabe)指し手のチョイス： bestmove＝[" + sfenText + "]" +
                                            //    "　棋譜＝" + KirokuGakari.ToJsaKifuText(this.Kifu));

                                            //----------------------------------------
                                            // スコア 試し
                                            //----------------------------------------
                                            {
                                                //int hyojiScore = (int)(bestScore / 100.0d);//FIXME:適当に調整した。
                                                int hyojiScore = (int)bestScore;
                                                if (usiLoop2.Kifu.CurNode.Value.KyokumenConst.KaisiPside == Playerside.P2)
                                                {
                                                    // 符号を逆転
                                                    hyojiScore = -hyojiScore;
                                                }
                                                Playing.Send("info time 1 depth 1 nodes 1 score cp " + hyojiScore.ToString() + " pv ");//FIXME:
                                                                                                                                       //+ " pv 3a3b L*4h 4c4d"
                                            }


                                            //----------------------------------------
                                            // 指し手を送ります。
                                            //----------------------------------------
                                            Playing.Send("bestmove " + sfenText);
                                        }
                                        else // 指し手がないときは、SFENが書けない☆　投了だぜ☆
                                        {
                                            // ログが重過ぎる☆！
                                            //OwataMinister.WARABE_ENGINE.Logger.WriteLineAddMemo("(Warabe)指し手のチョイス： 指し手がないときは、SFENが書けない☆　投了だぜ☆ｗｗ（＞＿＜）" +
                                            //    "　棋譜＝" + KirokuGakari.ToJsaKifuText(this.Kifu));

                                            //----------------------------------------
                                            // 投了ｗ！
                                            //----------------------------------------
                                            Playing.Send("bestmove resign");
                                        }

                                        //------------------------------------------------------------
                                        // 以前の手カッター
                                        //------------------------------------------------------------
                                        UtilKifuTree282.IzennoHenkaCutter(usiLoop2.Kifu);
                                    }
                                    break;
                            }
                            // ┗━━━━プログラム━━━━┛

                            //System.C onsole.WriteLine();

                            //throw new Exception("デバッグだぜ☆！　エラーはキャッチできたかな～☆？（＾▽＾）");
                        }
                        else if (line.StartsWith("stop"))
                        {
                            //------------------------------------------------------------
                            // あなたの手番です  （すぐ指してください！）
                            //------------------------------------------------------------
                            //
                            // 図.
                            //
                            //      log.txt
                            //      ┌────────────────────────────────────────
                            //      ～
                            //      │2014/08/02 2:03:35> stop
                            //      │
                            //

                            // 何らかの理由で  すぐ指してほしいときに、将棋所から送られてくる文字が stop です。
                            //
                            // 理由は２つ考えることができます。
                            //  （１）１手前に、将棋エンジンが  将棋所に向かって「予想手」付きで指し手を伝えたのだが、
                            //        相手の応手が「予想手」とは違ったので、予想手にもとづく思考を  今すぐ変えて欲しいとき。
                            //
                            //  （２）「急いで指すボタン」が押されたときなどに送られてくるようです？
                            //
                            // stop するのは思考です。  stop を受け取ったら  すぐに最善手を指してください。

                            if (usiLoop2.GoPonderNow)
                            {
                                //------------------------------------------------------------
                                // 将棋エンジン「（予想手が間違っていたって？）  △９二香 を指そうと思っていたんだが」
                                //------------------------------------------------------------
                                //
                                // 図.
                                //
                                //      log.txt
                                //      ┌────────────────────────────────────────
                                //      ～
                                //      │2014/08/02 2:36:21< bestmove 9a9b
                                //      │
                                //
                                //
                                //      １手前の指し手で、将棋エンジンが「bestmove ★ ponder ★」という形で  予想手付きで将棋所にメッセージを送っていたとき、
                                //      その予想手が外れていたならば、将棋所は「stop」を返してきます。
                                //      このとき  思考を打ち切って最善手の指し手をすぐに返信するわけですが、将棋所はこの返信を無視します☆ｗ
                                //      （この指し手は、外れていた予想手について考えていた“最善手”ですからゴミのように捨てられます）
                                //      その後、将棋所から「position」「go」が再送されてくるのだと思います。
                                //
                                //          将棋エンジン「bestmove ★ ponder ★」
                                //              ↓
                                //          将棋所      「stop」
                                //              ↓
                                //          将棋エンジン「うその指し手返信」（無視されます）←今ここ
                                //              ↓
                                //          将棋所      「position」「go」
                                //              ↓
                                //          将棋エンジン「本当の指し手」
                                //
                                //      という流れと思います。
                                // この指し手は、無視されます。（無視されますが、送る必要があります）
                                Playing.Send("bestmove 9a9b");
                            }
                            else
                            {
                                //------------------------------------------------------------
                                // じゃあ、△９二香で
                                //------------------------------------------------------------
                                //
                                // 図.
                                //
                                //      log.txt
                                //      ┌────────────────────────────────────────
                                //      ～
                                //      │2014/08/02 2:36:21< bestmove 9a9b
                                //      │
                                //
                                //
                                // 特に何もなく、すぐ指せというのですから、今考えている最善手をすぐに指します。
                                Playing.Send("bestmove 9a9b");
                            }
                        }
                        else if (line.StartsWith("gameover")) { usiLoop2.AtLoop_OnGameover(line, ref result_UsiLoop2); }
                        else if ("logdase" == line) { usiLoop2.AtLoop_OnLogdase(line, ref result_UsiLoop2); }
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

                        switch (result_UsiLoop2)
                        {
                            case PhaseResult_UsiLoop2.Break:
                                goto end_loop2;

                            default:
                                break;
                        }

                    gt_NextLine_loop2:
                        ;
                    }

                end_loop2:
                    ;

                    //-------------------+----------------------------------------------------------------------------------------------------
                    // スナップショット  |
                    //-------------------+----------------------------------------------------------------------------------------------------
                    // 対局後のタイミングで、データの中身を確認しておきます。
                    // Key と Value の表の形をしています。（順不同）
                    //
                    // 図.
                    //      ※内容はサンプルです。実際と異なる場合があります。
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
                    //      goDictionary
                    //      ┌──────┬──────┐
                    //      │Key         │Value       │
                    //      ┝━━━━━━┿━━━━━━┥
                    //      │btime       │599000      │
                    //      ├──────┼──────┤
                    //      │wtime       │600000      │
                    //      ├──────┼──────┤
                    //      │byoyomi     │60000       │
                    //      └──────┴──────┘
                    //
                    //      goMateDictionary
                    //      ┌──────┬──────┐
                    //      │Key         │Value       │
                    //      ┝━━━━━━┿━━━━━━┥
                    //      │mate        │599000      │
                    //      └──────┴──────┘
                    //
                    //      gameoverDictionary
                    //      ┌──────┬──────┐
                    //      │Key         │Value       │
                    //      ┝━━━━━━┿━━━━━━┥
                    //      │gameover    │lose        │
                    //      └──────┴──────┘
                    //
#if DEBUG
            Logger.EngineDefault.Logger.WriteLineAddMemo("KifuParserA_Impl.LOGGING_BY_ENGINE, ┏━確認━━━━setoptionDictionary ━┓");
            foreach (KeyValuePair<string, string> pair in this.owner.SetoptionDictionary)
            {
                Logger.EngineDefault.Logger.WriteLineAddMemo(pair.Key + "=" + pair.Value);
            }
            Logger.EngineDefault.Logger.WriteLineAddMemo("┗━━━━━━━━━━━━━━━━━━┛");
            Logger.EngineDefault.Logger.WriteLineAddMemo("┏━確認━━━━goDictionary━━━━━┓");
            foreach (KeyValuePair<string, string> pair in this.GoProperties)
            {
                Logger.EngineDefault.Logger.WriteLineAddMemo(pair.Key + "=" + pair.Value);
            }

            //Dictionary<string, string> goMateProperties = new Dictionary<string, string>();
            //goMateProperties["mate"] = "";
            //LarabeLoggerList_Warabe.ENGINE.WriteLineAddMemo("┗━━━━━━━━━━━━━━━━━━┛");
            //LarabeLoggerList_Warabe.ENGINE.WriteLineAddMemo("┏━確認━━━━goMateDictionary━━━┓");
            //foreach (KeyValuePair<string, string> pair in this.goMateProperties)
            //{
            //    LarabeLoggerList_Warabe.ENGINE.WriteLineAddMemo(pair.Key + "=" + pair.Value);
            //}

            Logger.EngineDefault.Logger.WriteLineAddMemo("┗━━━━━━━━━━━━━━━━━━┛");
            Logger.EngineDefault.Logger.WriteLineAddMemo("┏━確認━━━━gameoverDictionary━━┓");
            foreach (KeyValuePair<string, string> pair in this.GameoverProperties)
            {
                Logger.EngineDefault.Logger.WriteLineAddMemo(pair.Key + "=" + pair.Value);
            }
            Logger.EngineDefault.Logger.WriteLineAddMemo("┗━━━━━━━━━━━━━━━━━━┛");
#endif

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
