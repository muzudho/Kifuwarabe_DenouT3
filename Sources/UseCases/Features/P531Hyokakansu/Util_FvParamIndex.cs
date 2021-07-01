using System;
using System.Diagnostics;
using Grayscale.Kifuwarakaku.Entities.Features;



#if DEBUG

#endif

namespace Grayscale.Kifuwarakaku.UseCases.Features
{
    /// <summary>
    /// フィーチャー・ベクターのパラメーター・インデックスを返します。
    /// </summary>
    public abstract class Util_FvParamIndex
    {
        private static int[] paramIndex_Playerside = new int[Array_Playerside.Items_AllElements.Length];
        private static int[] paramIndex_KomaSyrui_Banjo = new int[Array_Komasyurui.Items_AllElements.Length];
        private static int[] paramIndex_KomaSyrui_Moti = new int[Array_Komasyurui.Items_AllElements.Length];

        static Util_FvParamIndex()
        {
            //
            // プレイヤー種類別　パラメーター・インデックス。
            //
            paramIndex_Playerside[(int)Playerside.Empty] = -1;
            paramIndex_Playerside[(int)Playerside.P1] = Const_NikomaKankeiP_ParamIx.PLAYER1;
            paramIndex_Playerside[(int)Playerside.P2] = Const_NikomaKankeiP_ParamIx.PLAYER2;

            //
            // 盤上の駒種類別　パラメーター・インデックス。
            //
            paramIndex_KomaSyrui_Banjo[(int)PieceType.None] = -1;
            paramIndex_KomaSyrui_Banjo[(int)PieceType.P] = Const_NikomaKankeiP_ParamIx.Ban_Fu__;
            paramIndex_KomaSyrui_Banjo[(int)PieceType.L] = Const_NikomaKankeiP_ParamIx.Ban_Kyo_;
            paramIndex_KomaSyrui_Banjo[(int)PieceType.N] = Const_NikomaKankeiP_ParamIx.Ban_Kei_;
            paramIndex_KomaSyrui_Banjo[(int)PieceType.S] = Const_NikomaKankeiP_ParamIx.Ban_Gin_;
            paramIndex_KomaSyrui_Banjo[(int)PieceType.G] = Const_NikomaKankeiP_ParamIx.Ban_Kin_;
            paramIndex_KomaSyrui_Banjo[(int)PieceType.K] = Const_NikomaKankeiP_ParamIx.Ban_Oh__;
            paramIndex_KomaSyrui_Banjo[(int)PieceType.R] = Const_NikomaKankeiP_ParamIx.Ban_Hi__;
            paramIndex_KomaSyrui_Banjo[(int)PieceType.B] = Const_NikomaKankeiP_ParamIx.Ban_Kaku;
            paramIndex_KomaSyrui_Banjo[(int)PieceType.PR] = Const_NikomaKankeiP_ParamIx.Ban_Hi__;//飛と同じ
            paramIndex_KomaSyrui_Banjo[(int)PieceType.PB] = Const_NikomaKankeiP_ParamIx.Ban_Kaku;//角と同じ
            paramIndex_KomaSyrui_Banjo[(int)PieceType.PP] = Const_NikomaKankeiP_ParamIx.Ban_Kin_;//金と同じ
            paramIndex_KomaSyrui_Banjo[(int)PieceType.PL] = Const_NikomaKankeiP_ParamIx.Ban_Kin_;//金と同じ
            paramIndex_KomaSyrui_Banjo[(int)PieceType.PN] = Const_NikomaKankeiP_ParamIx.Ban_Kin_;//金と同じ
            paramIndex_KomaSyrui_Banjo[(int)PieceType.PS] = Const_NikomaKankeiP_ParamIx.Ban_Kin_;//金と同じ

            //
            // 持ち駒の駒種類別　パラメーター・インデックス。
            //
            paramIndex_KomaSyrui_Moti[(int)PieceType.None] = -1;
            paramIndex_KomaSyrui_Moti[(int)PieceType.P] = Const_NikomaKankeiP_ParamIx.MotiFu__;
            paramIndex_KomaSyrui_Moti[(int)PieceType.L] = Const_NikomaKankeiP_ParamIx.MotiKyo_;
            paramIndex_KomaSyrui_Moti[(int)PieceType.N] = Const_NikomaKankeiP_ParamIx.MotiKei_;
            paramIndex_KomaSyrui_Moti[(int)PieceType.S] = Const_NikomaKankeiP_ParamIx.MotiGin_;
            paramIndex_KomaSyrui_Moti[(int)PieceType.G] = Const_NikomaKankeiP_ParamIx.MotiKin_;
            paramIndex_KomaSyrui_Moti[(int)PieceType.K] = -1; // 王は持ち駒にできない。
            paramIndex_KomaSyrui_Moti[(int)PieceType.R] = Const_NikomaKankeiP_ParamIx.MotiHi__;
            paramIndex_KomaSyrui_Moti[(int)PieceType.B] = Const_NikomaKankeiP_ParamIx.MotiKaku;
            paramIndex_KomaSyrui_Moti[(int)PieceType.PR] = Const_NikomaKankeiP_ParamIx.Ban_Hi__;//飛と同じ
            paramIndex_KomaSyrui_Moti[(int)PieceType.PB] = Const_NikomaKankeiP_ParamIx.Ban_Kaku;//角と同じ
            paramIndex_KomaSyrui_Moti[(int)PieceType.PP] = Const_NikomaKankeiP_ParamIx.MotiFu__;//歩と同じ
            paramIndex_KomaSyrui_Moti[(int)PieceType.PL] = Const_NikomaKankeiP_ParamIx.MotiKyo_;//香と同じ
            paramIndex_KomaSyrui_Moti[(int)PieceType.PN] = Const_NikomaKankeiP_ParamIx.MotiKei_;//桂と同じ
            paramIndex_KomaSyrui_Moti[(int)PieceType.PS] = Const_NikomaKankeiP_ParamIx.MotiGin_;//銀と同じ
        }

