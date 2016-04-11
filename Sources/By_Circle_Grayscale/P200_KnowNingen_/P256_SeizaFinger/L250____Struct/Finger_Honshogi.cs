using Grayscale.P224_Sky________.L500____Struct;
using Finger = ProjectDark.NamedInt.StrictNamedInt0; //スプライト番号

namespace Grayscale.P256_SeizaFinger.L250____Struct
{

    /// <summary>
    /// 本将棋用のスプライト番号
    /// </summary>
    public abstract class Finger_Honshogi
    {
        /// <summary>
        /// 40枚の駒に、０～３９の数字を付けているんだが、これに名前を付けたものなんだぜ☆
        /// Ｋｏｍａ４０の列挙型が、Ｋ４０だぜ☆
        /// KomaDoors[n]を直接指定するには良さそう。
        /// 
        /// エラーも含みます。
        /// </summary>
        public static Finger[] Items_All
        {
            get
            {
                return Finger_Honshogi.items_All;
            }
        }
        private static Finger[] items_All;


        /// <summary>
        /// 40枚の駒に、０～３９の数字を付けているんだが、これに名前を付けたものなんだぜ☆
        /// Ｋｏｍａ４０の列挙型が、Ｋ４０だぜ☆
        /// KomaDoors[n]を直接指定するには良さそう。
        /// 
        /// エラーを含みません。
        /// </summary>
        public static Finger[] Items_KomaOnly
        {
            get
            {
                return Finger_Honshogi.items_KomaOnly;
            }
        }
        private static Finger[] items_KomaOnly;



        static Finger_Honshogi()
        {
            Finger_Honshogi.items_All = new Finger[]{//41要素
                Finger_Honshogi.SenteOh,
                Finger_Honshogi.GoteOh,
                Finger_Honshogi.Hi1,
                Finger_Honshogi.Hi2,
                Finger_Honshogi.Kaku1,
                Finger_Honshogi.Kaku2,
                Finger_Honshogi.Kin1,
                Finger_Honshogi.Kin2,
                Finger_Honshogi.Kin3,
                Finger_Honshogi.Kin4,
                Finger_Honshogi.Gin1,
                Finger_Honshogi.Gin2,
                Finger_Honshogi.Gin3,
                Finger_Honshogi.Gin4,
                Finger_Honshogi.Kei1,
                Finger_Honshogi.Kei2,
                Finger_Honshogi.Kei3,
                Finger_Honshogi.Kei4,
                Finger_Honshogi.Kyo1,
                Finger_Honshogi.Kyo2,
                Finger_Honshogi.Kyo3,
                Finger_Honshogi.Kyo4,
                Finger_Honshogi.Fu1,
                Finger_Honshogi.Fu2,
                Finger_Honshogi.Fu3,
                Finger_Honshogi.Fu4,
                Finger_Honshogi.Fu5,
                Finger_Honshogi.Fu6,
                Finger_Honshogi.Fu7,
                Finger_Honshogi.Fu8,
                Finger_Honshogi.Fu9,
                Finger_Honshogi.Fu10,
                Finger_Honshogi.Fu11,
                Finger_Honshogi.Fu12,
                Finger_Honshogi.Fu13,
                Finger_Honshogi.Fu14,
                Finger_Honshogi.Fu15,
                Finger_Honshogi.Fu16,
                Finger_Honshogi.Fu17,
                Finger_Honshogi.Fu18,
                Fingers.Error_1,
            };

            Finger_Honshogi.items_KomaOnly = new Finger[40];
            for (int i = 0; i <= 39; i++)
            {
                Finger_Honshogi.items_KomaOnly[i] = Finger_Honshogi.items_All[i];
            }
        }


        /// <summary>
        /// 先手王。 constは使えないのでreadonlyで代用。
        /// </summary>
        public static readonly Finger SenteOh = 0;

        /// <summary>
        /// 後手王
        /// </summary>
        public static readonly Finger GoteOh = 1;

        /// <summary>
        /// 飛１
        /// </summary>
        public static readonly Finger Hi1 = 2;

