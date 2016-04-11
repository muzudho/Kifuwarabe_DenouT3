using Grayscale.P003_Log________.L___500_Struct;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P212_ConvPside__.L500____Converter;
using Grayscale.P213_Komasyurui_.L250____Word;
using Grayscale.P218_Starlight__.L___500_Struct;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P238_Seiza______.L250____Struct;
using Grayscale.P238_Seiza______.L500____Util;
using Grayscale.P258_UtilSky258_.L500____UtilSky;
using Grayscale.P521_FeatureVect.L500____Struct;
using Grayscale.P525_ConvFv_____.L500____Converter;
using Grayscale.P743_FvLearn____.L___400_54List;
using System.Text;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //フィンガー番号

namespace Grayscale.P743_FvLearn____.L400____54List
{
    public class Util_54List
    {

        private static void Error1(RO_Star koma, KwErrorHandler errH)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Util_54List#Error1：２駒関係FVの配列添え字がわからないぜ☆！処理は続けられない。");
            sb.AppendLine("koma1.Pside=[" + koma.Pside + "]");
            sb.AppendLine("koma1.Komasyurui=[" + koma.Komasyurui + "]");
            sb.AppendLine("koma1.Masu=[" + koma.Masu + "]");
            sb.AppendLine("Conv_SyElement.ToOkiba(koma1.Masu)=[" + Conv_SyElement.ToOkiba(koma.Masu) + "]");
            errH.DonimoNaranAkirameta(sb.ToString());
        }


        /// <summary>
        /// 54駒のリスト。
        /// 
        /// 盤上の40駒リスト。
        /// 駒台の14駒リスト。
        /// </summary>
        public static N54List Calc_54List(SkyConst src_Sky, KwErrorHandler errH)
        {
            N54List result_n54List = new N54ListImpl();


            //----------------------------------------
            // インナー・メソッド用 集計変数
            //----------------------------------------
            int p54Next = 0;
            int[] p54List = new int[54];

            src_Sky.Foreach_Starlights((Finger finger, Starlight light, ref bool toBreak) =>
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
                        Util_54List.Error1(koma, errH);
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
                        Util_54List.Error1(koma, errH);
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
