﻿using System.Collections.Generic;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.Kifuwarakaku.Entities.Features
{


    /// <summary>
    /// 基本的に、本将棋でなければ　正しく使えません。
    /// </summary>
    public abstract class Util_LegalMove
    {

        /// <summary>
        /// 指定された手の中から、王手局面を除外します。
        /// 
        /// 王手回避漏れを防ぎたいんだぜ☆
        /// </summary>
        /// <param name="km_available">自軍の各駒の移動できる升セット</param>
        /// <param name="sbGohosyu"></param>
        public static Maps_OneAndOne<Finger, SySet<SyElement>> LA_RemoveMate(
            int yomikaisiTemezumi,
            bool isHonshogi,
            Maps_OneAndMulti<Finger, IMove> genTeban_komabetuAllMove1,// 指定局面で、どの駒が、どんな手を指すことができるか
            SkyConst src_Sky,//指定局面。

#if DEBUG
            KaisetuBoards logF_kiki,
#endif

            string hint)
        {
            Node<IMove, KyokumenWrapper> hubNode = ConvStarbetuMoves.ToNextNodes_AsHubNode(
                genTeban_komabetuAllMove1,
                src_Sky
                );// ハブ・ノード自身はダミーノードなんだが、子ノードに、次のノードが入っている。
            Util_NodeAssert361.AssertNariMove(hubNode, "#LA_RemoveMate(1)");//ここはok
#if DEBUG
            Util_LegalMove.Log1(hubNode, src_Sky.Temezumi, hint);
#endif

            if (isHonshogi)
            {
                // 王手が掛かっている局面を除きます。

                Util_LegalMove.LAA_RemoveNextNode_IfMate(
                    yomikaisiTemezumi,
                    hubNode,
                    src_Sky.Temezumi,
                    src_Sky.KaisiPside

#if DEBUG
                    ,logF_kiki
#endif
                    );
            }
            Util_NodeAssert361.AssertNariMove(hubNode, "#LA_RemoveMate(2)王手局面削除直後");//ここはok


            // 「指し手一覧」を、「星別の全指し手」に分けます。
            Maps_OneAndMulti<Finger, IMove> starbetuAllMoves2 = Util_Sky258A.SplitMoveByStar(src_Sky, hubNode);
            Util_Sasu269.AssertNariMove(starbetuAllMoves2, "#LA_RemoveMate(3)更に変換後");//ここはok

            //
            // 「星別の指し手一覧」を、「星別の進むマス一覧」になるよう、データ構造を変換します。
            //
            Maps_OneAndOne<Finger, SySet<SyElement>> starbetuSusumuMasus = new Maps_OneAndOne<Finger, SySet<SyElement>>();// 「どの星を、どこに進める」の一覧
            foreach (KeyValuePair<Finger, List<IMove>> entry in starbetuAllMoves2.Items)
            {
                Finger finger = entry.Key;
                List<IMove> teList = entry.Value;

                // ポテンシャル・ムーブを調べます。
                SySet<SyElement> masus_PotentialMove = new SySet_Default<SyElement>("ポテンシャルムーブ");
                foreach (IMove te in teList)
                {
                    RO_Star koma = Util_Starlightable.AsKoma(te.Now);

                    masus_PotentialMove.AddElement(koma.Masu);
                }

                if (!masus_PotentialMove.IsEmptySet())
                {
                    // 空でないなら
                    Util_Sky258A.AddOverwrite(starbetuSusumuMasus, finger, masus_PotentialMove);
                }
            }

            return starbetuSusumuMasus;
        }

        private static void Log1(
            Node<IMove, KyokumenWrapper> hubNode,
            int temezumi_yomiGenTeban,
            string hint
            )
        {
            bool enableLog = false;//logTag.Logger.Enable
            Util_GraphicalLog.WriteHtml5(
                enableLog,
                "Util_LegalMove(王手回避漏れ02)王手を回避するかどうかに関わらず、ひとまず全ての次の手",
                "[" +
                ((KifuNode)hubNode).Json_NextNodes_MultiSky(
                    $"(王手回避漏れ02.{temezumi_yomiGenTeban}手目)",
                    $"{hint}_Lv3_RMHO",
                    temezumi_yomiGenTeban) + "]");// ログ出力
        }

        /// <summary>
        /// ハブノードの次手番の局面のうち、王手がかかった局面は取り除きます。
        /// </summary>
        public static void LAA_RemoveNextNode_IfMate(
            int yomikaisiTemezumi,
            Node<IMove, KyokumenWrapper> hubNode,
            int temezumi_yomiGenTeban_forLog,//読み進めている現在の手目
            Playerside pside_genTeban

#if DEBUG
            ,KaisetuBoards logF_kiki
#endif
            )
        {
            // Node<,>の形で。
            Dictionary<string, Node<IMove, KyokumenWrapper>> newNextNodes = new Dictionary<string, Node<IMove, KyokumenWrapper>>();

            hubNode.Foreach_ChildNodes((string key, Node<IMove, KyokumenWrapper> node, ref bool toBreak) =>
            {
                System.Diagnostics.Debug.Assert(node.Key != null);//指し手がヌルなはず無いはず。

                // 王様が利きに飛び込んだか？
                bool kingSuicide = Util_LegalMove.LAAA_KingSuicide(
                    yomikaisiTemezumi,
                    node.Value.KyokumenConst,
                    temezumi_yomiGenTeban_forLog,
                    pside_genTeban,//現手番＝攻め手視点
#if DEBUG
                    logF_kiki,
#endif
                    node.Key
                    );

                if (!kingSuicide)
                {
                    // 王様が利きに飛び込んでいない局面だけ、残します。
                    if (newNextNodes.ContainsKey(key))
                    {
                        newNextNodes[key] = node;
                    }
                    else
                    {
                        newNextNodes.Add(key, node);
                    }
                }
            });


            // 入替え
            hubNode.PutSet_ChildNodes(newNextNodes);
        }


        /// <summary>
        /// 利きに飛び込んでいないか（王手されていないか）、調べます。
        /// 
        /// GetAvailableMove()の中では使わないでください。循環してしまいます。
        /// </summary>
        public static bool LAAA_KingSuicide(
            int yomikaisiTemezumi,
            SkyConst src_Sky,//調べたい局面
            int temezumi_yomiCur_forLog,//読み進めている現在の手目
            Playerside pside_genTeban,//現手番側

#if DEBUG
            KaisetuBoards logF_kiki,
#endif
            IMove move_forLog
            )
        {
            bool isHonshogi = true;

            System.Diagnostics.Debug.Assert(src_Sky.Count == Masu_Honshogi.HONSHOGI_KOMAS);

            // 「相手の駒を動かしたときの利き」リスト
            // 持ち駒はどう考える？「駒を置けば、思い出王手だってある」
            List_OneAndMulti<Finger, SySet<SyElement>> sMs_effect_aiTeban = Util_LegalMove.LAAAA_GetEffect(
                yomikaisiTemezumi,
                isHonshogi,
                src_Sky,
                pside_genTeban,
                true,// 相手盤の利きを調べます。
#if DEBUG
                logF_kiki,
#endif
                "玉自殺ﾁｪｯｸ",
                temezumi_yomiCur_forLog,
                move_forLog);


            // 現手番側が受け手に回ったとします。現手番の、王の座標
            int genTeban_kingMasuNumber;

            if (Playerside.P2 == pside_genTeban)
            {
                // 現手番は、後手

                RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(Finger_Honshogi.GoteOh).Now);

                genTeban_kingMasuNumber = Conv_SyElement.ToMasuNumber(koma.Masu);
            }
            else
            {
                // 現手番は、先手
                RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(Finger_Honshogi.SenteOh).Now);

                genTeban_kingMasuNumber = Conv_SyElement.ToMasuNumber(koma.Masu);
            }


            // 相手の利きに、自分の王がいるかどうか確認します。
            bool mate = false;
            sMs_effect_aiTeban.Foreach_Entry((Finger koma, SySet<SyElement> kikis, ref bool toBreak) =>
            {
                foreach (New_Basho kiki in kikis.Elements)
                {
                    if (genTeban_kingMasuNumber == kiki.MasuNumber)
                    {
                        mate = true;
                        toBreak = true;
                    }
                }
            });

            return mate;
        }

        /// <summary>
        /// 指定された局面で、指定された手番の駒の、利きマスを算出します。
        /// 持ち駒は盤上にないので、利きを調べる必要はありません。
        /// 
        /// 「手目」は判定できません。
        /// 
        /// </summary>
        /// <param name="kouho"></param>
        /// <param name="sbGohosyu"></param>
        /// <param name="logger"></param>
        public static List_OneAndMulti<Finger, SySet<SyElement>> LAAAA_GetEffect(
            int yomikaisiTemezumi,
            bool isHonshogi,
            SkyConst src_Sky,
            Playerside pside_genTeban3,
            bool isAiteban,
#if DEBUG
            KaisetuBoards logF_kiki,
#endif
            string logBrd_caption,
            int temezumi_yomiCur_forLog,
            IMove move_forLog
            )
        {
#if DEBUG
            KaisetuBoard logBrd_kiki = new KaisetuBoard();
            logBrd_kiki.Caption = logBrd_caption;// "利き_" 
            logBrd_kiki.Temezumi = temezumi_yomiCur_forLog;
            logBrd_kiki.YomikaisiTemezumi = yomikaisiTemezumi;
            //logBrd_kiki.Score = 0.0d;
            logBrd_kiki.GenTeban = pside_genTeban3;// 現手番
            logF_kiki.boards.Add(logBrd_kiki);
#endif

            // 《１》
            List_OneAndMulti<Finger, SySet<SyElement>> sMs_effect = new List_OneAndMulti<Finger, SySet<SyElement>>();//盤上の駒の利き
            {
                // 《１．１》
                Playerside tebanSeme;//手番（利きを調べる側）
                Playerside tebanKurau;//手番（喰らう側）
                {
                    if (isAiteban)
                    {
                        tebanSeme = Conv_Playerside.Reverse(pside_genTeban3);
                        tebanKurau = pside_genTeban3;
                    }
                    else
                    {
                        tebanSeme = pside_genTeban3;
                        tebanKurau = Conv_Playerside.Reverse(pside_genTeban3);
                    }

#if DEBUG
                    if (Playerside.P1 == tebanSeme)
                    {
                        logBrd_kiki.NounaiSeme = Gkl_NounaiSeme.Sente;
                    }
                    else if (Playerside.P2 == tebanSeme)
                    {
                        logBrd_kiki.NounaiSeme = Gkl_NounaiSeme.Gote;
                    }
#endif
                }


                // 《１．２》
                Fingers fingers_seme_BANJO;//盤上駒（利きを調べる側）
                Fingers fingers_kurau_BANJO;//盤上駒（喰らう側）
                Fingers dust1;
                Fingers dust2;

                Util_Sky_FingersQueryFx.Split_BanjoSeme_BanjoKurau_MotiSeme_MotiKurau(
                        out fingers_seme_BANJO,
                        out fingers_kurau_BANJO,
                        out dust1,
                        out dust2,
                        src_Sky,
                        tebanSeme,
                        tebanKurau
                    );


                // 攻め手の駒の位置
#if DEBUG
                KaisetuBoard boardLog_clone = new KaisetuBoard(logBrd_kiki);
                foreach (Finger finger in fingers_seme_BANJO.Items)
                {
                    RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(finger).Now);


                    Gkl_KomaMasu km = new Gkl_KomaMasu(
                        Util_Converter_LogGraphicEx.PsideKs14_ToString(tebanSeme, Util_Komahaiyaku184.Syurui(koma.Haiyaku), ""),
                        Conv_SyElement.ToMasuNumber(koma.Masu)
                        );
                    boardLog_clone.KomaMasu1.Add(km);
                }
                foreach (Finger finger in fingers_kurau_BANJO.Items)
                {
                    RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(finger).Now);


                    logBrd_kiki.KomaMasu2.Add(new Gkl_KomaMasu(
                        Util_Converter_LogGraphicEx.PsideKs14_ToString(tebanKurau, Util_Komahaiyaku184.Syurui(koma.Haiyaku), ""),
                        Conv_SyElement.ToMasuNumber(koma.Masu)
                        ));
                }
                logBrd_kiki = boardLog_clone;
