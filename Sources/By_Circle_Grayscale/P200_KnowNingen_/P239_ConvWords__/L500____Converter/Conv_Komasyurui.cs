using Grayscale.P213_Komasyurui_.L250____Word;

namespace Grayscale.P239_ConvWords__.L500____Converter
{
    public abstract class Conv_Komasyurui
    {
        /// <summary>
        /// ************************************************************************************************************************
        /// 駒の文字を、列挙型へ変換。
        /// ************************************************************************************************************************
        /// </summary>
        /// <param name="moji"></param>
        /// <returns></returns>
        public static string ToStr_Ichimoji(Komasyurui14 ks14)
        {
            string syurui;

            switch (ks14)
            {
                case Komasyurui14.H01_Fu_____:
                    syurui = "歩";
                    break;

                case Komasyurui14.H02_Kyo____:
                    syurui = "香";
                    break;

                case Komasyurui14.H03_Kei____:
                    syurui = "桂";
                    break;

                case Komasyurui14.H04_Gin____:
                    syurui = "銀";
                    break;

                case Komasyurui14.H05_Kin____:
                    syurui = "金";
                    break;

                case Komasyurui14.H07_Hisya__:
                    syurui = "飛";
                    break;

                case Komasyurui14.H08_Kaku___:
                    syurui = "角";
                    break;

                case Komasyurui14.H06_Gyoku__:
                    syurui = "玉";
                    break;

                case Komasyurui14.H11_Tokin__:
                    syurui = "と";
                    break;

                case Komasyurui14.H12_NariKyo:
                    syurui = "杏";
                    break;

                case Komasyurui14.H13_NariKei:
                    syurui = "圭";
                    break;

                case Komasyurui14.H14_NariGin:
                    syurui = "全";
                    break;

                case Komasyurui14.H09_Ryu____:
                    syurui = "竜";
                    break;

                case Komasyurui14.H10_Uma____:
                    syurui = "馬";
                    break;

                default:
                    syurui = "×";
                    break;
            }

            return syurui;
        }

        /// <summary>
        /// 将棋の駒画像のファイル名に変換。
        /// </summary>
        /// <param name="ks14"></param>
        /// <returns></returns>
        public static string ToStr_ImageName(Komasyurui14 ks14)
        {
            string name;

            switch (ks14)
            {
                case Komasyurui14.H01_Fu_____:
                    name = "01_Fu_____";
                    break;

                case Komasyurui14.H02_Kyo____:
                    name = "02_Kyo____";
                    break;

                case Komasyurui14.H03_Kei____:
                    name = "03_Kei____";
                    break;

                case Komasyurui14.H04_Gin____:
                    name = "04_Gin____";
                    break;

                case Komasyurui14.H05_Kin____:
                    name = "05_Kin____";
                    break;

                case Komasyurui14.H07_Hisya__:
                    name = "07_Hisya__";
                    break;

                case Komasyurui14.H08_Kaku___:
                    name = "08_Kaku___";
                    break;

                case Komasyurui14.H06_Gyoku__:
                    name = "06_Gyoku__";
                    break;

                case Komasyurui14.H11_Tokin__:
                    name = "11_Tokin__";
                    break;

                case Komasyurui14.H12_NariKyo:
                    name = "12_NariKyo";
                    break;

                case Komasyurui14.H13_NariKei:
                    name = "13_NariKei";
                    break;

                case Komasyurui14.H14_NariGin:
                    name = "14_NariGin";
                    break;

                case Komasyurui14.H09_Ryu____:
                    name = "09_Ryu____";
                    break;

                case Komasyurui14.H10_Uma____:
                    name = "10_Uma____";
                    break;

                default:
                    name = "00_Null___";
                    break;
            }

            return name;
        }


    }
}
