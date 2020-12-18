#if DEBUG
    using Grayscale.Kifuwarakaku.Entities.Logger;
using Grayscale.P035Collection.L500Struct;
using Grayscale.P056Syugoron.I250Struct;
using Grayscale.P157KyokumenPng.I500Struct;
using Grayscale.P211WordShogi.L500Word;
using Grayscale.P218Starlight.I500Struct;
using Grayscale.P222LogKaisetu.L250Struct;
using Grayscale.P226Tree.I500Struct;
using Grayscale.P247KyokumenWra.L500Struct;
using Grayscale.P266KyokumMoves.L500Util;
using Grayscale.P324KifuTree.I250Struct;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

using Grayscale.P266KyokumMoves.L250Log;

namespace Grayscale.P542Scoreing.L061Util
{
    public abstract class Util_LogBuilder510
    {

        /// <summary>
        /// 盤１個分のログ。
        /// </summary>
        public static void Build_LogBoard(
            Node<IMove, KyokumenWrapper> node_forLog,
            string nodePath,
            KifuNode niniNode,//任意のノード
            //KifuTree kifu_forAssert,
            KyokumenPngEnvironment reportEnvironment,
            KaisetuBoards logF_kiki,
            IErrorController logTag
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
                        logTag
                    )
//#endif
                );

                logBrd_move1.moveOrNull = niniNode.Key;

                logBrd_move1.YomikaisiTemezumi = niniNode.Value.KyokumenConst.Temezumi;//読み開始手目済み    // int.MinValue;
                logBrd_move1.Temezumi = int.MinValue;
                logBrd_move1.Score = (int)niniNode.Score;

                logF_kiki.boards.Add(logBrd_move1);
            }
        }


    }
}
#endif
