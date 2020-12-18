using Grayscale.Kifuwarakaku.Entities.Logger;
using Grayscale.P325PnlTaikyoku.I250Struct;
using Grayscale.P355_KifuParserA.I500Parser;
using System;

namespace Grayscale.P355_KifuParserA.L500Parser
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
            ILogTag logTag
            )
        {
            nextState = this;

            try
            {
                if (genjo.InputLine.StartsWith("moves"))
                {
                    //>>>>> 棋譜が始まります。
#if DEUBG
                    logTag.Logger.WriteLineAddMemo("（＾△＾）「" + genjo.InputLine + "」vs【" + this.GetType().Name + "】　：　ｳﾑ☆　moves 分かるぜ☆");
#endif

                    genjo.InputLine = genjo.InputLine.Substring("moves".Length);
                    genjo.InputLine = genjo.InputLine.Trim();


                    nextState = KifuParserA_StateA2_SfenMoves.GetInstance();
                }
                else if (""==genjo.InputLine)
                {
                    // FIXME: コンピューターが先手のとき、ここにくる？

                    // 異常時。
                    Logger.WriteLineError(logTag,"＼（＾ｏ＾）／「" + genjo.InputLine + "」入力がない1☆！　終わるぜ☆");
                    genjo.ToBreak_Abnormal();
                }
                else
                {
                    // 異常時。
                    Logger.WriteLineError(logTag,"＼（＾ｏ＾）／「" + genjo.InputLine + "」vs【" + this.GetType().Name + "】　：　movesがない☆！　終わるぜ☆");
                    genjo.ToBreak_Abnormal();
                }
            }
            catch (Exception ex) { Logger.Panic(LogTags.Error, ex, "SFEN文字列の解析中。"); throw; }

            return genjo.InputLine;
        }

    }
}
