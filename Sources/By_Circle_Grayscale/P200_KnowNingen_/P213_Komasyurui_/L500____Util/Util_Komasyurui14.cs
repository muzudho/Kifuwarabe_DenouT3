using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P213_Komasyurui_.L250____Word;

namespace Grayscale.P213_Komasyurui_.L500____Util
{


    public abstract class Util_Komasyurui14
    {

        #region 静的プロパティー類

        /// <summary>
        /// 外字、後手
        /// </summary>
        public static char[] GaijiGote { get { return Util_Komasyurui14.gaijiGote; } }
        protected static char[] gaijiGote;

        /// <summary>
        /// 外字、先手
        /// </summary>
        public static char[] GaijiSente { get { return Util_Komasyurui14.gaijiSente; } }
        protected static char[] gaijiSente;


        public static string[] NimojiSente { get { return Util_Komasyurui14.nimojiSente; } }
        protected static string[] nimojiSente;


        public static string[] NimojiGote { get { return Util_Komasyurui14.nimojiGote; } }
        protected static string[] nimojiGote;


        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 駒の表示文字。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public static string[] Ichimoji { get { return Util_Komasyurui14.ichimoji; } }
        protected static string[] ichimoji;


        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 駒の符号用の単語。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public static string[] Fugo { get { return Util_Komasyurui14.fugo; } }
        protected static string[] fugo;


        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 駒のSFEN符号用の単語。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public static string[] Sfen1P { get { return Util_Komasyurui14.sfen1P; } }
        protected static string[] sfen1P;

        public static string[] Sfen2P { get { return Util_Komasyurui14.sfen2P; } }
        protected static string[] sfen2P;


        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 駒のSFEN(打)符号用の単語。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public static string[] SfenDa { get { return Util_Komasyurui14.sfenDa; } }
        protected static string[] sfenDa;


        /// <summary>
        /// ************************************************************************************************************************
        /// 成れる駒
        /// ************************************************************************************************************************
        /// </summary>
        public static bool[] FlagNareruKoma { get { return Util_Komasyurui14.flagNareruKoma; } }
        protected static bool[] flagNareruKoma;


        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 駒が成らなかったときの駒ハンドル
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public static Komasyurui14 NarazuCaseHandle(Komasyurui14 syurui)
        {
            return Util_Komasyurui14.narazuCaseHandle[(int)syurui];
        }
        protected static Komasyurui14[] narazuCaseHandle;


        /// <summary>
        /// ************************************************************************************************************************
        /// 成り駒なら真。
        /// ************************************************************************************************************************
        /// </summary>
        /// <returns></returns>
        public static bool[] FlagNari { get { return Util_Komasyurui14.flagNari; } }
        protected static bool[] flagNari;

        public static bool IsNari(Komasyurui14 syurui)
        {
            return Util_Komasyurui14.FlagNari[(int)syurui];
        }
        
        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 駒が成ったときの駒ハンドル
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public static Komasyurui14[] NariCaseHandle { get { return Util_Komasyurui14.nariCaseHandle; } }
        protected static Komasyurui14[] nariCaseHandle;

        public static Komasyurui14 ToNariCase(Komasyurui14 syurui)
        {
            return Util_Komasyurui14.NariCaseHandle[(int)syurui];
        }

