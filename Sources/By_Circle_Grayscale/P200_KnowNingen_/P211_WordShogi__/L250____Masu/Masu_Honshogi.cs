using Grayscale.P055_Conv_Sy.L500____Converter;
using Grayscale.P056_Syugoron___.L___250_Struct;
using Grayscale.P056_Syugoron___.L250____Struct;
using Grayscale.P211_WordShogi__.L___250_Masu;
using System.Collections.Generic;

namespace Grayscale.P211_WordShogi__.L250____Masu
{
    /// <summary>
    /// 駒を置ける場所２０１箇所だぜ☆
    /// 
    /// 将棋盤０～８０。（計８１マス）
    /// 先手駒台８１～１２０。（計４０マス）
    /// 後手駒台１２１～１６０。（計４０マス）
    /// 駒袋１６１～２００。（計４０マス）
    /// エラー２０１。
    /// 
    /// int型にキャストして使うんだぜ☆
    /// </summary>
    public class Masu_Honshogi
    {
        /// <summary>
        /// 本将棋の有効駒数は40個。
        /// </summary>
        public const int HONSHOGI_KOMAS = 40;

        /// <summary>
        /// 集合論の要素ディクショナリー。
        /// </summary>
        private static Dictionary<ulong, New_Basho> bitfieldBashoDictionary;


        /// <summary>
        /// 列挙型の要素を、配列に格納しておきます。
        /// 
        /// int型→列挙型　への変換を可能にします。
        /// </summary>
        public static SyElement[] Masus_All
        {
            get
            {
                return Masu_Honshogi.masus_All;
            }
        }
        private static SyElement[] masus_All;


        public static SyElement[] Items_81
        {
            get
            {
                return Masu_Honshogi.items_81;
            }
        }
        private static SyElement[] items_81;

        /// <summary>
        /// ２歩判定用。
        /// </summary>
        public static readonly SySet<SyElement>[] BAN_SUJIS = {
                                                                 null,//[0]
                                                                 new SySet_Default<SyElement>("1筋"),//[1]
                                                                 new SySet_Default<SyElement>("2筋"),
                                                                 new SySet_Default<SyElement>("3筋"),
                                                                 new SySet_Default<SyElement>("4筋"),
                                                                 new SySet_Default<SyElement>("5筋"),
                                                                 new SySet_Default<SyElement>("6筋"),
                                                                 new SySet_Default<SyElement>("7筋"),
                                                                 new SySet_Default<SyElement>("8筋"),
                                                                 new SySet_Default<SyElement>("9筋"),
                                                             };

