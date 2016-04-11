#if DEBUG
using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P035_Collection_.L500____Struct;
using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P157_KyokumenPng.L___500_Struct;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P222_Log_Kaisetu.L250____Struct;
using Grayscale.P226_Tree_______.L___500_Struct;
using Grayscale.P247_KyokumenWra.L500____Struct;
using Grayscale.P266_KyokumMoves.L500____Util;
using Grayscale.P324_KifuTree___.L___250_Struct;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

using Grayscale.P266_KyokumMoves.L250____Log;

namespace Grayscale.P542_Scoreing___.L061____Util
{
    public abstract class Util_LogBuilder510
    {

        /// <summary>
        /// 盤１個分のログ。
        /// </summary>
        public static void Build_LogBoard(
            Node<Starbeamable, KyokumenWrapper> node_forLog,
            string nodePath,
            KifuNode niniNode,//任意のノード
            //KifuTree kifu_forAssert,
            KyokumenPngEnvironment reportEnvironment,
            KaisetuBoards logF_kiki,
            KwErrorHandler errH
            )
        {
            //
            // HTMLﾛｸﾞ
            //
            if (logF_kiki.boards.Count < 30)//出力件数制限
            {
                KaisetuBoard logBrd_move1 = new KaisetuBoard();

                List_OneAndMulti<Finger, SySet<SyElement>> komaBETUSusumeruMasus;
                Playerside pside = niniNode.Value.KyokumenConst.KaisiPside;
                Util_KyokumenMoves.LA_Split_KomaBETUSusumeruMasus(
                    2,
                    //node_forLog,
                    out komaBETUSusumeruMasus,
                    true,//本将棋
                    niniNode.Value.KyokumenConst,//現在の局面
                    pside,
                    false
//#if DEBUG
                    ,
                    new MmLogGenjoImpl(
                        0,//読み開始手目済み
                        logBrd_move1,
                        0,//現在の手済み
                        niniNode.Key,
                        errH
                    )
//#endif
                );

                logBrd_move1.sasiteOrNull = niniNode.Key;

                logBrd_move1.YomikaisiTemezumi = niniNode.Value.KyokumenConst.Temezumi;//読み開始手目済み    // int.MinValue;
                logBrd_move1.Temezumi = int.MinValue;
                logBrd_move1.Score = (int)niniNode.Score;

                logF_kiki.boards.Add(logBrd_move1);
            }
        }


    }
}
#endif
