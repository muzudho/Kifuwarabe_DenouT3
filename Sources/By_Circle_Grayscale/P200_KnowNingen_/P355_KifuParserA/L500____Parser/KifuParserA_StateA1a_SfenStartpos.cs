using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P003_Log________.L500____Struct;
using Grayscale.P325_PnlTaikyoku.L___250_Struct;
using Grayscale.P355_KifuParserA.L___500_Parser;
using System;

namespace Grayscale.P355_KifuParserA.L500____Parser
{
    /// <summary>
    /// 平手の初期配置です。
    /// </summary>
    public class KifuParserA_StateA1a_SfenStartpos : KifuParserA_State
    {


        public static KifuParserA_StateA1a_SfenStartpos GetInstance()
        {
            if (null == instance)
            {
                instance = new KifuParserA_StateA1a_SfenStartpos();
            }

            return instance;
        }
        private static KifuParserA_StateA1a_SfenStartpos instance;



        private KifuParserA_StateA1a_SfenStartpos()
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
                if (genjo.InputLine.StartsWith("moves"))
                {
                    //>>>>> 棋譜が始まります。
#if DEUBG
                    errH.Logger.WriteLine_AddMemo("（＾△＾）「" + genjo.InputLine + "」vs【" + this.GetType().Name + "】　：　ｳﾑ☆　moves 分かるぜ☆");
#endif

                    genjo.InputLine = genjo.InputLine.Substring("moves".Length);
                    genjo.InputLine = genjo.InputLine.Trim();


                    nextState = KifuParserA_StateA2_SfenMoves.GetInstance();
                }
                else if (""==genjo.InputLine)
                {
                    // FIXME: コンピューターが先手のとき、ここにくる？

                    // 異常時。
                    errH.Logger.WriteLine_Error("＼（＾ｏ＾）／「" + genjo.InputLine + "」入力がない1☆！　終わるぜ☆");
                    genjo.ToBreak_Abnormal();
                }
                else
                {
                    // 異常時。
                    errH.Logger.WriteLine_Error("＼（＾ｏ＾）／「" + genjo.InputLine + "」vs【" + this.GetType().Name + "】　：　movesがない☆！　終わるぜ☆");
                    genjo.ToBreak_Abnormal();
                }
            }
            catch (Exception ex) { Util_OwataMinister.ERROR.DonimoNaranAkirameta(ex, "SFEN文字列の解析中。"); throw ex; }

            return genjo.InputLine;
        }

    }
}
