﻿using Grayscale.P003Log.I500Struct;
using Grayscale.P003Log.L500Struct;
using Grayscale.P276SeizaStartp.L500Struct;
using Grayscale.P325PnlTaikyoku.I250Struct;
using Grayscale.P355_KifuParserA.I500Parser;
using System;

namespace Grayscale.P355_KifuParserA.L500Parser
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
            KifuParserA_Genjo genjo,
            IErrorController errH
            )
        {
            nextState = this;

            try
            {

                errH.Logger.WriteLineError("（＾△＾）「" + genjo.InputLine + "」vs【" + this.GetType().Name + "】　：　さて、どんな内容なんだぜ☆？");

                StartposImporter startposImporter1;
                string restText;

                bool successful = StartposImporter.TryParse(
                    genjo.InputLine,
                    out startposImporter1,
                    out restText
                    );
                genjo.StartposImporter_OrNull = startposImporter1;
                errH.Logger.WriteLineError("（＾△＾）restText=「" + restText + "」 successful=【" + successful + "】");

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
                    //        errH
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

            }
            catch (Exception ex) { ErrorControllerReference.Error.Panic(ex, "SFEN解析中☆"); throw; }

            return genjo.InputLine;
        }

    }
}