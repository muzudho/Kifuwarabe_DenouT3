using Grayscale.P003Log.I500Struct;
using Grayscale.P003Log.L500Struct;
using Grayscale.P157KyokumenPng.I500Struct;
using Grayscale.P158LogKyokuPng.L500UtilWriter;
using Grayscale.P211WordShogi.L500Word;
using Grayscale.P212ConvPside.L500Converter;
using Grayscale.P213Komasyurui.L250Word;
using Grayscale.P213Komasyurui.L500Util;
using Grayscale.P218Starlight.I500Struct;
using Grayscale.P224Sky.L500Struct;
using Grayscale.P226Tree.I500Struct;
using Grayscale.P238Seiza.L250Struct;
using Grayscale.P238Seiza.L500Util;
using Grayscale.P247KyokumenWra.L500Struct;
using Grayscale.P256SeizaFinger.L250Struct;
using Grayscale.P258UtilSky258.L500UtilSky;
using Grayscale.P324KifuTree.I250Struct;
using Grayscale.P324KifuTree.L250Struct;
using Grayscale.P325PnlTaikyoku.I250Struct;
using Grayscale.P325PnlTaikyoku.L250Struct;
using Grayscale.P339ConvKyokume.L500Converter;
using Grayscale.P341Ittesasu.L125UtilB;
using Grayscale.P355_KifuParserA.I500Parser;
using Grayscale.P355_KifuParserA.L500Parser;
using Grayscale.P542Scoreing.I005UsiLoop;
using Grayscale.P542Scoreing.L240Shogisasi;
using Grayscale.P560UtilClient.L500Util;
using Grayscale.P571KifuWarabe.L125AjimiEngine;
using Grayscale.P571KifuWarabe.L249Noop;
using Grayscale.P440KifuTreeLog.L500Struct;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号



namespace Grayscale.P571KifuWarabe.L250UsiLoop
{

    /// <summary>
    /// USIの２番目のループです。
    /// </summary>
    public class UsiLoop2
    {
        #region プロパティー
        private ShogiEngine owner;
        private Shogisasi shogisasi;


        /// <summary>
        /// 棋譜です。
        /// </summary>
        public KifuTree Kifu { get { return this.kifu; } }
        public void SetKifu(KifuTree kifu)
        {
            this.kifu = kifu;
        }
        private KifuTree kifu;


        /// <summary>
        /// 「go」の属性一覧です。
        /// </summary>
        public Dictionary<string, string> GoProperties { get; set; }


        /// <summary>
        /// 「go ponder」の属性一覧です。
        /// </summary>
        public bool GoPonderNow { get; set; }


        /// <summary>
        /// USIの２番目のループで保持される、「gameover」の一覧です。
        /// </summary>
        public Dictionary<string, string> GameoverProperties { get; set; }

        #endregion

        public UsiLoop2(Shogisasi shogisasi, ShogiEngine owner)
        {
            this.owner = owner;
            this.shogisasi = shogisasi;

            #region ↓詳説  ＜n手目＞
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
            #endregion

            // 棋譜
            {
                Playerside firstPside = Playerside.P1;
                // FIXME:平手とは限らないが、平手という前提で作っておく。
                this.SetKifu( new KifuTreeImpl(
                        new KifuNodeImpl(
                            Util_Sky258A.ROOT_SASITE,
                            new KyokumenWrapper( SkyConst.NewInstance(
                                    Util_SkyWriter.New_Hirate(firstPside),
                                    0 // 初期局面は 0手目済み
                                ))// きふわらべ起動時
                        )
                ));
                this.Kifu.SetProperty(Word_KifuTree.PropName_Startpos, "startpos");// 平手 // FIXME:平手とは限らないが。

                Debug.Assert(!Conv_MasuHandle.OnKomabukuro(
                    Conv_SyElement.ToMasuNumber(((RO_Star)this.Kifu.CurNode.Value.KyokumenConst.StarlightIndexOf((Finger)0).Now).Masu)
                    ), "駒が駒袋にあった。");
            }

            // goの属性一覧
            {
                this.GoProperties = new Dictionary<string, string>();
                this.GoProperties["btime"] = "";
                this.GoProperties["wtime"] = "";
                this.GoProperties["byoyomi"] = "";
            }

            // go ponderの属性一覧
            {
                this.GoPonderNow = false;   // go ponderを将棋所に伝えたなら真
            }

            // gameoverの属性一覧
            {
                this.GameoverProperties = new Dictionary<string, string>();
                this.GameoverProperties["gameover"] = "";
            }
        }


