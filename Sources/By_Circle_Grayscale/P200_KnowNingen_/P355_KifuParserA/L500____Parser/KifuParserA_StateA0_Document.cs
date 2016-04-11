using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P003_Log________.L500____Struct;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P325_PnlTaikyoku.L___250_Struct;
using Grayscale.P355_KifuParserA.L___500_Parser;
using System;

namespace Grayscale.P355_KifuParserA.L500____Parser
{

    /// <summary>
    /// 変化なし
    /// </summary>
    public class KifuParserA_StateA0_Document : KifuParserA_State
    {


        public static KifuParserA_StateA0_Document GetInstance()
        {
            if (null == instance)
            {
                instance = new KifuParserA_StateA0_Document();
            }

            return instance;
        }
        private static KifuParserA_StateA0_Document instance;



        private KifuParserA_StateA0_Document()
        {
        }



        public string Execute(
            ref KifuParserA_Result result,
            Model_Taikyoku model_Taikyoku,
            out KifuParserA_State nextState,
            KifuParserA owner,
            KifuParserA_Genjo genjo,
            KwErrorHandler errH
            )
        {
            nextState = this;

            try
            {

                if (genjo.InputLine.StartsWith("position"))
                {
                    // SFEN形式の「position」コマンドが、入力欄に入っていました。
                    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                    //------------------------------------------------------------
                    // まずこのブロックで「position ～ moves 」まで(*1)を処理します。
                    //------------------------------------------------------------
                    //
                    //          *1…初期配置を作るということです。
                    // 

#if DEBUG
                    errH.Logger.WriteLine_AddMemo("（＾△＾）「" + genjo.InputLine + "」vs【" + this.GetType().Name + "】　：　ﾌﾑﾌﾑ... SFEN形式か...☆");
#endif
                    genjo.InputLine = genjo.InputLine.Substring("position".Length);
                    genjo.InputLine = genjo.InputLine.Trim();


                    nextState = KifuParserA_StateA1_SfenPosition.GetInstance();
                }
                else if (""==genjo.InputLine)
                {
                    // 異常時。
                    errH.Logger.WriteLine_Error("＼（＾ｏ＾）／「" + genjo.InputLine + "」入力がない2☆！　終わるぜ☆");
                    genjo.ToBreak_Abnormal();
                }
                else
                {
#if DEBUG
                    Playerside pside = model_Taikyoku.Kifu.CurNode.Value.KyokumenConst.KaisiPside;
                    errH.Logger.WriteLine_AddMemo("（＾△＾）「" + genjo.InputLine + "」vs【" + this.GetType().Name + "】　：　ﾌﾑﾌﾑ... positionじゃなかったぜ☆　日本式か☆？　SFENでmovesを読んだあとのプログラムに合流させるぜ☆　：　先後＝[" + pside + "]");
#endif
                    nextState = KifuParserA_StateA2_SfenMoves.GetInstance();
                }

            }
            catch (Exception ex) { Util_OwataMinister.ERROR.DonimoNaranAkirameta(ex, "棋譜ドキュメント解析中☆"); throw ex; }


            return genjo.InputLine;
        }

    }
}
