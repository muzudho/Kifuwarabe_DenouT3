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


namespace Grayscale.Kifuwarakaku.Engine
{

    /// <summary>
    /// USIの２番目のループです。
    /// </summary>
    public class UsiLoop2
    {
        private ShogiEngine owner;
        public Shogisasi shogisasi;

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

        public UsiLoop2(Shogisasi shogisasi, ShogiEngine owner)
        {
            this.owner = owner;
            this.shogisasi = shogisasi;

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
                Playerside firstPside = Playerside.P1;
                // FIXME:平手とは限らないが、平手という前提で作っておく。
                this.SetKifu(new KifuTreeImpl(
                        new KifuNodeImpl(
                            Util_Sky258A.RootMove,
                            new KyokumenWrapper(SkyConst.NewInstance(
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

        public void AtLoop_OnGameover(string line, ref PhaseResult_UsiLoop2 result_Usi)
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
                this.Kifu.GetRoot(), (int temezumi, KyokumenWrapper sky, Node<IMove, KyokumenWrapper> node, ref bool toBreak) =>
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
    }
}
