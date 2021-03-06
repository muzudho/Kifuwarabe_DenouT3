﻿using System.Runtime.CompilerServices;

namespace Grayscale.Kifuwarakaku.Entities.Features
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
            KifuParserA_Genjo genjo
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
            KifuParserA_Genjo genjo
            ,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "",
            [CallerLineNumber] int sourceLineNumber = 0
            );



    }
}
