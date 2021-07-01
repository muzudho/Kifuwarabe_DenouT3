using Grayscale.Kifuwarakaku.Entities.Features;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //フィンガー番号

namespace Grayscale.Kifuwarakaku.UseCases.Features
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
                Finger figK1 = Util_Sky_FingersQuery.InOkibaPsideKomasyuruiNow(src_Sky, Okiba.ShogiBan, pside, PieceType.K).ToFirst();
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
        public static int ToPIndex_FromBanjo_PsideKomasyuruiMasu(Playerside pside, PieceType komasyurui, SyElement masu, out int p_index)
        {
            p_index = 0;//ここに累計していく。

            if (Okiba.ShogiBan != Conv_SyElement.ToOkiba(masu))
            {
                // 盤上でなければ。
                p_index = -1;
                goto gt_EndMethod;
            }

            switch (pside)
            {
                case Playerside.P1: break;
                case Playerside.P2: p_index += FeatureVectorImpl.CHOSA_KOMOKU_2P; break;
                default: break;
            }

            switch (komasyurui)
            {
                case PieceType.PP: //thru
                case PieceType.P: p_index += FeatureVectorImpl.CHOSA_KOMOKU_____FU_____; break;
                case PieceType.PL: //thru
                case PieceType.L: p_index += FeatureVectorImpl.CHOSA_KOMOKU_____KYO____; break;
                case PieceType.PN: //thru
                case PieceType.N: p_index += FeatureVectorImpl.CHOSA_KOMOKU_____KEI____; break;
                case PieceType.PS: //thru
                case PieceType.S: p_index += FeatureVectorImpl.CHOSA_KOMOKU_____GIN____; break;
                case PieceType.G: p_index += FeatureVectorImpl.CHOSA_KOMOKU_____KIN____; break;
                case PieceType.K: p_index += FeatureVectorImpl.CHOSA_KOMOKU_____OH_____; break;
                case PieceType.PR: //thru
                case PieceType.R: p_index += FeatureVectorImpl.CHOSA_KOMOKU_____HISYA__; break;
                case PieceType.PB: //thru
                case PieceType.B: p_index += FeatureVectorImpl.CHOSA_KOMOKU_____KAKU___; break;
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
        public static int ToPIndex_FromMoti_PsideKomasyuruiMaisu(Playerside pside, PieceType komasyurui, int maisu, out int p_index)
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
                case PieceType.P: p_index += FeatureVectorImpl.CHOSA_KOMOKU_MOTIFU_____; break;
                //case Ks14.H12_NariKyo: //駒台にない。
                case PieceType.L: p_index += FeatureVectorImpl.CHOSA_KOMOKU_MOTIKYO____; break;
                //case Ks14.H13_NariKei: //駒台にない。
                case PieceType.N: p_index += FeatureVectorImpl.CHOSA_KOMOKU_MOTIKEI____; break;
                //case Ks14.H14_NariGin: //駒台にない。
                case PieceType.S: p_index += FeatureVectorImpl.CHOSA_KOMOKU_MOTIGIN____; break;
                case PieceType.G: p_index += FeatureVectorImpl.CHOSA_KOMOKU_MOTIKIN____; break;
                //case Ks14.H06_Oh_____: // 駒台にない。
                //case Ks14.H09_Ryu____: //駒台にない。
                case PieceType.R: p_index += FeatureVectorImpl.CHOSA_KOMOKU_MOTIHISYA__; break;
                //case Ks14.H10_Uma____: //駒台にない。
                case PieceType.B: p_index += FeatureVectorImpl.CHOSA_KOMOKU_MOTIKAKU___; break;
                default: p_index = -1; goto gt_EndMethod;
            }

            p_index += maisu;//持ち駒の数

        gt_EndMethod:
            ;
            return p_index;
        }

    }


}
