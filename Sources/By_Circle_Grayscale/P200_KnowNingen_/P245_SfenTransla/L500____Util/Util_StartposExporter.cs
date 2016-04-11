using Grayscale.P211_WordShogi__.L500____Word;
using Grayscale.P238_Seiza______.L250____Struct;
using System.Text;

namespace Grayscale.P245_SfenTransla.L500____Util
{
    public abstract class Util_StartposExporter
    {
        /// <summary>
        /// 「position [sfen ＜sfenstring＞ | startpos ] moves ＜move1＞ ... ＜movei＞」の中の、
        /// ＜sfenstring＞の部分を作成します。
        /// </summary>
        /// <returns></returns>
        public static string ToSfenstring(StartposExporterImpl se, bool outputKomabukuro_ForDebug)
        {
            StringBuilder sb = new StringBuilder();

            // 1段目
            {
                //マス番号は、72,63,54,45,36,27,18,9,0。
                sb.Append(se.CreateDanString(72));
            }
            sb.Append("/");

            // 2段目
            {
                //マス番号は、73,64,55,46,37,28,19,10,1。
                sb.Append(se.CreateDanString(73));
            }
            sb.Append("/");

            // 3段目
            {
                sb.Append(se.CreateDanString(74));
            }
            sb.Append("/");

            // 4段目
            {
                sb.Append(se.CreateDanString(75));
            }
            sb.Append("/");

            // 5段目
            {
                sb.Append(se.CreateDanString(76));
            }
            sb.Append("/");

            // 6段目
            {
                sb.Append(se.CreateDanString(77));
            }
            sb.Append("/");

            // 7段目
            {
                sb.Append(se.CreateDanString(78));
            }
            sb.Append("/");

            // 8段目
            {
                sb.Append(se.CreateDanString(79));
            }
            sb.Append("/");

            // 9段目
            {
                sb.Append(se.CreateDanString(80));
            }

            // 先後
            switch (se.KaisiPside)
            {
                case Playerside.P1: sb.Append(" b"); break;
                case Playerside.P2: sb.Append(" w"); break;
                default: sb.Append(" ?"); break;
            }

            // 持ち駒
            if (
                se.Moti1P < 1
                && se.Moti1L < 1
                && se.Moti1N < 1
                && se.Moti1S < 1
                && se.Moti1G < 1
                && se.Moti1K < 1
                && se.Moti1R < 1
                && se.Moti1B < 1
                && se.Moti2p < 1
                && se.Moti2l < 1
                && se.Moti2n < 1
                && se.Moti2s < 1
                && se.Moti2g < 1
                && se.Moti2k < 1
                && se.Moti2r < 1
                && se.Moti2b < 1
                )
            {
                sb.Append(" -");
            }
            else
            {
                sb.Append(" ");

                // 先手持ち駒
                //王
                if (0 < se.Moti1K)
                {
                    if (1 < se.Moti1K)
                    {
                        sb.Append(se.Moti1K);
                    }
                    sb.Append("K");
                }

                //飛車
                if (0 < se.Moti1R)
                {
                    if (1 < se.Moti1K)
                    {
                        sb.Append(se.Moti1R);
                    }
                    sb.Append("R");
                }

                //角
                if (0 < se.Moti1B)
                {
                    if (1 < se.Moti1K)
                    {
                        sb.Append(se.Moti1B);
                    }
                    sb.Append("B");
                }

                //金
                if (0 < se.Moti1G)
                {
                    if (1 < se.Moti1K)
                    {
                        sb.Append(se.Moti1G);
                    }
                    sb.Append("G");
                }

                //銀
                if (0 < se.Moti1S)
                {
                    if (1 < se.Moti1K)
                    {
                        sb.Append(se.Moti1S);
                    }
                    sb.Append("S");
                }

                //桂馬
                if (0 < se.Moti1N)
                {
                    if (1 < se.Moti1K)
                    {
                        sb.Append(se.Moti1N);
                    }
                    sb.Append("N");
                }

                //香車
                if (0 < se.Moti1L)
                {
                    if (1 < se.Moti1K)
                    {
                        sb.Append(se.Moti1L);
                    }
                    sb.Append("L");
                }

                //歩
                if (0 < se.Moti1P)
                {
                    if (1 < se.Moti1K)
                    {
                        sb.Append(se.Moti1P);
                    }
                    sb.Append("P");
                }

                // 後手持ち駒
                //王
                if (0 < se.Moti2k)
                {
                    if (1 < se.Moti1K)
                    {
                        sb.Append(se.Moti2k);
                    }
                    sb.Append("k");
                }

                //飛車
                if (0 < se.Moti2r)
                {
                    if (1 < se.Moti1K)
                    {
                        sb.Append(se.Moti2r);
                    }
                    sb.Append("r");
                }

                //角
                if (0 < se.Moti2b)
                {
                    if (1 < se.Moti1K)
                    {
                        sb.Append(se.Moti2b);
                    }
                    sb.Append("b");
                }

                //金
                if (0 < se.Moti2g)
                {
                    if (1 < se.Moti1K)
                    {
                        sb.Append(se.Moti2g);
                    }
                    sb.Append("g");
                }

                //銀
                if (0 < se.Moti2s)
                {
                    if (1 < se.Moti1K)
                    {
                        sb.Append(se.Moti2s);
                    }
                    sb.Append("s");
                }

                //桂馬
                if (0 < se.Moti2n)
                {
                    if (1 < se.Moti1K)
                    {
                        sb.Append(se.Moti2n);
                    }
                    sb.Append("n");
                }

                //香車
                if (0 < se.Moti2l)
                {
                    if (1 < se.Moti1K)
                    {
                        sb.Append(se.Moti2l);
                    }
                    sb.Append("l");
                }

                //歩
                if (0 < se.Moti2p)
                {
                    if (1 < se.Moti1K)
                    {
                        sb.Append(se.Moti2p);
                    }
                    sb.Append("p");
                }
            }

            // 1固定
            sb.Append(" 1");


            // デバッグ表示用
            if (outputKomabukuro_ForDebug)
            {
                // 駒袋
                sb.Append("(");
                //王
                if (0 < se.FukuroK)
                {
                    sb.Append("K");
                    sb.Append(se.FukuroK);
                }

                //飛車
                if (0 < se.FukuroR)
                {
                    sb.Append("R");
                    sb.Append(se.FukuroR);
                }

                //角
                if (0 < se.FukuroB)
                {
                    sb.Append("B");
                    sb.Append(se.FukuroB);
                }

                //金
                if (0 < se.FukuroG)
                {
                    sb.Append("G");
                    sb.Append(se.FukuroG);
                }

                //銀
                if (0 < se.FukuroS)
                {
                    sb.Append("S");
                    sb.Append(se.FukuroS);
                }

                //桂馬
                if (0 < se.FukuroN)
                {
                    sb.Append("N");
                    sb.Append(se.FukuroN);
                }

                //香車
                if (0 < se.FukuroL)
                {
                    sb.Append("L");
                    sb.Append(se.FukuroL);
                }

                //歩
                if (0 < se.FukuroP)
                {
                    sb.Append("P");
                    sb.Append(se.FukuroP);
                }

                sb.Append(")");
            }


            string sfenstring = sb.ToString();

            // 平手初期局面
            if ("sfen lnsgkgsnl/1r5b1/ppppppppp/9/9/9/PPPPPPPPP/1B5R1/LNSGKGSNL w - 1" == sfenstring)
            {
                sfenstring = "startpos";
            }

            return sfenstring;
        }
    }
}
