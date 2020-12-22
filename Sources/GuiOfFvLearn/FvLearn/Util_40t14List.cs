namespace Grayscale.Kifuwarakaku.GuiOfFvLearn.Features
{
    using System;
#if DEBUG
    using Grayscale.Kifuwarakaku.Entities.Features;
    using Grayscale.Kifuwarakaku.UseCases.Features;
    using System.Text;
    using Finger = ProjectDark.NamedInt.StrictNamedInt0; //フィンガー番号
#else
    using System.Text;
    using Grayscale.Kifuwarakaku.Entities.Features;
    using Grayscale.Kifuwarakaku.UseCases.Features;
    using Finger = ProjectDark.NamedInt.StrictNamedInt0; //フィンガー番号
#endif

    /// <summary>
    /// 52要素のリスト。
    /// </summary>
    public abstract class Util_40t14List
    {

        private static void Panic1(RO_Star koma)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Util_40t14List#Error1：２駒関係FVの配列添え字がわからないぜ☆！処理は続けられない。");
            sb.AppendLine($"koma1.Pside=[{koma.Pside}]");
            sb.AppendLine($"koma1.Komasyurui=[{koma.Komasyurui}]");
            sb.AppendLine($"koma1.Masu=[{koma.Masu}]");
            sb.AppendLine($"Conv_SyElement.ToOkiba(koma1.Masu)=[{Conv_SyElement.ToOkiba(koma.Masu)}]");
            throw new Exception(sb.ToString());
        }

        /// <summary>
        /// 54駒のリスト。
        /// 
        /// 盤上の40駒リスト。
        /// 駒台の14駒リスト。
        /// </summary>
        public static N40t14List Calc_40t14List(SkyConst src_Sky)
        {
            N40t14List result_n40t14List = new N40t14ListImpl();


            //----------------------------------------
            // インナー・メソッド用 集計変数
            //----------------------------------------
            int p40Next = 0;
            int[] p40List = new int[40];

            int p14Next = 0;
            int[] p14List = new int[14];

            src_Sky.Foreach_Starlights((Finger finger, IMoveHalf light, ref bool toBreak) =>
            {
                RO_Star koma = Util_Starlightable.AsKoma(light.Now);

                //----------------------------------------
                // まず、p を調べます。
                //----------------------------------------
                if (Conv_SyElement.ToOkiba(koma.Masu) == Okiba.ShogiBan)
                {
                    int pIndex = FeatureVectorImpl.CHOSA_KOMOKU_ERROR;// 調査項目Ｐ１
                    //----------------------------------------
                    // 盤上の駒
                    //----------------------------------------
                    Conv_FvKoumoku525.ToPIndex_FromBanjo_PsideKomasyuruiMasu(koma.Pside, koma.Komasyurui, koma.Masu, out pIndex);

                    if (FeatureVectorImpl.CHOSA_KOMOKU_ERROR == pIndex)
                    {
                        // p1 がエラーでは、処理は続けられない。
                        Util_40t14List.Panic1(koma);
                        goto gt_NextLoop_player1;
                    }

                    //----------------------------------------
                    // 盤上の駒だぜ☆！
                    //----------------------------------------
                    p40List[p40Next] = pIndex;
                    p40Next++;
                }
                else if (
                    Conv_SyElement.ToOkiba(koma.Masu) == Okiba.Sente_Komadai
                    || Conv_SyElement.ToOkiba(koma.Masu) == Okiba.Gote_Komadai)
                {
                    int pIndex = FeatureVectorImpl.CHOSA_KOMOKU_ERROR;// 調査項目Ｐ１
                    //----------------------------------------
                    // 持ち駒
                    //----------------------------------------
                    Komasyurui14 motiKomasyurui = koma.ToNarazuCase();//例：駒台に馬はない。角の数を数える。
                    // 駒の枚数
                    int maisu = Util_Sky_FingersQuery.InOkibaKomasyuruiNow(src_Sky, Conv_Playerside.ToKomadai(koma.Pside), motiKomasyurui).Items.Count;
                    Conv_FvKoumoku525.ToPIndex_FromMoti_PsideKomasyuruiMaisu(koma.Pside, motiKomasyurui, maisu, out pIndex);

                    if (FeatureVectorImpl.CHOSA_KOMOKU_ERROR == pIndex)
                    {
                        // p1 がエラーでは、処理は続けられない。
                        Util_40t14List.Panic1(koma);
                        goto gt_NextLoop_player1;
                    }

                    //----------------------------------------
                    // 駒台の駒だぜ☆！
                    //----------------------------------------
                    p14List[p14Next] = pIndex;
                    p14Next++;
                }

            gt_NextLoop_player1:
                ;
            });


            result_n40t14List.SetP40List_Unsorted(p40List);
            result_n40t14List.SetP40Next(p40Next);

            result_n40t14List.SetP14List_Unsorted(p14List);
            result_n40t14List.SetP14Next(p14Next);

            return result_n40t14List;
        }


    }
}
