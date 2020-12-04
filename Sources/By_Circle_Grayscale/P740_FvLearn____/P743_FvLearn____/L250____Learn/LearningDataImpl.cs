using Grayscale.P003Log.I500Struct;
using Grayscale.P003Log.L500Struct;
using Grayscale.P027Settei.L500Struct;
using Grayscale.P055_Conv_Sy.L500Converter;
using Grayscale.P056Syugoron.I250Struct;
using Grayscale.P157KyokumenPng.I500Struct;
using Grayscale.P157KyokumenPng.L500Struct;
using Grayscale.P158LogKyokuPng.L500UtilWriter;
using Grayscale.P163KifuCsa.I250Struct;
using Grayscale.P163KifuCsa.L250Struct;
using Grayscale.P211WordShogi.L250Masu;
using Grayscale.P211WordShogi.L500Word;
using Grayscale.P212ConvPside.L500Converter;
using Grayscale.P213Komasyurui.L250Word;
using Grayscale.P213Komasyurui.L500Util;
using Grayscale.P218Starlight.I500Struct;
using Grayscale.P222LogKaisetu.L250Struct;
using Grayscale.P224Sky.L500Struct;
using Grayscale.P226Tree.I500Struct;
using Grayscale.P236KomahaiyaTr.L500Table;
using Grayscale.P238Seiza.L250Struct;
using Grayscale.P238Seiza.L500Util;
using Grayscale.P247KyokumenWra.L500Struct;
using Grayscale.P248Michi.L500Word;
using Grayscale.P250KomahaiyaEx.L500Util;
using Grayscale.P258UtilSky258.L500UtilSky;
using Grayscale.P270ForcePromot.L250Struct;
using Grayscale.P296ConvJsa.L500Converter;
using Grayscale.P321_KyokumHyoka.I250Struct;
using Grayscale.P324_KifuTree___.I250Struct;
using Grayscale.P324_KifuTree___.L250Struct;
using Grayscale.P339_ConvKyokume.L500Converter;
using Grayscale.P341_Ittesasu___.L___250_OperationA;
using Grayscale.P341_Ittesasu___.L125____UtilB;
using Grayscale.P341_Ittesasu___.L250____OperationA;
using Grayscale.P341_Ittesasu___.L500____UtilA;
using Grayscale.P369_ConvCsa____.L500Converter;
using Grayscale.P521_FeatureVect.I500Struct;
using Grayscale.P521_FeatureVect.L500Struct;
using Grayscale.P531_Hyokakansu_.L500____Hyokakansu;
using Grayscale.P542_Scoreing___.L___240_Shogisasi;
using Grayscale.P542_Scoreing___.L___250_Args;
using Grayscale.P542_Scoreing___.L250____Args;
using Grayscale.P554_TansaFukasa.I500Struct;
using Grayscale.P554_TansaFukasa.L500Struct;
using Grayscale.P571_KifuWarabe_.L100____Shogisasi;
using Grayscale.P571_KifuWarabe_.L500____KifuWarabe;
using Grayscale.P743_FvLearn____.L___250_Learn;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P743_FvLearn____.L250____Learn
{
    /// <summary>
    /// 学習用データ。
    /// </summary>
    public class LearningDataImpl : LearningData
    {

        /// <summary>
        /// 読み用。
        /// </summary>
        private FeatureVector featureVector_ForYomi = new FeatureVectorImpl();
        private Shogisasi shogisasi_ForYomi = new ShogisasiImpl(new KifuWarabeImpl());

        public static KyokumenPngEnvironment REPORT_ENVIRONMENT;
        static LearningDataImpl()
        {
            LearningDataImpl.REPORT_ENVIRONMENT = new KyokumenPngEnvironmentImpl(
                        Const_Filepath.ENGINE_TO_LOGS,
                        Const_Filepath.ENGINE_TO_DATA + "img/gkLog/",
                        "koma1.png",//argsDic["kmFile"],
                        "suji1.png",//argsDic["sjFile"],
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

        public LearningDataImpl()
        {
            this.Fv = new FeatureVectorImpl();
        }

        /// <summary>
        /// 初期設定。
        /// </summary>
        public void AtBegin(Uc_Main uc_Main)
        {
            // データの読取「道」
            if (Michi187Array.Load(Const_Filepath.ENGINE_TO_DATA + "data_michi187.csv"))
            {
            }

            // データの読取「配役」
            Util_Array_KomahaiyakuEx184.Load(Const_Filepath.ENGINE_TO_DATA + "data_haiyaku185_UTF-8.csv", Encoding.UTF8);

            // データの読取「強制転成表」　※駒配役を生成した後で。
            Array_ForcePromotion.Load(Const_Filepath.ENGINE_TO_DATA + "data_forcePromotion_UTF-8.csv", Encoding.UTF8);
#if DEBUG
            {
                File.WriteAllText(Const_Filepath.ENGINE_TO_LOGS + "_log_強制転成表.html", Array_ForcePromotion.LogHtml());
            }
#endif

            // データの読取「配役転換表」
            Data_KomahaiyakuTransition.Load(Const_Filepath.ENGINE_TO_DATA + "data_syuruiToHaiyaku.csv", Encoding.UTF8);
#if DEBUG
            {
                File.WriteAllText(Const_Filepath.ENGINE_TO_LOGS + "_log_配役転換表.html", Data_KomahaiyakuTransition.Format_LogHtml());
            }
#endif

            // ファイルへのパス。
            uc_Main.TxtFvFilepath.Text = Path.GetFullPath(Const_Filepath.ENGINE_TO_DATA + "fv/fv_00_Komawari.csv");
            uc_Main.TxtStatus1.Text = "開くボタンで開いてください。";
        }
        /// <summary>
        /// 局面PNG画像を更新。
        /// </summary>
        public void ChangeKyokumenPng(Uc_Main uc_Main)
        {
            uc_Main.PctKyokumen.Image = null;//掴んでいる画像ファイルを放します。
            this.WritePng(Util_OwataMinister.LEARNER);
            uc_Main.PctKyokumen.ImageLocation = Const_Filepath.ENGINE_TO_LOGS + "_log_学習局面.png";
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
        public void Aa_Yomi(Starbeamable sasite, KwErrorHandler errH)
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
                    this.shogisasi_ForYomi,
                    LearningDataImpl.REPORT_ENVIRONMENT
#if DEBUG
                    ,
                    logF_kiki_orNull
#endif
                    );
            }
            this.Aaa_CreateNextNodes_Gohosyu(args, errH);
#if DEBUG
            sw2.Stop();
            Console.WriteLine("合法手作成　　　 　= {0}", sw2.Elapsed);
            Console.WriteLine("────────────────────────────────────────");
#endif


            ////
            //// 内部データ
            ////
            //{
            //    if (null != errH.Dlgt_OnNaibuDataClear_or_Null)
            //    {
            //        errH.Dlgt_OnNaibuDataClear_or_Null();
            //        errH.Dlgt_OnNaibuDataAppend_or_Null(this.DumpToAllGohosyu(this.Kifu.CurNode.Value.ToKyokumenConst));
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
            src_Sky.Foreach_Starlights((Finger finger, Starlight light, ref bool toBreak) =>
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
                sb.Append(Conv_Sy.Query_Word( koma.Masu.Bitfield));
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
        public void WritePng(KwErrorHandler errH)
        {
            int srcMasu_orMinusOne = -1;
            int dstMasu_orMinusOne = -1;
            if(null!=this.Kifu.CurNode.Key)
            {
                srcMasu_orMinusOne = Conv_SyElement.ToMasuNumber(((RO_Star)this.Kifu.CurNode.Key.LongTimeAgo).Masu);
                dstMasu_orMinusOne = Conv_SyElement.ToMasuNumber(((RO_Star)this.Kifu.CurNode.Key.Now).Masu);
            }

            KyokumenPngArgs_FoodOrDropKoma foodKoma;
            if (null != this.Kifu.CurNode.Key.FoodKomaSyurui)
            {
                switch (Util_Komasyurui14.NarazuCaseHandle((Komasyurui14)this.Kifu.CurNode.Key.FoodKomaSyurui))
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

            // 学習フォーム
            Util_KyokumenPng_Writer.Write1(
                Conv_KifuNode.ToRO_Kyokumen1(((KifuNode)this.Kifu.CurNode), errH),
                srcMasu_orMinusOne,
                dstMasu_orMinusOne,
                foodKoma,
                Conv_SasiteStr_Sfen.ToSasiteStr_Sfen(this.Kifu.CurNode.Key),
                "",
                "_log_学習局面.png",
                LearningDataImpl.REPORT_ENVIRONMENT,
                errH
                );
        }

        /// <summary>
        /// 合法手を一覧します。
        /// </summary>
        /// <param name="uc_Main"></param>
        public void Aaa_CreateNextNodes_Gohosyu(
            EvaluationArgs args,
            KwErrorHandler errH)
        {
            try
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
                    this.Kifu, isHonshogi, Mode_Tansaku.Learning, alphabeta_otherBranchDecidedValue, args, errH);
            }
            catch (Exception ex) { errH.DonimoNaranAkirameta(ex, "棋譜ツリーを作っていたときです。"); throw ex; }

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
                    this.Fv, //参照してもらうだけ。
                    Util_OwataMinister.LEARNER
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
                    this.Fv, //参照してもらうだけ。
                    Util_OwataMinister.LEARNER
                );
            }
        }

    }
}