        public void AtBegin()
        {
            this.shogisasi.OnTaikyokuKaisi();//対局開始時の処理。
        }



        public void AtBody(out bool out_isTimeoutShutdown, IErrorController errH)
        {
            out_isTimeoutShutdown = false;
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
                //string line = TimeoutReader.ReadLine(1000);//指定ミリ秒だけブロック

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
                        noopTimer._02_AtEmptyMessage(this.owner, out isTimeoutShutdown_temp,errH);
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
                errH.Logger.WriteLineC(line);

#if NOOPABLE
                if (this.owner.Option_enable_serverNoopable)
                {
                    noopTimer._03_AtResponsed(this.owner, line, errH);
                }
#endif


                if (line.StartsWith("position")) { this.AtLoop_OnPosition(line, ref result_UsiLoop2); }
                else if (line.StartsWith("go ponder")) { this.AtLoop_OnGoponder(line, ref result_UsiLoop2); }
                else if (line.StartsWith("go")) { this.AtLoop_OnGo(line, ref result_UsiLoop2); }// 「go ponder」「go mate」「go infinite」とは区別します。
                else if (line.StartsWith("stop")) { this.AtLoop_OnStop(line, ref result_UsiLoop2); }
                else if (line.StartsWith("gameover")) { this.AtLoop_OnGameover(line, ref result_UsiLoop2); }
                else if ("logdase" == line) { this.AtLoop_OnLogdase(line, ref result_UsiLoop2); }
                else
                {
                    //------------------------------------------------------------
                    // ○△□×！？
                    //------------------------------------------------------------
                    #region ↓詳説
                    //
                    // ／(＾×＾)＼
                    //

                    // 通信が届いていますが、このプログラムでは  聞かなかったことにします。
                    // USIプロトコルの独習を進め、対応／未対応を選んでください。
                    //
                    // ログだけ取って、スルーします。
                    #endregion
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
        }

        public void AtEnd()
        {
            //-------------------+----------------------------------------------------------------------------------------------------
            // スナップショット  |
            //-------------------+----------------------------------------------------------------------------------------------------
            #region ↓詳説
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
            #endregion
#if DEBUG
            ErrorControllerReference.ENGINE_DEFAULT.Logger.WriteLine_AddMemo("KifuParserA_Impl.LOGGING_BY_ENGINE, ┏━確認━━━━setoptionDictionary ━┓");
            foreach (KeyValuePair<string, string> pair in this.owner.SetoptionDictionary)
            {
                ErrorControllerReference.ENGINE_DEFAULT.Logger.WriteLine_AddMemo(pair.Key + "=" + pair.Value);
            }
            ErrorControllerReference.ENGINE_DEFAULT.Logger.WriteLine_AddMemo("┗━━━━━━━━━━━━━━━━━━┛");
            ErrorControllerReference.ENGINE_DEFAULT.Logger.WriteLine_AddMemo("┏━確認━━━━goDictionary━━━━━┓");
            foreach (KeyValuePair<string, string> pair in this.GoProperties)
            {
                ErrorControllerReference.ENGINE_DEFAULT.Logger.WriteLine_AddMemo(pair.Key + "=" + pair.Value);
            }

            //Dictionary<string, string> goMateProperties = new Dictionary<string, string>();
            //goMateProperties["mate"] = "";
            //LarabeLoggerList_Warabe.ENGINE.WriteLine_AddMemo("┗━━━━━━━━━━━━━━━━━━┛");
            //LarabeLoggerList_Warabe.ENGINE.WriteLine_AddMemo("┏━確認━━━━goMateDictionary━━━┓");
            //foreach (KeyValuePair<string, string> pair in this.goMateProperties)
            //{
            //    LarabeLoggerList_Warabe.ENGINE.WriteLine_AddMemo(pair.Key + "=" + pair.Value);
            //}

            ErrorControllerReference.ENGINE_DEFAULT.Logger.WriteLine_AddMemo("┗━━━━━━━━━━━━━━━━━━┛");
            ErrorControllerReference.ENGINE_DEFAULT.Logger.WriteLine_AddMemo("┏━確認━━━━gameoverDictionary━━┓");
            foreach (KeyValuePair<string, string> pair in this.GameoverProperties)
            {
                ErrorControllerReference.ENGINE_DEFAULT.Logger.WriteLine_AddMemo(pair.Key + "=" + pair.Value);
            }
            ErrorControllerReference.ENGINE_DEFAULT.Logger.WriteLine_AddMemo("┗━━━━━━━━━━━━━━━━━━┛");
#endif
        }


        public void AtLoop_OnPosition(string line, ref PhaseResult_UsiLoop2 result_Usi)
        {
            IErrorController errH = ErrorControllerReference.EngineDefault;

            try
            {
                //------------------------------------------------------------
                // これが棋譜です
                //------------------------------------------------------------
                #region ↓詳説
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
                #endregion
#if DEBUG
                this.Log1("（＾△＾）positionきたｺﾚ！");
#endif
                // 入力行を解析します。
                KifuParserA_Result result = new KifuParserA_ResultImpl();
                KifuParserA_Impl kifuParserA = new KifuParserA_Impl();
                Model_Taikyoku model_Taikyoku = new Model_TaikyokuImpl(this.Kifu);//FIXME:  この棋譜を委譲メソッドで修正するはず。 ShogiGui_Warabeは？
                KifuParserA_Genjo genjo = new KifuParserA_GenjoImpl(line);
                kifuParserA.Execute_All(
                    ref result,
                    model_Taikyoku,
                    genjo,
                    errH
                    );
                if (null != genjo.StartposImporter_OrNull)
                {
                    // SFENの解析結果を渡すので、
                    // その解析結果をどう使うかは、委譲します。
                    Util_InClient.OnChangeSky_Im_Client(
                        model_Taikyoku,
                        genjo,
                        errH
                        );
                }


#if DEBUG
                this.Log2_Png_Tyokkin(line, (KifuNode)result.Out_newNode_OrNull, errH);
#endif

                //------------------------------------------------------------
                // じっとがまん
                //------------------------------------------------------------
                #region ↓詳説
                //
                // 応答は無用です。
                // 多分、将棋所もまだ準備ができていないのではないでしょうか（？）
                //
                #endregion
            }
            catch (Exception ex)
            {
                // エラー：どうにもできないので  ログだけ取って無視します。
                ErrorControllerReference.EngineDefault.Panic("Program「position」：" + ex.GetType().Name + "：" + ex.Message);
            }
        }
        private void Log1(string message)
        {
            ErrorControllerReference.EngineDefault.Logger.WriteLineAddMemo(message);
        }
        private void Log2_Png_Tyokkin(string line, KifuNode kifuNode, IErrorController errH)
        {
            var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
            var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));

