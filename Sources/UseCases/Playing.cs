using System;
using System.Collections.Generic;
using System.IO;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.UseCases.Features;
using Nett;

namespace Grayscale.Kifuwarakaku.UseCases
{
    public class Playing
    {
        public Playing()
        {
            //-------------+----------------------------------------------------------------------------------------------------------
            // データ設計  |
            //-------------+----------------------------------------------------------------------------------------------------------
            // 将棋所から送られてくるデータを、一覧表に変えたものです。
            this.SetoptionDictionary = new Dictionary<string, string>(); // 不定形

            this.Option_enable_usiPonder = false; // ポンダーに対応している将棋サーバーなら真です。
            this.Option_enable_serverNoopable = false; // 独自実装のコマンドなので、ＯＦＦにしておきます。

            this.FeatureVector = new FeatureVectorImpl();
            this.EdagariEngine = new ScoreSiboriEngine();
        }

        /// <summary>
        /// 右脳。
        /// </summary>
        public FeatureVector FeatureVector { get; private set; }

        /// <summary>
        /// 枝狩りエンジン。
        /// </summary>
        public ScoreSiboriEngine EdagariEngine { get; set; }

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
        /// USI「setoption」コマンドのリストです。
        /// </summary>
        public Dictionary<string, string> SetoptionDictionary { get; set; }

        /// <summary>
        /// USI「ponder」の使用の有無です。
        /// </summary>
        public bool Option_enable_usiPonder { get; set; }

        /// <summary>
        /// サーバーに「noop」コマンドを投げてもよいかどうかです。
        /// サーバーに「noop」を送ると、「ok」を返してくれるものとします。１分間を空けてください。
        /// </summary>
        public bool Option_enable_serverNoopable { get; set; }

        /// <summary>
        /// 送信
        /// </summary>
        /// <param name="line">メッセージ</param>
        public static void Send(string line)
        {
            // 将棋サーバーに向かってメッセージを送り出します。
            Console.Out.WriteLine(line);

#if DEBUG
            // 送信記録をつけます。
            Logger.EngineNetwork.Logger.WriteLineS(line);
#endif
        }

        /// <summary>
        /// 未使用。
        /// </summary>
        /// <param name="line"></param>
        /// <param name="kifuNode"></param>
        private void Log2_Png_Tyokkin(string line, KifuNode kifuNode)
        {
            var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
            var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));

