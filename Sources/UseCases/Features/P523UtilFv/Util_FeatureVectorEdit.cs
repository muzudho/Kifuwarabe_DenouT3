using Grayscale.Kifuwarakaku.Entities.Features;

namespace Grayscale.Kifuwarakaku.UseCases.Features
{
    /// <summary>
    /// 主に学習時、あるいはゲーム起動時にだけ使い、対局中（ゲーム中）に使わないメソッドは、こちらに移動します。
    /// </summary>
    public abstract class Util_FeatureVectorEdit
    {
        /// <summary>
        /// 増減の確認用。
        /// </summary>
        /// <returns></returns>
        public static float GetTotal_PP(FeatureVector fv)
        {
            float result = 0.0f;

            for (int p1 = 0; p1 < FeatureVectorImpl.CHOSA_KOMOKU_P; p1++)
            {
                for (int p2 = 0; p2 < FeatureVectorImpl.CHOSA_KOMOKU_P; p2++)
                {
                    result += fv.NikomaKankeiPp_ForMemory[p1, p2];
                }
            }

            return result;
        }




        /// <summary>
        /// 旧型。
        /// 適当に数字を埋めます☆
        /// </summary>
        public static void Make_Random(FeatureVector fv)
        {
            //
            // 駒割は固定。
            // コーディングの利便上、エラー駒やヌル駒にもランダム値を入れておく。
            //
            foreach (PieceType komaSyurui in Array_Komasyurui.Items_AllElements)//
            {
                fv.Komawari[(int)komaSyurui] = KwRandom.Random.Next(0, 999);
            }
            //
            // 
            //
            fv.Komawari[(int)PieceType.None] = 0;
            fv.Komawari[(int)PieceType.P] = 100;
            fv.Komawari[(int)PieceType.L] = 800;
            fv.Komawari[(int)PieceType.N] = 200;
            fv.Komawari[(int)PieceType.S] = 500;
            fv.Komawari[(int)PieceType.G] = 600;
            // 玉はあとで。
            fv.Komawari[(int)PieceType.R] = 1600;
            fv.Komawari[(int)PieceType.B] = 1600;
            fv.Komawari[(int)PieceType.PR] = 2000;
            fv.Komawari[(int)PieceType.PB] = 2000;
            fv.Komawari[(int)PieceType.PP] = 600;
            fv.Komawari[(int)PieceType.PL] = 600;
            fv.Komawari[(int)PieceType.PN] = 600;
            fv.Komawari[(int)PieceType.PS] = 600;
            //
            // 玉の駒割は計算で求める。 歩100×18 ＋ 香800×4 ＋ 桂200×4 ＋ 銀500×4 ＋ 金600×4 ＋ 飛1600×2 ＋ 角1600×2。
            fv.Komawari[(int)PieceType.K] =
                fv.Komawari[(int)PieceType.P] * 18 +
                fv.Komawari[(int)PieceType.L] * 4 +
                fv.Komawari[(int)PieceType.N] * 4 +
                fv.Komawari[(int)PieceType.S] * 4 +
                fv.Komawari[(int)PieceType.G] * 4 +
                fv.Komawari[(int)PieceType.R] * 2 +
                fv.Komawari[(int)PieceType.B] * 2 +
                0;

            //
            // PP
            //
            for (int iChosaKomoku1 = 0; iChosaKomoku1 < FeatureVectorImpl.CHOSA_KOMOKU_P; iChosaKomoku1++)//調査項目Ｐ
            {
                for (int iChosaKomoku2 = 0; iChosaKomoku2 < FeatureVectorImpl.CHOSA_KOMOKU_P; iChosaKomoku2++)//調査項目Ｐ
                {
                    // 0.0～1.0
                    fv.NikomaKankeiPp_ForMemory[iChosaKomoku1, iChosaKomoku2] = KwRandom.Random.Next(0, 999);
                }
            }
        }

    }
}
