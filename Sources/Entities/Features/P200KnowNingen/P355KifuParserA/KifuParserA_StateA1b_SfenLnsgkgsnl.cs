﻿using Grayscale.Kifuwarakaku.Entities.Logging;

namespace Grayscale.Kifuwarakaku.Entities.Features
{
    /// <summary>
    /// 指定局面から始める配置です。
    /// 
    /// 「lnsgkgsnl/1r5b1/ppppppppp/9/9/6P2/PPPPPP1PP/1B5R1/LNSGKGSNL w - 1」といった文字の読込み
    /// </summary>
    public class KifuParserA_StateA1b_SfenLnsgkgsnl : KifuParserA_State
    {


        public static KifuParserA_StateA1b_SfenLnsgkgsnl GetInstance()
        {
            if (null == instance)
            {
                instance = new KifuParserA_StateA1b_SfenLnsgkgsnl();
            }

            return instance;
        }
        private static KifuParserA_StateA1b_SfenLnsgkgsnl instance;



        private KifuParserA_StateA1b_SfenLnsgkgsnl()
        {
        }


        public string Execute(
            ref KifuParserA_Result result,
            Model_Taikyoku model_Taikyoku,
            out KifuParserA_State nextState,
            KifuParserA owner,
            KifuParserA_Genjo genjo
            )
        {
            nextState = this;

            Logger.Error($"（＾△＾）「{genjo.InputLine}」vs【{this.GetType().Name}】　：　さて、どんな内容なんだぜ☆？");

            StartposImporter startposImporter1;
            string restText;

            bool successful = StartposImporter.TryParse(
                genjo.InputLine,
                out startposImporter1,
                out restText
                );
            genjo.StartposImporter_OrNull = startposImporter1;
            Logger.Error($"（＾△＾）restText=「{restText}」 successful=【{successful}】");

            if (successful)
            {
                genjo.InputLine = restText;

                //if(null!=genjo.StartposImporter_OrNull)
                //{
                //    // SFENの解析結果を渡すので、
                //    // その解析結果をどう使うかは、委譲します。
                //    owner.Delegate_OnChangeSky_Im(
                //        model_PnlTaikyoku,
                //        genjo,
                //        logTag
                //        );
                //}

                nextState = KifuParserA_StateA2_SfenMoves.GetInstance();
            }
            else
            {
                // 解析に失敗しました。
                //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                genjo.ToBreak_Abnormal();
            }

            return genjo.InputLine;
        }

    }
}
