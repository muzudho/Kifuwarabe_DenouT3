namespace Grayscale.Kifuwarakaku.Entities.Features
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
        public static string ToStr_Ichimoji(PieceType ks14)
        {
            string syurui;

            switch (ks14)
            {
                case PieceType.P:
                    syurui = "歩";
                    break;

                case PieceType.L:
                    syurui = "香";
                    break;

                case PieceType.N:
                    syurui = "桂";
                    break;

                case PieceType.S:
                    syurui = "銀";
                    break;

                case PieceType.G:
                    syurui = "金";
                    break;

                case PieceType.R:
                    syurui = "飛";
                    break;

                case PieceType.B:
                    syurui = "角";
                    break;

                case PieceType.K:
                    syurui = "玉";
                    break;

                case PieceType.PP:
                    syurui = "と";
                    break;

                case PieceType.PL:
                    syurui = "杏";
                    break;

                case PieceType.PN:
                    syurui = "圭";
                    break;

                case PieceType.PS:
                    syurui = "全";
                    break;

                case PieceType.PR:
                    syurui = "竜";
                    break;

                case PieceType.PB:
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
        public static string ToStr_ImageName(PieceType ks14)
        {
            string name;

            switch (ks14)
            {
                case PieceType.P:
                    name = "01_Fu_____";
                    break;

                case PieceType.L:
                    name = "02_Kyo____";
                    break;

                case PieceType.N:
                    name = "03_Kei____";
                    break;

                case PieceType.S:
                    name = "04_Gin____";
                    break;

                case PieceType.G:
                    name = "05_Kin____";
                    break;

                case PieceType.R:
                    name = "07_Hisya__";
                    break;

                case PieceType.B:
                    name = "08_Kaku___";
                    break;

                case PieceType.K:
                    name = "06_Gyoku__";
                    break;

                case PieceType.PP:
                    name = "11_Tokin__";
                    break;

                case PieceType.PL:
                    name = "12_NariKyo";
                    break;

                case PieceType.PN:
                    name = "13_NariKei";
                    break;

                case PieceType.PS:
                    name = "14_NariGin";
                    break;

                case PieceType.PR:
                    name = "09_Ryu____";
                    break;

                case PieceType.PB:
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
