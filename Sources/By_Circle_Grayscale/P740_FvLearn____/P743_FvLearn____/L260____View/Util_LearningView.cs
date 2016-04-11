using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P163_KifuCsa____.L___250_Struct;
using Grayscale.P211_WordShogi__.L250____Masu;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P213_Komasyurui_.L250____Word;
using Grayscale.P213_Komasyurui_.L500____Util;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P226_Tree_______.L___500_Struct;
using Grayscale.P238_Seiza______.L250____Struct;
using Grayscale.P238_Seiza______.L500____Util;
using Grayscale.P247_KyokumenWra.L500____Struct;
using Grayscale.P258_UtilSky258_.L500____UtilSky;
using Grayscale.P296_ConvJsa____.L500____Converter;
using Grayscale.P321_KyokumHyoka.L___250_Struct;
using Grayscale.P324_KifuTree___.L___250_Struct;
using Grayscale.P324_KifuTree___.L250____Struct;
using Grayscale.P339_ConvKyokume.L500____Converter;
using Grayscale.P341_Ittesasu___.L___250_OperationA;
using Grayscale.P341_Ittesasu___.L125____UtilB;
using Grayscale.P341_Ittesasu___.L250____OperationA;
using Grayscale.P341_Ittesasu___.L500____UtilA;
using Grayscale.P369_ConvCsa____.L500____Converter;
using Grayscale.P743_FvLearn____.L___250_Learn;
using Grayscale.P743_FvLearn____.L250____Learn;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P743_FvLearn____.L260____View
{
    public abstract class Util_LearningView
    {

        /// <summary>
        /// 指し手一覧を、リストボックスに表示します。
        /// </summary>
        /// <param name="uc_Main"></param>
        public static void ShowSasiteList(
            LearningData learningData,
            Uc_Main uc_Main,
            KwErrorHandler errH
            )
        {
            //
            // まず、リストを空っぽにします。
            //
            uc_Main.LstSasite.Items.Clear();

            Playerside firstPside = Playerside.P1;
            KifuTree kifu1 = new KifuTreeImpl(
                new KifuNodeImpl(
                    Util_Sky258A.ROOT_SASITE,
                    new KyokumenWrapper(SkyConst.NewInstance(
                        Util_SkyWriter.New_Hirate(firstPside),
                        0//初期局面は 0手済み。
                        ))//日本の符号読取時
                )
            );
            //kifu1.AssertPside(kifu1.CurNode, "ShowSasiteList",errH);

            List<CsaKifuSasite> sasiteList = learningData.CsaKifu.SasiteList;
            foreach (CsaKifuSasite csaSasite in sasiteList)
            {
                // 開始局面
                SkyConst kaisi_Sky = kifu1.CurNode.Value.KyokumenConst;

                //
                // csaSasite を データ指し手 に変換するには？
                //
                Starbeamable nextSasite;
                {
                    Playerside pside = Util_CsaSasite.ToPside(csaSasite);

                    // 元位置
                    SyElement srcMasu = Util_CsaSasite.ToSrcMasu(csaSasite);
                    Finger figSrcKoma;
                    if (Masu_Honshogi.IsErrorBasho(srcMasu))// 駒台の "00" かも。
                    {
                        //駒台の駒。
                        Komasyurui14 utuKomasyurui = Util_Komasyurui14.NarazuCaseHandle(Util_CsaSasite.ToKomasyurui(csaSasite));// 打つ駒の種類。

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
                        figSrcKoma = Util_Sky_FingerQuery.InMasuNow(kaisi_Sky, pside, srcMasu, errH);
                    }
                    RO_Star srcKoma = Util_Starlightable.AsKoma(kaisi_Sky.StarlightIndexOf(figSrcKoma).Now);

                    // 先位置
                    SyElement dstMasu = Util_CsaSasite.ToDstMasu(csaSasite);
                    Finger figFoodKoma = Util_Sky_FingerQuery.InShogibanMasuNow(kaisi_Sky, pside, dstMasu, errH);
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
                    Starlightable dstKoma = new RO_Star(
                        pside,
                        dstMasu,
                        Util_CsaSasite.ToKomasyurui(csaSasite)
                    );

                    nextSasite = new RO_Starbeam(
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
                            nextSasite,
                            kifu1.CurNode.Value.KyokumenConst.Temezumi + 1//1手進める
                        ),
                        out ittesasuResult,
                        //kifu1,//診断用
                        errH,
                        "Utli_LearningViews#ShowSasiteList"
                    );
                    Debug.Assert(ittesasuResult.Get_SyuryoNode_OrNull != null, "ittesasuResult.Get_SyuryoNode_OrNull がヌル☆？！");
                    Util_IttesasuRoutine.Before2(
                        ref ittesasuResult,
                        errH
                    );
                    //
                    //次ノートを追加します。次ノードを、これからのカレントとします。
                    //
                    //kifu1.AssertChildPside(kifu1.CurNode.Value.ToKyokumenConst.KaisiPside, ittesasuResult.Get_SyuryoNode_OrNull.Value.ToKyokumenConst.KaisiPside);
                    Util_IttesasuRoutine.After3_ChangeCurrent(
                        kifu1,
                        Conv_SasiteStr_Sfen.ToSasiteStr_Sfen(ittesasuResult.Get_SyuryoNode_OrNull.Key),// nextSasiteStr,
                        ittesasuResult.Get_SyuryoNode_OrNull,
                        errH
                        );
                    // これで、棋譜ツリーに、構造変更があったはず。
                    //↑↑一手指し
                }


                string sfen;
                if (kifu1.CurNode.IsRoot())
                {
                    sfen = Util_CsaSasite.ToSfen(csaSasite, null);
                }
                else
                {
                    sfen = Util_CsaSasite.ToSfen(csaSasite, kifu1.CurNode.GetParentNode().Value.KyokumenConst);
                }
                HonpuSasiteListItemImpl listItem = new HonpuSasiteListItemImpl(csaSasite, sfen);
                uc_Main.LstSasite.Items.Add(listItem);
            }

            //----------------------------------------
            // ソート
            //----------------------------------------
            //List<SasiteListItemImpl> list = new List<SasiteListItemImpl>();
            //list.Sort((SasiteListItemImpl a, SasiteListItemImpl b) =>
            //{
            //    return a - b;
            //});
        }



        /// <summary>
        /// ノード情報の表示
        /// </summary>
        /// <param name="uc_Main"></param>
        public static void Aa_ShowNode2(LearningData learningData, Uc_Main uc_Main, KwErrorHandler errH)
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
        public static void Aa_ShowGohosyu2(LearningData learningData, Uc_Main uc_Main, KwErrorHandler errH)
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
                ((KifuNode)learningData.Kifu.CurNode).Foreach_ChildNodes((string key, Node<Starbeamable, KyokumenWrapper> node, ref bool toBreak) =>
                {
                    KyHyokaMeisai_Koumoku komawariMeisai;
                    KyHyokaMeisai_Koumoku ppMeisai;
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
                        Conv_SasiteStr_Jsa.ToSasiteStr_Jsa(node, errH)
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
        public static void Ittesasu_ByBtnClick(
            ref bool isRequestShowGohosyu,
            ref bool isRequestChangeKyokumenPng,
            LearningData learningData, Uc_Main uc_Main, KwErrorHandler errH)
        {
#if DEBUG
            Stopwatch sw1 = new Stopwatch();
            sw1.Start();
#endif

            //
            // リストの先頭の項目を取得したい。
            //
            if (uc_Main.LstSasite.Items.Count < 1)
            {
                goto gt_EndMethod;
            }



            // リストボックスの先頭から指し手をSFEN形式で１つ取得。
            HonpuSasiteListItemImpl item = (HonpuSasiteListItemImpl)uc_Main.LstSasite.Items[0];
            string sfen = item.Sfen;
            if (null != errH.Dlgt_OnLog1Append_or_Null)
            {
                errH.Dlgt_OnLog1Append_or_Null("sfen=" + sfen + Environment.NewLine);
            }

            //
            // 現局面の合法手は、既に読んであるとします。（棋譜ツリーのNextNodesが既に設定されていること）
            //


            //
            // 合法手の一覧は既に作成されているものとします。
            // 次の手に進みます。
            //
            Starbeamable nextSasite;
            {
                if (learningData.Kifu.CurNode.HasChildNode(sfen))
                {
                    Node<Starbeamable, KyokumenWrapper> nextNode = learningData.Kifu.CurNode.GetChildNode(sfen);
                    nextSasite = nextNode.Key;//次の棋譜ノードのキーが、指し手（きふわらべ式）になっています。

                }
                else
                {
                    nextSasite = null;
                    StringBuilder sb = new StringBuilder();
                    sb.Append("指し手[" + sfen + "]はありませんでした。\n" + learningData.DumpToAllGohosyu(learningData.Kifu.CurNode.Value.KyokumenConst));

                    //Debug.Fail(sb.ToString());
                    errH.DonimoNaranAkirameta("Util_LearningView#Ittesasu_ByBtnClick：" + sb.ToString());
                    MessageBox.Show(sb.ToString(), "エラー");
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
                    nextSasite,// FIXME: これがヌルのことがあるのだろうか？
                    learningData.Kifu.CurNode.Value.KyokumenConst.Temezumi + 1//1手進める
                ),
                out ittesasuResult,
                //this.Kifu,//診断用
                errH,
                "Util_LearningView#Ittesasu_ByBtnClick"
            );
            Debug.Assert(ittesasuResult.Get_SyuryoNode_OrNull != null, "ittesasuResult.Get_SyuryoNode_OrNull がヌル☆？！");
            Util_IttesasuRoutine.Before2(
                ref ittesasuResult,
                errH
            );
            //
            //次ノートを追加します。次ノードを、これからのカレントとします。
            //
            //this.Kifu.AssertChildPside(this.Kifu.CurNode.Value.ToKyokumenConst.KaisiPside, ittesasuResult.Get_SyuryoNode_OrNull.Value.ToKyokumenConst.KaisiPside);
            Util_IttesasuRoutine.After3_ChangeCurrent(
                learningData.Kifu,
                Conv_SasiteStr_Sfen.ToSasiteStr_Sfen(ittesasuResult.Get_SyuryoNode_OrNull.Key),
                ittesasuResult.Get_SyuryoNode_OrNull,
                errH
                );
            // これで、棋譜ツリーに、構造変更があったはず。
            //↑↑一手指し

            //----------------------------------------
            // カレント・ノードより古い、以前読んだ手を削除したい。
            //----------------------------------------
            System.Console.WriteLine("カレント・ノード＝" + Conv_SasiteStr_Sfen.ToSasiteStr_Sfen(learningData.Kifu.CurNode.Key));
            int result_removedCount = Util_KifuTree282.IzennoHenkaCutter(learningData.Kifu, errH);
            System.Console.WriteLine("削除した要素数＝" + result_removedCount);

            ////----------------------------------------
            //// 合法手一覧を作成したい。
            ////----------------------------------------
            learningData.Aa_Yomi(nextSasite, errH);
            // ノード情報の表示
            Util_LearningView.Aa_ShowNode2(uc_Main.LearningData, uc_Main, errH);

            // 合法手表示の更新を要求します。 
            isRequestShowGohosyu = true;
            // 局面PNG画像を更新を要求。
            isRequestChangeKyokumenPng = true;            

            //
            // リストの頭１個を除外します。
            //
            uc_Main.LstSasite.Items.RemoveAt(0);

#if DEBUG
            sw1.Stop();
            Console.WriteLine("一手指すボタン合計 = {0}", sw1.Elapsed);
            Console.WriteLine("────────────────────────────────────────");
#endif

        gt_EndMethod:
            ;
            //----------------------------------------
            // ボタン表示の回復
            //----------------------------------------
            if (0 < uc_Main.LstSasite.Items.Count)
            {
                uc_Main.BtnUpdateKyokumenHyoka.Enabled = true;//[局面評価更新]ボタン連打防止解除。
            }
        }

    }
}
