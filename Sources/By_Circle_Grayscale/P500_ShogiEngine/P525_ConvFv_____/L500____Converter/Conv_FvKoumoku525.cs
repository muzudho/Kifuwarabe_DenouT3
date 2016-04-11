using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P212_ConvPside__.L500____Converter;
using Grayscale.P213_Komasyurui_.L250____Word;
using Grayscale.P214_Masu_______.L500____Util;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P238_Seiza______.L250____Struct;
using Grayscale.P238_Seiza______.L500____Util;
using Grayscale.P258_UtilSky258_.L500____UtilSky;
using Grayscale.P521_FeatureVect.L500____Struct;
using Grayscale.P522_Conv_FvKoumoku.L500____Converter;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //フィンガー番号

namespace Grayscale.P525_ConvFv_____.L500____Converter
{

    /// <summary>
    /// フィーチャーベクターの項目番号を算出します。
    /// </summary>
    public abstract class Conv_FvKoumoku525
    {
        /// <summary>
        /// ２駒関係[ＫＫ]用。
        /// フィーチャーベクターの調査項目インデックス。該当なければ-1。
        /// </summary>
        /// <param name="pside">プレイヤーサイド</param>
        /// <param name="komasyurui">盤上の種類</param>
        /// <param name="masu">盤上の駒の升</param>
        /// <returns></returns>
        public static int ToKIndex_From_PsideBanjoKomasyuruiMasu(SkyConst src_Sky, Playerside pside)
        {
            // 調査項目番号（Ｋ１、Ｋ２等）
            int result;

            SyElement masu;
            {
                Finger figK1 = Util_Sky_FingersQuery.InOkibaPsideKomasyuruiNow(src_Sky, Okiba.ShogiBan, pside, Komasyurui14.H06_Gyoku__).ToFirst();
                RO_Star komaK1 = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(figK1).Now);
                masu = komaK1.Masu;
            }

            if (Okiba.ShogiBan != Conv_SyElement.ToOkiba(masu))
            {
                // 盤上でなければ。
                result = -1;
                goto gt_EndMethod;
            }

            int kSuji;
            Util_MasuNum.TryMasuToSuji(masu, out kSuji);
            int kDan;
            Util_MasuNum.TryMasuToDan(masu, out kDan);


            int p1;
            Conv_FvKoumoku522.Converter_K1_to_P(Playerside.P1, kDan, kSuji, out p1);


            result = p1;

        gt_EndMethod:
            ;
            return result;
        }


        /// <summary>
        /// ２駒関係[ＰＰ]用。
        /// フィーチャーベクターの調査項目インデックス。該当なければ-1。
        /// </summary>
        /// <param name="pside">プレイヤーサイド</param>
        /// <param name="komasyurui">盤上の種類</param>
        /// <param name="masu">盤上の駒の升</param>
        /// <returns></returns>
        public static int ToPIndex_FromBanjo_PsideKomasyuruiMasu(Playerside pside, Komasyurui14 komasyurui, SyElement masu, out int p_index)
        {
            p_index = 0;//ここに累計していく。

            if (Okiba.ShogiBan!=Conv_SyElement.ToOkiba(masu))
            {
                // 盤上でなければ。
                p_index = -1;
                goto gt_EndMethod;
            }

            switch (pside)
            {
                case Playerside.P1: break;
                case Playerside.P2: p_index+=FeatureVectorImpl.CHOSA_KOMOKU_2P; break;
                default: break;
            }

            switch (komasyurui)
            {
                case Komasyurui14.H11_Tokin__: //thru
                case Komasyurui14.H01_Fu_____: p_index += FeatureVectorImpl.CHOSA_KOMOKU_____FU_____; break;
                case Komasyurui14.H12_NariKyo: //thru
                case Komasyurui14.H02_Kyo____: p_index += FeatureVectorImpl.CHOSA_KOMOKU_____KYO____; break;
                case Komasyurui14.H13_NariKei: //thru
                case Komasyurui14.H03_Kei____: p_index += FeatureVectorImpl.CHOSA_KOMOKU_____KEI____; break;
                case Komasyurui14.H14_NariGin: //thru
                case Komasyurui14.H04_Gin____: p_index += FeatureVectorImpl.CHOSA_KOMOKU_____GIN____; break;
                case Komasyurui14.H05_Kin____: p_index += FeatureVectorImpl.CHOSA_KOMOKU_____KIN____; break;
                case Komasyurui14.H06_Gyoku__: p_index += FeatureVectorImpl.CHOSA_KOMOKU_____OH_____; break;
                case Komasyurui14.H09_Ryu____: //thru
                case Komasyurui14.H07_Hisya__: p_index += FeatureVectorImpl.CHOSA_KOMOKU_____HISYA__; break;
                case Komasyurui14.H10_Uma____: //thru
                case Komasyurui14.H08_Kaku___: p_index += FeatureVectorImpl.CHOSA_KOMOKU_____KAKU___; break;
                default: p_index = -1; goto gt_EndMethod;
            }

            p_index += Conv_SyElement.ToMasuNumber(masu);

        gt_EndMethod:
            ;
            return p_index;
        }

        /// <summary>
        /// ２駒関係[ＰＰ]用。
        /// フィーチャーベクターの調査項目インデックス。該当なければ-1。
        /// </summary>
        /// <param name="pside">プレイヤーサイド</param>
        /// <param name="komasyurui">持駒の種類</param>
        /// <param name="maisu">その持駒の種類の、持っている個数</param>
        /// <returns></returns>
        public static int ToPIndex_FromMoti_PsideKomasyuruiMaisu(Playerside pside, Komasyurui14 komasyurui, int maisu, out int p_index)
        {
            p_index = 0;//ここに累計していく。

            switch (pside)
            {
                case Playerside.P1: break;
                case Playerside.P2: p_index += FeatureVectorImpl.CHOSA_KOMOKU_2P; break;
                default: break;
            }

            switch (komasyurui)
            {
                //case Ks14.H11_Tokin__: //駒台にない。
                case Komasyurui14.H01_Fu_____: p_index += FeatureVectorImpl.CHOSA_KOMOKU_MOTIFU_____; break;
                //case Ks14.H12_NariKyo: //駒台にない。
                case Komasyurui14.H02_Kyo____: p_index += FeatureVectorImpl.CHOSA_KOMOKU_MOTIKYO____; break;
                //case Ks14.H13_NariKei: //駒台にない。
                case Komasyurui14.H03_Kei____: p_index += FeatureVectorImpl.CHOSA_KOMOKU_MOTIKEI____; break;
                //case Ks14.H14_NariGin: //駒台にない。
                case Komasyurui14.H04_Gin____: p_index += FeatureVectorImpl.CHOSA_KOMOKU_MOTIGIN____; break;
                case Komasyurui14.H05_Kin____: p_index += FeatureVectorImpl.CHOSA_KOMOKU_MOTIKIN____; break;
                //case Ks14.H06_Oh_____: // 駒台にない。
                //case Ks14.H09_Ryu____: //駒台にない。
                case Komasyurui14.H07_Hisya__: p_index += FeatureVectorImpl.CHOSA_KOMOKU_MOTIHISYA__; break;
                //case Ks14.H10_Uma____: //駒台にない。
                case Komasyurui14.H08_Kaku___: p_index += FeatureVectorImpl.CHOSA_KOMOKU_MOTIKAKU___; break;
                default: p_index = -1; goto gt_EndMethod;
            }

            p_index += maisu;//持ち駒の数

        gt_EndMethod:
            ;
            return p_index;
        }

    }


}
