﻿using System.Text;

namespace Grayscale.Kifuwarakaku.Entities.Features
{

    /// <summary>
    /// 記録係
    /// </summary>
    public abstract class Util_KirokuGakari
    {
        /// <summary>
        /// 棋譜データを元に、符号リスト１(*1)を出力します。
        /// 
        ///     *1…「▲２六歩△８四歩▲７六歩」といった書き方。
        /// 
        /// 
        /// FIXME: 将棋GUII には要るものの、将棋エンジンには要らないはず。
        /// 
        /// </summary>
        /// <param name="fugoList"></param>
        public static string ToJsaFugoListString(
            KifuTree src_kifu,
            string hint
            )
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("position ");

            sb.Append(src_kifu.GetProperty(Word_KifuTree.PropName_Startpos));
            sb.Append(" moves ");

            // 採譜用に、新しい対局を用意します。
            KifuTree saifuKifu;
            {
                saifuKifu = new KifuTreeImpl(
                        new KifuNodeImpl(
                            Util_Sky258A.RootMove,
                            new KyokumenWrapper(Util_SkyWriter.New_Hirate(Playerside.P1))//日本の符号読取時
                        )
                );
                saifuKifu.Clear();// 棋譜を空っぽにします。
                saifuKifu.SetProperty(Word_KifuTree.PropName_Startpos, "startpos");//平手の初期局面 // FIXME:平手とは限らないのでは？
            }

            src_kifu.ForeachHonpu(src_kifu.CurNode, (int temezumi, KyokumenWrapper kWrap, Node<IMove, KyokumenWrapper> node, ref bool toBreak) =>
            {
                if (0 == temezumi)
                {
                    // 初期局面はスキップします。
                    goto gt_EndLoop;
                }

                //------------------------------
                // 符号の追加（記録係）
                //------------------------------
                KyokumenWrapper saifu_kWrap = saifuKifu.CurNode.Value;


                // 採譜用新ノード
                KifuNode saifu_newChild = new KifuNodeImpl(
                    node.Key,
                    new KyokumenWrapper(SkyConst.NewInstance_ReversePside(
                        saifu_kWrap.KyokumenConst,
                        temezumi//FIXME:合ってるのかどうか。
                        ))
                );

                // 記録係り用棋譜（採譜）
                UtilKifuTree282.AppendChild_And_ChangeCurrentToChild(saifuKifu, saifu_newChild, $"{hint}/ToJsaKifuText");// 新しい次ノードを追加。次ノードを、これからカレントとする。

                // 後手の符号がまだ含まれていない。
                string jsaFugoStr = ConvMoveStrJsa.ToMoveStrJsa(saifu_newChild
                    //saifu_newChild.Value,
                    );
                //sb.Append(Conv_MoveStr_Jsa.ToMoveStr_Jsa(node, saifu_kWrap));
                sb.Append(jsaFugoStr);

            gt_EndLoop:
                ;
            });

            return sb.ToString();
        }


        /// <summary>
        /// 棋譜データを元に、符号リスト２(*1)を出力します。
        /// 
        ///     *1…「position startpos moves 7g7f 3c3d 2g2f」といった書き方。
        /// 
        /// </summary>
        /// <param name="fugoList"></param>
        public static string ToSfen_PositionCommand(KifuTree src_kifu)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("position ");
            sb.Append(src_kifu.GetProperty(Word_KifuTree.PropName_Startpos));
            sb.Append(" moves ");

            // 本譜
            int count = 0;
            src_kifu.ForeachHonpu(src_kifu.CurNode, (int temezumi, KyokumenWrapper kWrap, Node<IMove, KyokumenWrapper> node, ref bool toBreak) =>
            {
                if (0 == temezumi)
                {
                    // 初期局面はスキップします。
                    goto gt_EndLoop;
                }

                sb.Append(ConvMoveStrSfen.ToMoveStrSfen(node.Key));

                //// TODO:デバッグ用
                //switch (move.TottaKoma)
                //{
                //    case KomaSyurui.UNKNOWN:
                //    case KomaSyurui.TOTTA_KOMA_NASI:
                //        break;
                //    default:
                //        sb.Append("(");
                //        sb.Append(Converter.SyuruiToSfen(move.Pside,move.TottaKoma));
                //        sb.Append(")");
                //        break;
                //}

                sb.Append(" ");


            gt_EndLoop:
                count++;
            });

            return sb.ToString();
        }

    }
}