        /// <summary>
        /// Pの将棋盤上の駒の位置の、配列の添え字番号。
        /// </summary>
        /// <param name="p_koma"></param>
        /// <returns></returns>
        public static int ParamIndex_Banjo(RO_Star p_koma)
        {
            int koumokuP;

            // 駒Ｐのマス番号
            int p_masuHandle = Conv_SyElement.ToMasuNumber(p_koma.Masu);

            int index_playerside = Util_FvParamIndex.paramIndex_Playerside[(int)p_koma.Pside];
            int index_komasyurui = Util_FvParamIndex.paramIndex_KomaSyrui_Banjo[(int)p_koma.Komasyurui];

            if (index_playerside == -1)
            {
                throw new Exception("二駒関係の先後不明の駒");
            }
            else if (index_komasyurui == -1)
            {
                throw new Exception("二駒関係の駒種類が対象外の駒");
            }

            koumokuP = index_playerside + index_komasyurui + p_masuHandle;
            Debug.Assert(0 <= koumokuP && koumokuP < FeatureVectorImpl.CHOSA_KOMOKU_P, $"koumokuP=[{koumokuP}]");
            return koumokuP;
        }

        /// <summary>
        /// Pの持ち駒項目の、配列の添え字番号。
        /// 
        /// 王は判定できない。
        /// </summary>
        /// <param name="src_Sky"></param>
        /// <param name="pside"></param>
        /// <param name="syurui"></param>
        /// <returns>エラー時は-1</returns>
        public static int ParamIndex_Moti(SkyConst src_Sky, Playerside pside, PieceType syurui)
        {
            Fingers fingers = Util_Sky_FingersQuery.InOkibaPsideKomasyuruiNow(src_Sky, Conv_Playerside.ToKomadai(pside), pside, syurui);

            int index_playerside = Util_FvParamIndex.paramIndex_Playerside[(int)pside];
            int index_komasyurui = Util_FvParamIndex.paramIndex_KomaSyrui_Moti[(int)syurui];

            if (index_playerside == -1)
            {
                throw new Exception("二駒関係の持ち駒_先後不明の駒");
            }
            else if (index_komasyurui == -1)
            {
                throw new Exception("二駒関係の持ち駒_駒種類が対象外の駒");
            }

            int koumokuP = index_playerside + index_komasyurui + fingers.Count;
            Debug.Assert(0 <= koumokuP && koumokuP < FeatureVectorImpl.CHOSA_KOMOKU_P, $"koumokuP=[{koumokuP}]");
            return koumokuP;
        }


    }
}