        static Masu_Honshogi()
        {
            Masu_Honshogi.bitfieldBashoDictionary = new Dictionary<ulong, New_Basho>();

            //盤上1筋
            {
                int[] array = new int[]{
                    Masu_Honshogi.nban11_１一,
                    Masu_Honshogi.nban12_１二,
                    Masu_Honshogi.nban13_１三,
                    Masu_Honshogi.nban14_１四,
                    Masu_Honshogi.nban15_１五,
                    Masu_Honshogi.nban16_１六,
                    Masu_Honshogi.nban17_１七,
                    Masu_Honshogi.nban18_１八,
                    Masu_Honshogi.nban19_１九,
                };
                foreach (int masuNumber in array)
                {
                    BAN_SUJIS[1].AddElement(Masu_Honshogi.Query_Basho(masuNumber));
                }
            }
            //盤上2筋
            {
                int[] array = new int[]{
                    Masu_Honshogi.nban21_２一,
                    Masu_Honshogi.nban22_２二,
                    Masu_Honshogi.nban23_２三,
                    Masu_Honshogi.nban24_２四,
                    Masu_Honshogi.nban25_２五,
                    Masu_Honshogi.nban26_２六,
                    Masu_Honshogi.nban27_２七,
                    Masu_Honshogi.nban28_２八,
                    Masu_Honshogi.nban29_２九,
                };
                foreach (int masuNumber in array)
                {
                    BAN_SUJIS[2].AddElement(Masu_Honshogi.Query_Basho(masuNumber));
                }
            }
            //盤上3筋
            {
                int[] array = new int[]{
                    Masu_Honshogi.nban31_３一,
                    Masu_Honshogi.nban32_３二,
                    Masu_Honshogi.nban33_３三,
                    Masu_Honshogi.nban34_３四,
                    Masu_Honshogi.nban35_３五,
                    Masu_Honshogi.nban36_３六,
                    Masu_Honshogi.nban37_３七,
                    Masu_Honshogi.nban38_３八,
                    Masu_Honshogi.nban39_３九,
                };
                foreach (int masuNumber in array)
                {
                    BAN_SUJIS[3].AddElement(Masu_Honshogi.Query_Basho(masuNumber));
                }
            }
            //盤上4筋
            {
                int[] array = new int[]{
                    Masu_Honshogi.nban41_４一,
                    Masu_Honshogi.nban42_４二,
                    Masu_Honshogi.nban43_４三,
                    Masu_Honshogi.nban44_４四,
                    Masu_Honshogi.nban45_４五,
                    Masu_Honshogi.nban46_４六,
                    Masu_Honshogi.nban47_４七,
                    Masu_Honshogi.nban48_４八,
                    Masu_Honshogi.nban49_４九,
                };
                foreach (int masuNumber in array)
                {
                    BAN_SUJIS[4].AddElement(Masu_Honshogi.Query_Basho(masuNumber));
                }
            }
            //盤上5筋
            {
                int[] array = new int[]{
                    Masu_Honshogi.nban51_５一,
                    Masu_Honshogi.nban52_５二,
                    Masu_Honshogi.nban53_５三,
                    Masu_Honshogi.nban54_５四,
                    Masu_Honshogi.nban55_５五,
                    Masu_Honshogi.nban56_５六,
                    Masu_Honshogi.nban57_５七,
                    Masu_Honshogi.nban58_５八,
                    Masu_Honshogi.nban59_５九,
                };
                foreach (int masuNumber in array)
                {
                    BAN_SUJIS[5].AddElement(Masu_Honshogi.Query_Basho(masuNumber));
                }
            }
            //盤上6筋
            {
                int[] array = new int[]{
                    Masu_Honshogi.nban61_６一,
                    Masu_Honshogi.nban62_６二,
                    Masu_Honshogi.nban63_６三,
                    Masu_Honshogi.nban64_６四,
                    Masu_Honshogi.nban65_６五,
                    Masu_Honshogi.nban66_６六,
                    Masu_Honshogi.nban67_６七,
                    Masu_Honshogi.nban68_６八,
                    Masu_Honshogi.nban69_６九,
                };
                foreach (int masuNumber in array)
                {
                    BAN_SUJIS[6].AddElement(Masu_Honshogi.Query_Basho(masuNumber));
                }
            }
            //盤上7筋
            {
                int[] array = new int[]{
                    Masu_Honshogi.nban71_７一,
                    Masu_Honshogi.nban72_７二,
                    Masu_Honshogi.nban73_７三,
                    Masu_Honshogi.nban74_７四,
                    Masu_Honshogi.nban75_７五,
                    Masu_Honshogi.nban76_７六,
                    Masu_Honshogi.nban77_７七,
                    Masu_Honshogi.nban78_７八,
                    Masu_Honshogi.nban79_７九,
                };
                foreach (int masuNumber in array)
                {
                    BAN_SUJIS[7].AddElement(Masu_Honshogi.Query_Basho(masuNumber));
                }
            }
            //盤上8筋
            {
                int[] array = new int[]{
                    Masu_Honshogi.nban81_８一,
                    Masu_Honshogi.nban82_８二,
                    Masu_Honshogi.nban83_８三,
                    Masu_Honshogi.nban84_８四,
                    Masu_Honshogi.nban85_８五,
                    Masu_Honshogi.nban86_８六,
                    Masu_Honshogi.nban87_８七,
                    Masu_Honshogi.nban88_８八,
                    Masu_Honshogi.nban89_８九,
                };
                foreach (int masuNumber in array)
                {
                    BAN_SUJIS[8].AddElement(Masu_Honshogi.Query_Basho(masuNumber));
                }
            }
            //盤上9筋
            {
                int[] array = new int[]{
                    Masu_Honshogi.nban91_９一,
                    Masu_Honshogi.nban92_９二,
                    Masu_Honshogi.nban93_９三,
                    Masu_Honshogi.nban94_９四,
                    Masu_Honshogi.nban95_９五,
                    Masu_Honshogi.nban96_９六,
                    Masu_Honshogi.nban97_９七,
                    Masu_Honshogi.nban98_９八,
                    Masu_Honshogi.nban99_９九,
                };
                foreach (int masuNumber in array)
                {
                    BAN_SUJIS[9].AddElement(Masu_Honshogi.Query_Basho(masuNumber));
                }
            }

            Masu_Honshogi.masus_All = new SyElement[]{
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+0),//１一
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+1),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+2),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+3),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+4),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+5),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+6),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+7),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+8),//１九

                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+9),//[9]２一
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+10),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+11),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+12),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+13),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+14),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+15),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+16),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+17),//２九

                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+18),//[18]３一
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+19),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+20),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+21),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+22),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+23),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+24),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+25),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+26),//３九

                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+27),//[27]４一
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+28),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+29),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+30),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+31),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+32),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+33),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+34),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+35),//４九

                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+36),//[36]５一
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+37),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+38),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+39),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+40),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+41),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+42),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+43),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+44),//５九

                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+45),//[45]６一
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+46),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+47),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+48),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+49),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+50),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+51),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+52),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+53),//６九

                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+54),//[54]７一
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+55),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+56),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+57),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+58),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+59),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+60),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+61),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+62),//７九

                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+63),//[63]８一
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+64),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+65),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+66),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+67),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+68),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+69),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+70),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+71),//８九

                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+72),//[72]９一
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+73),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+74),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+75),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+76),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+77),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+78),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+79),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nban11_１一+80),//[80]９九

                //先手駒台
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+0),//[81]
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+1),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+2),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+3),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+4),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+5),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+6),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+7),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+8),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+9),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+10),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+11),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+12),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+13),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+14),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+15),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+16),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+17),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+18),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+19),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+20),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+21),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+22),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+23),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+24),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+25),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+26),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+27),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+28),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+29),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+30),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+31),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+32),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+33),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+34),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+35),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+36),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+37),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+38),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nsen01+39),

                //後手駒台
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+0),//[121]
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+1),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+2),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+3),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+4),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+5),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+6),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+7),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+8),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+9),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+10),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+11),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+12),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+13),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+14),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+15),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+16),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+17),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+18),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+19),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+20),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+21),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+22),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+23),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+24),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+25),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+26),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+27),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+28),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+29),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+30),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+31),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+32),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+33),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+34),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+35),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+36),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+37),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+38),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.ngo01+39),

                //駒袋
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+0),//[161] fukuro01
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+1),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+2),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+3),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+4),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+5),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+6),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+7),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+8),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+9),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+10),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+11),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+12),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+13),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+14),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+15),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+16),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+17),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+18),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+19),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+20),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+21),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+22),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+23),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+24),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+25),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+26),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+27),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+28),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+29),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+30),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+31),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+32),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+33),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+34),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+35),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+36),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+37),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+38),
                Masu_Honshogi.Query_Basho(Masu_Honshogi.nfukuro01+39),

                Masu_Honshogi.Query_Basho(Masu_Honshogi.nError)//[201]

            };

            Masu_Honshogi.items_81 = new SyElement[81];
            for (int i = 0; i < 81; i++)
            {
                Masu_Honshogi.items_81[i] = Masu_Honshogi.masus_All[i];
            }
        }

        public static New_Basho Query_Basho(int masuNumber)
        {
            ulong bitfield = BashoImpl.ToBitfield(masuNumber);

            return Masu_Honshogi.Query_Basho(bitfield);
        }

        public static New_Basho Query_Basho(ulong bitfield)
        {
            New_Basho basho;
            if (Masu_Honshogi.bitfieldBashoDictionary.ContainsKey(bitfield))
            {
                basho = Masu_Honshogi.bitfieldBashoDictionary[bitfield];
            }
            else
            {
                string word = "升"+BashoImpl.ToMasuNumber(bitfield);
                basho = new BashoImpl(bitfield);//TODO:唯一の新規生成にしたい。
                Masu_Honshogi.bitfieldBashoDictionary.Add(bitfield, basho);
                Conv_Sy.Put_BitfieldWord(bitfield, word);
            }

            return basho;
        }

        public const int nban11_１一 = 0;
        public const int nban12_１二 = Masu_Honshogi.nban11_１一 + 1;
        public const int nban13_１三 = Masu_Honshogi.nban12_１二 + 1;
        public const int nban14_１四 = Masu_Honshogi.nban13_１三 + 1;
        public const int nban15_１五 = Masu_Honshogi.nban14_１四 + 1;
        public const int nban16_１六 = Masu_Honshogi.nban15_１五 + 1;
        public const int nban17_１七 = Masu_Honshogi.nban16_１六 + 1;
        public const int nban18_１八 = Masu_Honshogi.nban17_１七 + 1;
        public const int nban19_１九 = Masu_Honshogi.nban18_１八 + 1;

        public const int nban21_２一 = Masu_Honshogi.nban19_１九 +1;
        public const int nban22_２二 = Masu_Honshogi.nban21_２一 +1;
        public const int nban23_２三 = Masu_Honshogi.nban22_２二 + 1;
        public const int nban24_２四 = Masu_Honshogi.nban23_２三 + 1;
        public const int nban25_２五 = Masu_Honshogi.nban24_２四 + 1;
        public const int nban26_２六 = Masu_Honshogi.nban25_２五 + 1;
        public const int nban27_２七 = Masu_Honshogi.nban26_２六 + 1;
        public const int nban28_２八 = Masu_Honshogi.nban27_２七 + 1;
        public const int nban29_２九 = Masu_Honshogi.nban28_２八 + 1;

        public const int nban31_３一 = Masu_Honshogi.nban29_２九 + 1;
        public const int nban32_３二 = Masu_Honshogi.nban31_３一 + 1;
        public const int nban33_３三 = Masu_Honshogi.nban32_３二 + 1;
        public const int nban34_３四 = Masu_Honshogi.nban33_３三 + 1;
        public const int nban35_３五 = Masu_Honshogi.nban34_３四 + 1;
        public const int nban36_３六 = Masu_Honshogi.nban35_３五 + 1;
        public const int nban37_３七 = Masu_Honshogi.nban36_３六 + 1;
        public const int nban38_３八 = Masu_Honshogi.nban37_３七 + 1;
        public const int nban39_３九 = Masu_Honshogi.nban38_３八 + 1;

        public const int nban41_４一 = Masu_Honshogi.nban39_３九 + 1;
        public const int nban42_４二 = Masu_Honshogi.nban41_４一 + 1;
        public const int nban43_４三 = Masu_Honshogi.nban42_４二 + 1;
        public const int nban44_４四 = Masu_Honshogi.nban43_４三 + 1;
        public const int nban45_４五 = Masu_Honshogi.nban44_４四 + 1;
        public const int nban46_４六 = Masu_Honshogi.nban45_４五 + 1;
        public const int nban47_４七 = Masu_Honshogi.nban46_４六 + 1;
        public const int nban48_４八 = Masu_Honshogi.nban47_４七 + 1;
        public const int nban49_４九 = Masu_Honshogi.nban48_４八 + 1;

        public const int nban51_５一 = Masu_Honshogi.nban49_４九 + 1;
        public const int nban52_５二 = Masu_Honshogi.nban51_５一 + 1;
        public const int nban53_５三 = Masu_Honshogi.nban52_５二 + 1;
        public const int nban54_５四 = Masu_Honshogi.nban53_５三 + 1;
        public const int nban55_５五 = Masu_Honshogi.nban54_５四 + 1;
        public const int nban56_５六 = Masu_Honshogi.nban55_５五 + 1;
        public const int nban57_５七 = Masu_Honshogi.nban56_５六 + 1;
        public const int nban58_５八 = Masu_Honshogi.nban57_５七 + 1;
        public const int nban59_５九 = Masu_Honshogi.nban58_５八 + 1;

        public const int nban61_６一 = Masu_Honshogi.nban59_５九 + 1;
        public const int nban62_６二 = Masu_Honshogi.nban61_６一 + 1;
        public const int nban63_６三 = Masu_Honshogi.nban62_６二 + 1;
        public const int nban64_６四 = Masu_Honshogi.nban63_６三 + 1;
        public const int nban65_６五 = Masu_Honshogi.nban64_６四 + 1;
        public const int nban66_６六 = Masu_Honshogi.nban65_６五 + 1;
        public const int nban67_６七 = Masu_Honshogi.nban66_６六 + 1;
        public const int nban68_６八 = Masu_Honshogi.nban67_６七 + 1;
        public const int nban69_６九 = Masu_Honshogi.nban68_６八 + 1;

        public const int nban71_７一 = Masu_Honshogi.nban69_６九 + 1;
        public const int nban72_７二 = Masu_Honshogi.nban71_７一 + 1;
        public const int nban73_７三 = Masu_Honshogi.nban72_７二 + 1;
        public const int nban74_７四 = Masu_Honshogi.nban73_７三 + 1;
        public const int nban75_７五 = Masu_Honshogi.nban74_７四 + 1;
        public const int nban76_７六 = Masu_Honshogi.nban75_７五 + 1;
        public const int nban77_７七 = Masu_Honshogi.nban76_７六 + 1;
        public const int nban78_７八 = Masu_Honshogi.nban77_７七 + 1;
        public const int nban79_７九 = Masu_Honshogi.nban78_７八 + 1;

        public const int nban81_８一 = Masu_Honshogi.nban79_７九 + 1;
        public const int nban82_８二 = Masu_Honshogi.nban81_８一 + 1;
        public const int nban83_８三 = Masu_Honshogi.nban82_８二 + 1;
        public const int nban84_８四 = Masu_Honshogi.nban83_８三 + 1;
        public const int nban85_８五 = Masu_Honshogi.nban84_８四 + 1;
        public const int nban86_８六 = Masu_Honshogi.nban85_８五 + 1;
        public const int nban87_８七 = Masu_Honshogi.nban86_８六 + 1;
        public const int nban88_８八 = Masu_Honshogi.nban87_８七 + 1;
        public const int nban89_８九 = Masu_Honshogi.nban88_８八 + 1;

        public const int nban91_９一 = Masu_Honshogi.nban89_８九 + 1;
        public const int nban92_９二 = Masu_Honshogi.nban91_９一 + 1;
        public const int nban93_９三 = Masu_Honshogi.nban92_９二 + 1;
        public const int nban94_９四 = Masu_Honshogi.nban93_９三 + 1;
        public const int nban95_９五 = Masu_Honshogi.nban94_９四 + 1;
        public const int nban96_９六 = Masu_Honshogi.nban95_９五 + 1;
        public const int nban97_９七 = Masu_Honshogi.nban96_９六 + 1;
        public const int nban98_９八 = Masu_Honshogi.nban97_９七 + 1;
        public const int nban99_９九 = Masu_Honshogi.nban98_９八 + 1;


        // 先手駒台
        public const int nsen01 = Masu_Honshogi.nban99_９九 + 1;
        public const int nsen02 = Masu_Honshogi.nsen01 + 1;
        public const int nsen03 = Masu_Honshogi.nsen02 + 1;
        public const int nsen04 = Masu_Honshogi.nsen03 + 1;
        public const int nsen05 = Masu_Honshogi.nsen04 + 1;
        public const int nsen06 = Masu_Honshogi.nsen05 + 1;
        public const int nsen07 = Masu_Honshogi.nsen06 + 1;
        public const int nsen08 = Masu_Honshogi.nsen07 + 1;
        public const int nsen09 = Masu_Honshogi.nsen08 + 1;
        public const int nsen10 = Masu_Honshogi.nsen09 + 1;
        public const int nsen11 = Masu_Honshogi.nsen10 + 1;
        public const int nsen12 = Masu_Honshogi.nsen11 + 1;
        public const int nsen13 = Masu_Honshogi.nsen12 + 1;
        public const int nsen14 = Masu_Honshogi.nsen13 + 1;
        public const int nsen15 = Masu_Honshogi.nsen14 + 1;
        public const int nsen16 = Masu_Honshogi.nsen15 + 1;
        public const int nsen17 = Masu_Honshogi.nsen16 + 1;
        public const int nsen18 = Masu_Honshogi.nsen17 + 1;
        public const int nsen19 = Masu_Honshogi.nsen18 + 1;
        public const int nsen20 = Masu_Honshogi.nsen19 + 1;
        public const int nsen21 = Masu_Honshogi.nsen20 + 1;
        public const int nsen22 = Masu_Honshogi.nsen21 + 1;
        public const int nsen23 = Masu_Honshogi.nsen22 + 1;
        public const int nsen24 = Masu_Honshogi.nsen23 + 1;
        public const int nsen25 = Masu_Honshogi.nsen24 + 1;
        public const int nsen26 = Masu_Honshogi.nsen25 + 1;
        public const int nsen27 = Masu_Honshogi.nsen26 + 1;
        public const int nsen28 = Masu_Honshogi.nsen27 + 1;
        public const int nsen29 = Masu_Honshogi.nsen28 + 1;
        public const int nsen30 = Masu_Honshogi.nsen29 + 1;
        public const int nsen31 = Masu_Honshogi.nsen30 + 1;
        public const int nsen32 = Masu_Honshogi.nsen31 + 1;
        public const int nsen33 = Masu_Honshogi.nsen32 + 1;
        public const int nsen34 = Masu_Honshogi.nsen33 + 1;
        public const int nsen35 = Masu_Honshogi.nsen34 + 1;
        public const int nsen36 = Masu_Honshogi.nsen35 + 1;
        public const int nsen37 = Masu_Honshogi.nsen36 + 1;
        public const int nsen38 = Masu_Honshogi.nsen37 + 1;
        public const int nsen39 = Masu_Honshogi.nsen38 + 1;
        public const int nsen40 = Masu_Honshogi.nsen39 + 1;

        // 後手駒台
        public const int ngo01 = Masu_Honshogi.nsen40 + 1;
        public const int ngo02 = Masu_Honshogi.ngo01 + 1;
        public const int ngo03 = Masu_Honshogi.ngo02 + 1;
        public const int ngo04 = Masu_Honshogi.ngo03 + 1;
        public const int ngo05 = Masu_Honshogi.ngo04 + 1;
        public const int ngo06 = Masu_Honshogi.ngo05 + 1;
        public const int ngo07 = Masu_Honshogi.ngo06 + 1;
        public const int ngo08 = Masu_Honshogi.ngo07 + 1;
        public const int ngo09 = Masu_Honshogi.ngo08 + 1;
        public const int ngo10 = Masu_Honshogi.ngo09 + 1;
        public const int ngo11 = Masu_Honshogi.ngo10 + 1;
        public const int ngo12 = Masu_Honshogi.ngo11 + 1;
        public const int ngo13 = Masu_Honshogi.ngo12 + 1;
        public const int ngo14 = Masu_Honshogi.ngo13 + 1;
        public const int ngo15 = Masu_Honshogi.ngo14 + 1;
        public const int ngo16 = Masu_Honshogi.ngo15 + 1;
        public const int ngo17 = Masu_Honshogi.ngo16 + 1;
        public const int ngo18 = Masu_Honshogi.ngo17 + 1;
        public const int ngo19 = Masu_Honshogi.ngo18 + 1;
        public const int ngo20 = Masu_Honshogi.ngo19 + 1;
        public const int ngo21 = Masu_Honshogi.ngo20 + 1;
        public const int ngo22 = Masu_Honshogi.ngo21 + 1;
        public const int ngo23 = Masu_Honshogi.ngo22 + 1;
        public const int ngo24 = Masu_Honshogi.ngo23 + 1;
        public const int ngo25 = Masu_Honshogi.ngo24 + 1;
        public const int ngo26 = Masu_Honshogi.ngo25 + 1;
        public const int ngo27 = Masu_Honshogi.ngo26 + 1;
        public const int ngo28 = Masu_Honshogi.ngo27 + 1;
        public const int ngo29 = Masu_Honshogi.ngo28 + 1;
        public const int ngo30 = Masu_Honshogi.ngo29 + 1;
        public const int ngo31 = Masu_Honshogi.ngo30 + 1;
        public const int ngo32 = Masu_Honshogi.ngo31 + 1;
        public const int ngo33 = Masu_Honshogi.ngo32 + 1;
        public const int ngo34 = Masu_Honshogi.ngo33 + 1;
        public const int ngo35 = Masu_Honshogi.ngo34 + 1;
        public const int ngo36 = Masu_Honshogi.ngo35 + 1;
        public const int ngo37 = Masu_Honshogi.ngo36 + 1;
        public const int ngo38 = Masu_Honshogi.ngo37 + 1;
        public const int ngo39 = Masu_Honshogi.ngo38 + 1;
        public const int ngo40 = Masu_Honshogi.ngo39 + 1;

        // 駒袋
        public const int nfukuro01 = Masu_Honshogi.ngo40 + 1;
        public const int nfukuro02 = Masu_Honshogi.nfukuro01 + 1;
        public const int nfukuro03 = Masu_Honshogi.nfukuro02 + 1;
        public const int nfukuro04 = Masu_Honshogi.nfukuro03 + 1;
        public const int nfukuro05 = Masu_Honshogi.nfukuro04 + 1;
        public const int nfukuro06 = Masu_Honshogi.nfukuro05 + 1;
        public const int nfukuro07 = Masu_Honshogi.nfukuro06 + 1;
        public const int nfukuro08 = Masu_Honshogi.nfukuro07 + 1;
        public const int nfukuro09 = Masu_Honshogi.nfukuro08 + 1;
        public const int nfukuro10 = Masu_Honshogi.nfukuro09 + 1;
        public const int nfukuro11 = Masu_Honshogi.nfukuro10 + 1;
        public const int nfukuro12 = Masu_Honshogi.nfukuro11 + 1;
        public const int nfukuro13 = Masu_Honshogi.nfukuro12 + 1;
        public const int nfukuro14 = Masu_Honshogi.nfukuro13 + 1;
        public const int nfukuro15 = Masu_Honshogi.nfukuro14 + 1;
        public const int nfukuro16 = Masu_Honshogi.nfukuro15 + 1;
        public const int nfukuro17 = Masu_Honshogi.nfukuro16 + 1;
        public const int nfukuro18 = Masu_Honshogi.nfukuro17 + 1;
        public const int nfukuro19 = Masu_Honshogi.nfukuro18 + 1;
        public const int nfukuro20 = Masu_Honshogi.nfukuro19 + 1;
        public const int nfukuro21 = Masu_Honshogi.nfukuro20 + 1;
        public const int nfukuro22 = Masu_Honshogi.nfukuro21 + 1;
        public const int nfukuro23 = Masu_Honshogi.nfukuro22 + 1;
        public const int nfukuro24 = Masu_Honshogi.nfukuro23 + 1;
        public const int nfukuro25 = Masu_Honshogi.nfukuro24 + 1;
        public const int nfukuro26 = Masu_Honshogi.nfukuro25 + 1;
        public const int nfukuro27 = Masu_Honshogi.nfukuro26 + 1;
        public const int nfukuro28 = Masu_Honshogi.nfukuro27 + 1;
        public const int nfukuro29 = Masu_Honshogi.nfukuro28 + 1;
        public const int nfukuro30 = Masu_Honshogi.nfukuro29 + 1;
        public const int nfukuro31 = Masu_Honshogi.nfukuro30 + 1;
        public const int nfukuro32 = Masu_Honshogi.nfukuro31 + 1;
        public const int nfukuro33 = Masu_Honshogi.nfukuro32 + 1;
        public const int nfukuro34 = Masu_Honshogi.nfukuro33 + 1;
        public const int nfukuro35 = Masu_Honshogi.nfukuro34 + 1;
        public const int nfukuro36 = Masu_Honshogi.nfukuro35 + 1;
        public const int nfukuro37 = Masu_Honshogi.nfukuro36 + 1;
        public const int nfukuro38 = Masu_Honshogi.nfukuro37 + 1;
        public const int nfukuro39 = Masu_Honshogi.nfukuro38 + 1;
        public const int nfukuro40 = Masu_Honshogi.nfukuro39 + 1;

        // エラー用の升
        public const int nError = Masu_Honshogi.nfukuro40 + 1;



        public static bool Basho_Equals(object obj1, object obj2)
        {
            //objがnullか、型が違うときは、等価でない
            if (obj1 == null || obj2 == null)// || obj2.GetType() != obj1.GetType()
            {
                return false;
            }

            return ((New_Basho)obj2).MasuNumber == ((New_Basho)obj1).MasuNumber;
        }

        public static bool IsErrorBasho(SyElement masu)
        {
            bool result;

            if (Masu_Honshogi.nError == ((New_Basho)masu).MasuNumber)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }
    }
}
