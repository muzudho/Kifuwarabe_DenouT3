namespace Grayscale.Kifuwarakaku.GuiOfFvLearn.Features
{
#if DEBUG
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using Grayscale.Kifuwarakaku.Entities.Features;
    using Grayscale.Kifuwarakaku.Entities.Logging;
    using Grayscale.Kifuwarakaku.UseCases.Features;
    using Nett;
    using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号
#else
using System;
using System.IO;
using System.Text;
    using Grayscale.Kifuwarakaku.Engine.Configuration;
    using Grayscale.Kifuwarakaku.Entities.Configuration;
    using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.UseCases.Features;
using Nett;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号
#endif

    /// <summary>
    /// 学習用データ。
    /// </summary>
    public class LearningDataImpl : LearningData
    {

        /// <summary>
        /// 読み用。
        /// </summary>
        private FeatureVector featureVector_ForYomi = new FeatureVectorImpl();

        public static KyokumenPngEnvironment REPORT_ENVIRONMENT;
        static LearningDataImpl()
        {
            var engineConf = new EngineConf();

            LearningDataImpl.REPORT_ENVIRONMENT = new KyokumenPngEnvironmentImpl(
                        engineConf.LogDirectory,
                        Path.Combine(engineConf.DataDirectory, "img/gkLog/"),
                        engineConf.GetResourceBasename("Koma1PngBasename"),//argsDic["kmFile"],
                        engineConf.GetResourceBasename("Suji1PngBasename"),//argsDic["sjFile"],
                        "20",//argsDic["kmW"],
                        "20",//argsDic["kmH"],
                        "8",//argsDic["sjW"],
                        "12"//argsDic["sjH"]
                        );
        }

        public CsaKifu CsaKifu { get; set; }

        public KifuTree Kifu { get; set; }

        /// <summary>
        /// フィーチャー・ベクター。
        /// </summary>
        public FeatureVector Fv { get; set; }

        public LearningDataImpl(IEngineConf engineConf)
        {
            EngineConf = engineConf;
            this.Fv = new FeatureVectorImpl();
        }
        IEngineConf EngineConf { get; set; }

        /// <summary>
        /// 初期設定。
        /// </summary>
        public void AtBegin(Uc_Main uc_Main)
        {
            var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
            var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));

            // データの読取「道」
            if (Michi187Array.Load(this.EngineConf.GetResourceFullPath("Michi187")))
            {
            }

            // データの読取「配役」
            Util_Array_KomahaiyakuEx184.Load(this.EngineConf.GetResourceFullPath("Haiyaku185"), Encoding.UTF8);

            // データの読取「強制転成表」　※駒配役を生成した後で。
            Array_ForcePromotion.Load(this.EngineConf.GetResourceFullPath("InputForcePromotion"), Encoding.UTF8);
#if DEBUG
            {
                File.WriteAllText(this.EngineConf.GetResourceFullPath("OutputForcePromotion"), Array_ForcePromotion.LogHtml());
            }
#endif

            // データの読取「配役転換表」
            Data_KomahaiyakuTransition.Load(this.EngineConf.GetResourceFullPath("InputSyuruiToHaiyaku"), Encoding.UTF8);
#if DEBUG
            {
                File.WriteAllText(this.EngineConf.GetResourceFullPath("OutputSyuruiToHaiyaku"), Data_KomahaiyakuTransition.Format_LogHtml());
            }
#endif

            // ファイルへのパス。
            uc_Main.TxtFvFilepath.Text = Path.GetFullPath(this.EngineConf.GetResourceFullPath("Fv00Komawari"));
            uc_Main.TxtStatus1.Text = "開くボタンで開いてください。";
        }
        /// <summary>
        /// 局面PNG画像を更新。
        /// </summary>
        public void ChangeKyokumenPng(Uc_Main uc_Main)
        {
            uc_Main.PctKyokumen.Image = null;//掴んでいる画像ファイルを放します。
            this.WritePng();

            uc_Main.PctKyokumen.ImageLocation = this.EngineConf.GetResourceFullPath("LearningPositionLogPng");
        }

        /// <summary>
        /// 棋譜読込み。
        /// </summary>
        public void ReadKifu(Uc_Main uc_Main)
        {

            if (!File.Exists(uc_Main.TxtKifuFilepath.Text))
            {
                goto gt_EndMethod;
            }


            // CSA棋譜テキスト→対訳→データ
            this.CsaKifu = Util_Csa.ReadFile(uc_Main.TxtKifuFilepath.Text);



            //----------------------------------------
            // 読み用。
            //----------------------------------------
            this.featureVector_ForYomi = new FeatureVectorImpl();


        gt_EndMethod:
            ;
        }


        public void WriteFv()
        {
        }


        /// <summary>
        /// 合法手一覧を作成したい。
        /// </summary>
        public void Aa_Yomi(IMove move)
        {
            //----------------------------------------
            // 合法手のNextNodesを作成します。
            //----------------------------------------

#if DEBUG
            KaisetuBoards logF_kiki_orNull = null;// デバッグ用。
            Stopwatch sw2 = new Stopwatch();
            sw2.Start();
            logF_kiki_orNull = new KaisetuBoards();
#endif
            EvaluationArgs args;
            {
                args = new EvaluationArgsImpl(
                    this.Kifu.GetSennititeCounter(),
                    this.featureVector_ForYomi,
                    LearningDataImpl.REPORT_ENVIRONMENT
#if DEBUG
                    ,
                    logF_kiki_orNull
#endif
                    );
            }
            this.Aaa_CreateNextNodes_Gohosyu(args);
#if DEBUG
            sw2.Stop();
            Logger.Trace($"合法手作成　　　 　= {sw2.Elapsed}");
            Logger.Trace("────────────────────────────────────────");
#endif


            ////
            //// 内部データ
            ////
            //{
            //    if (null != logTag.Dlgt_OnNaibuDataClear_or_Null)
            //    {
            //        logTag.Dlgt_OnNaibuDataClear_or_Null();
            //        logTag.Dlgt_OnNaibuDataAppend_or_Null(this.DumpToAllGohosyu(this.Kifu.CurNode.Value.ToKyokumenConst));
            //    }
            //}
        }

        /// <summary>
        /// 全合法手をダンプ。
        /// </summary>
        /// <returns></returns>
        public string DumpToAllGohosyu(SkyConst src_Sky)
        {
            StringBuilder sb = new StringBuilder();
            //sb.AppendLine("--------------------");
            //sb.AppendLine("カレントノード内部データ");
            //sb.AppendLine("--------------------");
            src_Sky.Foreach_Starlights((Finger finger, IMoveHalf light, ref bool toBreak) =>
            {
                // 番号
                sb.Append("Fig.");
                sb.Append(finger);
                sb.Append("　");

                RO_Star koma = Util_Starlightable.AsKoma(light.Now);

                // P1,P2
                sb.Append(koma.Pside);
                sb.Append("　");

                // 升00
                sb.Append(Conv_Sy.Query_Word(koma.Masu.Bitfield));
                sb.Append("　");

                // 歩、香…
                sb.Append(Util_Komasyurui14.ToIchimoji(koma.Komasyurui));

                sb.AppendLine();
            });

            return sb.ToString();
        }

        /// <summary>
        /// 局面PNG画像書き出し。
        /// </summary>
        public void WritePng()
        {
            var profilePath = System.Configuration.ConfigurationManager.AppSettings["Profile"];
            var toml = Toml.ReadFile(Path.Combine(profilePath, "Engine.toml"));

            int srcMasu_orMinusOne = -1;
            int dstMasu_orMinusOne = -1;
            if (null != this.Kifu.CurNode.Key)
            {
                srcMasu_orMinusOne = Conv_SyElement.ToMasuNumber(((RO_Star)this.Kifu.CurNode.Key.LongTimeAgo).Masu);
                dstMasu_orMinusOne = Conv_SyElement.ToMasuNumber(((RO_Star)this.Kifu.CurNode.Key.Now).Masu);
            }

            KyokumenPngArgs_FoodOrDropKoma foodKoma;
            if (null != this.Kifu.CurNode.Key.FoodKomaSyurui)
            {
                switch (Util_Komasyurui14.NarazuCaseHandle((PieceType)this.Kifu.CurNode.Key.FoodKomaSyurui))
                {
                    case PieceType.None: foodKoma = KyokumenPngArgs_FoodOrDropKoma.NONE; break;
                    case PieceType.P: foodKoma = KyokumenPngArgs_FoodOrDropKoma.FU__; break;
                    case PieceType.L: foodKoma = KyokumenPngArgs_FoodOrDropKoma.KYO_; break;
                    case PieceType.N: foodKoma = KyokumenPngArgs_FoodOrDropKoma.KEI_; break;
                    case PieceType.S: foodKoma = KyokumenPngArgs_FoodOrDropKoma.GIN_; break;
                    case PieceType.G: foodKoma = KyokumenPngArgs_FoodOrDropKoma.KIN_; break;
                    case PieceType.R: foodKoma = KyokumenPngArgs_FoodOrDropKoma.HI__; break;
                    case PieceType.B: foodKoma = KyokumenPngArgs_FoodOrDropKoma.KAKU; break;
                    default: foodKoma = KyokumenPngArgs_FoodOrDropKoma.UNKNOWN; break;
                }
            }
            else
            {
                foodKoma = KyokumenPngArgs_FoodOrDropKoma.NONE;
            }

            // 学習フォーム
            Util_KyokumenPng_Writer.Write1(
                Conv_KifuNode.ToRO_Kyokumen1(((KifuNode)this.Kifu.CurNode)),
                srcMasu_orMinusOne,
                dstMasu_orMinusOne,
                foodKoma,
                ConvMoveStrSfen.ToMoveStrSfen(this.Kifu.CurNode.Key),
                "",
                toml.Get<TomlTable>("Resources").Get<string>("LearningPositionLogPngBasename"),
                LearningDataImpl.REPORT_ENVIRONMENT
                );
        }

        /// <summary>
        /// 合法手を一覧します。
        /// </summary>
        /// <param name="uc_Main"></param>
        public void Aaa_CreateNextNodes_Gohosyu(
            EvaluationArgs args)
        {
            //
            // 指し手生成ルーチンで、棋譜ツリーを作ります。
            //
            bool isHonshogi = true;
            float alphabeta_otherBranchDecidedValue;
            switch (((KifuNode)this.Kifu.CurNode).Value.KyokumenConst.KaisiPside)
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


            new Tansaku_FukasaYusen_Routine().WAA_Yomu_Start(
                this.Kifu, isHonshogi, Mode_Tansaku.Learning, alphabeta_otherBranchDecidedValue, args);
        }

        /// <summary>
        /// 二駒関係の評価値を算出します。
        /// </summary>
        public void DoScoreing_ForLearning(
            KifuNode node
#if DEBUG || LEARN
            ,
            out KyHyokaMeisai_Koumoku out_komawariMeisai,
            out KyHyokaMeisai_Koumoku out_ppMeisai
#endif
        )
        {
            //----------------------------------------
            // Komawari
            //----------------------------------------
            {
                Hyokakansu_Komawari handan = new Hyokakansu_Komawari();
                float score;
                handan.Evaluate(
                    out score,
#if DEBUG || LEARN
                    out out_komawariMeisai,
#endif
                    node.Value.KyokumenConst,
                    this.Fv //参照してもらうだけ。
                );
            }
            //----------------------------------------
            // PP
            //----------------------------------------
            {
                Hyokakansu_NikomaKankeiPp handan_pp = new Hyokakansu_NikomaKankeiPp();
                float score;
                handan_pp.Evaluate(
                    out score,
#if DEBUG || LEARN
                    out out_ppMeisai,
#endif
                    node.Value.KyokumenConst,
                    this.Fv //参照してもらうだけ。
                );
            }
        }

    }
}
