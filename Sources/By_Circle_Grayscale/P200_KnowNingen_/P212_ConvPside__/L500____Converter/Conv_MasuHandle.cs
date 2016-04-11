using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P211_WordShogi__.L250____Masu;
using System.Diagnostics;

namespace Grayscale.P212_ConvPside__.L500____Converter
{
    public abstract class Conv_MasuHandle
    {
        public static SyElement ToMasu(int masuHandle)
        {
            SyElement masu;

            if (
                !Conv_MasuHandle.Yuko(masuHandle)
            )
            {
                masu = Masu_Honshogi.Query_Basho( Masu_Honshogi.nError);
            }
            else
            {
                masu = Masu_Honshogi.Masus_All[masuHandle];
            }

            return masu;
        }


        #region 範囲妥当性チェック

        public static bool Yuko(int masuHandle)
        {
            return 0 <= masuHandle && masuHandle <= 201;
        }

        #endregion

        public static bool OnAll(int masuHandle)
        {
            return Masu_Honshogi.nban11_１一 <= masuHandle && masuHandle <= Masu_Honshogi.nError;
        }

        public static bool OnShogiban(int masuHandle)
        {
            return Masu_Honshogi.nban11_１一 <= masuHandle && masuHandle <= Masu_Honshogi.nban99_９九;
        }

        /// <summary>
        /// 駒台の上なら真。
        /// </summary>
        /// <param name="masuHandle"></param>
        /// <returns></returns>
        public static bool OnKomadai(int masuHandle)
        {
            return Masu_Honshogi.nsen01 <= masuHandle && masuHandle <= Masu_Honshogi.ngo40;
        }

        public static bool OnSenteKomadai(int masuHandle)
        {
            return Masu_Honshogi.nsen01 <= masuHandle && masuHandle <= Masu_Honshogi.nsen40;
        }

        public static bool OnGoteKomadai(int masuHandle)
        {
            return Masu_Honshogi.ngo01 <= masuHandle && masuHandle <= Masu_Honshogi.ngo40;
        }

        public static bool OnKomabukuro(int masuHandle)
        {
            Debug.Assert(Masu_Honshogi.nfukuro01 == 161, "fukuro01=[" + Masu_Honshogi.nfukuro01.ToString() + "]");

            return Masu_Honshogi.nfukuro01 <= masuHandle && masuHandle <= Masu_Honshogi.nfukuro40;
        }

    }
}
