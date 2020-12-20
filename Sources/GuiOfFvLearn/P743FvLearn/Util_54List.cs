namespace Grayscale.P743FvLearn.L400N54List
{
    using System;
#if DEBUG
    using Grayscale.Kifuwarakaku.Entities.Logger;
using Grayscale.P211WordShogi.L500Word;
using Grayscale.P212ConvPside.L500Converter;
using Grayscale.P213Komasyurui.L250Word;
using Grayscale.P218Starlight.I500Struct;
using Grayscale.P224Sky.L500Struct;
using Grayscale.P238Seiza.L250Struct;
using Grayscale.P238Seiza.L500Util;
using Grayscale.P258UtilSky258.L500UtilSky;
using Grayscale.P521FeatureVect.L500Struct;
using Grayscale.P525ConvFv.L500Converter;
using Grayscale.P743FvLearn.I400N54List;
using System.Text;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //フィンガー番号
#else
    using System.Text;
    using Grayscale.Kifuwarakaku.Entities.Logging;
    using Grayscale.P211WordShogi.L500Word;
    using Grayscale.P212ConvPside.L500Converter;
    using Grayscale.P213Komasyurui.L250Word;
    using Grayscale.P218Starlight.I500Struct;
    using Grayscale.P224Sky.L500Struct;
    using Grayscale.P238Seiza.L250Struct;
    using Grayscale.P238Seiza.L500Util;
    using Grayscale.P258UtilSky258.L500UtilSky;
    using Grayscale.P521FeatureVect.L500Struct;
    using Grayscale.P525ConvFv.L500Converter;
    using Grayscale.P743FvLearn.I400N54List;
    using Finger = ProjectDark.NamedInt.StrictNamedInt0; //フィンガー番号
#endif

    public class Util_54List
    {

        private static void Panic1(RO_Star koma)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Util_54List#Error1：２駒関係FVの配列添え字がわからないぜ☆！処理は続けられない。");
            sb.AppendLine("koma1.Pside=[" + koma.Pside + "]");
            sb.AppendLine("koma1.Komasyurui=[" + koma.Komasyurui + "]");
            sb.AppendLine("koma1.Masu=[" + koma.Masu + "]");
            sb.AppendLine("Conv_SyElement.ToOkiba(koma1.Masu)=[" + Conv_SyElement.ToOkiba(koma.Masu) + "]");
            throw new Exception(sb.ToString());
        }


        /// <summary>
        /// 54駒のリスト。
        /// 
        /// 盤上の40駒リスト。
        /// 駒台の14駒リスト。
        /// </summary>
        public static N54List Calc_54List(SkyConst src_Sky)
        {
            N54List result_n54List = new N54ListImpl();


            //----------------------------------------
            // インナー・メソッド用 集計変数
            //----------------------------------------
            int p54Next = 0;
            int[] p54List = new int[54];

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
                        Util_54List.Panic1(koma);
                        goto gt_NextLoop_player1;
                    }

                    //----------------------------------------
                    // 盤上の駒だぜ☆！
                    //----------------------------------------
                    p54List[p54Next] = pIndex;
                    p54Next++;
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
                        Util_54List.Panic1(koma);
                        goto gt_NextLoop_player1;
                    }

                    //----------------------------------------
                    // 駒台の駒だぜ☆！
                    //----------------------------------------
                    p54List[p54Next] = pIndex;
                    p54Next++;
                }

            gt_NextLoop_player1:
                ;
            });


            result_n54List.SetP54List_Unsorted(p54List);
            result_n54List.SetP54Next(p54Next);

            return result_n54List;
        }

    }
}
