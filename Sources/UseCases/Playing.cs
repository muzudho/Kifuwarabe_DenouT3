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
        /// USIの２番目のループで保持される、「gameover」の一覧です。
        /// </summary>
        public Dictionary<string, string> GameoverProperties { get; set; }

        /// <summary>
        /// USI「setoption」コマンドのリストです。
        /// </summary>
        public Dictionary<string, string> SetoptionDictionary { get; set; }

        /// <summary>
        /// 将棋エンジンの中の一大要素「思考エンジン」です。
        /// 指す１手の答えを出すのが仕事です。
        /// </summary>
        public Shogisasi shogisasi;

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
            Util_Message.Upload(line);

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

    }
}