#endif




                // 《１．３》
                SySet<SyElement> masus_seme_BANJO = Conv_Fingers.ToMasus(fingers_seme_BANJO, src_Sky);// 盤上のマス（利きを調べる側の駒）
                SySet<SyElement> masus_kurau_BANJO = Conv_Fingers.ToMasus(fingers_kurau_BANJO, src_Sky);// 盤上のマス（喰らう側の駒）

                // 駒のマスの位置は、特にログに取らない。

                // 《１．４》
                Maps_OneAndOne<Finger, SySet<SyElement>> kmEffect_seme_BANJO = Query_FingersMasusSky.To_KomabetuKiki_OnBanjo(
                    fingers_seme_BANJO,//この中身がおかしい。
                    masus_seme_BANJO,
                    masus_kurau_BANJO,
                    src_Sky
                    //Conv_Move.Move_To_KsString_ForLog(move_forLog, pside_genTeban3),
                    );// 利きを調べる側の利き（戦駒）

                // 盤上駒の利き
#if DEBUG
                logBrd_kiki = new KaisetuBoard(logBrd_kiki);
                kmEffect_seme_BANJO.Foreach_Entry((Finger key, SySet<SyElement> value, ref bool toBreak) =>
                {
                    RO_Star koma = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(key).Now);


                    string komaImg = Util_Converter_LogGraphicEx.PsideKs14_ToString(tebanSeme, Util_Komahaiyaku184.Syurui(koma.Haiyaku), "");

                    foreach (New_Basho masu in value.Elements)
                    {
                        boardLog_clone.Masu_theEffect.Add(masu.MasuNumber);
                    }
                });
                logBrd_kiki = boardLog_clone;
#endif


                // 《１》　＝　《１．４》の盤上駒＋持駒
                sMs_effect.AddRange_New(kmEffect_seme_BANJO);
            }

            return sMs_effect;
        }
    }
}
