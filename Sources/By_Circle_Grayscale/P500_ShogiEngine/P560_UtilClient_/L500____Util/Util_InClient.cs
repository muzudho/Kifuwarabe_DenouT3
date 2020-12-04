using Grayscale.P003Log.I500Struct;
using Grayscale.P145SfenStruct.I250Struct;
using Grayscale.P145SfenStruct.L250Struct;
using Grayscale.P146ConvSfen.L500Converter;
using Grayscale.P211WordShogi.L500Word;
using Grayscale.P224Sky.L500Struct;
using Grayscale.P307UtilSky.L500Util;
using Grayscale.P324_KifuTree___.L250Struct;
using Grayscale.P325_PnlTaikyoku.I250Struct;
using Grayscale.P355_KifuParserA.L___500_Parser;

namespace Grayscale.P560_UtilClient_.L500Util
{
    public abstract class Util_InClient
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="kifu"></param>
        /// <param name="restText"></param>
        /// <param name="startposImporter"></param>
        /// <param name="logTag"></param>
        public static void OnChangeSky_Im_Client(
            Model_Taikyoku model_Taikyoku,
            KifuParserA_Genjo genjo,
            KwErrorHandler errH
            )
        {
            errH.Logger.WriteLine_Error("（＾△＾）「" + genjo.InputLine + "」Util_InClient　：　クライアントの委譲メソッドｷﾀｰ☆");


            string old_inputLine = genjo.InputLine;//退避
            string rest;
            RO_Kyokumen2_ForTokenize ro_Kyokumen2_ForTokenize;
            Conv_Sfenstring146.ToKyokumen2(
                genjo.InputLine,
                out rest,
                out ro_Kyokumen2_ForTokenize
                );

            errH.Logger.WriteLine_Error("（＾△＾）old_inputLine=「" + old_inputLine + "」 rest=「" + rest + "」 Util_InClient　：　ﾊﾊｯ☆");

            //string old_inputLine = genjo.InputLine;
            //genjo.InputLine = "";


            //----------------------------------------
            // 棋譜を空っぽにし、指定の局面を与えます。
            //----------------------------------------
            {
                model_Taikyoku.Kifu.Clear();// 棋譜を空っぽにします。

                // 文字列から、指定局面を作成します。
                Playerside pside = Playerside.P1;
                int temezumi = 0;
                model_Taikyoku.Kifu.GetRoot().Value.SetKyokumen(
                    SkyConst.NewInstance(
                        Conv_Sfenstring307.ToSkyConst(new SfenstringImpl(old_inputLine), pside, temezumi),
                        temezumi//初期配置は 0手目済み。
                    )
                );//SFENのstartpos解析時
                model_Taikyoku.Kifu.SetProperty(Word_KifuTree.PropName_Startpos, old_inputLine);//指定の初期局面
            }


        }

    }
}
