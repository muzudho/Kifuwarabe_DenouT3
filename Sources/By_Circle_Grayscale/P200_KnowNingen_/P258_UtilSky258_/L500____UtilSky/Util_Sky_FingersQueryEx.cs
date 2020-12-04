﻿using Grayscale.P003Log.I500Struct;
using Grayscale.P056Syugoron.I250Struct;
using Grayscale.P211WordShogi.L500Word;
using Grayscale.P218Starlight.I500Struct;
using Grayscale.P224Sky.L500Struct;
using Grayscale.P256_SeizaFinger.L500____Util;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //フィンガー番号

namespace Grayscale.P258_UtilSky258_.L500____UtilSky
{

    /// <summary>
    /// 指定局面から、『指差し番号』を問い合わせます。
    /// 
    /// 特殊なもの。
    /// </summary>
    public abstract class Util_Sky_FingersQueryEx
    {

        /// <summary>
        /// ************************************************************************************************************************
        /// 軌道上の駒たち
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="km"></param>
        /// <returns></returns>
        public static void Fingers_EachSrcNow(out Fingers out_fingers, SySet<SyElement> srcList, SkyConst src_Sky, Playerside pside, Starlight itaru, KwErrorHandler errH)
        {
            out_fingers = new Fingers();

            foreach (SyElement masu in srcList.Elements)
            {
                Finger finger = Util_Sky_FingerQuery.InShogibanMasuNow(src_Sky, pside, masu, errH);
                if (Util_Finger.ForHonshogi(finger))
                {
                    // 指定の升に駒がありました。
                    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                    out_fingers.Add(finger);
                }
            }
        }

    }
}