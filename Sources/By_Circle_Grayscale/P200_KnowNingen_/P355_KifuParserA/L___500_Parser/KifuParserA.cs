using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P325_PnlTaikyoku.L___250_Struct;
using System.Runtime.CompilerServices;

namespace Grayscale.P355_KifuParserA.L___500_Parser
{

    public interface KifuParserA
    {

        /// <summary>
        /// １ステップずつ実行します。
        /// </summary>
        /// <param name="inputLine"></param>
        /// <param name="kifu"></param>
        /// <param name="larabeLogger"></param>
        /// <returns></returns>
        string Execute_Step(
            ref KifuParserA_Result result,
            Model_Taikyoku model_Taikyoku,
            KifuParserA_Genjo genjo,
            KwErrorHandler errH
            ,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
            );

        /// <summary>
        /// 最初から最後まで実行します。（きふわらべCOMP用）
        /// </summary>
        /// <param name="inputLine"></param>
        /// <param name="kifu"></param>
        /// <param name="larabeLogger"></param>
        void Execute_All(
            ref KifuParserA_Result result,
            Model_Taikyoku model_Taikyoku,
            KifuParserA_Genjo genjo,
            KwErrorHandler errH
            ,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
            );



    }
}
