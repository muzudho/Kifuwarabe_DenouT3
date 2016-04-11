using Grayscale.P145_SfenStruct_.L___250_Struct;
using System.Text;

namespace Grayscale.P147_FormatSfen_.L260____Format
{
    public abstract class Format_Kyokumen1
    {
        public static string ToSfenstring(RO_Kyokumen1_ForFormat ro_kyokumen1, bool white)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("sfen ");

            for (int dan = 1; dan <= 9; dan++)
            {
                int spaceCount = 0;

                for (int suji = 9; suji >= 1; suji--)
                {
                    // 将棋盤上のどこかにある駒？
                    string koma0 = ro_kyokumen1.Ban[suji,dan];

                    if ("" != koma0)
                    {
                        if (0 < spaceCount)
                        {
                            sb.Append(spaceCount);
                            spaceCount = 0;
                        }

                        sb.Append(koma0);
                    }
                    else
                    {
                        spaceCount++;
                    }

                }

                if (0 < spaceCount)
                {
                    sb.Append(spaceCount);
                    spaceCount = 0;
                }

                if (dan != 9)
                {
                    sb.Append("/");
                }
            }

            sb.Append(" ");

            //------------------------------------------------------------
            // 先後
            //------------------------------------------------------------
            if (white)
            {
                sb.Append("w");
            }
            else
            {
                sb.Append("b");
            }

            sb.Append(" ");

            //------------------------------------------------------------
            // 持ち駒
            //------------------------------------------------------------
            {
                int mK = 0;
                int mR = 0;
                int mB = 0;
                int mG = 0;
                int mS = 0;
                int mN = 0;
                int mL = 0;
                int mP = 0;

                int mk = 0;
                int mr = 0;
                int mb = 0;
                int mg = 0;
                int ms = 0;
                int mn = 0;
                int ml = 0;
                int mp = 0;
                ro_kyokumen1.GetMoti(
                    out mK,
                    out mR,
                    out mB,
                    out mG,
                    out mS,
                    out mN,
                    out mL,
                    out mP,

                    out mk,
                    out mr,
                    out mb,
                    out mg,
                    out ms,
                    out mn,
                    out ml,
                    out mp
                    );



                if (0 == mK + mR + mB + mG + mS + mN + mL + mP + mk + mr + mb + mg + ms + mn + ml + mp)
                {
                    sb.Append("-");
                }
                else
                {
                    if (0 < mK)
                    {
                        if (1 < mK)
                        {
                            sb.Append(mK);
                        }
                        sb.Append("K");
                    }

                    if (0 < mR)
                    {
                        if (1 < mR)
                        {
                            sb.Append(mR);
                        }
                        sb.Append("R");
                    }

                    if (0 < mB)
                    {
                        if (1 < mB)
                        {
                            sb.Append(mB);
                        }
                        sb.Append("B");
                    }

                    if (0 < mG)
                    {
                        if (1 < mG)
                        {
                            sb.Append(mG);
                        }
                        sb.Append("G");
                    }

                    if (0 < mS)
                    {
                        if (1 < mS)
                        {
                            sb.Append(mS);
                        }
                        sb.Append("S");
                    }

                    if (0 < mN)
                    {
                        if (1 < mN)
                        {
                            sb.Append(mN);
                        }
                        sb.Append("N");
                    }

                    if (0 < mL)
                    {
                        if (1 < mL)
                        {
                            sb.Append(mL);
                        }
                        sb.Append("L");
                    }

                    if (0 < mP)
                    {
                        if (1 < mP)
                        {
                            sb.Append(mP);
                        }
                        sb.Append("P");
                    }

                    if (0 < mk)
                    {
                        if (1 < mk)
                        {
                            sb.Append(mk);
                        }
                        sb.Append("k");
                    }

                    if (0 < mr)
                    {
                        if (1 < mr)
                        {
                            sb.Append(mr);
                        }
                        sb.Append("r");
                    }

                    if (0 < mb)
                    {
                        if (1 < mb)
                        {
                            sb.Append(mb);
                        }
                        sb.Append("b");
                    }

                    if (0 < mg)
                    {
                        if (1 < mg)
                        {
                            sb.Append(mg);
                        }
                        sb.Append("g");
                    }

                    if (0 < ms)
                    {
                        if (1 < ms)
                        {
                            sb.Append(ms);
                        }
                        sb.Append("s");
                    }

                    if (0 < mn)
                    {
                        if (1 < mn)
                        {
                            sb.Append(mn);
                        }
                        sb.Append("n");
                    }

                    if (0 < ml)
                    {
                        if (1 < ml)
                        {
                            sb.Append(ml);
                        }
                        sb.Append("l");
                    }

                    if (0 < mp)
                    {
                        if (1 < mp)
                        {
                            sb.Append(mp);
                        }
                        sb.Append("p");
                    }
                }

            }

            // 手目
            sb.Append(" ");
            sb.Append(ro_kyokumen1.Temezumi);

            return sb.ToString();
        }

    }
}
