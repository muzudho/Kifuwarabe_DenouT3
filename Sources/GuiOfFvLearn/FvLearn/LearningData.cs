﻿namespace Grayscale.Kifuwarakaku.GuiOfFvLearn.Features
{
#if DEBUG
    using Grayscale.Kifuwarakaku.Entities.Features;
    using Grayscale.Kifuwarakaku.UseCases.Features;
#else
    using Grayscale.Kifuwarakaku.Entities.Features;
    using Grayscale.Kifuwarakaku.UseCases.Features;
#endif

    /// <summary>
    /// 学習用データ。
    /// </summary>
    public interface LearningData
    {
        CsaKifu CsaKifu { get; set; }

        KifuTree Kifu { get; set; }

        /// <summary>
        /// フィーチャー・ベクター。
        /// </summary>
        FeatureVector Fv { get; set; }

        /// <summary>
        /// 初期設定。
        /// </summary>
        void AtBegin(Uc_Main uc_Main);

        /// <summary>
        /// 棋譜読込み。
        /// </summary>
        void ReadKifu(Uc_Main uc_Main);

        /// <summary>
        /// 局面PNG画像を更新。
        /// </summary>
        void ChangeKyokumenPng(Uc_Main uc_Main);


        /// <summary>
        /// 局面PNG画像書き出し。
        /// </summary>
        void WritePng();


        /// <summary>
        /// 合法手を一覧します。
        /// </summary>
        /// <param name="uc_Main"></param>
        void Aaa_CreateNextNodes_Gohosyu(EvaluationArgs args);

        /// <summary>
        /// 全合法手をダンプ。デバッグ用途。
        /// </summary>
        /// <returns></returns>
        string DumpToAllGohosyu(SkyConst src_Sky);


        /// <summary>
        /// 評価値を算出します。
        /// </summary>
        void DoScoreing_ForLearning(
            KifuNode node
#if DEBUG || LEARN
,
            out KyHyokaMeisai_Koumoku komawariMeisai,
            out KyHyokaMeisai_Koumoku ppMeisai
#endif
            );


        /// <summary>
        /// 合法手一覧を作成したい。
        /// </summary>
        void Aa_Yomi(IMove move);

    }
}
