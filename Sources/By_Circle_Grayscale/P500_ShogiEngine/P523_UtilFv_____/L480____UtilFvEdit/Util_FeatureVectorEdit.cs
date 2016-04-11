using Grayscale.P031_Random_____.L500____Struct;
using Grayscale.P213_Komasyurui_.L250____Word;
using Grayscale.P521_FeatureVect.L___500_Struct;
using Grayscale.P521_FeatureVect.L500____Struct;

namespace Grayscale.P523_UtilFv_____.L480____UtilFvEdit
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
            foreach (Komasyurui14 komaSyurui in Array_Komasyurui.Items_AllElements)//
            {
                fv.Komawari[(int)komaSyurui] = KwRandom.Random.Next(0, 999);
            }
            //
            // 
            //
            fv.Komawari[(int)Komasyurui14.H00_Null___] = 0;
            fv.Komawari[(int)Komasyurui14.H01_Fu_____] = 100;
            fv.Komawari[(int)Komasyurui14.H02_Kyo____] = 800;
            fv.Komawari[(int)Komasyurui14.H03_Kei____] = 200;
            fv.Komawari[(int)Komasyurui14.H04_Gin____] = 500;
            fv.Komawari[(int)Komasyurui14.H05_Kin____] = 600;
            // 玉はあとで。
            fv.Komawari[(int)Komasyurui14.H07_Hisya__] = 1600;
            fv.Komawari[(int)Komasyurui14.H08_Kaku___] = 1600;
            fv.Komawari[(int)Komasyurui14.H09_Ryu____] = 2000;
            fv.Komawari[(int)Komasyurui14.H10_Uma____] = 2000;
            fv.Komawari[(int)Komasyurui14.H11_Tokin__] = 600;
            fv.Komawari[(int)Komasyurui14.H12_NariKyo] = 600;
            fv.Komawari[(int)Komasyurui14.H13_NariKei] = 600;
            fv.Komawari[(int)Komasyurui14.H14_NariGin] = 600;
            //
            // 玉の駒割は計算で求める。 歩100×18 ＋ 香800×4 ＋ 桂200×4 ＋ 銀500×4 ＋ 金600×4 ＋ 飛1600×2 ＋ 角1600×2。
            fv.Komawari[(int)Komasyurui14.H06_Gyoku__] =
                fv.Komawari[(int)Komasyurui14.H01_Fu_____] * 18 +
                fv.Komawari[(int)Komasyurui14.H02_Kyo____] * 4 +
                fv.Komawari[(int)Komasyurui14.H03_Kei____] * 4 +
                fv.Komawari[(int)Komasyurui14.H04_Gin____] * 4 +
                fv.Komawari[(int)Komasyurui14.H05_Kin____] * 4 +
                fv.Komawari[(int)Komasyurui14.H07_Hisya__] * 2 +
                fv.Komawari[(int)Komasyurui14.H08_Kaku___] * 2 +
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
