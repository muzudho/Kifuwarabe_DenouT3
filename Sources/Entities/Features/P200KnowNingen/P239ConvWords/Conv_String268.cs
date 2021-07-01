namespace Grayscale.Kifuwarakaku.Entities.Features
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
        public static PieceType Str_ToSyurui(string moji)
        {
            PieceType syurui;

            switch (moji)
            {
                case "歩":
                    syurui = PieceType.P;
                    break;

                case "香":
                    syurui = PieceType.L;
                    break;

                case "桂":
                    syurui = PieceType.N;
                    break;

                case "銀":
                    syurui = PieceType.S;
                    break;

                case "金":
                    syurui = PieceType.G;
                    break;

                case "飛":
                    syurui = PieceType.R;
                    break;

                case "角":
                    syurui = PieceType.B;
                    break;

                case "王"://thru
                case "玉":
                    syurui = PieceType.K;
                    break;

                case "と":
                    syurui = PieceType.PP;
                    break;

                case "成香":
                    syurui = PieceType.PL;
                    break;

                case "成桂":
                    syurui = PieceType.PN;
                    break;

                case "成銀":
                    syurui = PieceType.PS;
                    break;

                case "竜"://thru
                case "龍":
                    syurui = PieceType.PR;
                    break;

                case "馬":
                    syurui = PieceType.PB;
                    break;

                default:
                    syurui = PieceType.None;
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
        public static void SfenUttaSyurui(string sfen, out PieceType syurui)
        {
            switch (sfen)
            {
                case "P":
                    syurui = PieceType.P;
                    break;

                case "L":
                    syurui = PieceType.L;
                    break;

                case "N":
                    syurui = PieceType.N;
                    break;

                case "S":
                    syurui = PieceType.S;
                    break;

                case "G":
                    syurui = PieceType.G;
                    break;

                case "R":
                    syurui = PieceType.R;
                    break;

                case "B":
                    syurui = PieceType.B;
                    break;

                case "K":
                    syurui = PieceType.K;
                    break;

                case "+P":
                    syurui = PieceType.PP;
                    break;

                case "+L":
                    syurui = PieceType.PL;
                    break;

                case "+N":
                    syurui = PieceType.PN;
                    break;

                case "+S":
                    syurui = PieceType.PS;
                    break;

                case "+R":
                    syurui = PieceType.R;
                    break;

                case "+B":
                    syurui = PieceType.B;
                    break;

                default:
                    Util_Message.Show($"▲バグ【駒種類】Sfen=[{sfen}]");
                    syurui = PieceType.None;
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
        public static void SfenSyokihaichi_ToSyurui(string sfen, out Playerside pside, out PieceType syurui)
        {
            switch (sfen)
            {
                case "P":
                    pside = Playerside.P1;
                    syurui = PieceType.P;
                    break;

                case "p":
                    pside = Playerside.P2;
                    syurui = PieceType.P;
                    break;

                case "L":
                    pside = Playerside.P1;
                    syurui = PieceType.L;
                    break;

                case "l":
                    pside = Playerside.P2;
                    syurui = PieceType.L;
                    break;

                case "N":
                    pside = Playerside.P1;
                    syurui = PieceType.N;
                    break;

                case "n":
                    pside = Playerside.P2;
                    syurui = PieceType.N;
                    break;

                case "S":
                    pside = Playerside.P1;
                    syurui = PieceType.S;
                    break;

                case "s":
                    pside = Playerside.P2;
                    syurui = PieceType.S;
                    break;

                case "G":
                    pside = Playerside.P1;
                    syurui = PieceType.G;
                    break;

                case "g":
                    pside = Playerside.P2;
                    syurui = PieceType.G;
                    break;

                case "R":
                    pside = Playerside.P1;
                    syurui = PieceType.R;
                    break;

                case "r":
                    pside = Playerside.P2;
                    syurui = PieceType.R;
                    break;

                case "B":
                    pside = Playerside.P1;
                    syurui = PieceType.B;
                    break;

                case "b":
                    pside = Playerside.P2;
                    syurui = PieceType.B;
                    break;

                case "K":
                    pside = Playerside.P1;
                    syurui = PieceType.K;
                    break;

                case "k":
                    pside = Playerside.P2;
                    syurui = PieceType.K;
                    break;

                case "+P":
                    pside = Playerside.P1;
                    syurui = PieceType.PP;
                    break;

                case "+p":
                    pside = Playerside.P2;
                    syurui = PieceType.PP;
                    break;

                case "+L":
                    pside = Playerside.P1;
                    syurui = PieceType.PL;
                    break;

                case "+l":
                    pside = Playerside.P2;
                    syurui = PieceType.PL;
                    break;

                case "+N":
                    pside = Playerside.P1;
                    syurui = PieceType.PN;
                    break;

                case "+n":
                    pside = Playerside.P2;
                    syurui = PieceType.PN;
                    break;

                case "+S":
                    pside = Playerside.P1;
                    syurui = PieceType.PS;
                    break;

                case "+s":
                    pside = Playerside.P2;
                    syurui = PieceType.PS;
                    break;

                case "+R":
                    pside = Playerside.P1;
                    syurui = PieceType.R;
                    break;

                case "+r":
                    pside = Playerside.P2;
                    syurui = PieceType.R;
                    break;

                case "+B":
                    pside = Playerside.P1;
                    syurui = PieceType.B;
                    break;

                case "+b":
                    pside = Playerside.P2;
                    syurui = PieceType.B;
                    break;

                default:
                    pside = Playerside.P2;
                    syurui = PieceType.None;
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