            //OwataMinister.WARABE_ENGINE.Logger.WriteLine_AddMemo(
            //    Util_Sky307.Json_1Sky(this.Kifu.CurNode.Value.ToKyokumenConst, "現局面になっているのかなんだぜ☆？　line=[" + line + "]　棋譜＝" + KirokuGakari.ToJsaKifuText(this.Kifu, OwataMinister.WARABE_ENGINE),
            //        "PgCS",
            //        this.Kifu.CurNode.Value.ToKyokumenConst.Temezumi
            //    )
            //);

            //
            // 局面画像ﾛｸﾞ
            //
            {
                // 出力先
                string fileName = Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("ChokkinNoSasiteLogPngBasename"));

                int srcMasu_orMinusOne = -1;
                int dstMasu_orMinusOne = -1;
                if (null != kifuNode.Key)
                {
                    srcMasu_orMinusOne = Conv_SyElement.ToMasuNumber(((RO_Star)kifuNode.Key.LongTimeAgo).Masu);
                    dstMasu_orMinusOne = Conv_SyElement.ToMasuNumber(((RO_Star)kifuNode.Key.Now).Masu);
                }

                KyokumenPngArgs_FoodOrDropKoma foodKoma;
                if (null != kifuNode.Key.FoodKomaSyurui)
                {
                    switch (Util_Komasyurui14.NarazuCaseHandle((Komasyurui14)kifuNode.Key.FoodKomaSyurui))
                    {
                        case Komasyurui14.H00_Null___: foodKoma = KyokumenPngArgs_FoodOrDropKoma.NONE; break;
                        case Komasyurui14.H01_Fu_____: foodKoma = KyokumenPngArgs_FoodOrDropKoma.FU__; break;
                        case Komasyurui14.H02_Kyo____: foodKoma = KyokumenPngArgs_FoodOrDropKoma.KYO_; break;
                        case Komasyurui14.H03_Kei____: foodKoma = KyokumenPngArgs_FoodOrDropKoma.KEI_; break;
                        case Komasyurui14.H04_Gin____: foodKoma = KyokumenPngArgs_FoodOrDropKoma.GIN_; break;
                        case Komasyurui14.H05_Kin____: foodKoma = KyokumenPngArgs_FoodOrDropKoma.KIN_; break;
                        case Komasyurui14.H07_Hisya__: foodKoma = KyokumenPngArgs_FoodOrDropKoma.HI__; break;
                        case Komasyurui14.H08_Kaku___: foodKoma = KyokumenPngArgs_FoodOrDropKoma.KAKU; break;
                        default: foodKoma = KyokumenPngArgs_FoodOrDropKoma.UNKNOWN; break;
                    }
                }
                else
                {
                    foodKoma = KyokumenPngArgs_FoodOrDropKoma.NONE;
                }

