using Grayscale.P005_Tushin_____.L500____Util;
using Grayscale.P209_KifuJsa____.L500____Word;
using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P213_Komasyurui_.L250____Word;


namespace Grayscale.P239_ConvWords__.L500____Converter
{
    public abstract class Conv_String268
    {
        /// <summary>
        /// ************************************************************************************************************************
        /// 駒の文字を、列挙型へ変換。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="moji"></param>
        /// <returns></returns>
        public static Komasyurui14 Str_ToSyurui(string moji)
        {
            Komasyurui14 syurui;

            switch (moji)
            {
                case "歩":
                    syurui = Komasyurui14.H01_Fu_____;
                    break;

                case "香":
                    syurui = Komasyurui14.H02_Kyo____;
                    break;

                case "桂":
                    syurui = Komasyurui14.H03_Kei____;
                    break;

                case "銀":
                    syurui = Komasyurui14.H04_Gin____;
                    break;

                case "金":
                    syurui = Komasyurui14.H05_Kin____;
                    break;

                case "飛":
                    syurui = Komasyurui14.H07_Hisya__;
                    break;

                case "角":
                    syurui = Komasyurui14.H08_Kaku___;
                    break;

                case "王"://thru
                case "玉":
                    syurui = Komasyurui14.H06_Gyoku__;
                    break;

                case "と":
                    syurui = Komasyurui14.H11_Tokin__;
                    break;

                case "成香":
                    syurui = Komasyurui14.H12_NariKyo;
                    break;

                case "成桂":
                    syurui = Komasyurui14.H13_NariKei;
                    break;

                case "成銀":
                    syurui = Komasyurui14.H14_NariGin;
                    break;

                case "竜"://thru
                case "龍":
                    syurui = Komasyurui14.H09_Ryu____;
                    break;

                case "馬":
                    syurui = Komasyurui14.H10_Uma____;
                    break;

                default:
                    syurui = Komasyurui14.H00_Null___;
                    break;
            }

            return syurui;
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 寄、右、左、直、なし
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="migiHidariStr"></param>
        /// <returns></returns>
        public static MigiHidari Str_ToMigiHidari(string migiHidariStr)
        {
            MigiHidari migiHidari;

            switch (migiHidariStr)
            {
                case "右":
                    migiHidari = MigiHidari.Migi;
                    break;

                case "左":
                    migiHidari = MigiHidari.Hidari;
                    break;

                case "直":
                    migiHidari = MigiHidari.Sugu;
                    break;

                default:
                    migiHidari = MigiHidari.No_Print;
                    break;
            }

            return migiHidari;
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 打表示。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="daStr"></param>
        /// <returns></returns>
        public static DaHyoji Str_ToDaHyoji(string daStr)
        {
            DaHyoji daHyoji = DaHyoji.No_Print;

            if (daStr == "打")
            {
                daHyoji = DaHyoji.Visible;
            }

            return daHyoji;
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 上がる、引く。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="agaruHikuStr"></param>
        /// <returns></returns>
        public static AgaruHiku Str_ToAgaruHiku(string agaruHikuStr)
        {
            AgaruHiku agaruHiku;

            switch (agaruHikuStr)
            {
                case "寄":
                    agaruHiku = AgaruHiku.Yoru;
                    break;

                case "引":
                    agaruHiku = AgaruHiku.Hiku;
                    break;

                case "上":
                    agaruHiku = AgaruHiku.Agaru;
                    break;

                default:
                    agaruHiku = AgaruHiku.No_Print;
                    break;
            }

            return agaruHiku;
        }

        /// <summary>
        /// ************************************************************************************************************************
        /// 打った駒の種類。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="syurui"></param>
        /// <returns></returns>
        public static void SfenUttaSyurui(string sfen, out Komasyurui14 syurui)
        {
            switch (sfen)
            {
                case "P":
                    syurui = Komasyurui14.H01_Fu_____;
                    break;

                case "L":
                    syurui = Komasyurui14.H02_Kyo____;
                    break;

                case "N":
                    syurui = Komasyurui14.H03_Kei____;
                    break;

                case "S":
                    syurui = Komasyurui14.H04_Gin____;
                    break;

                case "G":
                    syurui = Komasyurui14.H05_Kin____;
                    break;

                case "R":
                    syurui = Komasyurui14.H07_Hisya__;
                    break;

                case "B":
                    syurui = Komasyurui14.H08_Kaku___;
                    break;

                case "K":
                    syurui = Komasyurui14.H06_Gyoku__;
                    break;

                case "+P":
                    syurui = Komasyurui14.H11_Tokin__;
                    break;

                case "+L":
                    syurui = Komasyurui14.H12_NariKyo;
                    break;

                case "+N":
                    syurui = Komasyurui14.H13_NariKei;
                    break;

                case "+S":
                    syurui = Komasyurui14.H14_NariGin;
                    break;

                case "+R":
                    syurui = Komasyurui14.H07_Hisya__;
                    break;

                case "+B":
                    syurui = Komasyurui14.H08_Kaku___;
                    break;

                default:
                    Util_Message.Show("▲バグ【駒種類】Sfen=[" + sfen + "]");
                    syurui = Komasyurui14.H00_Null___;
                    break;
            }
        }

        /// <summary>
        /// FIXME: 使ってない？
        /// 
        /// ************************************************************************************************************************
        /// 駒の種類。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="syurui"></param>
        /// <returns></returns>
        public static void SfenSyokihaichi_ToSyurui(string sfen, out Playerside pside, out Komasyurui14 syurui)
        {
            switch (sfen)
            {
                case "P":
                    pside = Playerside.P1;
                    syurui = Komasyurui14.H01_Fu_____;
                    break;

                case "p":
                    pside = Playerside.P2;
                    syurui = Komasyurui14.H01_Fu_____;
                    break;

                case "L":
                    pside = Playerside.P1;
                    syurui = Komasyurui14.H02_Kyo____;
                    break;

                case "l":
                    pside = Playerside.P2;
                    syurui = Komasyurui14.H02_Kyo____;
                    break;

                case "N":
                    pside = Playerside.P1;
                    syurui = Komasyurui14.H03_Kei____;
                    break;

                case "n":
                    pside = Playerside.P2;
                    syurui = Komasyurui14.H03_Kei____;
                    break;

                case "S":
                    pside = Playerside.P1;
                    syurui = Komasyurui14.H04_Gin____;
                    break;

                case "s":
                    pside = Playerside.P2;
                    syurui = Komasyurui14.H04_Gin____;
                    break;

                case "G":
                    pside = Playerside.P1;
                    syurui = Komasyurui14.H05_Kin____;
                    break;

                case "g":
                    pside = Playerside.P2;
                    syurui = Komasyurui14.H05_Kin____;
                    break;

                case "R":
                    pside = Playerside.P1;
                    syurui = Komasyurui14.H07_Hisya__;
                    break;

                case "r":
                    pside = Playerside.P2;
                    syurui = Komasyurui14.H07_Hisya__;
                    break;

                case "B":
                    pside = Playerside.P1;
                    syurui = Komasyurui14.H08_Kaku___;
                    break;

                case "b":
                    pside = Playerside.P2;
                    syurui = Komasyurui14.H08_Kaku___;
                    break;

                case "K":
                    pside = Playerside.P1;
                    syurui = Komasyurui14.H06_Gyoku__;
                    break;

                case "k":
                    pside = Playerside.P2;
                    syurui = Komasyurui14.H06_Gyoku__;
                    break;

                case "+P":
                    pside = Playerside.P1;
                    syurui = Komasyurui14.H11_Tokin__;
                    break;

                case "+p":
                    pside = Playerside.P2;
                    syurui = Komasyurui14.H11_Tokin__;
                    break;

                case "+L":
                    pside = Playerside.P1;
                    syurui = Komasyurui14.H12_NariKyo;
                    break;

                case "+l":
                    pside = Playerside.P2;
                    syurui = Komasyurui14.H12_NariKyo;
                    break;

                case "+N":
                    pside = Playerside.P1;
                    syurui = Komasyurui14.H13_NariKei;
                    break;

                case "+n":
                    pside = Playerside.P2;
                    syurui = Komasyurui14.H13_NariKei;
                    break;

                case "+S":
                    pside = Playerside.P1;
                    syurui = Komasyurui14.H14_NariGin;
                    break;

                case "+s":
                    pside = Playerside.P2;
                    syurui = Komasyurui14.H14_NariGin;
                    break;

                case "+R":
                    pside = Playerside.P1;
                    syurui = Komasyurui14.H07_Hisya__;
                    break;

                case "+r":
                    pside = Playerside.P2;
                    syurui = Komasyurui14.H07_Hisya__;
                    break;

                case "+B":
                    pside = Playerside.P1;
                    syurui = Komasyurui14.H08_Kaku___;
                    break;

                case "+b":
                    pside = Playerside.P2;
                    syurui = Komasyurui14.H08_Kaku___;
                    break;

                default:
                    pside = Playerside.P2;
                    syurui = Komasyurui14.H00_Null___;
                    break;
            }
        }


        /// <summary>
        /// ************************************************************************************************************************
        /// 先後。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="psideStr"></param>
        /// <returns></returns>
        public static Playerside Pside_ToEnum(string psideStr)
        {
            Playerside pside;

            switch (psideStr)
            {
                case "△":
                    pside = Playerside.P2;
                    break;

                case "▲":
                default:
                    pside = Playerside.P1;
                    break;
            }

            return pside;
        }


        /// <summary>
        /// ************************************************************************************************************************
        /// 成り。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="nariStr"></param>
        /// <returns></returns>
        public static NariNarazu Nari_ToBool(string nariStr)
        {
            NariNarazu nari;

            if ("成" == nariStr)
            {
                nari = NariNarazu.Nari;
            }
            else if ("不成" == nariStr)
            {
                nari = NariNarazu.Narazu;
            }
            else
            {
                nari = NariNarazu.CTRL_SONOMAMA;
            }

            return nari;
        }

    }
}
