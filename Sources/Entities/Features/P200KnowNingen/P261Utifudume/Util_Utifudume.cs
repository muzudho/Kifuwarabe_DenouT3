using System;
using System.Collections.Generic;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

#if DEBUG

#endif

namespace Grayscale.Kifuwarakaku.Entities.Features
{
    /// <summary>
    /// 打ち歩詰め
    /// </summary>
    public abstract class Util_Utifudume
    {

        /// <summary>
        /// 打ち歩詰め処理。
        /// 
        /// TODO:打ち歩詰めチェック
        /// </summary>
        public static void Utifudume(
            SkyConst src_Sky,
            SySet<SyElement> masus_mikata_onBanjo,//打ち歩詰めチェック用
            SySet<SyElement> masus_aite_onBanjo,//打ち歩詰めチェック用
            SySet<SyElement>[] aMasus//駒種類別、置こうとする升
            )
        {
            // 攻め側
            Playerside pside_seme = src_Sky.KaisiPside;

            // 相手の王の位置
            RO_Star king_aite;
            Finger figKing_aite;
            Playerside pside_aite;

            switch (src_Sky.KaisiPside)
            {
                case Playerside.P1:
                    pside_aite = Playerside.P2;
                    figKing_aite = Finger_Honshogi.GoteOh;
                    king_aite = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(figKing_aite).Now);
                    break;
                case Playerside.P2:
                    pside_aite = Playerside.P1;
                    figKing_aite = Finger_Honshogi.SenteOh;
                    king_aite = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(figKing_aite).Now);
                    break;
                default: throw new Exception("エラー：打ち歩詰めチェック中。プレイヤー不明。");
            }

            // 相手の玉頭の升。
            SyElement masu_gyokutou = null;
            {
                SySet<SyElement> sySet = KomanoKidou.DstIppo_上(pside_aite, king_aite.Masu);
                foreach (SyElement element2 in sySet.Elements)//最初の１件を取る。
                {
                    masu_gyokutou = element2;
                    break;
                }

                if (null == masu_gyokutou)
                {
                    goto gt_EndUtifudume;
                }
            }

            // 相手の玉。
            Fingers fingers_aiteKing = new Fingers();
            fingers_aiteKing.Add(figKing_aite);

            // 相手の玉頭に、自分側の利きがあるかどうか。
            bool isKiki_mikata = false;
            {
                // 利き一覧
                Maps_OneAndOne<Finger, SySet<SyElement>> kikiMap = Query_FingersMasusSky.To_KomabetuKiki_OnBanjo(
                    fingers_aiteKing,
                    masus_mikata_onBanjo,
                    masus_aite_onBanjo,
                    src_Sky
                );

                int gyokutouMasuNumber = Conv_SyElement.ToMasuNumber(masu_gyokutou);
                kikiMap.Foreach_Values((SySet<SyElement> values, ref bool toBreak) =>
                {
                    foreach (SyElement element in values.Elements)
                    {
                        int masuNumber = Conv_SyElement.ToMasuNumber(element);
                        if (masuNumber == gyokutouMasuNumber)
                        {
                            isKiki_mikata = true;
                            toBreak = true;
                            break;
                        }
                    }
                });
            }

            if (!isKiki_mikata)
            {
                goto gt_EndUtifudume;
            }

            // 相手の玉頭に、利きのある相手側の駒の種類の一覧。
            List<PieceType> ksList = new List<PieceType>();
            SySet<SyElement> aitegyokuKiki;
            {
                // 相手側の盤上の駒一覧。
                Fingers fingers_aiteKoma_Banjo = Util_Sky_FingersQuery.InOkibaPsideNow(src_Sky, Okiba.ShogiBan, pside_aite);

                // 利き一覧
                Maps_OneAndOne<Finger, SySet<SyElement>> kikiMap = Query_FingersMasusSky.To_KomabetuKiki_OnBanjo(
                    fingers_aiteKoma_Banjo,
                    masus_aite_onBanjo,//相手の駒は、味方
                    masus_mikata_onBanjo,//味方の駒は、障害物。
                    src_Sky
                );
                aitegyokuKiki = kikiMap.ElementAt(figKing_aite);

                int gyokutouMasuNumber = Conv_SyElement.ToMasuNumber(masu_gyokutou);
                kikiMap.Foreach_Entry((Finger figKoma, SySet<SyElement> values, ref bool toBreak) =>
                {
                    foreach (SyElement element in values.Elements)
                    {
                        int masuNumber = Conv_SyElement.ToMasuNumber(element);
                        if (masuNumber == gyokutouMasuNumber)
                        {
                            ksList.Add(Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(figKoma).Now).Komasyurui);
                            break;
                        }
                    }
                });
            }

            // 「王様でしか取れない状態」ではなければ、スルー。
            if (ksList.Count != 1)
            {
                goto gt_EndUtifudume;
            }
            else if (ksList[0] != PieceType.K)
            {
                goto gt_EndUtifudume;
            }

            // 「王様に逃げ道がある」なら、スルー。
            MasubetuKikisu masubetuKikisu_semeKoma = Util_SkyPside.ToMasubetuKikisu(src_Sky, pside_seme);
            Dictionary<int, int> nigerarenaiMap = new Dictionary<int, int>();
            switch (src_Sky.KaisiPside)
            {
                case Playerside.P1: nigerarenaiMap = masubetuKikisu_semeKoma.Kikisu_AtMasu_2P; break;
                case Playerside.P2: nigerarenaiMap = masubetuKikisu_semeKoma.Kikisu_AtMasu_1P; break;
                default: throw new Exception("エラー：打ち歩詰めチェック中。プレイヤー不明。");
            }
            foreach (SyElement element in aitegyokuKiki.Elements)
            {
                // 攻撃側の利きが利いていない、空きマスがあるかどうか。
                int movableMasuNumber_king = Conv_SyElement.ToMasuNumber(element);

                if (nigerarenaiMap[movableMasuNumber_king] == 0)
                {
                    // 逃げ切った☆！
                    goto gt_EndUtifudume;
                }
            }

            //----------------------------------------
            // 打ち歩詰め確定
            //----------------------------------------

            // １升（玉頭升）を、クリアーします。

            aMasus[(int)PieceType.P].Minus_Closed(
                masu_gyokutou, Util_SyElement_BinaryOperator.Dlgt_Equals_MasuNumber);


        gt_EndUtifudume:
            ;
        }

    }
}