                // 直近の指し手。
                Util_KyokumenPng_Writer.Write1(
                    Conv_KifuNode.ToRO_Kyokumen1(kifuNode, errH),
                    srcMasu_orMinusOne,
                    dstMasu_orMinusOne,
                    foodKoma,
                    Conv_SasiteStr_Sfen.ToSasiteStr_Sfen(kifuNode.Key),//Conv_SasiteStr_Jsa.ToSasiteStr_Jsa(kifuNode, kifuNode.Value, errH),
                    "",
                    fileName,
                    Util_KifuTreeLogWriter.REPORT_ENVIRONMENT,
                    errH
                    );
            }
        }
        

        public void AtLoop_OnGoponder(string line, ref PhaseResult_UsiLoop2 result_Usi)
        {
            try
            {

                //------------------------------------------------------------
                // 将棋所が次に呼びかけるまで、考えていてください
                //------------------------------------------------------------
                #region ↓詳説
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
                #endregion


                //------------------------------------------------------------
                // じっとがまん
                //------------------------------------------------------------
                #region ↓詳説
                //
                // まだ指してはいけません。
                // 指したら反則です。相手はまだ指していないのだ☆ｗ
                //
                #endregion
            }
            catch (Exception ex)
            {
                // エラーが起こりました。
                //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                // どうにもできないので  ログだけ取って無視します。
                ErrorControllerReference.EngineDefault.Panic("Program「go ponder」：" + ex.GetType().Name + "：" + ex.Message);
            }
        }


        /// <summary>
        /// 「go ponder」「go mate」「go infinite」とは区別します。
        /// </summary>
        /// <param name="line"></param>
        /// <param name="result_Usi"></param>
        public void AtLoop_OnGo(string line, ref PhaseResult_UsiLoop2 result_Usi)
        {
            IErrorController errH = ErrorControllerReference.EngineDefault;
            int exceptionArea = 0;

            try
            {

                exceptionArea = 1000;
                //------------------------------------------------------------
                // あなたの手番です
                //------------------------------------------------------------
                #region ↓詳説
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
                #endregion


                //------------------------------------------------------------
                // 先手 3:00  後手 0:00  記録係「50秒ぉ～」
                //------------------------------------------------------------
                #region ↓詳説
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
                #endregion
                Regex regex = new Regex(@"go btime (\d+) wtime (\d+) byoyomi (\d+)", RegexOptions.Singleline);
                Match m = regex.Match(line);

                if (m.Success)
                {
                    this.GoProperties["btime"] = (string)m.Groups[1].Value;
                    this.GoProperties["wtime"] = (string)m.Groups[2].Value;
                    this.GoProperties["byoyomi"] = (string)m.Groups[3].Value;
                }
                else
                {
                    this.GoProperties["btime"] = "";
                    this.GoProperties["wtime"] = "";
                    this.GoProperties["byoyomi"] = "";
                }



                //----------------------------------------
                // 棋譜ツリー、局面データは、position コマンドで先に与えられているものとします。
                //----------------------------------------

                // ┏━━━━プログラム━━━━┓

                int latestTemezumi = this.Kifu.CurNode.Value.KyokumenConst.Temezumi;//現・手目済

                //#if DEBUG
                // MessageBox.Show("["+latestTemezumi+"]手目済　["+this.owner.PlayerInfo.Playerside+"]の手番");
                //#endif

                SkyConst src_Sky = this.Kifu.NodeAt(latestTemezumi).Value.KyokumenConst;//現局面

                //errH.Logger.WriteLine_AddMemo("将棋サーバー「" + latestTemezumi + "手目、きふわらべ　さんの手番ですよ！」　" + line);


                //----------------------------------------
                // 王の状態を調べます。
                //----------------------------------------
                Result_KingState result_kingState;
                {
                    result_kingState = Result_KingState.Empty;

                    RO_Star king1p = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(Finger_Honshogi.SenteOh).Now);
                    RO_Star king2p = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(Finger_Honshogi.GoteOh).Now);
                    //OwataMinister.WARABE_ENGINE.Logger.WriteLine_AddMemo("将棋サーバー「ではここで、王さまがどこにいるか確認してみましょう」");
                    //OwataMinister.WARABE_ENGINE.Logger.WriteLine_AddMemo("▲王の置き場＝" + Conv_SyElement.Masu_ToOkiba(koma1.Masu));
                    //OwataMinister.WARABE_ENGINE.Logger.WriteLine_AddMemo("△王の置き場＝" + Conv_SyElement.Masu_ToOkiba(koma2.Masu));

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

                exceptionArea = 2000;
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
                            #region ↓詳説
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
                            #endregion
                            this.owner.Send("bestmove resign");//投了
                        }
                        break;
                    default:// どちらの王さまも、まだまだ健在だぜ☆！
                        {
                            List<KifuNode> bestKifuNodeList = new List<KifuNode>();

                            exceptionArea = 2100;
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
                                    bestKifuNodeList.Add(this.shogisasi.WA_Bestmove(
                                        isHonshogi,
                                        this.Kifu,
                                        errH)
                                        );
                                }