            //OwataMinister.WARABE_ENGINE.Logger.WriteLineAddMemo(
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
                string fileName = Path.Combine(profilePath, toml.Get<TomlTable>("Resources").Get<string>("ChokkinNoMoveLogPngBasename"));

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
                    Conv_KifuNode.ToRO_Kyokumen1(kifuNode),
                    srcMasu_orMinusOne,
                    dstMasu_orMinusOne,
                    foodKoma,
                    ConvMoveStrSfen.ToMoveStrSfen(kifuNode.Key),//Conv_MoveStr_Jsa.ToMoveStr_Jsa(kifuNode, kifuNode.Value),
                    "",
                    fileName,
                    Util_KifuTreeLogWriter.REPORT_ENVIRONMENT
                    );
            }
        }

        /// <summary>
        /// 指し手を決めます。
        /// </summary>
        /// <param name="enableLog"></param>
        /// <param name="isHonshogi"></param>
        /// <param name="kifu"></param>
        /// <returns></returns>
        public KifuNode WA_Bestmove(
            bool isHonshogi,
            KifuTree kifu
            )
        {
#if DEBUG
            KaisetuBoards logF_kiki = new KaisetuBoards();// デバッグ用だが、メソッドはこのオブジェクトを必要としてしまう。
#endif
            EvaluationArgs args = new EvaluationArgsImpl(
                kifu.GetSennititeCounter(),
                this.FeatureVector,
                Util_KifuTreeLogWriter.REPORT_ENVIRONMENT
#if DEBUG
                ,
                logF_kiki
#endif
                );

            float alphabeta_otherBranchDecidedValue;
            switch (((KifuNode)kifu.CurNode).Value.KyokumenConst.KaisiPside)
            {
                case Playerside.P1:
                    // 2プレイヤーはまだ、小さな数を見つけていないという設定。
                    alphabeta_otherBranchDecidedValue = float.MaxValue;
                    break;
                case Playerside.P2:
                    // 1プレイヤーはまだ、大きな数を見つけていないという設定。
                    alphabeta_otherBranchDecidedValue = float.MinValue;
                    break;
                default: throw new Exception("探索直前、プレイヤーサイドのエラー");
            }

            //
            // 指し手生成ルーチンで、棋譜ツリーを作ります。
            //
            new Tansaku_FukasaYusen_Routine().WAA_Yomu_Start(
                kifu, isHonshogi, Mode_Tansaku.Shogi_ENgine, alphabeta_otherBranchDecidedValue, args);

#if DEBUG
            //
            // 評価明細ログの書出し
            //
            Util_KifuTreeLogWriter.A_Write_KifuTreeLog(
                logF_kiki,
                kifu,
                logTag
                );
            //Util_LogWriter500_HyokaMeisai.Log_Html5(
            //    this,
            //    logF_kiki,
            //    kifu,
            //    playerInfo,
            //    logTag
            //    );
#endif

            // 評価値の高いノードだけを残します。
            this.EdagariEngine.EdaSibori_HighScore(kifu);

            // 評価値の同点があれば、同点決勝をして　1手に決めます。
            KifuNode bestKifuNode = null;
            bestKifuNode = this.ChoiceNode_DoutenKessyou(kifu, isHonshogi);

            return bestKifuNode;
        }

        /// <summary>
        /// 同点決勝。
        /// 
        /// 評価値が同点のノード（指し手）の中で、ランダムに１つ選びます。
        /// </summary>
        /// <param name="kifu">ツリー構造になっている棋譜</param>
        /// <returns></returns>
        public KifuNode ChoiceNode_DoutenKessyou(
            KifuTree kifu,
            bool isHonshogi)
        {
            KifuNode bestKifuNode = null;

            {
                // 次のノードをリストにします。
                //List<KifuNode> nextNodes = Util_Converter280.NextNodes_ToList(kifu.CurNode);

                // 次のノードをシャッフル済みリストにします。
                List<KifuNode> nextNodes_shuffled = Conv_NextNodes.ToList(kifu.CurNode);
                LarabeShuffle<KifuNode>.Shuffle_FisherYates(ref nextNodes_shuffled);

                // シャッフルした最初のノードを選びます。
                if (0 < nextNodes_shuffled.Count)
                {
                    bestKifuNode = nextNodes_shuffled[0];
                }
            }

            return bestKifuNode;
        }

        public void UsiOk(string engineName, string engineAuthor)
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

            Playing.Send($@"option name 子 type check default true
option name USI type spin default 2 min 1 max 13
option name 寅 type combo default tiger var マウス var うし var tiger var ウー var 龍 var へび var 馬 var ひつじ var モンキー var バード var ドッグ var うりぼー
option name 卯 type button default うさぎ
option name 辰 type string default DRAGON
option name 巳 type filename default スネーク.html
id name {engineName}
id author {engineAuthor}
usiok");
        }

        public void SetOption(string name, string value)
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
            if (this.SetoptionDictionary.ContainsKey(name))
            {
                // 設定を上書きします。
                this.SetoptionDictionary[name] = value;
            }
            else
            {
                // 設定を追加します。
                this.SetoptionDictionary.Add(name, value);
            }

            if (this.SetoptionDictionary.ContainsKey("USI_ponder"))
            {
                bool result;
                if (Boolean.TryParse(this.SetoptionDictionary["USI_ponder"], out result))
                {
                    this.Option_enable_usiPonder = result;
                }
            }
            else if (this.SetoptionDictionary.ContainsKey("noopable"))
            {
                //
                // 独自実装。
                //
                bool result;
                if (Boolean.TryParse(this.SetoptionDictionary["noopable"], out result))
                {
                    this.Option_enable_serverNoopable = result;
                }
            }
        }

        public void ReadyOk()
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

        public void UsiNewGame()
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
        }

        public void Quit()
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
        }

        public void Position()
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
        }

        public void GoPonder()
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

        public void Go(string btime, string wtime, string byoyomi, string binc, string winc)
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
            //----------------------------------------
            // 棋譜ツリー、局面データは、position コマンドで先に与えられているものとします。
            //----------------------------------------

            // ┏━━━━プログラム━━━━┓

            int latestTemezumi = this.Kifu.CurNode.Value.KyokumenConst.Temezumi;//現・手目済

            //#if DEBUG
            // MessageBox.Show("["+latestTemezumi+"]手目済　["+this.owner.PlayerInfo.Playerside+"]の手番");
            //#endif

            SkyConst src_Sky = this.Kifu.NodeAt(latestTemezumi).Value.KyokumenConst;//現局面

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
                                bestKifuNodeList.Add(this.WA_Bestmove(
                                    isHonshogi,
                                    this.Kifu)
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
                                if (this.Kifu.CurNode.Value.KyokumenConst.KaisiPside == Playerside.P2)
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
                        UtilKifuTree282.IzennoHenkaCutter(this.Kifu);
                    }
                    break;
            }
            // ┗━━━━プログラム━━━━┛

            // Logger.Trace();

            //throw new Exception("デバッグだぜ☆！　エラーはキャッチできたかな～☆？（＾▽＾）");
        }

        public void Stop()
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

            if (this.GoPonderNow)
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

        public void GameOver(string result)
        {
            //------------------------------------------------------------
            // 対局が終わりました
            //------------------------------------------------------------
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

            //------------------------------------------------------------
            // 「あ、勝ちました」「あ、引き分けました」「あ、負けました」
            //------------------------------------------------------------
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

        }

    }
}
