using System;
using System.Collections.Generic;
using System.Diagnostics;
using Grayscale.Kifuwarakaku.Entities.Features;
using Grayscale.Kifuwarakaku.Entities.Logging;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.Kifuwarakaku.GuiOfFvLearn.Features
{
    public abstract class Util_LearningView
    {

        /// <summary>
        /// 指し手一覧を、リストボックスに表示します。
        /// </summary>
        /// <param name="uc_Main"></param>
        public static void ShowMoveList(
            LearningData learningData,
            Uc_Main uc_Main
            )
        {
            //
            // まず、リストを空っぽにします。
            //
            uc_Main.LstMove.Items.Clear();

            Playerside firstPside = Playerside.P1;
            KifuTree kifu1 = new KifuTreeImpl(
                new KifuNodeImpl(
                    Util_Sky258A.RootMove,
                    new KyokumenWrapper(SkyConst.NewInstance(
                        Util_SkyWriter.New_Hirate(firstPside),
                        0//初期局面は 0手済み。
                        ))//日本の符号読取時
                )
            );
            //kifu1.AssertPside(kifu1.CurNode, "ShowMoveList",logTag);

            List<CsaKifuMove> moveList = learningData.CsaKifu.MoveList;
            foreach (CsaKifuMove csaMove in moveList)
            {
                // 開始局面
                SkyConst kaisi_Sky = kifu1.CurNode.Value.KyokumenConst;

                //
                // csaMove を データ指し手 に変換するには？
                //
                IMove nextMove;
                {
                    Playerside pside = UtilCsaMove.ToPside(csaMove);

                    // 元位置
                    SyElement srcMasu = UtilCsaMove.ToSrcMasu(csaMove);
                    Finger figSrcKoma;
                    if (Masu_Honshogi.IsErrorBasho(srcMasu))// 駒台の "00" かも。
                    {
                        //駒台の駒。
                        Komasyurui14 utuKomasyurui = Util_Komasyurui14.NarazuCaseHandle(UtilCsaMove.ToKomasyurui(csaMove));// 打つ駒の種類。

                        Okiba komadai;
                        switch (pside)
                        {
                            case Playerside.P1: komadai = Okiba.Sente_Komadai; break;
                            case Playerside.P2: komadai = Okiba.Gote_Komadai; break;
                            default: komadai = Okiba.Empty; break;
                        }

                        figSrcKoma = Util_Sky_FingersQuery.InOkibaPsideKomasyuruiNow(kaisi_Sky, komadai, pside, utuKomasyurui).ToFirst();
                    }
                    else
                    {
                        // 盤上の駒。
                        figSrcKoma = Util_Sky_FingerQuery.InMasuNow(kaisi_Sky, pside, srcMasu);
                    }
                    RO_Star srcKoma = Util_Starlightable.AsKoma(kaisi_Sky.StarlightIndexOf(figSrcKoma).Now);

                    // 先位置
                    SyElement dstMasu = UtilCsaMove.ToDstMasu(csaMove);
                    Finger figFoodKoma = Util_Sky_FingerQuery.InShogibanMasuNow(kaisi_Sky, pside, dstMasu);
                    Komasyurui14 foodKomasyurui;
                    if (figFoodKoma == Fingers.Error_1)
                    {
                        // 駒のない枡
                        foodKomasyurui = Komasyurui14.H00_Null___;//取った駒無し。
                    }
                    else
                    {
                        // 駒のある枡
                        foodKomasyurui = Util_Starlightable.AsKoma(kaisi_Sky.StarlightIndexOf(figFoodKoma).Now).Komasyurui;//取った駒有り。
                    }
                    IMoveSource dstKoma = new RO_Star(
                        pside,
                        dstMasu,
                        UtilCsaMove.ToKomasyurui(csaMove)
                    );

                    nextMove = new RO_Starbeam(
                        srcKoma,// 移動元
                        dstKoma,// 移動先
                        foodKomasyurui////取った駒
                    );
                }

                {
                    //----------------------------------------
                    // 一手指したい。
                    //----------------------------------------
                    //
                    //↓↓一手指し
                    IttesasuResult ittesasuResult;
                    Util_IttesasuRoutine.Before1(
                        new IttesasuArgImpl(
                            kifu1.CurNode.Value,
                            ((KifuNode)kifu1.CurNode).Value.KyokumenConst.KaisiPside,
                            nextMove,
                            kifu1.CurNode.Value.KyokumenConst.Temezumi + 1//1手進める
                        ),
                        out ittesasuResult,
                        //kifu1,//診断用
                        "Utli_LearningViews#ShowMoveList"
                    );
                    Debug.Assert(ittesasuResult.Get_SyuryoNode_OrNull != null, "ittesasuResult.Get_SyuryoNode_OrNull がヌル☆？！");
                    Util_IttesasuRoutine.Before2(
                        ref ittesasuResult
                    );
                    //
                    //次ノートを追加します。次ノードを、これからのカレントとします。
                    //
                    //kifu1.AssertChildPside(kifu1.CurNode.Value.ToKyokumenConst.KaisiPside, ittesasuResult.Get_SyuryoNode_OrNull.Value.ToKyokumenConst.KaisiPside);
                    Util_IttesasuRoutine.After3_ChangeCurrent(
                        kifu1,
                        ConvMoveStrSfen.ToMoveStrSfen(ittesasuResult.Get_SyuryoNode_OrNull.Key),// nextMoveStr,
                        ittesasuResult.Get_SyuryoNode_OrNull
                        );
                    // これで、棋譜ツリーに、構造変更があったはず。
                    //↑↑一手指し
                }


                string sfen;
                if (kifu1.CurNode.IsRoot())
                {
                    sfen = UtilCsaMove.ToSfen(csaMove, null);
                }
                else
                {
                    sfen = UtilCsaMove.ToSfen(csaMove, kifu1.CurNode.GetParentNode().Value.KyokumenConst);
                }
                HonpuMoveListItemImpl listItem = new HonpuMoveListItemImpl(csaMove, sfen);
                uc_Main.LstMove.Items.Add(listItem);
            }

            //----------------------------------------
            // ソート
            //----------------------------------------
            //List<MoveListItemImpl> list = new List<MoveListItemImpl>();
            //list.Sort((MoveListItemImpl a, MoveListItemImpl b) =>
            //{
            //    return a - b;
            //});
        }



        /// <summary>
        /// ノード情報の表示
        /// </summary>
        /// <param name="uc_Main"></param>
        public static void Aa_ShowNode2(LearningData learningData, Uc_Main uc_Main)
        {
            // 手目済み
            uc_Main.TxtTemezumi.Text = learningData.Kifu.CurNode.Value.KyokumenConst.Temezumi.ToString();

            // 総ノード数
            uc_Main.TxtAllNodesCount.Text = learningData.Kifu.CountAllNodes().ToString();

            // 合法手の数
            uc_Main.TxtGohosyuTe.Text = ((KifuNode)learningData.Kifu.CurNode).Count_ChildNodes.ToString();
        }

        /// <summary>
        /// 合法手リストの表示
        /// </summary>
        /// <param name="uc_Main"></param>
        public static void Aa_ShowGohosyu2(LearningData learningData, Uc_Main uc_Main)
        {
            //----------------------------------------
            // フォルダー作成
            //----------------------------------------
            //this.Kifu.CreateAllFolders(Const_Filepath.LOGS + "temp", 4);

            {

                //----------------------------------------
                // 合法手のリストを作成
                //----------------------------------------
                List<GohosyuListItem> list = new List<GohosyuListItem>();
                //uc_Main.LstGohosyu.Items.Clear();
                int itemNumber = 0;
                ((KifuNode)learningData.Kifu.CurNode).Foreach_ChildNodes((string key, Node<IMove, KyokumenWrapper> node, ref bool toBreak) =>
                {
#if DEBUG || LEARN
                    KyHyokaMeisai_Koumoku komawariMeisai;
                    KyHyokaMeisai_Koumoku ppMeisai;
#endif
                    learningData.DoScoreing_ForLearning(
                        (KifuNode)node
#if DEBUG || LEARN
,
                        out komawariMeisai,
                        out ppMeisai
#endif
);

                    GohosyuListItem item = new GohosyuListItem(
                        itemNumber,
                        key,
                        ConvMoveStrJsa.ToMoveStrJsa(node)
#if DEBUG || LEARN
,
                        komawariMeisai,
                        ppMeisai
#endif
);
                    list.Add(item);

                    itemNumber++;
                });

                //----------------------------------------
                // ソート
                //----------------------------------------
                //
                // 先手は正の数、後手は負の数で、絶対値の高いもの順。
                list.Sort((GohosyuListItem a, GohosyuListItem b) =>
                {
                    int result;

                    int aScore =
#if DEBUG || LEARN
                        (int)(
                        a.KomawariMeisai.UtiwakeValue +
                        a.PpMeisai.UtiwakeValue);
#else
 0;
#endif

                    int bScore =
#if DEBUG || LEARN
 (int)(
                        b.KomawariMeisai.UtiwakeValue +
                        b.PpMeisai.UtiwakeValue);
#else
 0;
#endif

                    switch (learningData.Kifu.CurNode.Value.KyokumenConst.KaisiPside)
                    {
                        case Playerside.P1: result = bScore - aScore; break;
                        case Playerside.P2: result = aScore - bScore; break;
                        default: result = 0; break;
                    }
                    return result;
                });



                uc_Main.LstGohosyu.Items.Clear();
                uc_Main.LstGohosyu.Items.AddRange(list.ToArray());
                //foreach (GohosyuListItem item in list)
                //{
                //    uc_Main.LstGohosyu.Items.Add(item);
                //}
            }
        }



        /// <summary>
        /// [一手指す]ボタン。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void IttesasuByBtnClick(
            ref bool isRequestShowGohosyu,
            ref bool isRequestChangeKyokumenPng,
            LearningData learningData, Uc_Main ucMain)
        {
#if DEBUG
            Stopwatch sw1 = new Stopwatch();
            sw1.Start();
#endif
            if (ucMain is null) throw new ArgumentNullException(nameof(ucMain));
            // if (logTag is null) throw new ArgumentNullException(nameof(logTag));

            //
            // リストの先頭の項目を取得したい。
            //
            if (ucMain.LstMove.Items.Count < 1)
            {
                goto gt_EndMethod;
            }

            // リストボックスの先頭から指し手をSFEN形式で１つ取得。
            HonpuMoveListItemImpl item = (HonpuMoveListItemImpl)ucMain.LstMove.Items[0];
            string sfen = item.Sfen;

            // (2020-12-18 fri)この機能むずかしいからいったん廃止☆（＾～＾）
            // logTag.OnAppendLog?.Invoke("sfen=" + sfen + Environment.NewLine);

            //
            // 現局面の合法手は、既に読んであるとします。（棋譜ツリーのNextNodesが既に設定されていること）
            //


            //
            // 合法手の一覧は既に作成されているものとします。
            // 次の手に進みます。
            //
            IMove nextMove;
            {
                if (learningData.Kifu.CurNode.HasChildNode(sfen))
                {
                    Node<IMove, KyokumenWrapper> nextNode = learningData.Kifu.CurNode.GetChildNode(sfen);
                    nextMove = nextNode.Key;//次の棋譜ノードのキーが、指し手（きふわらべ式）になっています。

                }
                else
                {
                    nextMove = null;
                    throw new Exception($@"指し手[{sfen}]はありませんでした。
{learningData.DumpToAllGohosyu(learningData.Kifu.CurNode.Value.KyokumenConst)}");
                }
            }

            //----------------------------------------
            // 一手指したい。
            //----------------------------------------
            //↓↓一手指し
            IttesasuResult ittesasuResult;
            Util_IttesasuRoutine.Before1(
                new IttesasuArgImpl(
                    learningData.Kifu.CurNode.Value,
                    ((KifuNode)learningData.Kifu.CurNode).Value.KyokumenConst.KaisiPside,
                    nextMove,// FIXME: これがヌルのことがあるのだろうか？
                    learningData.Kifu.CurNode.Value.KyokumenConst.Temezumi + 1//1手進める
                ),
                out ittesasuResult,
                //this.Kifu,//診断用
                "Util_LearningView#Ittesasu_ByBtnClick"
            );
            Debug.Assert(ittesasuResult.Get_SyuryoNode_OrNull != null, "ittesasuResult.Get_SyuryoNode_OrNull がヌル☆？！");
            Util_IttesasuRoutine.Before2(
                ref ittesasuResult
            );
            //
            //次ノートを追加します。次ノードを、これからのカレントとします。
            //
            //this.Kifu.AssertChildPside(this.Kifu.CurNode.Value.ToKyokumenConst.KaisiPside, ittesasuResult.Get_SyuryoNode_OrNull.Value.ToKyokumenConst.KaisiPside);
            Util_IttesasuRoutine.After3_ChangeCurrent(
                learningData.Kifu,
                ConvMoveStrSfen.ToMoveStrSfen(ittesasuResult.Get_SyuryoNode_OrNull.Key),
                ittesasuResult.Get_SyuryoNode_OrNull
                );
            // これで、棋譜ツリーに、構造変更があったはず。
            //↑↑一手指し

            //----------------------------------------
            // カレント・ノードより古い、以前読んだ手を削除したい。
            //----------------------------------------
            Logger.Trace("カレント・ノード＝" + ConvMoveStrSfen.ToMoveStrSfen(learningData.Kifu.CurNode.Key));
            int result_removedCount = UtilKifuTree282.IzennoHenkaCutter(learningData.Kifu);
            Logger.Trace("削除した要素数＝" + result_removedCount);

            ////----------------------------------------
            //// 合法手一覧を作成したい。
            ////----------------------------------------
            learningData.Aa_Yomi(nextMove);
            // ノード情報の表示
            Util_LearningView.Aa_ShowNode2(ucMain.LearningData, ucMain);

            // 合法手表示の更新を要求します。 
            isRequestShowGohosyu = true;
            // 局面PNG画像を更新を要求。
            isRequestChangeKyokumenPng = true;

            //
            // リストの頭１個を除外します。
            //
            ucMain.LstMove.Items.RemoveAt(0);

#if DEBUG
            sw1.Stop();
            Logger.Trace("一手指すボタン合計 = {0}", sw1.Elapsed);
            Logger.Trace("────────────────────────────────────────");
#endif

        gt_EndMethod:
            ;
            //----------------------------------------
            // ボタン表示の回復
            //----------------------------------------
            if (0 < ucMain.LstMove.Items.Count)
            {
                ucMain.BtnUpdateKyokumenHyoka.Enabled = true;//[局面評価更新]ボタン連打防止解除。
            }
        }

    }
}
