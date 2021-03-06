﻿using Grayscale.Kifuwarakaku.Entities.Logging;

namespace Grayscale.Kifuwarakaku.Entities.Features
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
            KifuParserA_Genjo genjo
            )
        {
            nextState = this;

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
                Logger.Trace($"（＾△＾）「{genjo.InputLine}」vs【{this.GetType().Name}】　：　ﾌﾑﾌﾑ... SFEN形式か...☆");
#endif
                genjo.InputLine = genjo.InputLine.Substring("position".Length);
                genjo.InputLine = genjo.InputLine.Trim();


                nextState = KifuParserA_StateA1_SfenPosition.GetInstance();
            }
            else if ("" == genjo.InputLine)
            {
                // 異常時。
                Logger.Error($"＼（＾ｏ＾）／「{genjo.InputLine}」入力がない2☆！　終わるぜ☆");
                genjo.ToBreak_Abnormal();
            }
            else
            {
#if DEBUG
                Playerside pside = model_Taikyoku.Kifu.CurNode.Value.KyokumenConst.KaisiPside;
                Logger.Trace($"（＾△＾）「{genjo.InputLine}」vs【{this.GetType().Name}】　：　ﾌﾑﾌﾑ... positionじゃなかったぜ☆　日本式か☆？　SFENでmovesを読んだあとのプログラムに合流させるぜ☆　：　先後＝[{pside}]");
#endif
                nextState = KifuParserA_StateA2_SfenMoves.GetInstance();
            }

            return genjo.InputLine;
        }

    }
}
