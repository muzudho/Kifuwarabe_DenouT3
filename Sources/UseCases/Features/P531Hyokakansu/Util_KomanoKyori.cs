﻿using System;
using Grayscale.Kifuwarakaku.Entities.Features;

namespace Grayscale.Kifuwarakaku.UseCases.Features
{
    /// <summary>
    /// 近似測定エンジン。
    /// </summary>
    public abstract class Util_KomanoKyori
    {

        /// <summary>
        /// 距離の近さ
        /// </summary>
        /// <returns></returns>
        public static int GetKyori(SyElement mokuhyo, SyElement genzai)
        {
            int masuNumber = Conv_SyElement.ToMasuNumber(mokuhyo);
            return Util_KomanoKyori.GetKyori(masuNumber, genzai);
        }

        /// <summary>
        /// 距離の近さ
        /// </summary>
        /// <returns></returns>
        public static int GetKyori(int masuNumber, SyElement genzai)
        {
            //
            // とりあえず　おおざっぱに計算します。
            //

            int mokuhyoDan;
            Util_MasuNum.TryMasuToDan(masuNumber, out mokuhyoDan);

            int mokuhyoSuji;
            Util_MasuNum.TryMasuToSuji(masuNumber, out mokuhyoSuji);

            int genzaiDan;
            Util_MasuNum.TryMasuToDan(genzai, out genzaiDan);

            int genzaiSuji;
            Util_MasuNum.TryMasuToSuji(genzai, out genzaiSuji);

            int kyori = Math.Abs(mokuhyoDan - genzaiDan) + Math.Abs(mokuhyoSuji - genzaiSuji);

            return kyori;
        }

    }
}
