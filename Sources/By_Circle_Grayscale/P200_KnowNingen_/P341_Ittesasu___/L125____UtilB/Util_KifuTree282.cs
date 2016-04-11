using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P226_Tree_______.L___500_Struct;
using Grayscale.P239_ConvWords__.L500____Converter;
using Grayscale.P247_KyokumenWra.L500____Struct;
using Grayscale.P324_KifuTree___.L___250_Struct;
using Grayscale.P324_KifuTree___.L250____Struct;
using Grayscale.P339_ConvKyokume.L500____Converter;
using System.Collections.Generic;
using System.Diagnostics;

namespace Grayscale.P341_Ittesasu___.L125____UtilB
{

    /// <summary>
    /// 棋譜ツリーのユーティリティー。
    /// </summary>
    public abstract class Util_KifuTree282
    {

        /// <summary>
        /// 『以前の変化カッター』
        /// 
        /// 本譜を残して、カレントノードより以前の変化は　ツリーから削除します。
        /// </summary>
        public static int IzennoHenkaCutter(
            KifuTree kifu_mutable,
            KwErrorHandler errH
            )
        {
            int result_removedCount = 0;

            //----------------------------------------
            // 本譜以外の変化を削除します。
            //----------------------------------------

            if (kifu_mutable.CurNode.IsRoot())
            {
                //----------------------------------------
                // ルートノードでは何もできません。
                //----------------------------------------
                goto gt_EndMethod;
            }

            //----------------------------------------
            // 本譜の手
            //----------------------------------------
            string sasiteStr = Conv_SasiteStr_Sfen.ToSasiteStr_Sfen(kifu_mutable.CurNode.Key);


            //----------------------------------------
            // １手前の分岐点
            //----------------------------------------
            Node<Starbeamable, KyokumenWrapper> parentNode = kifu_mutable.CurNode.GetParentNode();

            //----------------------------------------
            // 選ばなかった変化を、ここに入れます。
            //----------------------------------------
            List<string> removeeList = new List<string>();

            //----------------------------------------
            // 選んだ変化と、選ばなかった変化の一覧
            //----------------------------------------
            parentNode.Foreach_ChildNodes((string key1, Node<Starbeamable, KyokumenWrapper> nextNode1, ref bool toBreak1) =>
            {
                if (key1 == sasiteStr)
                {
                    //----------------------------------------
                    // 本譜の手はスキップ
                    //----------------------------------------
                    //System.Console.WriteLine("残すsasiteStr=[" + sasiteStr + "] key1=[" + key1 + "] ★");
                    goto gt_Next1;
                }
                //else
                //{
                //    System.Console.WriteLine("残すsasiteStr=[" + sasiteStr + "] key1=[" + key1 + "]");
                //}

                //----------------------------------------
                // 選ばなかった変化をピックアップ
                //----------------------------------------
                removeeList.Add(key1);

            gt_Next1:
                ;
            });


            //----------------------------------------
            // どんどん削除
            //----------------------------------------
            result_removedCount = removeeList.Count;
            foreach (string key in removeeList)
            {
                parentNode.RemoveChild(key);
            }

        gt_EndMethod:
            return result_removedCount;
        }


        /// <summary>
        /// 新しいノードを、次ノードとして追加します。
        /// そして、追加した新しいノードを、カレント・ノードとします。
        /// </summary>
        /// <param name="nextNode_and_nextCurrent"></param>
        public static void AppendChild_And_ChangeCurrentToChild(
            KifuTree kifuRef,
            KifuNode nextNode_and_nextCurrent,
            string hint,
            KwErrorHandler errH
            )
        {
            string sasiteStr = Conv_SasiteStr_Sfen.ToSasiteStr_Sfen(nextNode_and_nextCurrent.Key);

            if (!((KifuNode)kifuRef.CurNode).HasTuginoitte(sasiteStr))
            {
                //----------------------------------------
                // 次ノート追加
                //----------------------------------------
                kifuRef.GetSennititeCounter().CountUp_New(Conv_Sky.ToKyokumenHash(nextNode_and_nextCurrent.Value.KyokumenConst), hint+"/AppendChild_And_ChangeCurrentToChild");
                ((KifuNode)kifuRef.CurNode).PutTuginoitte_New(nextNode_and_nextCurrent);
            }

            kifuRef.SetCurNode( nextNode_and_nextCurrent);//次ノードを、これからのカレントとします。
            Debug.Assert(kifuRef.CurNode != null, "カレントノードがヌル。");
        }