        /// <summary>
        /// 飛２
        /// </summary>
        public static readonly Finger Hi2 = 3;

        /// <summary>
        /// 角１
        /// </summary>
        public static readonly Finger Kaku1 = 4;

        /// <summary>
        /// 角２
        /// </summary>
        public static readonly Finger Kaku2 = 5;

        /// <summary>
        /// 金１
        /// </summary>
        public static readonly Finger Kin1 = 6;

        /// <summary>
        /// 金２
        /// </summary>
        public static readonly Finger Kin2 = 7;

        /// <summary>
        /// 金３
        /// </summary>
        public static readonly Finger Kin3 = 8;

        /// <summary>
        /// 金４
        /// </summary>
        public static readonly Finger Kin4 = 9;

        /// <summary>
        /// 銀１
        /// </summary>
        public static readonly Finger Gin1 = 10;

        /// <summary>
        /// 銀２
        /// </summary>
        public static readonly Finger Gin2 = 11;

        /// <summary>
        /// 銀３
        /// </summary>
        public static readonly Finger Gin3 = 12;

        /// <summary>
        /// 銀４
        /// </summary>
        public static readonly Finger Gin4 = 13;

        /// <summary>
        /// 桂１
        /// </summary>
        public static readonly Finger Kei1 = 14;

        /// <summary>
        /// 桂２
        /// </summary>
        public static readonly Finger Kei2 = 15;

        /// <summary>
        /// 桂３
        /// </summary>
        public static readonly Finger Kei3 = 16;

        /// <summary>
        /// 桂４
        /// </summary>
        public static readonly Finger Kei4 = 17;

        /// <summary>
        /// 香１
        /// </summary>
        public static readonly Finger Kyo1 = 18;

        /// <summary>
        /// 香２
        /// </summary>
        public static readonly Finger Kyo2 = 19;

        /// <summary>
        /// 香３
        /// </summary>
        public static readonly Finger Kyo3 = 20;

        /// <summary>
        /// 香４
        /// </summary>
        public static readonly Finger Kyo4 = 21;

        /// <summary>
        /// 歩１
        /// </summary>
        public static readonly Finger Fu1 = 22;

        /// <summary>
        /// 歩２
        /// </summary>
        public static readonly Finger Fu2 = 23;

        /// <summary>
        /// 歩３
        /// </summary>
        public static readonly Finger Fu3 = 24;

        /// <summary>
        /// 歩４
        /// </summary>
        public static readonly Finger Fu4 = 25;

        /// <summary>
        /// 歩５
        /// </summary>
        public static readonly Finger Fu5 = 26;

        /// <summary>
        /// 歩６
        /// </summary>
        public static readonly Finger Fu6 = 27;

        /// <summary>
        /// 歩７
        /// </summary>
        public static readonly Finger Fu7 = 28;

        /// <summary>
        /// 歩８
        /// </summary>
        public static readonly Finger Fu8 = 29;

        /// <summary>
        /// 歩９
        /// </summary>
        public static readonly Finger Fu9 = 30;

        /// <summary>
        /// 歩１０
        /// </summary>
        public static readonly Finger Fu10 = 31;

        /// <summary>
        /// 歩１１
        /// </summary>
        public static readonly Finger Fu11 = 32;

        /// <summary>
        /// 歩１２
        /// </summary>
        public static readonly Finger Fu12 = 33;

        /// <summary>
        /// 歩１３
        /// </summary>
        public static readonly Finger Fu13 = 34;

        /// <summary>
        /// 歩１４
        /// </summary>
        public static readonly Finger Fu14 = 35;

        /// <summary>
        /// 歩１５
        /// </summary>
        public static readonly Finger Fu15 = 36;

        /// <summary>
        /// 歩１６
        /// </summary>
        public static readonly Finger Fu16 = 37;

        /// <summary>
        /// 歩１７
        /// </summary>
        public static readonly Finger Fu17 = 38;

        /// <summary>
        /// 歩１８
        /// </summary>
        public static readonly Finger Fu18 = 39;
    }


}