#if DEBUG
                                //// 内容をログ出力
                                //// 最善手、次善手、三次善手、四次善手、五次善手
                                //StringBuilder sb = new StringBuilder();
                                //for (int iMultiPV = 0; iMultiPV < 5; iMultiPV++)
                                //{
                                //    string sfenText = Util_Sky.ToSfenSasiteText(bestSasiteList[iMultiPV]);
                                //    sb.AppendLine("[" + iMultiPV + "]" + sfenText);
                                //}
                                //System.Windows.Forms.MessageBox.Show(sb.ToString());
#endif
                            }

                            exceptionArea = 2200;
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

                            exceptionArea = 2300;
                            Starbeamable bestSasite2;
                            if (null == bestKifuNode)
                            {
                                // 投了
                                bestSasite2 = Util_Sky258A.NULL_OBJECT_SASITE;
                            }
                            else
                            {
                                bestSasite2 = bestKifuNode.Key;
                            }

                            exceptionArea = 2400;
                            if (Util_Sky_BoolQuery.isEnableSfen(bestSasite2))
                            {
                                string sfenText = Conv_SasiteStr_Sfen.ToSasiteStr_Sfen(bestSasite2);

                                // ログが重過ぎる☆！
                                //OwataMinister.WARABE_ENGINE.Logger.WriteLine_AddMemo("(Warabe)指し手のチョイス： bestmove＝[" + sfenText + "]" +
                                //    "　棋譜＝" + KirokuGakari.ToJsaKifuText(this.Kifu, errH));

                                //----------------------------------------
                                // スコア 試し
                                //----------------------------------------
                                {
                                    //int hyojiScore = (int)(bestScore / 100.0d);//FIXME:適当に調整した。
                                    int hyojiScore = (int)bestScore;
                                    if (this.Kifu.CurNode.Value.KyokumenConst.KaisiPside == Playerside.P2)
                                    {
                                        // 符号を逆転
                                        hyojiScore = -hyojiScore;
                                    }
                                    this.owner.Send("info time 1 depth 1 nodes 1 score cp " + hyojiScore.ToString() + " pv ");//FIXME:
                                     //+ " pv 3a3b L*4h 4c4d"
                                }


                                //----------------------------------------
                                // 指し手を送ります。
                                //----------------------------------------
                                this.owner.Send("bestmove " + sfenText);
                            }
                            else // 指し手がないときは、SFENが書けない☆　投了だぜ☆
                            {
                                // ログが重過ぎる☆！
                                //OwataMinister.WARABE_ENGINE.Logger.WriteLine_AddMemo("(Warabe)指し手のチョイス： 指し手がないときは、SFENが書けない☆　投了だぜ☆ｗｗ（＞＿＜）" +
                                //    "　棋譜＝" + KirokuGakari.ToJsaKifuText(this.Kifu, errH));

                                //----------------------------------------
                                // 投了ｗ！
                                //----------------------------------------
                                this.owner.Send("bestmove resign");
                            }


                            exceptionArea = 2500;
                            //------------------------------------------------------------
                            // 以前の手カッター
                            //------------------------------------------------------------
                            UtilKifuTree282.IzennoHenkaCutter(this.Kifu, errH);
                        }
                        break;
                }
                // ┗━━━━プログラム━━━━┛


            }
            catch (Exception ex)
            {
                switch(exceptionArea)
                {
                    case 2100:
                        {
                            ErrorControllerReference.EngineDefault.Panic(ex, "マルチＰＶから、ベスト指し手をチョイスしようとしたときの１０です。");
                            throw;
                        }
                    case 2200:
                        {
                            ErrorControllerReference.EngineDefault.Panic(ex, "マルチＰＶから、ベスト指し手をチョイスしようとしたときの４０です。");
                            throw;
                        }
                    case 2300:
                        {
                            ErrorControllerReference.EngineDefault.Panic(ex, "マルチＰＶから、ベスト指し手をチョイスしようとしたときの５０です。");
                            throw;
                        }
                    case 2400:
                        {
                            ErrorControllerReference.EngineDefault.Panic(ex, "マルチＰＶから、ベスト指し手をチョイスしようとしたときの９０です。");
                            throw;
                        }
                    default:
                        {
                            // エラーが起こりました。
                            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                            // どうにもできないので  ログだけ取って無視します。
                            ErrorControllerReference.EngineDefault.Panic("Program「go」：" + ex.GetType().Name + " " + ex.Message + "：goを受け取ったときです。：");
                        }
                        break;
                }
            }


            //System.C onsole.WriteLine();

            //throw new Exception("デバッグだぜ☆！　エラーはキャッチできたかな～☆？（＾▽＾）");
        }


        public void AtLoop_OnStop(string line, ref PhaseResult_UsiLoop2 result_Usi)
        {
            try
            {

                //------------------------------------------------------------
                // あなたの手番です  （すぐ指してください！）
                //------------------------------------------------------------
                #region ↓詳説
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
                #endregion

                if (this.GoPonderNow)
                {
                    //------------------------------------------------------------
                    // 将棋エンジン「（予想手が間違っていたって？）  △９二香 を指そうと思っていたんだが」
                    //------------------------------------------------------------
                    #region ↓詳説
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
                    #endregion
                    // この指し手は、無視されます。（無視されますが、送る必要があります）
                    this.owner.Send("bestmove 9a9b");
                }
                else
                {
                    //------------------------------------------------------------
                    // じゃあ、△９二香で
                    //------------------------------------------------------------
                    #region ↓詳説
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
                    #endregion
                    this.owner.Send("bestmove 9a9b");
                }

            }
            catch (Exception ex)
            {
                // エラーが起こりました。
                //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                // どうにもできないので  ログだけ取って無視します。
                ErrorControllerReference.EngineDefault.Panic("Program「stop」：" + ex.GetType().Name + " " + ex.Message);
            }
        }

        public void AtLoop_OnGameover(string line, ref PhaseResult_UsiLoop2 result_Usi)
        {
            try
            {
                //------------------------------------------------------------
                // 対局が終わりました
                //------------------------------------------------------------
                #region ↓詳説
                //
                // 図.
                //
                //      log.txt
                //      ┌────────────────────────────────────────
                //      ～
                //      │2014/08/02 3:08:34> gameover lose
                //      │
                //

                // 対局が終わったときに送られてくる文字が gameover です。
                #endregion

                //------------------------------------------------------------
                // 「あ、勝ちました」「あ、引き分けました」「あ、負けました」
                //------------------------------------------------------------
                #region ↓詳説
                //
                // 上図のメッセージのままだと使いにくいので、
                // あとで使いやすいように Key と Value の表に分けて持ち直します。
                //
                // 図.
                //
                //      gameoverDictionary
                //      ┌──────┬──────┐
                //      │Key         │Value       │
                //      ┝━━━━━━┿━━━━━━┥
                //      │gameover    │lose        │
                //      └──────┴──────┘
                //
                #endregion
                Regex regex = new Regex(@"gameover (.)", RegexOptions.Singleline);
                Match m = regex.Match(line);

                if (m.Success)
                {
                    this.GameoverProperties["gameover"] = (string)m.Groups[1].Value;
                }
                else
                {
                    this.GameoverProperties["gameover"] = "";
                }


                // 無限ループ（２つ目）を抜けます。無限ループ（１つ目）に戻ります。
                result_Usi = PhaseResult_UsiLoop2.Break;
            }
            catch (Exception ex)
            {
                // エラー続行
                ErrorControllerReference.EngineDefault.Panic(ex, "Program「gameover」：" + ex.GetType().Name + " " + ex.Message);
            }
        }

        /// <summary>
        /// 独自コマンド「ログ出せ」
        /// </summary>
        /// <param name="line"></param>
        /// <param name="result_Usi"></param>
        public void AtLoop_OnLogdase(string line, ref PhaseResult_UsiLoop2 result_Usi)
        {
            var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
            var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));

            StringBuilder sb = new StringBuilder();
            sb.Append("ログだせ～（＾▽＾）");

            this.Kifu.ForeachZenpuku(
                this.Kifu.GetRoot(), (int temezumi, KyokumenWrapper sky, Node<Starbeamable, KyokumenWrapper> node, ref bool toBreak) =>
                {
                    //sb.AppendLine("(^-^)");

                    if(null!=node)
                    {
                        if (null != node.Key)
                        {
                            string sfenText = Conv_SasiteStr_Sfen.ToSasiteStr_Sfen(node.Key);
                            sb.Append(sfenText);
                            sb.AppendLine();
                        }
                    }
                });

            File.WriteAllText(Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("LogDaseMeirei")), sb.ToString());
        }
    }
}