        /*
        /// <summary>
        /// 取った駒を差替えます。
        /// 
        /// 棋譜読取時用です。マウス操作時は、流れが異なるので使えません。
        /// </summary>
        public static void AppendChildB_Swap(
            Komasyurui14 tottaSyurui,
            SkyConst src_Sky,
            KifuTree kifu282,
            out Node<ShootingStarlightable, KyokumenWrapper> out_swapedNode,
            string hint,
            KwErrorHandler errH
            )
        {
            out_swapedNode = null;

            if (kifu282.CurNode.IsRoot())
            {
                // ルート・ノードなら
                goto gt_EndMethod;
                // 子要素をスワップするためには親要素が必要だが、ルートには親ノードがないので、このあとの操作はできません。
            }

            if (null == src_Sky)
            {
                string message = "ノードを追加しようとしましたが、指定されたnewSkyがヌルです。";
                Exception ex = new Exception(message);
                OwataMinister.ERROR.DonimoNaranAkirameta(ex, "取った駒を差替え中☆"); throw ex;
            }

            // 現手番
            Playerside genTebanside = ((KifuNode)kifu282.CurNode).Tebanside;
            // 現ノード削除。元のキーは退避。
            ShootingStarlightable motoKey = (ShootingStarlightable)kifu282.PopCurrentNode().Key;

            // 元のキーの、取った駒の種類だけを差替えます。
            RO_ShootingStarlight swapedSasite = Util_Sky258A.BuildSasite(motoKey.LongTimeAgo, motoKey.Now, tottaSyurui);

            // キーを差替えたノード
            out_swapedNode = new KifuNodeImpl(swapedSasite, new KyokumenWrapper(src_Sky), genTebanside);

            System.Diagnostics.Debug.Assert(!kifu282.CurNode.ContainsKey_ChildNodes(Util_Sky278.TranslateSasite_StarlightToText(out_swapedNode.Key)));


            // さきほど　カレントノードを削除したので、
            // 今、カレントノードは、１つ前のノードになっています。
            // ここに、差替えたノードを追加します。
            kifu282.CurNode.Add_ChildNode(Util_Sky278.TranslateSasite_StarlightToText(out_swapedNode.Key), out_swapedNode);
            out_swapedNode.ParentNode = kifu282.CurNode;

            errH.Logger.WriteLine_AddMemo("リンクトリストの、最終ノードは差し替えられた hint=[" + hint + "] item=[" + Util_Sky278.TranslateSasite_StarlightToText(swapedSasite) + "]");
        // memberName=[" + memberName + "] sourceFilePath=[" + sourceFilePath + "] sourceLineNumber=[" + sourceLineNumber + "]

        gt_EndMethod:
            ;
        }
        */

        /// <summary>
        /// ************************************************************************************************************************
        /// [ここから採譜]機能
        /// ************************************************************************************************************************
        /// </summary>
        public static void SetStartpos_KokokaraSaifu(KifuTree kifu, Playerside pside, KwErrorHandler errH)
        {

            //------------------------------------------------------------
            // 棋譜を空に
            //------------------------------------------------------------
            kifu.Clear();
            kifu.SetProperty(Word_KifuTree.PropName_Startpos, Conv_KifuNode.ToSfenstring((KifuNode)kifu.CurNode, pside, errH));
        }

    }
}
