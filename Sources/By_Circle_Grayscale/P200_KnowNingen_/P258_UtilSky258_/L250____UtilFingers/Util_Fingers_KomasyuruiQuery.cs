using Grayscale.P213_Komasyurui_.L250____Word;
using Grayscale.P224_Sky________.L500____Struct;
using Grayscale.P238_Seiza______.L500____Util;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P258_UtilSky258_.L250____UtilFingers
{
    public abstract class Util_Fingers_KomasyuruiQuery
    {

        ///// <summary>
        ///// 駒sを渡すと、駒の種類のカウントを返します。
        ///// </summary>
        //public static void Translate_Fingers_ToKomasyuruiCount(
        //    SkyConst src_Sky,
        //    Fingers figKomas,
        //    out int[] out_komasyuruisCount
        //    )
        //{
        //    out_komasyuruisCount = new int[Array_Komasyurui.Items_All.Length];

        //    foreach (Finger figMotiKoma in figKomas.Items)
        //    {
        //        // 持ち駒の種類
        //        Komasyurui14 motikomaSyurui = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(figMotiKoma).Now).Komasyurui;
        //        out_komasyuruisCount[(int)motikomaSyurui]++;
        //    }
        //}

        /// <summary>
        /// 駒sを渡すと、駒を１つずつ返します。
        /// </summary>
        public static void Translate_Fingers_ToKomasyuruiBETUFirst(
            SkyConst src_Sky,
            Fingers figKomas,
            out Finger[] out_figKomasFirst
            )
        {
            out_figKomasFirst = new Finger[Array_Komasyurui.Items_AllElements.Length];

            foreach (Komasyurui14 komasyurui in Array_Komasyurui.Items_AllElements)
            {
                out_figKomasFirst[(int)komasyurui] = Fingers.Error_1; //ヌル値は無い。指定が必要。
            }

            foreach (Finger figMotiKoma in figKomas.Items)
            {
                // 持ち駒の種類
                Komasyurui14 motikomaSyurui = Util_Starlightable.AsKoma(src_Sky.StarlightIndexOf(figMotiKoma).Now).Komasyurui;
                if (out_figKomasFirst[(int)motikomaSyurui] == Fingers.Error_1)
                {
                    out_figKomasFirst[(int)motikomaSyurui] = figMotiKoma;
                }
            }
        }

    }
}