        static Util_Komasyurui14()
        {

            Util_Komasyurui14.nariCaseHandle = new Komasyurui14[]{
                Komasyurui14.H00_Null___,//[0]ヌル
                Komasyurui14.H11_Tokin__,
                Komasyurui14.H12_NariKyo,
                Komasyurui14.H13_NariKei,
                Komasyurui14.H14_NariGin,
                Komasyurui14.H05_Kin____,
                Komasyurui14.H06_Gyoku__,
                Komasyurui14.H09_Ryu____,
                Komasyurui14.H10_Uma____,
                Komasyurui14.H09_Ryu____,
                Komasyurui14.H10_Uma____,
                Komasyurui14.H11_Tokin__,
                Komasyurui14.H12_NariKyo,
                Komasyurui14.H13_NariKei,
                Komasyurui14.H14_NariGin,
            };

            Util_Komasyurui14.flagNari = new bool[]{
                false,//[0]ヌル
                false,//[1]歩
                false,//[2]香
                false,//[3]桂
                false,//[4]銀
                false,//[5]金
                false,//[6]王
                false,//[7]飛車
                false,//[8]角
                true,//[9]竜
                true,//[10]馬
                true,//[11]と
                true,//[12]杏
                true,//[13]圭
                true,//[14]全
                false,//[15]エラー
            };

            Util_Komasyurui14.narazuCaseHandle = new Komasyurui14[]{
                Komasyurui14.H00_Null___,//[0]ヌル
                Komasyurui14.H01_Fu_____,//[1]歩
                Komasyurui14.H02_Kyo____,//[2]香
                Komasyurui14.H03_Kei____,//[3]桂
                Komasyurui14.H04_Gin____,//[4]銀
                Komasyurui14.H05_Kin____,//[5]金
                Komasyurui14.H06_Gyoku__,//[6]王
                Komasyurui14.H07_Hisya__,//[7]飛車
                Komasyurui14.H08_Kaku___,//[8]角
                Komasyurui14.H07_Hisya__,//[9]竜→飛車
                Komasyurui14.H08_Kaku___,//[10]馬→角
                Komasyurui14.H01_Fu_____,//[11]と→歩
                Komasyurui14.H02_Kyo____,//[12]杏→香
                Komasyurui14.H03_Kei____,//[13]圭→桂
                Komasyurui14.H04_Gin____,//[14]全→銀
            };

            Util_Komasyurui14.flagNareruKoma = new bool[]{
                false,//[0]ヌル
                true,//[1]歩
                true,//[2]香
                true,//[3]桂
                true,//[4]銀
                false,//[5]金
                false,//[6]王
                true,//[7]飛
                true,//[8]角
                false,//[9]竜
                false,//[10]馬
                false,//[11]と
                false,//[12]杏
                false,//[13]圭
                false,//[14]全
                false,//[15]エラー
            };

            Util_Komasyurui14.sfenDa = new string[]{
                "×",//[0]ヌル
                "P",//[1]
                "L",
                "N",
                "S",
                "G",
                "K",
                "R",
                "B",
                "R",
                "B",
                "P",
                "L",
                "N",
                "S",
                "＜打×Ｕ＞",//[15]
            };

            Util_Komasyurui14.sfen1P = new string[]{
                "×",//[0]ヌル
                "P",
                "L",
                "N",
                "S",
                "G",
                "K",
                "R",
                "B",
                "+R",
                "+B",
                "+P",
                "+L",
                "+N",
                "+S",
                "Ｕ×SFEN",
            };

            Util_Komasyurui14.sfen2P = new string[]{
                "×",//[0]ヌル
                "p",
                "l",
                "n",
                "s",
                "g",
                "k",
                "r",
                "b",
                "+r",
                "+b",
                "+p",
                "+l",
                "+n",
                "+s",
                "Ｕ×sfen",
            };

            Util_Komasyurui14.fugo = new string[]{
                "×",//[0]ヌル
                "歩",
                "香",
                "桂",
                "銀",
                "金",
                "王",
                "飛",
                "角",
                "竜",
                "馬",
                "と",
                "成香",
                "成桂",
                "成銀",
                "Ｕ×符",
            };

            Util_Komasyurui14.ichimoji = new string[]{
                "×",//[0]ヌル
                "歩",
                "香",
                "桂",
                "銀",
                "金",
                "王",
                "飛",
                "角",
                "竜",
                "馬",
                "と",
                "杏",
                "圭",
                "全",
                "Ｕ×",
            };

            Util_Komasyurui14.nimojiGote = new string[]{
                "△×",//[0]ヌル
                "△歩",
                "△香",
                "△桂",
                "△銀",
                "△金",
                "△王",
                "△飛",
                "△角",
                "△竜",
                "△馬",
                "△と",
                "△杏",
                "△圭",
                "△全",
                "△×",
            };



            Util_Komasyurui14.nimojiSente = new string[]{
                "▲×",//[0]ヌル
                "▲歩",
                "▲香",
                "▲桂",
                "▲銀",
                "▲金",
                "▲王",
                "▲飛",
                "▲角",
                "▲竜",
                "▲馬",
                "▲と",
                "▲杏",
                "▲圭",
                "▲全",
                "▲×",
            };

            Util_Komasyurui14.gaijiSente = new char[]{
                'ｘ',//[0]ヌル
                '歩',
                '香',
                '桂',
                '銀',
                '金',
                '王',
                '飛',
                '角',
                '竜',
                '馬',
                'と',
                '杏',
                '圭',
                '全',
                'ｘ',
            };

            //逆さ歩（外字）
            Util_Komasyurui14.gaijiGote = new char[]{
                'ｘ',//[0]ヌル
                '',//逆さ歩（外字）
                '',//逆さ香（外字）
                '',//逆さ桂（外字）
                '',//逆さ銀（外字）
                '',//逆さ金（外字）
                '',//逆さ王（外字）
                '',//逆さ飛（外字）
                '',//逆さ角（外字）
                '',//逆さ竜（外字）
                '',//逆さ馬（外字）
                '',//逆さと（外字）
                '',//逆さ杏（外字）
                '',//逆さ圭（外字）
                '',//逆さ全（外字）
                'Ｘ',//[15]エラー
            };
        }

        #endregion



        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 外字を利用した表示文字。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public static char ToGaiji(Komasyurui14 koma, Playerside pside)
        {
            char result;

            switch (pside)
            {
                case Playerside.P2:
                    result = Util_Komasyurui14.GaijiGote[(int)koma];
                    break;
                case Playerside.P1:
                    result = Util_Komasyurui14.GaijiSente[(int)koma];
                    break;
                default:
                    result = '×';
                    break;
            }

            return result;
        }

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 「歩」といった、外字を利用しない表示文字。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public static string ToIchimoji(Komasyurui14 koma)
        {
            return Util_Komasyurui14.Ichimoji[(int)koma];
        }

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 「▲歩」といった、外字を利用しない表示文字。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public static string ToNimoji(Komasyurui14 koma, Playerside pside)
        {
            string result;

            switch (pside)
            {
                case Playerside.P2:
                    result = Util_Komasyurui14.NimojiGote[(int)koma];
                    break;
                case Playerside.P1:
                    result = Util_Komasyurui14.NimojiSente[(int)koma];
                    break;
                default:
                    result = "××";
                    break;
            }

            return result;
        }

        /// <summary>
        /// ------------------------------------------------------------------------------------------------------------------------
        /// 駒のSFEN符号用の単語。
        /// ------------------------------------------------------------------------------------------------------------------------
        /// </summary>
        public static string SfenText(Komasyurui14 komaSyurui, Playerside pside)
        {
            string str;

            if (Playerside.P1 == pside)
            {
                str = Util_Komasyurui14.Sfen1P[(int)komaSyurui];
            }
            else
            {
                str = Util_Komasyurui14.Sfen2P[(int)komaSyurui];
            }

            return str;
        }


        public static bool Matches(Komasyurui14 koma1, Komasyurui14 koma2)
        {
            return (int)koma2 == (int)koma1;
        }

    }


}
