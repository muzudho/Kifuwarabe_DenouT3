namespace Grayscale.Kifuwarakaku.Engine
{
#if DEBUG
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using Grayscale.Kifuwarakaku.Engine.Features;
    using Grayscale.Kifuwarakaku.Entities.Features;
    using Grayscale.Kifuwarakaku.Entities.Logging;
    using Grayscale.Kifuwarakaku.UseCases;
    using Grayscale.Kifuwarakaku.UseCases.Features;
    using Nett;
    using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号
#else
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using Grayscale.Kifuwarakaku.Engine.Features;
    using Grayscale.Kifuwarakaku.Entities.Features;
    using Grayscale.Kifuwarakaku.Entities.Logging;
    using Grayscale.Kifuwarakaku.UseCases;
    using Grayscale.Kifuwarakaku.UseCases.Features;
    using Nett;
    using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号
#endif

    /// <summary>
    /// プログラムのエントリー・ポイントです。
    /// </summary>
    class Program
    {
        static readonly Regex regexOfGoFisherClock = new Regex(@"go btime (\d+) wtime (\d+) binc (\d+) winc (\d+)", RegexOptions.Singleline | RegexOptions.Compiled);
        static readonly Regex regexOfSetoption = new Regex(@"setoption name ([^ ]+)(?: value (.*))?", RegexOptions.Singleline | RegexOptions.Compiled);
        static readonly Regex regexOfGo = new Regex(@"go btime (\d+) wtime (\d+) byoyomi (\d+)", RegexOptions.Singleline | RegexOptions.Compiled);
        static readonly Regex regexOfGameover = new Regex(@"gameover (.)", RegexOptions.Singleline | RegexOptions.Compiled);

        /// <summary>
        /// Ｃ＃のプログラムは、
        /// この Main 関数から始まり、 Main 関数を抜けて終わります。
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // 将棋エンジン　きふわらべ
            var playing = new Playing();

            var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
            var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));

            //-------------------+----------------------------------------------------------------------------------------------------
            // ログファイル削除  |
            //-------------------+----------------------------------------------------------------------------------------------------
            {
                //
                // 図.
                //
                //      フォルダー
                //          ├─ Engine.KifuWarabe.exe
                //          └─ log.txt               ←これを削除
                //
                Logger.RemoveAllLogFiles();
            }

            //------------------------------------------------------------------------------------------------------------------------
            // 思考エンジンの、記憶を読み取ります。
            //------------------------------------------------------------------------------------------------------------------------
            Util_FvLoad.OpenFv(playing.FeatureVector, Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("Fv00Komawari")));

            //------------------------------------------------------------------------------------------------------------------------
            // ファイル読込み
            //------------------------------------------------------------------------------------------------------------------------
            {
                // データの読取「道」
                if (Michi187Array.Load(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("Michi187"))))
                {
                }

                // データの読取「配役」
                Util_Array_KomahaiyakuEx184.Load(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("Haiyaku185")), Encoding.UTF8);

                // データの読取「強制転成表」　※駒配役を生成した後で。
                Array_ForcePromotion.Load(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("InputForcePromotion")), Encoding.UTF8);

#if DEBUG
                {
                    File.WriteAllText(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("OutputForcePromotion")), Array_ForcePromotion.LogHtml());
                }
#endif

                // データの読取「配役転換表」
                Data_KomahaiyakuTransition.Load(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("InputSyuruiToHaiyaku")), Encoding.UTF8);

#if DEBUG
                {
                    File.WriteAllText(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("OutputSyuruiToHaiyaku")), Data_KomahaiyakuTransition.Format_LogHtml());
                }
