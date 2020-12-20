using Grayscale.Kifuwarakaku.Entities.Logging;
using Grayscale.P211WordShogi.L500Word;
using Grayscale.P224Sky.L500Struct;
using Grayscale.P258UtilSky258.L500UtilSky;
using Grayscale.P324KifuTree.L250Struct;
using Grayscale.P325PnlTaikyoku.I250Struct;
using Grayscale.P355_KifuParserA.I500Parser;
using System;

namespace Grayscale.P355_KifuParserA.L500Parser
{

    /// <summary>
    /// 「position」を読込みました。
    /// </summary>
    public class KifuParserA_StateA1_SfenPosition : KifuParserA_State
    {


        public static KifuParserA_StateA1_SfenPosition GetInstance()
        {
            if (null == instance)
            {
                instance = new KifuParserA_StateA1_SfenPosition();
            }

            return instance;
        }
        private static KifuParserA_StateA1_SfenPosition instance;


        private KifuParserA_StateA1_SfenPosition()
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

            if (genjo.InputLine.StartsWith("startpos"))
            {
                // 平手の初期配置です。
                //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

#if DEBUG
                    logTag.Logger.WriteLineAddMemo("（＾△＾）「" + genjo.InputLine + "」vs【" + this.GetType().Name + "】　：　平手のようなんだぜ☆");
#endif

                genjo.InputLine = genjo.InputLine.Substring("startpos".Length);
                genjo.InputLine = genjo.InputLine.Trim();

                //----------------------------------------
                // 棋譜を空っぽにし、平手初期局面を与えます。
                //----------------------------------------
                {
                    model_Taikyoku.Kifu.Clear();// 棋譜を空っぽにします。

                    model_Taikyoku.Kifu.GetRoot().Value.SetKyokumen(
                        SkyConst.NewInstance(Util_SkyWriter.New_Hirate(Playerside.P1),
                        0//初期配置は 0手目済み。)
                        ));//SFENのstartpos解析時
                    model_Taikyoku.Kifu.SetProperty(Word_KifuTree.PropName_Startpos, "startpos");//平手の初期局面
                }

                nextState = KifuParserA_StateA1a_SfenStartpos.GetInstance();
            }
            else
            {
                //#if DEBUG
                Logger.Error( "（＾△＾）ここはスルーして次に状態遷移するんだぜ☆\n「" + genjo.InputLine + "」vs【" + this.GetType().Name + "】");//　：　局面の指定のようなんだぜ☆　対応していない☆？
                                                                                                                                     // Logger.WriteLineError(logTag, "（＾△＾）「" + genjo.InputLine + "」vs【" + this.GetType().Name + "】　：　局面の指定のようなんだぜ☆　対応していない☆？");
                                                                                                                                     //#endif
                nextState = KifuParserA_StateA1b_SfenLnsgkgsnl.GetInstance();
            }

            return genjo.InputLine;
        }

    }
}