#endif
            }

            //-------------+----------------------------------------------------------------------------------------------------------
            // ログ書込み  |  ＜この将棋エンジン＞  製品名、バージョン番号
            //-------------+----------------------------------------------------------------------------------------------------------
            //
            // 図.
            //
            //      log.txt
            //      ┌────────────────────────────────────────
            //      │2014/08/02 1:04:59> v(^▽^)v ｲｪｰｲ☆ ... fugafuga 1.00.0
            //      │
            //      │
            //
            //
            // 製品名とバージョン番号は、次のファイルに書かれているものを使っています。
            // 場所：  [ソリューション エクスプローラー]-[ソリューション名]-[プロジェクト名]-[Properties]-[AssemblyInfo.cs] の中の、[AssemblyProduct]と[AssemblyVersion] を参照。
            //
            // バージョン番号を「1.00.0」形式（メジャー番号.マイナー番号.ビルド番号)で書くのは作者の趣味です。
            //
            {
                string versionStr;

                // バージョン番号
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                versionStr = String.Format("{0}.{1}.{2}", version.Major, version.Minor.ToString("00"), version.Build);

                //seihinName += $" {versionStr}";
#if DEBUG
                var engineName = toml.Get<TomlTable>("Engine").Get<string>("Name");
                Logger.Trace($"v(^▽^)v ｲｪｰｲ☆ ... {engineName} {versionStr}");
#endif
            }

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


                // ループ（全体）
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
                            var engineName = toml.Get<TomlTable>("Engine").Get<string>("Name");
                            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                            var engineAuthor = toml.Get<TomlTable>("Engine").Get<string>("Author");
                            playing.UsiOk($"{engineName} {version.Major}.{version.Minor}.{version.Build}", engineAuthor);
                        }
                        else if (line.StartsWith("setoption"))
                        {
                            Match m = regexOfSetoption.Match(line);

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

                                playing.SetOption(name, value);
                            }
                        }
                        else if ("isready" == line)
                        {
                            playing.ReadyOk();
                        }
                        else if ("usinewgame" == line)
                        {
                            playing.UsiNewGame();

                            // 無限ループ（１つ目）を抜けます。無限ループ（２つ目）に進みます。
                            goto end_loop1;
                        }
                        else if ("quit" == line)
                        {
                            playing.Quit();

                            // このプログラムを終了します。
                            goto gt_EndMethod;//全体ループを抜けます。
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

                    gt_NextTime1:
                        ;
                    }
                end_loop1:

                    if (isTimeoutShutdown)
                    {
                        //MessageBox.Show("ループ１で矯正終了するんだぜ☆！");
                        goto gt_EndMethod;
                    }

                    //
                    // USIループ（２つ目）
                    //

                    //
                    // 図.
                    //
                    //      この将棋エンジンが後手とします。
                    //
                    //      ┌──┬─────────────┬──────┬──────┬────────────────────────────────────┐
                    //      │順番│                          │計算        │temezumiCount │解説                                                                    │
                    //      ┝━━┿━━━━━━━━━━━━━┿━━━━━━┿━━━━━━┿━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━┥
                    //      │   1│初回                      │            │            │相手が先手、この将棋エンジンが後手とします。                            │
                    //      │    │                          │            │0           │もし、この将棋エンジンが先手なら、初回は temezumiCount = -1 とします。    │
                    //      ├──┼─────────────┼──────┼──────┼────────────────────────────────────┤
                    //      │   2│position                  │+-0         │            │                                                                        │
                    //      │    │    (相手が指しても、     │            │            │                                                                        │
                    //      │    │     指していないときでも │            │            │                                                                        │
                    //      │    │     送られてきます)      │            │0           │                                                                        │
                    //      ├──┼─────────────┼──────┼──────┼────────────────────────────────────┤
                    //      │   3│go                        │+2          │            │+2 します                                                               │
                    //      │    │    (相手が指した)        │            │2           │    ※「go」は、「go ponder」「go mate」「go infinite」とは区別します。 │
                    //      ├──┼─────────────┼──────┼──────┼────────────────────────────────────┤
                    //      │   4│go ponder                 │+-0         │            │                                                                        │
                    //      │    │    (相手はまだ指してない)│            │2           │                                                                        │
                    //      ├──┼─────────────┼──────┼──────┼────────────────────────────────────┤
                    //      │   5│自分が指した              │+-0         │            │相手が指してから +2 すると決めたので、                                  │
                    //      │    │                          │            │2           │自分が指したときにはカウントを変えません。                              │
                    //      └──┴─────────────┴──────┴──────┴────────────────────────────────────┘
                    //

                    // 棋譜
                    {
                        Debug.Assert(!Conv_MasuHandle.OnKomabukuro(
                            Conv_SyElement.ToMasuNumber(((RO_Star)playing.Game.Kifu.CurNode.Value.KyokumenConst.StarlightIndexOf((Finger)0).Now).Masu)
                            ), "駒が駒袋にあった。");
                    }

                    // go ponderの属性一覧
                    {
                        playing.Game.GoPonderNow = false;   // go ponderを将棋所に伝えたなら真
                    }

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

                        // ブロッキングIO。
                        string line = Console.In.ReadLine();

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
#if DEBUG
                            Logger.Trace("（＾△＾）positionきたｺﾚ！");
#endif
                            // 入力行を解析します。
                            KifuParserA_Result result = new KifuParserA_ResultImpl();
                            KifuParserA_Impl kifuParserA = new KifuParserA_Impl();
                            Model_Taikyoku model_Taikyoku = new Model_TaikyokuImpl(playing.Game.Kifu);//FIXME:  この棋譜を委譲メソッドで修正するはず。 ShogiGui_Warabeは？
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
                            Playing.Log2_Png_Tyokkin(line, (KifuNode)result.Out_newNode_OrNull);
#endif

                            //------------------------------------------------------------
                            // じっとがまん
                            //------------------------------------------------------------
                            //
                            // 応答は無用です。
                            // 多分、将棋所もまだ準備ができていないのではないでしょうか（？）
                            //
                            playing.Position();
                        }
                        else if (line.StartsWith("go ponder"))
                        {
                            playing.GoPonder();
                        }
                        // 「go ponder」「go mate」「go infinite」とは区別します。
                        else if (line.StartsWith("go"))
                        {
                            Match m = regexOfGo.Match(line);
                            if (m.Success)
                            {
                                playing.Go((string)m.Groups[1].Value, (string)m.Groups[2].Value, (string)m.Groups[3].Value, "", "");
                            }
                            else
                            {
                                // (2020-12-16 wed) フィッシャー・クロック・ルールに対応☆（＾～＾）
                                m = regexOfGoFisherClock.Match(line);

                                playing.Go((string)m.Groups[1].Value, (string)m.Groups[2].Value, "", (string)m.Groups[3].Value, (string)m.Groups[4].Value);
                            }
                        }
                        else if (line.StartsWith("stop"))
                        {
                            playing.Stop();
                        }
                        else if (line.StartsWith("gameover"))
                        {
                            Match m = regexOfGameover.Match(line);
                            if (m.Success)
                            {
                                playing.GameOver((string)m.Groups[1].Value);
                            }

                            // 無限ループ（２つ目）を抜けます。無限ループ（１つ目）に戻ります。
                            result_UsiLoop2 = PhaseResult_UsiLoop2.Break;
                        }
                        else if ("logdase" == line)
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append("ログだせ～（＾▽＾）");

                            playing.Game.Kifu.ForeachZenpuku(
                                playing.Game.Kifu.GetRoot(), (int temezumi, KyokumenWrapper sky, Node<IMove, KyokumenWrapper> node, ref bool toBreak) =>
                                {
                                    //sb.AppendLine("(^-^)");

                                    if (null != node)
                                    {
                                        if (null != node.Key)
                                        {
                                            string sfenText = ConvMoveStrSfen.ToMoveStrSfen(node.Key);
                                            sb.Append(sfenText);
                                            sb.AppendLine();
                                        }
                                    }
                                });

                            File.WriteAllText(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("LogDaseMeirei")), sb.ToString());
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

                        switch (result_UsiLoop2)
                        {
                            case PhaseResult_UsiLoop2.Break:
                                goto end_loop2;

                            default:
                                break;
                        }

#pragma warning disable CS0164 // このラベルは参照されていません
                    gt_NextLine_loop2:
#pragma warning restore CS0164 // このラベルは参照されていません
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
                    Logger.Trace("KifuParserA_Impl.LOGGING_BY_ENGINE, ┏━確認━━━━setoptionDictionary ━┓");
                    foreach (KeyValuePair<string, string> pair in playing.SetoptionDictionary)
                    {
                        Logger.Trace($"{pair.Key}={pair.Value}");
                    }
                    Logger.Trace("┗━━━━━━━━━━━━━━━━━━┛");

                    //Dictionary<string, string> goMateProperties = new Dictionary<string, string>();
                    //goMateProperties["mate"] = "";
                    //LarabeLoggerList_Warabe.ENGINE.WriteLineAddMemo("┗━━━━━━━━━━━━━━━━━━┛");
                    //LarabeLoggerList_Warabe.ENGINE.WriteLineAddMemo("┏━確認━━━━goMateDictionary━━━┓");
                    //foreach (KeyValuePair<string, string> pair in this.goMateProperties)
                    //{
                    //    LarabeLoggerList_Warabe.ENGINE.WriteLineAddMemo($"{pair.Key}={pair.Value}");
                    //}
#endif

                    if (isTimeoutShutdown)
                    {
                        //MessageBox.Show("ループ２で矯正終了するんだぜ☆！");
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
        }
    }
}
